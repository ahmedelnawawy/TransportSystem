using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.Maintenance
{
    public partial class OperSalesInvoice : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public List<SalesInvoice> SalesInvoiceList = new List<SalesInvoice>();
        SalesInvoice mySalesInvoice = new SalesInvoice();

        public static List<SalesInvoiceDetail> SalesInvoiceDetailList = new List<SalesInvoiceDetail>();
        SalesInvoiceDetail mySalesInvoiceDetail = new SalesInvoiceDetail();

        public static List<Cars> CarsList = new List<Cars>();
        Cars myCars = new Cars();

        public static List<Services> ServicesList = new List<Services>();
        Services myServices = new Services();

        public static List<Product> ProductList = new List<Product>();
        Product myproduct = new Product();

        public static string opertypeforinvoice = "";

        public static bool VisitedAtEdit = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string EditID = Request.QueryString["id"];
                if (EditID != "" && EditID != null) { opertypeforinvoice = "Edit"; }
                else { opertypeforinvoice = "add"; }

                Set_initial();

                if (opertypeforinvoice == "Edit" && VisitedAtEdit == false)
                {
                    GetEditInvoice(EditID);
                }
            }
            if (ProductListtxt.Items.Count > 0)
            {
                Product tempProd = new Product();
                tempProd.ID = ProductListtxt.SelectedValue;
                var ProPrice = Convert.ToDouble(db.Product.Where(pro => pro.ID == tempProd.ID).FirstOrDefault().SPrice);
                ProductPrice.Text = Math.Round(ProPrice, 2).ToString();
            }
            BindGridList();
        }
        protected void Set_initial()
        {
            
            //initial values of all invoice
            var NewId = Convert.ToInt32(db.SalesInvoice.Select(item => item.Id).Max()) + 1;
            InvoiceNo.Text = NewId.ToString();
            InvoiceDate.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            SalesInvoiceDetailList = new List<SalesInvoiceDetail>();
            //make custom For Product (innerDetails) >>><<<<
            string vgId = Guid.NewGuid().ToString();
            ProdNameFilt.ValidationGroup = vgId;
            ProductPrice.ValidationGroup = vgId;
            RequiredFieldValidator4.ValidationGroup = vgId;
            Qty.ValidationGroup = vgId;
            RequiredFieldValidator1.ValidationGroup = vgId;
            Save_prodcut.ValidationGroup = vgId;

            //Product  (For Adding will be forced to choose)
            ProductList = db.Product.ToList();
            BindDropDownList(ProductList, ProductListtxt);
            //Cars  (For Adding will be forced to choose)
            CarsList = db.Cars.ToList();
            CarsListtxt.DataSource = CarsList;
            CarsListtxt.DataTextField = "CarNo";
            CarsListtxt.DataValueField = "Id";
            CarsListtxt.DataBind();
            //ServicesList  (For Adding will be forced to choose)
            ServicesList = db.Services.ToList();
            ServicesListtxt.DataSource = ServicesList;
            ServicesListtxt.DataTextField = "Name";
            ServicesListtxt.DataValueField = "Id";
            ServicesListtxt.DataBind();
        }
        protected void BindDropDownList(object DataSourceList, object DropdownList)
        {
            DropDownList Droplist = DropdownList as DropDownList;
            Droplist.DataSource = DataSourceList;
            Droplist.DataTextField = "name";
            Droplist.DataValueField = "ID";
            Droplist.DataBind();
            // hea mosh most5dma 8er m3a al product
            if (ProductListtxt.Items.Count > 0)
            {
                Product tempProd = new Product();
                tempProd.ID = ProductListtxt.SelectedValue;
                var ProPrice = Convert.ToDouble(db.Product.Where(pro => pro.ID == tempProd.ID).FirstOrDefault().SPrice);
                ProductPrice.Text = Math.Round(ProPrice, 2).ToString();
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
        //product Part
        public bool ThereEnoughGoods(double NeedQty , string ProductId)
        {
            var OpiningBalance = db.Product.Where(pro => pro.ID == ProductId).FirstOrDefault().CurrentBalance;

            var AllBoughtQty = db.PurchaseInvoiceDetail.Where(pro => pro.ProductID == ProductId).ToList().Select(qty=> qty.Qty).Sum();

            //current Sales list
            var CurrentSoldQty =  SalesInvoiceDetailList.Where(pro => pro.ProductID == ProductId).Select(qty => qty.Qty).Sum();

            var AllSaledQty = db.SalesInvoiceDetail.Where(pro => pro.ProductID == ProductId).ToList().Select(qty => qty.Qty).Sum();

            var cuurentQty = (OpiningBalance + AllBoughtQty) - (AllSaledQty + CurrentSoldQty);

            return (NeedQty <= cuurentQty) ? true : false ;
        }
        protected void Save_prodcut_Click(object sender, EventArgs e)
        {
            mySalesInvoiceDetail = new SalesInvoiceDetail();

            //add New item to virtual Details List
            var last_index = SalesInvoiceDetailList.Count();
            if (last_index != 0)
            {
                last_index = SalesInvoiceDetailList[last_index - 1].ID;
            }
            mySalesInvoiceDetail = SalesInvoiceDetailModel();

            // Check of The quntity in Is there or Not
            bool IsThereEnoughGoods = ThereEnoughGoods(Convert.ToDouble(mySalesInvoiceDetail.Qty), mySalesInvoiceDetail.ProductID);
            if(IsThereEnoughGoods)
            {
                if (mySalesInvoiceDetail.ID == 0)
                {
                    mySalesInvoiceDetail.ID = last_index + 1;
                    SalesInvoiceDetailList.Add(mySalesInvoiceDetail);
                }
                else
                {
                    int removeditemindex = 0;
                    for (int i = 0; i < SalesInvoiceDetailList.Count; i++)
                    {
                        if (mySalesInvoiceDetail.ID == SalesInvoiceDetailList[i].ID)
                        {
                            removeditemindex = i;
                            break;
                        }
                    }
                    SalesInvoiceDetailList[removeditemindex] = mySalesInvoiceDetail;
                }
                //Calculate new Total
                if (SalesInvoiceDetailList.Count > 0)
                {
                    double totalPrice = 0;
                    for (int i = 0; i < SalesInvoiceDetailList.Count; i++)
                    {
                        totalPrice = totalPrice + Convert.ToDouble(SalesInvoiceDetailList[i].ProductPrice * SalesInvoiceDetailList[i].Qty);
                    }
                    Total.Text = totalPrice.ToString();
                }
                AddErrorTxt.Text = "";
                BindGridList();
                ClearPurchaseInvoiceDetailsModel();
            }
            else
            {
                ShowMessage("No Enough Quntity Of this Product");
            }
        }
        protected void edit_product_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<SalesInvoiceDetail> gvr = (List<SalesInvoiceDetail>)GridView1.DataSource;
            SalesInvoiceDetail SalesInvoDetEdit = gvr[index];
            HidenIdpurchasInvDe.Value = SalesInvoDetEdit.ID.ToString();
            ProductListtxt.SelectedValue = SalesInvoDetEdit.ProductID.ToString();
            Qty.Text = SalesInvoDetEdit.Qty.ToString();
            ProductPrice.Text = SalesInvoDetEdit.ProductPrice.ToString();
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            double totalPrice = 0;
            if (SalesInvoiceDetailList.Count > 0)
            {
                for (int i = 0; i < SalesInvoiceDetailList.Count; i++)
                {
                    totalPrice = totalPrice + Convert.ToDouble(SalesInvoiceDetailList[i].ProductPrice * SalesInvoiceDetailList[i].Qty);
                }
            }
            //total edit
            int index = Convert.ToInt32(e.CommandArgument);
            totalPrice = totalPrice - Convert.ToDouble(SalesInvoiceDetailList[index].ProductPrice * SalesInvoiceDetailList[index].Qty);
            //temptotal = totalPrice.ToString();
            Total.Text = totalPrice.ToString();
            // remove item
            SalesInvoiceDetailList.RemoveAt(index);
            BindGridList();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void BindGridList()
        {
            foreach (var item in SalesInvoiceDetailList)
            {
                item.Product = db.Product.Where(col => col.ID == item.ProductID).FirstOrDefault();
            }
            GridView1.DataSource = SalesInvoiceDetailList;
            GridView1.DataBind();
        }
        protected void ProdNameFilt_TextChanged(object sender, EventArgs e)
        {
            ProductList = new List<Product>();
            myproduct = new Product();
            if (ProdNameFilt.Text != "")
            {
                var name = new SqlParameter();
                var SectorID = new SqlParameter();

                name = new SqlParameter("@name", ProdNameFilt.Text.Trim());
                SectorID = new SqlParameter("@SectorID", DBNull.Value);

                ProductList = db.Database
               .SqlQuery<Product>("SP_Product @name ,@SectorID  ", name, SectorID).ToList();
            }
            else
            {
                ProductList = db.Product.ToList();
            }
            //Product  (For Adding will be forced to choose)
            ProductListtxt.Items.Clear();
            BindDropDownList(ProductList, ProductListtxt);
        }
        protected SalesInvoiceDetail SalesInvoiceDetailModel()
        {
            mySalesInvoiceDetail = new SalesInvoiceDetail();

            if (HidenIdpurchasInvDe.Value != "0")
            {
                mySalesInvoiceDetail.ID = Convert.ToInt32(HidenIdpurchasInvDe.Value);
            }
            //
            if (ProductListtxt.SelectedValue != "")
            {
                mySalesInvoiceDetail.ProductID = ProductListtxt.SelectedValue;
            }
            //
            if (ProductPrice.Text != "")
            {
                mySalesInvoiceDetail.ProductPrice = Convert.ToDouble(ProductPrice.Text);
            }
            //
            if (Qty.Text != "")
            {
                mySalesInvoiceDetail.Qty = Convert.ToDouble(Qty.Text);
            }
            mySalesInvoiceDetail.PricePerRecord = Convert.ToDouble(mySalesInvoiceDetail.Qty * mySalesInvoiceDetail.ProductPrice);
            return mySalesInvoiceDetail;
        }
        protected void ClearPurchaseInvoiceDetailsModel()
        {
            ProductListtxt.SelectedIndex = 0;
            HidenIdpurchasInvDe.Value = "0";
            ProductPrice.Text = "0";
            Qty.Text = "0";
        }
        //All invoice Part
        protected void Save_Invoice_Click(object sender, EventArgs e)
        {
            AddErrorTxt.Text = "";
            mySalesInvoice = new SalesInvoice();
            mySalesInvoice = SalesInvoiceModel();
            if (opertypeforinvoice == "add")
            {
                SalesInvoice TempSalesInvoice = new SalesInvoice();
                TempSalesInvoice = db.SalesInvoice.Where(pro => pro.Id == mySalesInvoice.Id).FirstOrDefault();
                if (TempSalesInvoice == null)
                {
                    try
                    {
                        db.SalesInvoice.Add(mySalesInvoice);
                        db.SaveChanges();
                        //add Inner Details
                        for (int i = 0; i < SalesInvoiceDetailList.Count; i++)
                        {
                            try
                            {
                                SalesInvoiceDetailList[i].ID = 0;
                                SalesInvoiceDetailList[i].PurchaseInvoiceID = mySalesInvoice.Id;
                                db.SalesInvoiceDetail.Add(SalesInvoiceDetailList[i]);
                                db.SaveChanges();
                            }
                            catch (Exception except)
                            {
                                ShowMessage("Something Wrong Will Adding Inner Details");
                            }
                        }
                    }
                    catch (Exception except)
                    {
                        ShowMessage("Something Wrong Will Adding");
                    }
                }
                else
                {
                    ShowMessage("Dubplicated Product Code");
                }

            }
            else if (opertypeforinvoice == "Edit")
            {
                try
                {
                    db.SalesInvoice.AddOrUpdate(mySalesInvoice);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                    ShowMessage("An Error Will Editing Main Item ..");
                }
                for (int i = 0; i < SalesInvoiceDetailList.Count; i++)
                {
                    mySalesInvoiceDetail = new SalesInvoiceDetail();
                    try
                    {
                        SalesInvoiceDetailList[i].PurchaseInvoiceID = mySalesInvoice.Id;
                        db.SalesInvoiceDetail.AddOrUpdate(SalesInvoiceDetailList[i]);
                        db.SaveChanges();
                    }
                    catch (Exception except)
                    {
                        ShowMessage("Something Wrong Will Adding Inner Details");
                    }
                }
            }
            if (AddErrorTxt.Text == "")
            {
                var CarChangeRateOnDisList = db.CarChangeRateOnDis.Where(item => item.CarId == mySalesInvoice.CarId && item.ServiceId == mySalesInvoice.ServiceId).ToList();
                for(int i=0; i < CarChangeRateOnDisList.Count; i++)
                {
                    CarChangeRateOnDisList[i].State = true;
                    db.CarChangeRateOnDis.AddOrUpdate(CarChangeRateOnDisList[i]);
                    db.SaveChanges();
                }
                ClearPurchaseInvoiceDetailsModel();
                ClearPurchaseInvoiceModel();
                opertypeforinvoice = "add";
                VisitedAtEdit = false;
                Response.Redirect("../Maintenance/ListingSalesInvoice.aspx");
            }
        }
        protected void close_Save_Invoice_Click(object sender, EventArgs e)
        {
            ClearPurchaseInvoiceDetailsModel();
            ClearPurchaseInvoiceModel();
            opertypeforinvoice = "add";
            VisitedAtEdit = false;
            Response.Redirect("../Maintenance/ListingSalesInvoice.aspx");
        }
        protected SalesInvoice SalesInvoiceModel()
        {
            mySalesInvoice = new SalesInvoice();

            if (InvoiceNo.Text != "") { mySalesInvoice.Id = InvoiceNo.Text; }
            //
            if (InvoiceDate.Text != "") { mySalesInvoice.InvoiceDate =ExtendedMethod.FormatDate(InvoiceDate.Text); }
            //
            if (Sale.Checked) { mySalesInvoice.PurchaseType = Convert.ToBoolean(true); }
            else if (Discarded.Checked) { mySalesInvoice.PurchaseType = Convert.ToBoolean(false); }
            //
            if (CarsListtxt.SelectedValue != "") { mySalesInvoice.CarId = Convert.ToInt32(CarsListtxt.SelectedValue); }
            //
            if (ServicesListtxt.SelectedValue != "") { mySalesInvoice.ServiceId = Convert.ToInt32(ServicesListtxt.SelectedValue); }
            //
            if (Total.Text != "") { mySalesInvoice.Total = Convert.ToDouble(Total.Text); }
            //
            mySalesInvoice.UserID = 1;

            return mySalesInvoice;
        }
        protected void ClearPurchaseInvoiceModel()
        {
            InvoiceNo.Text = "";
            InvoiceDate.Text = "";
            Sale.Checked = false;
            Discarded.Checked = false;
            Total.Text = "";
            CarsListtxt.SelectedIndex = 0;
            ServicesListtxt.SelectedIndex = 0;
            SalesInvoiceDetailList = new List<SalesInvoiceDetail>();
        }
        //Get Edit Item With its Details
        protected void GetEditInvoice(string Id)
        {
            mySalesInvoice = new SalesInvoice();
            SalesInvoiceDetailList = new List<SalesInvoiceDetail>();
            //get Details List and bind it
            SalesInvoiceDetailList = db.SalesInvoiceDetail.Where(PID => PID.PurchaseInvoiceID == Id).ToList();
            BindGridList();
            //get Item and bind Its Values
            mySalesInvoice = db.SalesInvoice.Where(PI => PI.Id == Id).FirstOrDefault();

            if (mySalesInvoice.Id != null) { InvoiceNo.Text = mySalesInvoice.Id.ToString(); }

            if (mySalesInvoice.InvoiceDate != null) { InvoiceDate.Text = mySalesInvoice.InvoiceDate.ToString(); }

            if (mySalesInvoice.CarId != 0) { CarsListtxt.SelectedValue = mySalesInvoice.CarId.ToString(); }

            if (mySalesInvoice.ServiceId != 0) { ServicesListtxt.SelectedValue = mySalesInvoice.ServiceId.ToString(); }

            if (mySalesInvoice.PurchaseType == true)
            {
                Sale.Checked = true;
                Discarded.Checked = false;
            }
            else
            {
                Discarded.Checked = true;
                Sale.Checked = false;
            }
            //
            if (mySalesInvoice.Total != 0) { Total.Text = mySalesInvoice.Total.ToString(); }
            
            VisitedAtEdit = true;
        }

    }
}