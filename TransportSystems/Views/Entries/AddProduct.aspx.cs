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
    public partial class AddProduct : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<Product> SearchProductList = new List<Product>();
        List<Product> ProductList = new List<Product>();
        Product myProduct = new Product();

        public static List<SubStore> SubStoreList = new List<SubStore>();
        SubStore mysubStore = new SubStore();

        public static List<Sector> SectorList = new List<Sector>();
        Sector mySector = new Sector();

        public static string SelectedSubStoreId = "", SelectedSectorId = "";

        public static bool EditFlag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Set_initial();
                EditFlag = false;
            }
            SelectedSubStoreId = AddStoreListhtml.SelectedValue;
            SelectedSectorId = AddSectorListhtml.SelectedValue;
            ProductList = db.Product.ToList();
            BindGridList();
        }
        protected void Set_initial()
        {
            //make custom ValidationGroup >>><<<<
            string vgId = Guid.NewGuid().ToString();

            ProductIDTxt.ValidationGroup = vgId;
            RequiredFieldValidator6.ValidationGroup = vgId;

            NameTxt.ValidationGroup = vgId;
            RequiredFieldValidator1.ValidationGroup = vgId;

            PPriceTxt.ValidationGroup = vgId;
            RequiredFieldValidator2.ValidationGroup = vgId;

            SPriceTxt.ValidationGroup = vgId;
            RequiredFieldValidator3.ValidationGroup = vgId;

            MinBalanceTxt.ValidationGroup = vgId;
            RequiredFieldValidator4.ValidationGroup = vgId;

            DescriptionTxt.ValidationGroup = vgId;


            CurrentBalanceTxt.ValidationGroup = vgId;
            RequiredFieldValidator5.ValidationGroup = vgId;

            ValidationSummary1.ValidationGroup = vgId;
            Save.ValidationGroup = vgId;

            // get all substore and bind to its list (For Adding will be forced to choose)
            SubStoreList = db.SubStore.ToList();
            AddStoreListhtml.DataSource = SubStoreList;
            AddStoreListhtml.DataTextField = "name";
            AddStoreListhtml.DataValueField = "ID";
            AddStoreListhtml.DataBind();
            //  (For Adding will be forced to choose)
            SectorList = db.Sector.ToList();
            AddSectorListhtml.DataSource = SectorList;
            AddSectorListhtml.DataTextField = "Name";
            AddSectorListhtml.DataValueField = "ID";
            AddSectorListhtml.DataBind();
            // (For search will be optional to choose)
            List<Sector> TempSectorlist = new List<Sector>();
            TempSectorlist.Add(new Sector { name = "-- select Sector --", id = 0 });
            TempSectorlist.AddRange(SectorList);
            SectorSearchListtxt.DataSource = TempSectorlist;
            SectorSearchListtxt.DataTextField = "Name";
            SectorSearchListtxt.DataValueField = "ID";
            SectorSearchListtxt.DataBind();
            //
            var NewId = (from Product in db.Product.AsEnumerable()
                         where !String.IsNullOrEmpty(Product.ID)
                         select Convert.ToInt32(Product.ID)).Max();
            ProductIDTxt.Text = (NewId + 1).ToString();
            PPriceTxt.Text = "0";
            SPriceTxt.Text = "0";
            MinBalanceTxt.Text = "0";
            CurrentBalanceTxt.Text = "0";
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = ProductList;
            GridView1.DataBind();
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
        protected void Save_Click(object sender, EventArgs e)
        {
            myProduct = new Product();
            Product product = ProductModel();
            if(EditFlag == false)
            {
                myProduct = db.Product.Where(pro => pro.ID == product.ID).FirstOrDefault();
                if(myProduct == null)
                {
                    try
                    {
                       
                        db.Product.Add(product);
                        db.SaveChanges();
                        ClearProductModel();
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
            }else
            {
                try
                {
                    db.Product.AddOrUpdate(product);
                    db.SaveChanges();
                    ClearProductModel();
                }
                catch (Exception except)
                {
                    ShowMessage("Something Wrong Will Editing");
                }
            }
            EditFlag = false;
        }
        protected Product ProductModel()
        {
            Product product = new Product();
            if (ProductIDTxt.Text != "") { product.ID = ProductIDTxt.Text; }

            if (NameTxt.Text != "") { product.name = NameTxt.Text; }

            if (PPriceTxt.Text != "") { product.PPrice =Convert.ToDecimal(PPriceTxt.Text); }

            if (SPriceTxt.Text != "") { product.SPrice = Convert.ToDecimal(SPriceTxt.Text); }

            product.BalanceQ = 0; // untill required

            if (MinBalanceTxt.Text != "") { product.MinBalance = Convert.ToDouble(MinBalanceTxt.Text); }

            if (DescriptionTxt.Text != "") { product.Description = DescriptionTxt.Text; }

            if (CurrentBalanceTxt.Text != "") { product.CurrentBalance = Convert.ToDouble(CurrentBalanceTxt.Text); }

            if (SelectedSubStoreId != "") { product.StorID = Convert.ToInt32(SelectedSubStoreId); }

            if (SelectedSectorId != "") { product.SectorID = Convert.ToInt32(SelectedSectorId); }
            product.LoginID= ExtendedMethod.LoginedUser.Id; 
            return product;
        }
        protected void ClearProductModel()
        {
            var NewId = Convert.ToInt32(db.Product.Select(item => item.ID).Max()) + 1;
            ProductIDTxt.Text = NewId.ToString();
            NameTxt.Text = "";
            PPriceTxt.Text = "0";
            SPriceTxt.Text = "0";
            MinBalanceTxt.Text = "0";
            DescriptionTxt.Text = "";
            CurrentBalanceTxt.Text = "0";
            AddStoreListhtml.SelectedIndex =0;
            AddSectorListhtml.SelectedIndex = 0;
            Page.Response.Redirect(Page.Request.Url.ToString(), false);

        }
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            EditFlag = true;
            Product temProduct = new Product();
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchProductList.Count > 0)
            { temProduct = SearchProductList[index]; }
            else { temProduct = ProductList[index]; }

            ProductIDTxt.Text = temProduct.ID.ToString();
            NameTxt.Text = temProduct.name;
            PPriceTxt.Text = temProduct.PPrice.ToString();
            SPriceTxt.Text = temProduct.SPrice.ToString();
            MinBalanceTxt.Text = temProduct.MinBalance.ToString();
            DescriptionTxt.Text = temProduct.Description;
            CurrentBalanceTxt.Text = temProduct.CurrentBalance.ToString(); 
            AddStoreListhtml.SelectedValue = temProduct.StorID.ToString();
            AddSectorListhtml.SelectedValue = temProduct.SectorID.ToString();

            SearchProductList = new List<Product>();
            UpdatePanel3.Update();
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            string deleted_index = "";
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchProductList.Count > 0)
            { deleted_index = SearchProductList[index].ID; }
            else { deleted_index = ProductList[index].ID; }

            try
            {
                var deletedProduct = db.Product.Where(SS => SS.ID == deleted_index).FirstOrDefault();
                db.Product.Remove(deletedProduct);
                db.SaveChanges();
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch(Exception excpt)
            {
                ShowMessage("Something Wrong Will Deleting");
            }
        }
        //search Part
        protected void Search_Click(object sender, EventArgs e)
        {
            SearchProductList = new List<Product>();
            ProductList = new List<Product>();
            myProduct = new Product();
            myProduct = SearchModel();

            var name = new SqlParameter();
            var SectorID = new SqlParameter();
            //
            if (myProduct.name != "")
            {
                name = new SqlParameter("@name", myProduct.name.Trim());
            }
            else { name = new SqlParameter("@name", DBNull.Value); }
            //
            if (myProduct.SectorID != 0)
            {
                SectorID = new SqlParameter("@SectorID", myProduct.SectorID);
            }
            else { SectorID = new SqlParameter("@SectorID", DBNull.Value); }

            ProductList = db.Database
                .SqlQuery<Product>("SP_Product @name , @SectorID", name, SectorID).ToList();

            SearchProductList = ProductList;
            BindGridList();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            SearchProductList = new List<Product>();
            ClearSearchModel();
            ProductList = new List<Product>();
            myProduct = new Product();
            ProductList = db.Product.ToList();
            BindGridList();
        }
        protected Product SearchModel()
        {
            Product product = new Product();
            product.name = ProductNametxt.Text.Trim();
            product.SectorID = Convert.ToInt32(SectorSearchListtxt.SelectedValue);
            return product;
        }
        protected void ClearSearchModel()
        {
            ProductNametxt.Text = "";
            SectorSearchListtxt.SelectedIndex = 0;
        }
    }
}