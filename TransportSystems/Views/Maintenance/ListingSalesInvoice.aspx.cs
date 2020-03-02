using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.Maintenance
{
    public partial class ListingSalesInvoice : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        // detalis obj and list
        public static List<SalesInvoiceDetail> SalesInvoiceDetaillist = new List<SalesInvoiceDetail>();
        SalesInvoiceDetail mySalesInvoiceDetail = new SalesInvoiceDetail();
        //
        public static List<SalesInvoice> SearchSalesInvoiceList = new List<SalesInvoice>();
        //
        public static List<SalesInvoice> SalesInvoiceList = new List<SalesInvoice>();

        public static SalesInvoice mySalesInvoice = new SalesInvoice();

        public static List<Cars> CarsList = new List<Cars>();
        Cars myCars = new Cars();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarsList = db.Cars.ToList();
                List<Cars> TempCarsList = new List<Cars>();
                TempCarsList.Add(new Cars { CarNo = "-- select Car Number --", id = 0 });
                TempCarsList.AddRange(CarsList);
                CarsListtxt.DataSource = TempCarsList;
                CarsListtxt.DataBind();
                CarsListtxt.DataTextField = "CarNo";
                CarsListtxt.DataValueField = "Id";
                CarsListtxt.DataBind();
                SalesInvoiceList = db.SalesInvoice.ToList();
            }
            BindGridList();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void BindGridList()
        {
            foreach (var item in SalesInvoiceList)
            {
                item.Cars = db.Cars.Where(col => col.id == item.CarId).FirstOrDefault();
                item.AspUser = db.AspUser.Where(col => col.Id == item.UserID).FirstOrDefault();
            }
            GridView1.DataSource = SalesInvoiceList;
            GridView1.DataBind();
        }
        //Search Part
        protected void Search_Click(object sender, EventArgs e)
        {
            SearchSalesInvoiceList = new List<SalesInvoice>();
            SalesInvoiceList = new List<SalesInvoice>();
            mySalesInvoice = new SalesInvoice();
            mySalesInvoice = SearchModel();

            var CarId = new SqlParameter();
            var Id = new SqlParameter();
            var InvoiceDate = new SqlParameter();
            var PurchaseType = new SqlParameter();
            //CarId
            if (mySalesInvoice.CarId != 0) { CarId = new SqlParameter("@CarId", mySalesInvoice.CarId); }
            else { CarId = new SqlParameter("@CarId", DBNull.Value); }
            // Invoice ID
            if (mySalesInvoice.Id != "") { Id = new SqlParameter("@Id", mySalesInvoice.Id.Trim()); }
            else { Id = new SqlParameter("@Id", DBNull.Value); }
            // InvoiceDate
            if (mySalesInvoice.InvoiceDate.ToString() != "") { InvoiceDate = new SqlParameter("@InvoiceDate", mySalesInvoice.InvoiceDate); }
            else { InvoiceDate = new SqlParameter("@InvoiceDate", DBNull.Value); }
            // PurchaseType
            if (mySalesInvoice.PurchaseType != null) { PurchaseType = new SqlParameter("@PurchaseType", mySalesInvoice.PurchaseType); }
            else { PurchaseType = new SqlParameter("@PurchaseType", DBNull.Value); }

            SalesInvoiceList = db.Database
                .SqlQuery<SalesInvoice>("SP_SalesInvoice @CarId , @Id, @InvoiceDate, @PurchaseType", CarId, Id, InvoiceDate, PurchaseType).ToList();

            SearchSalesInvoiceList = SalesInvoiceList;
            BindGridList();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            SearchSalesInvoiceList = new List<SalesInvoice>();
            ClearSearchModel();
            SalesInvoiceList = new List<SalesInvoice>();
            mySalesInvoice = new SalesInvoice();
            SalesInvoiceList = db.SalesInvoice.ToList();
            BindGridList();
        }
        protected SalesInvoice SearchModel()
        {
            SalesInvoice salesInvoice = new SalesInvoice();

            salesInvoice.CarId = Convert.ToInt32(CarsListtxt.SelectedValue);

            salesInvoice.Id = InvoiceNo.Text;

            salesInvoice.InvoiceDate =ExtendedMethod.FormatDate(InvoiceDate.Text);

            if (Sale.Checked) { salesInvoice.PurchaseType = Convert.ToBoolean(true); }
            else if (Discarded.Checked) { salesInvoice.PurchaseType = Convert.ToBoolean(false); }

            return salesInvoice;
        }
        protected void ClearSearchModel()
        {
            CarsListtxt.SelectedIndex = 0;
            InvoiceNo.Text = "";
            InvoiceDate.Text = "";
            RadioButton1.Checked = true;
        }
        //Operation Part
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (SearchSalesInvoiceList.Count > 0)
            { index = Convert.ToInt32(SearchSalesInvoiceList[index].Id); }
            else { index = Convert.ToInt32(SalesInvoiceList[index].Id); }
            Response.Redirect("../Maintenance/OperSalesInvoice.aspx?id=" + index);
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            SalesInvoiceDetaillist = new List<SalesInvoiceDetail>();
            string deleted_index = "";
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchSalesInvoiceList.Count > 0)
            { deleted_index = SearchSalesInvoiceList[index].Id; }
            else { deleted_index = SalesInvoiceList[index].Id; }

            try
            {
                var deletedItem = db.SalesInvoice.Where(SS => SS.Id == deleted_index).FirstOrDefault();
                // deleting inner Details
                if (deletedItem != null)
                {
                    //remove its Details
                    SalesInvoiceDetaillist = db.SalesInvoiceDetail.Where(PID => PID.PurchaseInvoiceID == deletedItem.Id).ToList();
                    for (int i = 0; i < SalesInvoiceDetaillist.Count; i++)
                    {
                        try
                        {
                            db.SalesInvoiceDetail.Remove(SalesInvoiceDetaillist[i]);
                            db.SaveChanges();
                        }
                        catch (Exception excpt)
                        {
                            ShowMessage("Something Wrong Will Deleting inner Details");
                        }
                    }
                }
                else
                {
                    ShowMessage("Can not Find This Item ... ");
                }
                // deleting all item
                db.SalesInvoice.Remove(deletedItem);
                db.SaveChanges();
                //Bind New List After Deleting
                SalesInvoiceList = db.SalesInvoice.ToList();
                BindGridList();
            }
            catch (Exception excpt)
            {
                ShowMessage("Something Wrong Will Deleting");
            }
        }
        public void ShowMessage(string message)
        {
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("function(){");
            //sb.Append("alert('");
            //sb.Append(message);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString(),true);
            //Response.Write("<script type='text/javascript'>alert('" + message + "');</script>");
            AddErrorTxt.Text = message;
            AddErrorTxt.ForeColor = System.Drawing.Color.Red;
        }
    }
}