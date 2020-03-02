using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems
{
    public partial class LoginPage : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(User.Identity.IsAuthenticated)
            {
                ExtendedMethod.LoginedUser = ExtendedMethod.GetUserData(User.Identity.Name);
                Response.Redirect("~/Views/Default.aspx");
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            AspUser myuser = new AspUser();
            // check if he is valid
            if (AuthenticatedUser(Username.Text, Password.Text) == 1)
            {
                // get from db to get its information
                myuser = db.AspUser.Where(u => u.Username == Username.Text && u.Password == Password.Text).FirstOrDefault();
                ExtendedMethod.LoginedUser=myuser;
                var rolename = myuser.Role.Name;

                // make a ticket to it
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, myuser.Username, DateTime.Now, DateTime.Now.AddMinutes(2880),
                    RememberMe.Checked, rolename, FormsAuthentication.FormsCookiePath);

                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookie);
                Response.Redirect(FormsAuthentication.GetRedirectUrl(myuser.Username, RememberMe.Checked));
            }
            else
            {
                LoginError.Text = "Error Login : Username Or Password Wrong .";
            }
        }
        private int AuthenticatedUser(string LUsername, string LPassword)
        {
            int result = 0;
            var Username = new SqlParameter();
            var Password = new SqlParameter();
            //
            if (LUsername != "")
            {
                Username = new SqlParameter("@Username", LUsername.Trim());
            }
            else { Username = new SqlParameter("@Username", DBNull.Value); }
            //
            if (LPassword != "")
            {
                Password = new SqlParameter("@Password", LPassword.Trim());
            }
            else { Password = new SqlParameter("@Password", DBNull.Value); }

            return result = db.Database.SqlQuery<int>("SP_Login @Username , @Password ", Username, Password).First();
        }
    }
}