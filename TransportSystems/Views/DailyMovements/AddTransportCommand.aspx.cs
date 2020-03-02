using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;
using AjaxControlToolkit;

namespace TransportSystems.Views.DailyMovements
{
    public partial class AddTransportCommand : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<TransportCommand> SearchTransCmdList = new List<TransportCommand>();
        List<TransportCommand> TransCmdList = new List<TransportCommand>();
        TransportCommand myTransCmd = new TransportCommand();

        //SubAccount_client
        public static List<SubAccount> SubAccClientList = new List<SubAccount>();
        SubAccount mySubAccClient = new SubAccount();
        //SubAccount_Vendor
        public static List<SubAccount> SubAccViendorList = new List<SubAccount>();
        SubAccount mySubAccViendor = new SubAccount();
        //Cars
        public static List<Cars> CarsList = new List<Cars>();
        Cars myCars = new Cars();
        //Product // elbda3a
        public static List<Product> ProductList = new List<Product>();
        Product myProduct = new Product();
        //FromRegion_FromRegion
        public static List<FromRegion> FromRegionList = new List<FromRegion>();
        FromRegion myFromRegion = new FromRegion();
        //FromRegion_ToRegion
        public static List<FromRegion> ToRegionList = new List<FromRegion>();
        FromRegion myToRegion = new FromRegion();

        public static string SelectedSubAccClientId = "", SelectedSubAccViendorId = ""
            , SelectedCarsId = "", SelectedProductId = "", SelectedFromRegionId = "", SelectedToRegionId = ""
            , SelectedTransportTypeId = "", SelectedPaymentWayId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Set_initial();
            }
            SelectedSubAccClientId = AddClientListhtml.SelectedValue;
            SelectedSubAccViendorId = AddVendorListhtml.SelectedValue;
            SelectedCarsId = AddCarsListhtml.SelectedValue;
            SelectedProductId = AddProductListhtml.SelectedValue;
            SelectedFromRegionId = AddFromRegionListhtml.SelectedValue;
            SelectedToRegionId = AddRegionToListhtml.SelectedValue;
            SelectedTransportTypeId = AddTransportTypeListhtml.SelectedValue;
            SelectedPaymentWayId = AddPaymentMethodListhtml.SelectedValue;
            TransCmdList = db.TransportCommand.ToList();
            BindGridList();
        }
        protected void Set_initial()
        {
            //make custom ValidationGroup >>><<<<
            string vgId = Guid.NewGuid().ToString();
            //
            TransCmdTxt.ValidationGroup = vgId;
            RequiredFieldValidator1.ValidationGroup = vgId;
            //
            TransCmdDate.ValidationGroup = vgId;
            RequiredFieldValidator2.ValidationGroup = vgId;
            //
            AtHour.ValidationGroup = vgId;
            RequiredFieldValidator3.ValidationGroup = vgId;
            //
            TransportPrice.ValidationGroup = vgId;
            RequiredFieldValidator4.ValidationGroup = vgId; 
            //
            Quantity.ValidationGroup = vgId;
            RequiredFieldValidator5.ValidationGroup = vgId;
            //
            TotalTransportPrice.ValidationGroup = vgId;
            RequiredFieldValidator6.ValidationGroup = vgId;
            //
            ValidationSummary1.ValidationGroup = vgId;
            Save.ValidationGroup = vgId;
            //SubAccount_client
            SubAccClientList = db.SubAccount.Where(subc=>subc.UpAccount =="1101").ToList();
            BindDropDownList2(SubAccClientList, AddClientListhtml);
            List<SubAccount> TempAccClientlist = new List<SubAccount>();
            TempAccClientlist.Add(new SubAccount { name = "-- select Client --", ID = 0 });
            TempAccClientlist.AddRange(SubAccClientList);
            BindDropDownList2(TempAccClientlist, SubClientSearchListtxt);
            //SubAccount_Vendor
            SubAccViendorList = db.SubAccount.Where(subc => subc.UpAccount == "2103").ToList();
            BindDropDownList2(SubAccViendorList, AddVendorListhtml);
            List<SubAccount> TempAccViendorlist = new List<SubAccount>();
            TempAccViendorlist.Add(new SubAccount { name = "-- select Vendor --", ID = 0 });
            TempAccViendorlist.AddRange(SubAccViendorList);
            BindDropDownList2(TempAccViendorlist, SubVendorSearchListtxt);
            //Cars  (For Adding will be forced to choose)
            CarsList = db.Cars.ToList();
            BindDropDownList3(CarsList, AddCarsListhtml);
            List<Cars> TempCarslist = new List<Cars>();
            TempCarslist.Add(new Cars { CarNo = "-- select Car Number --", id = 0 ,});
            TempCarslist.AddRange(CarsList);
            BindDropDownList3(TempCarslist, CarSearchListtxt);
            //Product  (For Adding will be forced to choose)
            ProductList = db.Product.Where(pro=>pro.SectorID == 4).ToList();
            BindDropDownList2(ProductList, AddProductListhtml);
            List<Product> TempProductlist = new List<Product>();
            TempProductlist.Add(new Product { name = "-- select Product Type --", ID = "-1" });
            TempProductlist.AddRange(ProductList);
            BindDropDownList2(TempProductlist, ProductSearchListtxt);
            //FromRegionList(For Adding will be forced to choose)
            FromRegionList = db.FromRegion.ToList();
            BindDropDownList(FromRegionList, AddFromRegionListhtml);
            List<FromRegion> TempFromRegionlist = new List<FromRegion>();
            TempFromRegionlist.Add(new FromRegion { Name = "-- select From Region --", Id = 0 });
            TempFromRegionlist.AddRange(FromRegionList);
            BindDropDownList(TempFromRegionlist, FromRegionSearchListtxt);
            //ToRegionList(For Adding will be forced to choose)
            ToRegionList = db.FromRegion.ToList();
            BindDropDownList(ToRegionList, AddRegionToListhtml);
            List<FromRegion> TempToRegionlist = new List<FromRegion>();
            TempToRegionlist.Add(new FromRegion { Name = "-- select To Region --", Id = 0 });
            TempToRegionlist.AddRange(ToRegionList);
            BindDropDownList(TempToRegionlist, ToRegionSearchListtxt);
            // Initial Values
            if (db.TransportCommand.ToList().Count > 0)
            {
                TransCmdTxt.Text = (db.TransportCommand.Select(item => item.Id).Max() + 1).ToString();
            }else { TransCmdTxt.Text = "1"; }
            //
            TransCmdDate.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            //
            Quantity.Text = "0";
            TotalTransportPrice.Text = "0";
            //get TransportPrice in its textBox
            GetTransportPrice();
            //get WholeTransportPrice in its textBox
            GetTotalTransportPrice();
        }
        protected void GetTotalTransportPrice()
        {
            double Qty , TransPrice;
            //
            if (Quantity.Text != "") { Qty = Convert.ToDouble(Quantity.Text);} else { Qty = 0; }
            //
            if (TransportPrice.Text != "") { TransPrice = Convert.ToDouble(TransportPrice.Text); } else { TransPrice = 0; }
            //
            if (AddTransportTypeListhtml.SelectedIndex ==0)
            {TotalTransportPrice.Text = (Qty * TransPrice).ToString();}
            else {TotalTransportPrice.Text = TransPrice.ToString();}
        }
        protected void GetTransportPrice()
        {
            int FromReg = Convert.ToInt32(AddFromRegionListhtml.SelectedValue);
            int ToReg = Convert.ToInt32(AddRegionToListhtml.SelectedValue);
            int vendor = Convert.ToInt32(AddVendorListhtml.SelectedValue);
            ListPrice LPrice = db.ListPrice.Where(lP => lP.RegionFromId == FromReg &&
            lP.RegionToId == ToReg && lP.SubAccId == vendor).FirstOrDefault();
            if(LPrice != null) { TransportPrice.Text = LPrice.Price.ToString(); }
            else { TransportPrice.Text ="0"; }
            // get all Total Again
            GetTotalTransportPrice();
        }
        //get TransportPrice on change lists
        protected void AddFromRegionListhtml_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTransportPrice();
        }
        protected void AddRegionToListhtml_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTransportPrice();
        }
        protected void AddVendorListhtml_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTransportPrice();
            Quantity.Text = "5678909876";
           
           

            ListPrice _ListPrice=db.ListPrice.ToList().FirstOrDefault(o => o.SubAccId == int.Parse(AddVendorListhtml.SelectedValue));
            AddFromRegionListhtml.SelectedValue = _ListPrice.RegionFromId.ToString();
            AddRegionToListhtml.SelectedValue = _ListPrice.RegionToId.ToString();
            TransportPrice.Text = _ListPrice.Price.ToString() ;
            AddTransportTypeListhtml.SelectedValue = _ListPrice.ProductType.ToString();


        }
        //Bind All DropDowns
        protected void BindDropDownList(object DataSourceList, object DropdownList)
        {
            DropDownList Droplist = DropdownList as DropDownList;
            Droplist.DataSource = DataSourceList;
            Droplist.DataTextField = "Name";
            Droplist.DataValueField = "Id";
            Droplist.DataBind();
        }
        protected void BindDropDownList2(object DataSourceList, object DropdownList)
        {
            DropDownList Droplist = DropdownList as DropDownList;
            Droplist.DataSource = DataSourceList;
            Droplist.DataTextField = "name";
            Droplist.DataValueField = "ID";
            Droplist.DataBind();
        }
        protected void BindDropDownList3(object DataSourceList, object DropdownList)
        {
            DropDownList Droplist = DropdownList as DropDownList;
            Droplist.DataSource = DataSourceList;
            Droplist.DataTextField = "CarNo";
            Droplist.DataValueField = "Id";
            Droplist.DataBind();
        }

        //get TotalTransportPrice on change lists
        protected void AddTransportTypeListhtml_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalTransportPrice();
        }
        protected void TransportPrice_TextChanged(object sender, EventArgs e)
        {
            GetTotalTransportPrice();
        }
        protected void Quantity_TextChanged(object sender, EventArgs e)
        {
            GetTotalTransportPrice();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void BindGridList()
        {
            foreach (var item in TransCmdList)
            {
                item.SubAccount = db.SubAccount.Where(col => col.ID == item.SubAccClientId).FirstOrDefault();
                item.SubAccount1 = db.SubAccount.Where(col => col.ID == item.SubAccVendorId).FirstOrDefault();
                item.Cars = db.Cars.Where(col => col.id == item.CarId).FirstOrDefault();
                item.Product = db.Product.Where(col => col.ID == item.ProductId).FirstOrDefault();
                item.FromRegion = db.FromRegion.Where(col => col.Id == item.FromRegionId).FirstOrDefault();
                item.FromRegion1 = db.FromRegion.Where(col => col.Id == item.ToRegionId).FirstOrDefault();
            }
            GridView1.DataSource = TransCmdList;
            GridView1.DataBind();
        }
        //Operation Part
        protected void Save_Click(object sender, EventArgs e)
        {
            TransportCommand TransCmd = TransportCommandModel();
            if (TransCmd.Id == 0)
            {
                try
                {
                    db.TransportCommand.Add(TransCmd);
                    db.SaveChanges();
                  //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "تنبيه", "alert('تم الحفظ بنجاح')", true);

                }
                catch (Exception except)
                {
                  //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "تنبيه", "alert('حدثت مشكلة لم يتم الحفظ')", true);


                }
            }
            else
            {
                try
                {
                    db.TransportCommand.AddOrUpdate(TransCmd);
                    db.SaveChanges();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "تنبيه", "alert('تم الحفظ بنجاح')", true);

                }
                catch (Exception except)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "تنبيه", "alert('حدثت مشكلة لم يتم الحفظ')", true);

                }
            }
            ClearModel();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            TransportCommand EditTransCmd = new TransportCommand();
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchTransCmdList.Count > 0)
            { EditTransCmd = SearchTransCmdList[index]; }
            else { EditTransCmd = TransCmdList[index]; }
            try
            {
                TransCmdTxt.Text = EditTransCmd.Id.ToString();
                TransCmdDate.Text = EditTransCmd.TransportCommandTime.ToString();
                AddClientListhtml.SelectedValue = EditTransCmd.SubAccClientId.ToString();
                AddVendorListhtml.SelectedValue = EditTransCmd.SubAccVendorId.ToString();
                AddCarsListhtml.SelectedValue = EditTransCmd.CarId.ToString();
                AddProductListhtml.SelectedValue = EditTransCmd.ProductId.ToString();
                AddFromRegionListhtml.SelectedValue = EditTransCmd.FromRegionId.ToString();
                AddRegionToListhtml.SelectedValue = EditTransCmd.ToRegionId.ToString();
                TransportPrice.Text = EditTransCmd.TransportPrice.ToString();
                Quantity.Text = EditTransCmd.Quantity.ToString();
                AddTransportTypeListhtml.SelectedValue = EditTransCmd.TransportType.ToString();
                TotalTransportPrice.Text = EditTransCmd.TotalTransportPrice.ToString();
                AddPaymentMethodListhtml.SelectedValue = EditTransCmd.PaymentWay.ToString();
                AtHour.Text = EditTransCmd.TimeOfShipping.ToString();
            }
            catch (Exception Exec)
            {

            }
            SearchTransCmdList = new List<TransportCommand>();
            UpdatePanel3.Update();
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int deleted_index = 0;
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchTransCmdList.Count > 0)
            { deleted_index = Convert.ToInt32(SearchTransCmdList[index].Id); }
            else { deleted_index = Convert.ToInt32(TransCmdList[index].Id); }

            var deletedItem = db.TransportCommand.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.TransportCommand.Remove(deletedItem);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        public static string ReformateDateFromPicker(string date)
        {
            var alldate = date.Split('-');
            return alldate[0] + "-" + alldate[1] + "-" + alldate[2];
        }

        protected TransportCommand TransportCommandModel()
        {
            TransportCommand TransCmd = new TransportCommand();
            try {
                //DateTime CDate = DateTime.ParseExact(TransCmdDate.Text,
                //                     "dd/MM/yyyy",
                //                     CultureInfo.CreateSpecificCulture("ar-EG"));

                if (TransCmdTxt.Text != "") { TransCmd.Id = Convert.ToInt32(TransCmdTxt.Text); }
                if (TransCmdDate.Text != "") { TransCmd.TransportCommandTime =DateTime.Parse(TransCmdDate.Text,CultureInfo.CreateSpecificCulture("ar-EG")); }

                if (SelectedSubAccClientId != "") { TransCmd.SubAccClientId = Convert.ToInt32(SelectedSubAccClientId); }
            }catch(Exception ex)
            {

            }
            if (SelectedSubAccViendorId != "") { TransCmd.SubAccVendorId = Convert.ToInt32(SelectedSubAccViendorId); }

            if (SelectedCarsId != "") { TransCmd.CarId = Convert.ToInt32(SelectedCarsId); }

            if (SelectedProductId != "") { TransCmd.ProductId = SelectedProductId;}

            if (SelectedFromRegionId != "") { TransCmd.FromRegionId = Convert.ToInt32(SelectedFromRegionId); }

            if (SelectedToRegionId != "") { TransCmd.ToRegionId = Convert.ToInt32(SelectedToRegionId); }

            if (TransportPrice.Text != "") { TransCmd.TransportPrice = Convert.ToDouble(TransportPrice.Text); }

            if (Quantity.Text != "") { TransCmd.Quantity = Convert.ToDouble(Quantity.Text); }

            if (SelectedTransportTypeId != "") { TransCmd.TransportType = SelectedTransportTypeId ; }

            if (TotalTransportPrice.Text != "") { TransCmd.TotalTransportPrice = Convert.ToDouble(TotalTransportPrice.Text); }

            if (SelectedPaymentWayId != "") { TransCmd.PaymentWay = SelectedPaymentWayId; }

            if (AtHour.Text != "") { TransCmd.TimeOfShipping = AtHour.Text; }
            TransCmd.LoginID= ExtendedMethod.GetUserData(User.Identity.Name).Id;
            return TransCmd;
        }
        protected void ClearModel()
        {
            TransCmdTxt.Text = "";
            TransCmdDate.Text = "";
            AddClientListhtml.SelectedIndex = 0;
            AddVendorListhtml.SelectedIndex = 0;
            AddCarsListhtml.SelectedIndex = 0;
            AddProductListhtml.SelectedIndex = 0;
            AddFromRegionListhtml.SelectedIndex = 0;
            AddRegionToListhtml.SelectedIndex = 0;
            TransportPrice.Text = "";
            Quantity.Text = "";
            AddTransportTypeListhtml.SelectedIndex = 0;
            TotalTransportPrice.Text = "90";
            AddPaymentMethodListhtml.SelectedIndex = 0;
            AtHour.Text = "";
        }
        // Searching Part
        protected void Search_Click(object sender, EventArgs e)
        {
            SearchTransCmdList = new List<TransportCommand>();
            TransCmdList = new List<TransportCommand>();
            myTransCmd = new TransportCommand();
            myTransCmd = SearchModel();

            var SubAccClientId = new SqlParameter();
            var SubAccVendorId = new SqlParameter();
            var CarId = new SqlParameter();
            var ProductId = new SqlParameter();
            var FromRegionId = new SqlParameter();
            var ToRegionId = new SqlParameter();
            var TransportType = new SqlParameter();
            var PaymentWay = new SqlParameter();
            //
            if (myTransCmd.SubAccClientId != 0){ SubAccClientId = new SqlParameter("@SubAccClientId", myTransCmd.SubAccClientId);}
            else { SubAccClientId = new SqlParameter("@SubAccClientId", DBNull.Value); }
            //
            if (myTransCmd.SubAccVendorId != 0) { SubAccVendorId = new SqlParameter("@SubAccVendorId", myTransCmd.SubAccVendorId); }
            else { SubAccVendorId = new SqlParameter("@SubAccVendorId", DBNull.Value); }
            //
            if (myTransCmd.CarId != 0) { CarId = new SqlParameter("@CarId", myTransCmd.CarId); }
            else { CarId = new SqlParameter("@CarId", DBNull.Value); }
            //
            if (myTransCmd.ProductId != "-1") { ProductId = new SqlParameter("@ProductId", myTransCmd.ProductId.Trim()); }
            else { ProductId = new SqlParameter("@ProductId", DBNull.Value); }
            //
            if (myTransCmd.FromRegionId != 0) { FromRegionId = new SqlParameter("@FromRegionId", myTransCmd.FromRegionId); }
            else { FromRegionId = new SqlParameter("@FromRegionId", DBNull.Value); }
            //
            if (myTransCmd.ToRegionId != 0) { ToRegionId = new SqlParameter("@ToRegionId", myTransCmd.ToRegionId); }
            else { ToRegionId = new SqlParameter("@ToRegionId", DBNull.Value); }
            //
            if (myTransCmd.TransportType != "اختار نوع النقل") { TransportType = new SqlParameter("@TransportType", myTransCmd.TransportType.Trim()); }
            else { TransportType = new SqlParameter("@TransportType", DBNull.Value); }
            //
            if (myTransCmd.PaymentWay != "اختار نوع الدفع") { PaymentWay = new SqlParameter("@PaymentWay", myTransCmd.PaymentWay.Trim()); }
            else { PaymentWay = new SqlParameter("@PaymentWay", DBNull.Value); }

            TransCmdList = db.Database
                .SqlQuery<TransportCommand>("SP_TransportCommand @SubAccClientId , @SubAccVendorId , @CarId , @ProductId , @FromRegionId , @ToRegionId , @TransportType , @PaymentWay"
                , SubAccClientId, SubAccVendorId, CarId, ProductId, FromRegionId, ToRegionId, TransportType, PaymentWay).ToList();

            SearchTransCmdList = TransCmdList;
            BindGridList();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            SearchTransCmdList = new List<TransportCommand>();
            ClearSearchModel();
            TransCmdList = new List<TransportCommand>();
            myTransCmd = new TransportCommand();
            TransCmdList = db.TransportCommand.ToList();
            BindGridList();
        }

        protected TransportCommand SearchModel()
        {
            TransportCommand TransCmd = new TransportCommand();
            TransCmd.SubAccClientId = Convert.ToInt32(SubClientSearchListtxt.SelectedValue);
            TransCmd.SubAccVendorId = Convert.ToInt32(SubVendorSearchListtxt.SelectedValue);
            TransCmd.CarId = Convert.ToInt32(CarSearchListtxt.SelectedValue);
            TransCmd.ProductId = ProductSearchListtxt.SelectedValue;
            TransCmd.FromRegionId = Convert.ToInt32(FromRegionSearchListtxt.SelectedValue);
            TransCmd.ToRegionId = Convert.ToInt32(ToRegionSearchListtxt.SelectedValue);
            TransCmd.TransportType = TransTypeSearchListtxt.SelectedValue;
            TransCmd.PaymentWay = PayMethSearchListtxt.SelectedValue;
            return TransCmd;
        }
        protected void ClearSearchModel()
        {
            SubClientSearchListtxt.SelectedIndex = 0;
            SubVendorSearchListtxt.SelectedIndex = 0;
            CarSearchListtxt.SelectedIndex = 0;
            ProductSearchListtxt.SelectedIndex = 0;
            FromRegionSearchListtxt.SelectedIndex = 0;
            ToRegionSearchListtxt.SelectedIndex = 0;
            TransTypeSearchListtxt.SelectedIndex = 0;
            PayMethSearchListtxt.SelectedIndex = 0;
        }
        //Payment Way Part
        protected void AddPaymentMethodListhtml_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}