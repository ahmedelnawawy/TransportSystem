using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransportSystems.LayoutUsersControls
{
    public partial class Header : System.Web.UI.UserControl
    {
        public string lang = Thread.CurrentThread.CurrentCulture.Name;
        public string username;
        public string firstChar;
        public bool IsAuthenticated;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.Name != "")
            {
                username = HttpContext.Current.User.Identity.Name;
                firstChar = HttpContext.Current.User.Identity.Name.Substring(0, 1);
                IsAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated;

            }
            if (!IsPostBack) {

                Page.DataBind();
            }
            SiteMaster.glang = lang;
            //HttpContext.Current.Session.Clear();
        }
        protected void LogOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/LoginPage.aspx", false);
        }
    }
}