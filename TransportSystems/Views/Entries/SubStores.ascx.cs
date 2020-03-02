using TransportSystems.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.Entries
{
    public partial class SubStores : System.Web.UI.UserControl
    {
        private TransportModel db = new TransportModel();

        public static List<SubStore> SubStoreList;
        public static string selectedvalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // make custom ValidationGroup>>><<<<
                string vgId = Guid.NewGuid().ToString();
                Store_Name.ValidationGroup = vgId;
                RequiredFieldValidator1.ValidationGroup = vgId;
                ValidationSummary1.ValidationGroup = vgId;
                Save_Store.ValidationGroup = vgId;

                // get all subStore For frist time
                SubStoreList = new List<SubStore>();
                SubStoreList = db.SubStore.ToList();
            }
            selectedvalue = SubStoreListhtml.SelectedValue;
            BindSubStoreDrop();
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = SubStoreList;
            GridView1.DataBind();
        }
        protected void BindSubStoreDrop()
        {
            SubStoreListhtml.DataSource = SubStoreList;
            SubStoreListhtml.DataBind();
            SubStoreListhtml.DataTextField = "Name";
            SubStoreListhtml.DataValueField = "ID";
            SubStoreListhtml.DataBind();
        }
        protected SubStore SubStoreModel()
        {
            SubStore SubStore = new SubStore();
            if (StoreIdHid.Value != "0")
            {
                SubStore.ID = Convert.ToInt32(StoreIdHid.Value);
            }
            if (Store_Name.Text != "")
            {
                SubStore.name = Store_Name.Text;
            }
            SubStore.LoginID = ExtendedMethod.LoginedUser.Id;
            return SubStore;
        }
        protected void ClearSearchModel()
        {
            StoreIdHid.Value = "";
            Store_Name.Text = "";
        }
        public string text
        {
            get
            {
                return selectedvalue;
            }
            set
            {
                SubStoreListhtml.SelectedValue = value;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void Save_Store_Click(object sender, EventArgs e)
        {
            SubStore subStore = SubStoreModel();
            if (subStore.ID == 0)
            {
                try
                {
                    db.SubStore.Add(subStore);
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
                    db.SubStore.AddOrUpdate(subStore);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                }
            }
            ClearSearchModel();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void edit_Btn_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<SubStore> gvr = (List<SubStore>)GridView1.DataSource;
            SubStore SubStoreEdit = gvr[index];
            StoreIdHid.Value = SubStoreEdit.ID.ToString();
            Store_Name.Text = SubStoreEdit.name;
        }

        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<SubStore> gvr = (List<SubStore>)GridView1.DataSource;
            int deleted_index = gvr[index].ID;
            var deletedSub_Store = db.SubStore.Where(SS => SS.ID == deleted_index).FirstOrDefault();
            db.SubStore.Remove(deletedSub_Store);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}