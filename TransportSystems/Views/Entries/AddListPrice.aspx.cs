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

namespace TransportSystems.Views.Entries
{
    public partial class AddListPrice : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<ListPrice> SearchListPrice = new List<ListPrice>();
        List<ListPrice> ListPrice = new List<ListPrice>();
        ListPrice myListPrice = new ListPrice();

        public static List<FromRegion> FromRegionList = new List<FromRegion>();
        
        FromRegion myFromRegion = new FromRegion();

        public static List<FromRegion> ToRegionList = new List<FromRegion>();
        FromRegion myToRegion = new FromRegion();

        public static List<SubAccount> SubAccountList = new List<SubAccount>();
        SubAccount mySubAccount = new SubAccount();

        public static List<TransferProductType> TransferProductTypeList = new List<TransferProductType>();

        public static string SelectedFromRegionId = "", SelectedToRegionId = "", SelectedSubAccountId = "",ProductType="";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Set_initial();
            }
            SelectedFromRegionId = AddRegionFromListhtml.SelectedValue;
            SelectedToRegionId = AddRegionToListhtml.SelectedValue;
            SelectedSubAccountId = AddSupplierListhtml.SelectedValue;
            ProductType = TransportProductTypeDrop.SelectedValue;
            ListPrice = db.ListPrice.ToList();
            BindGridList();
        }
        protected void Set_initial()
        {
            //make custom ValidationGroup >>><<<<
            string vgId = Guid.NewGuid().ToString();

            PriceTxt.ValidationGroup = vgId;
            RequiredFieldValidator1.ValidationGroup = vgId;
            ValidationSummary1.ValidationGroup = vgId;
            Save.ValidationGroup = vgId;
            // bind list Region From and To (For Adding will be forced to choose)
            FromRegionList = db.FromRegion.ToList();
             TransferProductTypeList = db.TransferProductType.ToList();
            BindDropDownList(FromRegionList, AddRegionFromListhtml);
            BindDropDownList(FromRegionList, AddRegionToListhtml);
            BindDropDownList(TransferProductTypeList, TransportProductTypeDrop);

            // (For search will be optional to choose)
            List<FromRegion> TempFromRegionlist = new List<FromRegion>();
            TempFromRegionlist.Add(new FromRegion { Name = "-- select Region --", Id = 0 });
            TempFromRegionlist.AddRange(FromRegionList);
            BindDropDownList(TempFromRegionlist, RegionFromSearchListtxt);
            BindDropDownList(TempFromRegionlist, RegionToSearchListtxt);

            // bind list SubAcc (For Adding will be forced to choose)
            SubAccountList = db.SubAccount.Where(sub => sub.UpAccount == "2103").ToList();
            AddSupplierListhtml.DataSource = SubAccountList;
            AddSupplierListhtml.DataTextField = "name";
            AddSupplierListhtml.DataValueField = "ID";
            AddSupplierListhtml.DataBind();
            // (For search will be optional to choose)
            List<SubAccount> TempSubAccountlist = new List<SubAccount>();
            TempSubAccountlist.Add(new SubAccount { name = "-- select Supplier --", ID = 0 });
            TempSubAccountlist.AddRange(SubAccountList);
            SupplierSearchListtxt.DataSource = TempSubAccountlist;
            SupplierSearchListtxt.DataTextField = "name";
            SupplierSearchListtxt.DataValueField = "ID";
            SupplierSearchListtxt.DataBind();

        }
        protected void BindDropDownList(object DataSourceList, object DropdownList)
        {
            DropDownList Droplist = DropdownList as DropDownList;
            Droplist.DataSource = DataSourceList;
            Droplist.DataTextField = "Name";
            Droplist.DataValueField = "Id";
            Droplist.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = ListPrice;
            GridView1.DataBind();
        }

        // Save , Edit  and Delete Part
        protected void Save_Click(object sender, EventArgs e)
        {
            ListPrice listPrice = ListPriceModel();
            if (listPrice.Id == 0)
            {
                try
                {
                    
                    db.ListPrice.Add(listPrice);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                }
            }
            else
            {
                try
                {
                    db.ListPrice.AddOrUpdate(listPrice);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                }
            }
            ClearModel();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        protected ListPrice ListPriceModel()
        {
            ListPrice listPrice = new ListPrice();
            if (IdHid.Value != "0") { listPrice.Id = Convert.ToInt32(IdHid.Value); }

            if (PriceTxt.Text != "") { listPrice.Price = Convert.ToInt32(PriceTxt.Text); }

            if (SelectedFromRegionId != "") { listPrice.RegionFromId = Convert.ToInt32(SelectedFromRegionId); }

            if (SelectedToRegionId != "") { listPrice.RegionToId = Convert.ToInt32(SelectedToRegionId); }

            if (SelectedSubAccountId != "") { listPrice.SubAccId = Convert.ToInt32(SelectedSubAccountId); }
            if(ProductType!="") {  listPrice.ProductType = Convert.ToInt32(ProductType); }
            listPrice.LoginID= ExtendedMethod.LoginedUser.Id;
            return listPrice;
        }
        protected void ClearModel()
        {
            IdHid.Value = "0";
            PriceTxt.Text = "";
            AddRegionFromListhtml.SelectedIndex = 0;
            AddRegionToListhtml.SelectedIndex = 0;
            AddSupplierListhtml.SelectedIndex = 0;
            TransportProductTypeDrop.SelectedIndex = 0;

        }
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            ListPrice temListPrice = new ListPrice();
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchListPrice.Count > 0)
            { temListPrice = SearchListPrice[index]; }
            else { temListPrice = ListPrice[index]; }
            try
            {
                IdHid.Value = temListPrice.Id.ToString();

                PriceTxt.Text = temListPrice.Price.ToString();

                AddRegionFromListhtml.SelectedValue = temListPrice.RegionFromId.ToString();

                AddRegionToListhtml.SelectedValue = temListPrice.RegionToId.ToString();

                AddSupplierListhtml.SelectedValue = temListPrice.SubAccId.ToString();
                TransportProductTypeDrop.SelectedValue = temListPrice.ProductType.ToString();
              //  TransportProductTypeDrop.SelectedValue=temListPrice.
            }
            catch (Exception Exec)
            {

            }
            SearchListPrice = new List<ListPrice>();
            UpdatePanel3.Update();
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int deleted_index = 0;
            int index = Convert.ToInt32(e.CommandArgument);
            if (SearchListPrice.Count > 0)
            { deleted_index = Convert.ToInt32(SearchListPrice[index].Id); }
            else { deleted_index = Convert.ToInt32(ListPrice[index].Id); }

            var deletedListPrice = db.ListPrice.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.ListPrice.Remove(deletedListPrice);
            db.SaveChanges();

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        // Sarch Part
        protected void Search_Click(object sender, EventArgs e)
        {
            ListPrice = new List<ListPrice>();
            myListPrice = new ListPrice();
            myListPrice = SearchModel();

            var RegionFromId = new SqlParameter();
            var RegionToId = new SqlParameter();
            var SubAccId = new SqlParameter();
            //
            if (myListPrice.RegionFromId != null)
            {
                RegionFromId = new SqlParameter("@RegionFromId", myListPrice.RegionFromId);
            }
            else { RegionFromId = new SqlParameter("@RegionFromId", DBNull.Value); }
            //
            if (myListPrice.RegionToId != null)
            {
                RegionToId = new SqlParameter("@RegionToId", myListPrice.RegionToId);
            }
            else { RegionToId = new SqlParameter("@RegionToId", DBNull.Value); }
            //
            if (myListPrice.SubAccId != null)
            {
                SubAccId = new SqlParameter("@SubAccId", myListPrice.SubAccId);
            }
            else { SubAccId = new SqlParameter("@SubAccId", DBNull.Value); }

            ListPrice = db.Database
                .SqlQuery<ListPrice>("SP_ListPrice @RegionFromId , @RegionToId , @SubAccId", RegionFromId, RegionToId, SubAccId).ToList();

            foreach (var item in ListPrice)
            {
                item.FromRegion = db.FromRegion.Where(c => c.Id == item.RegionFromId).FirstOrDefault();
                item.FromRegion1 = db.FromRegion.Where(c => c.Id == item.RegionToId).FirstOrDefault();
                item.SubAccount = db.SubAccount.Where(c => c.ID == item.SubAccId).FirstOrDefault();
            }
            SearchListPrice = ListPrice;
            BindGridList();
            ClearSearchModel();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            SearchListPrice = new List<ListPrice>();
            ClearSearchModel();
            myListPrice = new ListPrice();
            ListPrice = new List<ListPrice>();
            ListPrice = db.ListPrice.ToList();
            BindGridList();
        }
        protected ListPrice SearchModel()
        {
            ListPrice listPrice = new ListPrice();

            if (PriceTxt.Text != "") { listPrice.Price = Convert.ToInt32(PriceTxt.Text); }

            if (RegionFromSearchListtxt.SelectedValue != "0") { listPrice.RegionFromId = Convert.ToInt32(RegionFromSearchListtxt.SelectedValue); }

            if (RegionToSearchListtxt.SelectedValue != "0") { listPrice.RegionToId = Convert.ToInt32(RegionToSearchListtxt.SelectedValue); }

            if (SupplierSearchListtxt.SelectedValue != "0") { listPrice.SubAccId = Convert.ToInt32(SupplierSearchListtxt.SelectedValue); }

            return listPrice;
        }
        protected void ClearSearchModel()
        {
            PriceTxt.Text = "";
            RegionFromSearchListtxt.SelectedIndex = 0;
            RegionToSearchListtxt.SelectedIndex = 0;
            SupplierSearchListtxt.SelectedIndex = 0;
        }
    }
}