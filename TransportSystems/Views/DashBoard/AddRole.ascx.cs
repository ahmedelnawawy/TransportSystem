using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.DashBoard
{
    public partial class AddRole : System.Web.UI.UserControl
    {
        private TransportModel db = new TransportModel();

        public static List<Role> RoleList;
        public static string selectedvalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // make custom ValidationGroup>>><<<<
                string vgId = Guid.NewGuid().ToString();
                Role_Name.ValidationGroup = vgId;
                RequiredFieldValidator1.ValidationGroup = vgId;
                ValidationSummary1.ValidationGroup = vgId;
                Save.ValidationGroup = vgId;

                // get all subStore For frist time
                RoleList = new List<Role>();
                RoleList = db.Role.ToList();
            }
            selectedvalue = RoleListhtml.SelectedValue;
            BindSubStoreDrop();
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = RoleList;
            GridView1.DataBind();
        }
        protected void BindSubStoreDrop()
        {
            RoleListhtml.DataSource = RoleList;
            RoleListhtml.DataBind();
            RoleListhtml.DataTextField = "Name";
            RoleListhtml.DataValueField = "ID";
            RoleListhtml.DataBind();
        }
        protected Role RoleModel()
        {
            Role Role = new Role();
            if (RoleIdHid.Value != "0")
            {
                Role.Id = Convert.ToInt32(RoleIdHid.Value);
            }
            if (Role_Name.Text != "")
            {
                Role.Name = Role_Name.Text;
            }
            Role.LoginID = ExtendedMethod.LoginedUser.Id;
            return Role;
        }
        protected void ClearSearchModel()
        {
            RoleIdHid.Value = "";
            Role_Name.Text = "";
        }
        public string text
        {
            get
            {
                return selectedvalue;
            }
            set
            {
                RoleListhtml.SelectedValue = value;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Role Role = RoleModel();
            if (Role.Id == 0)
            {
                try
                {
                    db.Role.Add(Role);
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
                    db.Role.AddOrUpdate(Role);
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
            List<Role> gvr = (List<Role>)GridView1.DataSource;
            Role RoleEdit = gvr[index];
            RoleIdHid.Value = RoleEdit.Id.ToString();
            Role_Name.Text = RoleEdit.Name;
        }

        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<Role> gvr = (List<Role>)GridView1.DataSource;
            int deleted_index = gvr[index].Id;
            var deletedRole = db.Role.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.Role.Remove(deletedRole);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}