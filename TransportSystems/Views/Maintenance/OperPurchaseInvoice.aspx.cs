using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.Maintenance
{
    public partial class OperPurchaseInvoice : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public  List<PurchaseInvoice> PurchaseInvoiceList = new List<PurchaseInvoice>();
        PurchaseInvoice myPurchaseInvoice = new PurchaseInvoice();

        public static List<PurchaseInvoiceDetail> PurchaseInvoiceDetailList = new List<PurchaseInvoiceDetail>();
        PurchaseInvoiceDetail myPurchaseInvoiceDetail = new PurchaseInvoiceDetail();

        public static List<SubAccount> SubAccountList = new List<SubAccount>();
        SubAccount mySubAccount = new SubAccount();

        public static List<Product> ProductList = new List<Product>();
        Product myproduct = new Product();

        public static string opertypeforinvoice = "";

        public static bool VisitedAtEdit = false;
        // el khazna
        public static List<KhznaMoved> KhznaMovedList;
        KhznaMoved myKhznaMoved ;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
            // make payment value and khzna list not displayed if not checked payment chck box
            PaymentValueId.Visible = false;
            TreasuryId.Visible = false;
            //initial values of all invoice
            var NewId = Convert.ToInt32(db.PurchaseInvoice.Select(item => item.Id).Max()) + 1;
            InvoiceNo.Text = NewId.ToString();
            InvoiceDate.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            PurchaseInvoiceDetailList = new List<PurchaseInvoiceDetail>();
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
            SubAccountList = db.SubAccount.Where(sub=>sub.UpAccount == "2103").ToList();
            vendorsListtxt.DataSource = SubAccountList;
            vendorsListtxt.DataTextField = "name";
            vendorsListtxt.DataValueField = "ID";
            vendorsListtxt.DataBind();
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
                var ProPrice =Convert.ToDouble(db.Product.Where(pro => pro.ID == tempProd.ID).FirstOrDefault().SPrice);
                ProductPrice.Text = Math.Round(ProPrice, 2).ToString();
            }
        }

        //Save all invoice Part
        protected void MakeKhaznaMovement(PurchaseInvoice myPurInvo, string state)
        {
            string descript = "أذن استلام بضاعه لفتوره رقم" + myPurInvo.Id;
            myKhznaMoved = new KhznaMoved();
            myKhznaMoved = db.KhznaMoved.Where(khz => khz.Description == descript).FirstOrDefault();
            // h3mlo add if it add or edit
            if (myKhznaMoved == null)
            {
                myKhznaMoved = new KhznaMoved();
                var newID = db.KhznaMoved.Where(kh => kh.state == true).Select(khm => khm.ID).Max();
                myKhznaMoved.ID = newID + 1;
                myKhznaMoved.state = true;
                myKhznaMoved.Value = Convert.ToDecimal(myPurInvo.PaymentValue);
                myKhznaMoved.AccountID = Convert.ToInt64(myPurInvo.SubAccountId);
                myKhznaMoved.Description = "أذن استلام بضاعه لفتوره رقم" + myPurInvo.Id;
                myKhznaMoved.Date = DateTime.Parse(myPurInvo.InvoiceDate.ToString(), CultureInfo.CreateSpecificCulture("ar-EG"));
                myKhznaMoved.EntryState = false;
                myKhznaMoved.EntryID = 0;
                myKhznaMoved.TreasuryID = Convert.ToInt64(TreasuryListtxt.SelectedValue);
                try
                {
                    db.KhznaMoved.Add(myKhznaMoved);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                    ShowMessage("Something Wrong Will Adding Khzna Movement");
                }
            }
            else
            {
                // if there are a movement and its state add Refuse it
                if (state == "add")
                {
                    ShowMessage("there are Khzna Movement with the Same Description");
                }
                else // if there are a movement and its state Edit update it
                {
                    KhznaMoved tempKhznaMoved = new KhznaMoved();
                    tempKhznaMoved.ID = myKhznaMoved.ID;
                    tempKhznaMoved.state = myKhznaMoved.state;
                    tempKhznaMoved.Value = Convert.ToDecimal(myPurInvo.PaymentValue);
                    tempKhznaMoved.AccountID = Convert.ToInt64(myPurInvo.SubAccountId);
                    tempKhznaMoved.Description = "أذن استلام بضاعه لفتوره رقم" + myPurInvo.Id;
                    tempKhznaMoved.Date = DateTime.Parse(myPurInvo.InvoiceDate.ToString(), CultureInfo.CreateSpecificCulture("ar-EG"));
                    tempKhznaMoved.TreasuryID = Convert.ToInt64(TreasuryListtxt.SelectedValue);
                    //Entry Update if Its he had made it لازم تجيب القيد وتغيره اوتو ماتك تاني هنا بناء علي التعديلات الي تمت
                    tempKhznaMoved.EntryID = 0; // hnrg3 n3del hna tany 3la al 2ed lma ydaf
                    tempKhznaMoved.EntryState = false;
                    try
                    {
                        db.KhznaMoved.AddOrUpdate(tempKhznaMoved);
                        db.SaveChanges();
                    }
                    catch (Exception except)
                    {
                        ShowMessage("Something Wrong Will Updating Khzna Movement");
                    }
                }
            }
        }
        protected void Save_Invoice_Click(object sender, EventArgs e)
        {
            AddErrorTxt.Text = "";
            myPurchaseInvoice = new PurchaseInvoice();
            myPurchaseInvoice = PurchaseInvoiceModel();
            if (opertypeforinvoice == "add")
            {
                PurchaseInvoice TempPurchaseInvo = new PurchaseInvoice();
                TempPurchaseInvo = db.PurchaseInvoice.Where(pro => pro.Id == myPurchaseInvoice.Id).FirstOrDefault();
                if (TempPurchaseInvo == null)
                {
                    try
                    {
                        db.PurchaseInvoice.Add(myPurchaseInvoice);
                        db.SaveChanges();
                        //add Inner Details
                        for (int i = 0; i < PurchaseInvoiceDetailList.Count; i++)
                        {
                            try
                            {
                                PurchaseInvoiceDetailList[i].ID = 0;
                                PurchaseInvoiceDetailList[i].PurchaseInvoiceID = myPurchaseInvoice.Id;
                                db.PurchaseInvoiceDetail.Add(PurchaseInvoiceDetailList[i]);
                                db.SaveChanges();
                            }
                            catch (Exception except)
                            {
                                ShowMessage("Something Wrong Will Adding Inner Details");
                            }
                        }
                        //add Movement To Khazna
                        if(paymentMethod.Checked == true)
                        {
                            MakeKhaznaMovement(myPurchaseInvoice, "add");
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
                    db.PurchaseInvoice.AddOrUpdate(myPurchaseInvoice);
                    db.SaveChanges();
                }
                catch(Exception except)
                {
                    ShowMessage("An Error Will Editing Main Item ..");
                }
                for (int i = 0; i < PurchaseInvoiceDetailList.Count; i++)
                {
                    myPurchaseInvoiceDetail = new PurchaseInvoiceDetail();
                    try
                    {
                        PurchaseInvoiceDetailList[i].PurchaseInvoiceID = myPurchaseInvoice.Id;
                        db.PurchaseInvoiceDetail.AddOrUpdate(PurchaseInvoiceDetailList[i]);
                        db.SaveChanges();
                    }
                    catch (Exception except)
                    {
                        ShowMessage("Something Wrong Will Adding Inner Details");
                    }
                }
                //add Movement To Khazna
                if (paymentMethod.Checked == true)
                {
                    MakeKhaznaMovement(myPurchaseInvoice, "Edit");
                }
            }
           if(AddErrorTxt.Text == "")
            {
                ClearPurchaseInvoiceDetailsModel();
                ClearPurchaseInvoiceModel();
                opertypeforinvoice = "add";
                VisitedAtEdit = false;
                Response.Redirect("../Maintenance/ListingPurchaseInvoice.aspx");
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
        protected void close_Save_Invoice_Click(object sender, EventArgs e)
        {
            ClearPurchaseInvoiceDetailsModel();
            ClearPurchaseInvoiceModel();
            opertypeforinvoice = "add";
            VisitedAtEdit = false;
            Response.Redirect("../Maintenance/ListingPurchaseInvoice.aspx");
        }
        protected PurchaseInvoice PurchaseInvoiceModel()
        {
            myPurchaseInvoice = new PurchaseInvoice();
           
            if (InvoiceNo.Text != "") { myPurchaseInvoice.Id = InvoiceNo.Text; }
            //
            if (InvoiceDate.Text != "") { myPurchaseInvoice.InvoiceDate =ExtendedMethod.FormatDate(InvoiceDate.Text); }
            //
            if (Purchase.Checked) { myPurchaseInvoice.PurchaseType = Convert.ToBoolean(true); }
            else if (Discarded.Checked) { myPurchaseInvoice.PurchaseType = Convert.ToBoolean(false); }
            //
            if (vendorsListtxt.SelectedValue != "") { myPurchaseInvoice.SubAccountId = Convert.ToInt32(vendorsListtxt.SelectedValue); }
            //
            if (PaymentValue.Text != "") { myPurchaseInvoice.PaymentValue = Convert.ToDouble(PaymentValue.Text); }
            //
            if (Total.Text != "") { myPurchaseInvoice.Total = Convert.ToDouble(Total.Text); }
            //
            myPurchaseInvoice.PaymentMethod = PaymentMethod(myPurchaseInvoice.Total, myPurchaseInvoice.PaymentValue);
            //
            myPurchaseInvoice.UserID = 1;
      
            return myPurchaseInvoice;
        }
        protected string PaymentMethod(double? total, double? paidValue)
        {
            string PaymentMethodtype = "";
            if (total != null || paidValue != null)
            {
                if (paidValue == 0) { PaymentMethodtype = "غير مدفوع"; }
                else if (total > paidValue) { PaymentMethodtype = "مدفوع جزء"; }
                else { PaymentMethodtype = "مدفوع كليا"; }
            }
            return PaymentMethodtype;
        }
        protected void ClearPurchaseInvoiceModel()
        {
            InvoiceNo.Text = "";
            InvoiceDate.Text = "";
            Purchase.Checked = false;
            Discarded.Checked = false;
            PaymentValue.Text = "";
            Total.Text = "";
            vendorsListtxt.SelectedIndex = 0;
            PurchaseInvoiceDetailList = new List<PurchaseInvoiceDetail>();
        }
        //Get Edit Item With its Details
        protected void GetEditInvoice(string Id)
        {
            myPurchaseInvoice = new PurchaseInvoice();
            PurchaseInvoiceDetailList = new List<PurchaseInvoiceDetail>();
            //get Details List and bind it
            PurchaseInvoiceDetailList = db.PurchaseInvoiceDetail.Where(PID => PID.PurchaseInvoiceID == Id).ToList();
            BindGridList();
            //get Item and bind Its Values
            myPurchaseInvoice = db.PurchaseInvoice.Where(PI => PI.Id == Id).FirstOrDefault();
           
            if (myPurchaseInvoice.Id != null) { InvoiceNo.Text = myPurchaseInvoice.Id.ToString(); }

            if (myPurchaseInvoice.InvoiceDate != null) { InvoiceDate.Text = myPurchaseInvoice.InvoiceDate.ToString(); }

            if (myPurchaseInvoice.SubAccountId != 0) { vendorsListtxt.SelectedValue = myPurchaseInvoice.SubAccountId.ToString(); }

            if (myPurchaseInvoice.PurchaseType == true)
            {
                Purchase.Checked = true;
                Discarded.Checked = false;
            }
            else
            {
                Discarded.Checked = true;
                Purchase.Checked = false;
            }
            //
            if (myPurchaseInvoice.Total != 0) { Total.Text = myPurchaseInvoice.Total.ToString(); }
            // get its moveMent if have 
            string descript = "أذن استلام بضاعه لفتوره رقم" + myPurchaseInvoice.Id;
            myKhznaMoved = new KhznaMoved();
            myKhznaMoved = db.KhznaMoved.Where(khz => khz.Description == descript).FirstOrDefault();
            if(myKhznaMoved != null)
            {
                //make check Box True
                paymentMethod.Checked = true;
                //make payment Filed and Treasury Visible
                PaymentValueId.Visible = true;
                TreasuryId.Visible = true;
                if (TreasuryListtxt.Items.Count > 0) { TreasuryListtxt.Items.Clear(); }
                TreasuryListtxt.DataSource = db.SubAccount.Where(sub => sub.UpAccount == "1103").ToList();
                TreasuryListtxt.DataValueField = "ID";
                TreasuryListtxt.DataTextField = "name";
                TreasuryListtxt.DataBind();
                //Select treasury Which in movement
                TreasuryListtxt.SelectedValue = myKhznaMoved.TreasuryID.ToString();
                PaymentValue.Text = myKhznaMoved.Value.ToString();
            }
            VisitedAtEdit = true;
        }
        //Product Part
        protected void Save_prodcut_Click(object sender, EventArgs e)
        {
            myPurchaseInvoiceDetail = new PurchaseInvoiceDetail();
            
            //add New item to virtual Details List
            var last_index = PurchaseInvoiceDetailList.Count();
            if (last_index != 0)
            {
                last_index = PurchaseInvoiceDetailList[last_index - 1].ID;
            }
            myPurchaseInvoiceDetail = PurchaseInvoiceDetailsModel();
            if (myPurchaseInvoiceDetail.ID == 0)
            {
                
                myPurchaseInvoiceDetail.ID = last_index + 1;
                PurchaseInvoiceDetailList.Add(myPurchaseInvoiceDetail);
            }
            else
            {
                int removeditemindex = 0;
                for (int i = 0; i < PurchaseInvoiceDetailList.Count; i++)
                {
                    if (myPurchaseInvoiceDetail.ID == PurchaseInvoiceDetailList[i].ID)
                    {
                        removeditemindex = i;
                        break;
                    }
                }
                PurchaseInvoiceDetailList[removeditemindex] = myPurchaseInvoiceDetail;
            }
            //Calculate new Total
            if (PurchaseInvoiceDetailList.Count > 0)
            {
                double totalPrice = 0;
                for (int i = 0; i < PurchaseInvoiceDetailList.Count; i++)
                {
                    totalPrice = totalPrice + Convert.ToDouble(PurchaseInvoiceDetailList[i].ProductPrice * PurchaseInvoiceDetailList[i].Qty);
                }
                Total.Text = totalPrice.ToString();
            }
            BindGridList();
            ClearPurchaseInvoiceDetailsModel();

        }
        protected void edit_product_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<PurchaseInvoiceDetail> gvr = (List<PurchaseInvoiceDetail>)GridView1.DataSource;
            PurchaseInvoiceDetail PurInvoDetEdit = gvr[index];
            HidenIdpurchasInvDe.Value = PurInvoDetEdit.ID.ToString();
            ProductListtxt.SelectedValue = PurInvoDetEdit.ProductID.ToString();
            Qty.Text = PurInvoDetEdit.Qty.ToString();
            ProductPrice.Text = PurInvoDetEdit.ProductPrice.ToString();
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            double totalPrice = 0;
            if (PurchaseInvoiceDetailList.Count > 0)
            {
                for (int i = 0; i < PurchaseInvoiceDetailList.Count; i++)
                {
                    totalPrice = totalPrice +Convert.ToDouble(PurchaseInvoiceDetailList[i].ProductPrice * PurchaseInvoiceDetailList[i].Qty);
                }
            }
            //total edit
            int index = Convert.ToInt32(e.CommandArgument);
            totalPrice = totalPrice - Convert.ToDouble(PurchaseInvoiceDetailList[index].ProductPrice * PurchaseInvoiceDetailList[index].Qty);
            //temptotal = totalPrice.ToString();
            Total.Text = totalPrice.ToString();
            // remove item
            PurchaseInvoiceDetailList.RemoveAt(index);
            BindGridList();
           
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void BindGridList()
        {
            foreach(var item in PurchaseInvoiceDetailList)
            {
                item.Product = db.Product.Where(col => col.ID == item.ProductID).FirstOrDefault();
            }
            GridView1.DataSource = PurchaseInvoiceDetailList;
            GridView1.DataBind();
        }
        protected void ProdNameFilt_TextChanged(object sender, EventArgs e)
        {
            ProductList = new List<Product>();
            myproduct = new Product();
            if(ProdNameFilt.Text !="")
            {
                var name = new SqlParameter();
                var SectorID = new SqlParameter();

                name = new SqlParameter("@name", ProdNameFilt.Text.Trim());
                SectorID = new SqlParameter("@SectorID", DBNull.Value);

                ProductList = db.Database
               .SqlQuery<Product>("SP_Product @name ,@SectorID  ", name, SectorID).ToList();
            }else
            {
                ProductList = db.Product.ToList();
            }
            //Product  (For Adding will be forced to choose)
            ProductListtxt.Items.Clear();
            BindDropDownList(ProductList, ProductListtxt);
        }
        protected PurchaseInvoiceDetail PurchaseInvoiceDetailsModel()
        {
            myPurchaseInvoiceDetail = new PurchaseInvoiceDetail();
          
            if (HidenIdpurchasInvDe.Value != "0")
            {
                myPurchaseInvoiceDetail.ID = Convert.ToInt32(HidenIdpurchasInvDe.Value);
            }
            //
            if (ProductListtxt.SelectedValue != "")
            {
                myPurchaseInvoiceDetail.ProductID = ProductListtxt.SelectedValue;
            }
            //
            if (ProductPrice.Text != "")
            {
                myPurchaseInvoiceDetail.ProductPrice = Convert.ToDouble(ProductPrice.Text);
            }
            //
            if (Qty.Text != "")
            {
                myPurchaseInvoiceDetail.Qty = Convert.ToDouble(Qty.Text);
            }
            myPurchaseInvoiceDetail.PricePerRecord =Convert.ToDouble(myPurchaseInvoiceDetail.Qty * myPurchaseInvoiceDetail.ProductPrice);
            return myPurchaseInvoiceDetail;
        }
        protected void ClearPurchaseInvoiceDetailsModel()
        {
            ProductListtxt.SelectedIndex = 0;
            HidenIdpurchasInvDe.Value = "0";
            ProductPrice.Text = "0";
            Qty.Text = "0";
        }

        protected void paymentMethod_CheckedChanged(object sender, EventArgs e)
        {
            if(paymentMethod.Checked==true)
            {
                //display khazna and payment value
                PaymentValueId.Visible = true;
                TreasuryId.Visible = true;

                if (TreasuryListtxt.Items.Count > 0) { TreasuryListtxt.Items.Clear(); }

                TreasuryListtxt.DataSource = db.SubAccount.Where(sub => sub.UpAccount == "1103").ToList();
                TreasuryListtxt.DataValueField = "ID";
                TreasuryListtxt.DataTextField = "name";
                TreasuryListtxt.DataBind();
            }
            else
            {
                //display khazna and payment value
                PaymentValueId.Visible = false;
                TreasuryId.Visible = false;
            }
            
        }

    }
}