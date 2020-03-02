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
    public partial class ListingPurchaseInvoice : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        // detalis obj and list
        public static List<PurchaseInvoiceDetail> PurchInvDelist = new List<PurchaseInvoiceDetail>();
        PurchaseInvoiceDetail myPurchInvDe = new PurchaseInvoiceDetail();
        //
        public static List<PurchaseInvoice> SearchPurchaseInvoiceList = new List<PurchaseInvoice>();
        //
        public static List<PurchaseInvoice> PurchaseInvoiceList = new List<PurchaseInvoice>();

        public static PurchaseInvoice myPurchaseInvoice = new PurchaseInvoice();

        public static List<SubAccount> SubAccountList = new List<SubAccount>();
        SubAccount mySubAccount = new SubAccount();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SubAccountList = db.SubAccount.Where(sub=> sub.UpAccount == "2103").ToList();
                List<SubAccount> TempSubAccountList = new List<SubAccount>();
                TempSubAccountList.Add(new SubAccount { name = "-- select Vendor Number --", ID = 0 });
                TempSubAccountList.AddRange(SubAccountList);
                vendorsListtxt.DataSource = TempSubAccountList;
                vendorsListtxt.DataBind();
                vendorsListtxt.DataTextField = "name";
                vendorsListtxt.DataValueField = "ID";
                vendorsListtxt.DataBind();
                PurchaseInvoiceList = db.PurchaseInvoice.ToList();
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
            foreach (var item in PurchaseInvoiceList)
            {
                item.SubAccount = db.SubAccount.Where(col => col.ID == item.SubAccountId).FirstOrDefault();
                item.AspUser= db.AspUser.Where(col => col.Id == item.UserID).FirstOrDefault();
            }
            GridView1.DataSource = PurchaseInvoiceList;
            GridView1.DataBind();
        }
        //Search Part
        protected void Search_Click(object sender, EventArgs e)
        {
            SearchPurchaseInvoiceList = new List<PurchaseInvoice>();
            PurchaseInvoiceList = new List<PurchaseInvoice>();
            myPurchaseInvoice = new PurchaseInvoice();
            myPurchaseInvoice = SearchModel();

            var SubAccountId = new SqlParameter();
            var Id = new SqlParameter();
            var InvoiceDate = new SqlParameter();
            var PurchaseType = new SqlParameter();
            //CarId
            if (myPurchaseInvoice.SubAccountId != 0){ SubAccountId = new SqlParameter("@SubAccountId", myPurchaseInvoice.SubAccountId);}
            else { SubAccountId = new SqlParameter("@SubAccountId", DBNull.Value); }
            // Invoice ID
            if (myPurchaseInvoice.Id != ""){Id = new SqlParameter("@Id", myPurchaseInvoice.Id.Trim());}
            else { Id = new SqlParameter("@Id", DBNull.Value); }
            // InvoiceDate
            if (myPurchaseInvoice.InvoiceDate.ToString() != "") { InvoiceDate = new SqlParameter("@InvoiceDate", myPurchaseInvoice.InvoiceDate); }
            else { InvoiceDate = new SqlParameter("@InvoiceDate", DBNull.Value); }
            // PurchaseType
            if (myPurchaseInvoice.PurchaseType != null) { PurchaseType = new SqlParameter("@PurchaseType", myPurchaseInvoice.PurchaseType); }
            else { PurchaseType = new SqlParameter("@PurchaseType", DBNull.Value); }

            PurchaseInvoiceList = db.Database
                .SqlQuery<PurchaseInvoice>("SP_PurchaseInvoice @SubAccountId , @Id, @InvoiceDate, @PurchaseType", SubAccountId, Id, InvoiceDate, PurchaseType).ToList();

            SearchPurchaseInvoiceList = PurchaseInvoiceList;
            BindGridList();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            SearchPurchaseInvoiceList = new List<PurchaseInvoice>();
            ClearSearchModel();
            PurchaseInvoiceList = new List<PurchaseInvoice>();
            myPurchaseInvoice = new PurchaseInvoice();
            PurchaseInvoiceList = db.PurchaseInvoice.ToList();
            BindGridList();
        }
        protected PurchaseInvoice SearchModel()
        {
            PurchaseInvoice purchaseInvoice = new PurchaseInvoice();

            purchaseInvoice.SubAccountId = Convert.ToInt32(vendorsListtxt.SelectedValue);

            purchaseInvoice.Id = InvoiceNo.Text;

            purchaseInvoice.InvoiceDate =ExtendedMethod.FormatDate(InvoiceDate.Text);

            if (Purchase.Checked) { purchaseInvoice.PurchaseType = Convert.ToBoolean(true); }
            else if (Discarded.Checked) { purchaseInvoice.PurchaseType = Convert.ToBoolean(false); }

            return purchaseInvoice;
        }
        protected void ClearSearchModel()
        {
            vendorsListtxt.SelectedIndex = 0;
            InvoiceNo.Text = "";
            InvoiceDate.Text = "";
            RadioButton1.Checked = true;
        }
        // Operations
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (SearchPurchaseInvoiceList.Count > 0)
            { index = Convert.ToInt32(SearchPurchaseInvoiceList[index].Id); }
            else { index = Convert.ToInt32(PurchaseInvoiceList[index].Id); }
            Response.Redirect("../Maintenance/OperPurchaseInvoice.aspx?id=" + index);
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            PurchInvDelist = new List<PurchaseInvoiceDetail>();
            string deleted_index = "";
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchPurchaseInvoiceList.Count > 0)
            { deleted_index = SearchPurchaseInvoiceList[index].Id; }
            else { deleted_index = PurchaseInvoiceList[index].Id; }

            try
            {
                var deletedItem = db.PurchaseInvoice.Where(SS => SS.Id == deleted_index).FirstOrDefault();
                // deleting inner Details
                if(deletedItem != null)
                {
                    //remove its Details
                    PurchInvDelist = db.PurchaseInvoiceDetail.Where(PID => PID.PurchaseInvoiceID == deletedItem.Id).ToList();
                    for(int i=0; i< PurchInvDelist.Count; i++)
                    {
                        try
                        {
                            db.PurchaseInvoiceDetail.Remove(PurchInvDelist[i]);
                            db.SaveChanges();
                        }
                        catch(Exception excpt)
                        {
                            ShowMessage("Something Wrong Will Deleting inner Details");
                        }
                    }
                    //remove its Movement
                    string descript = "أذن استلام بضاعه لفتوره رقم" + deletedItem.Id;
                    KhznaMoved  myKhznaMoved = new KhznaMoved();
                    myKhznaMoved = db.KhznaMoved.Where(khz => khz.Description == descript).FirstOrDefault();
                    try
                    {
                        db.KhznaMoved.Remove(myKhznaMoved);
                        db.SaveChanges();
                    }
                    catch (Exception excpt)
                    {
                        ShowMessage("Something Wrong Will Khazna Movement");
                    }
                }
                else
                {
                    ShowMessage("Can not Find This Item ... ");
                }
                // deleting all item
                db.PurchaseInvoice.Remove(deletedItem);
                db.SaveChanges();
                //Bind New List After Deleting
                PurchaseInvoiceList = db.PurchaseInvoice.ToList();
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