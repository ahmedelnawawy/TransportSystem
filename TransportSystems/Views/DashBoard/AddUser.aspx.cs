using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.DashBoard
{
    public partial class AddUser : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<Role> RoleList = new List<Role>();
        Role myRole = new Role();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string vgId = Guid.NewGuid().ToString();

                Username.ValidationGroup = vgId;
                RequiredFieldValidator1.ValidationGroup = vgId;

                Email.ValidationGroup = vgId;
                RequiredFieldValidator2.ValidationGroup = vgId;

                Phone.ValidationGroup = vgId;
                RequiredFieldValidator3.ValidationGroup = vgId;

                Password.ValidationGroup = vgId;
                RequiredFieldValidator4.ValidationGroup = vgId;

                ValidationSummary1.ValidationGroup = vgId;
                SignIn.ValidationGroup = vgId;

                RoleList = db.Role.ToList();
                BindDropDownList(RoleList, AddRoleListTxt);
            }
        }
        protected void BindDropDownList(object DataSourceList, object DropdownList)
        {
            DropDownList Droplist = DropdownList as DropDownList;
            Droplist.DataSource = DataSourceList;
            Droplist.DataTextField = "Name";
            Droplist.DataValueField = "Id";
            Droplist.DataBind();
        }
        protected void SignIn_Click(object sender, EventArgs e)
        {
            string result = validationUser();
            if (result == "")
            {
                try
                {
                    var myser = UserModel();
                   
                    db.AspUser.Add(myser);
                    db.SaveChanges();
                    Response.Redirect("../Default.aspx");
                }
                catch (Exception exept)
                {
                }
            }
            else
            {
                LoginError.Text = result;
            }
        }
        protected string validationUser()
        {
            if (db.AspUser.Where(us => us.Username == Username.Text).ToList().Count() > 0)
            {
                return " There Are The Same Username";
            }
            else
            {
                return "";
            }
        }
        protected AspUser UserModel()
        {
            string EncryptPass = FormsAuthentication.HashPasswordForStoringInConfigFile(Password.Text, "SHA1");
            AspUser myuser = new AspUser();
            myuser.Username = Username.Text;
            myuser.Email = Email.Text;
            myuser.Phone = Phone.Text;
            myuser.Password = Password.Text;
            myuser.RoleID = Convert.ToInt32(AddRoleListTxt.SelectedValue);
            myuser.LoginID = ExtendedMethod.LoginedUser.Id;

            return myuser;
        }

        protected void ClosePageReg_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("../Default.aspx");
        }
    }
}