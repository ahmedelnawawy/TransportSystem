using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.Entries
{
    public partial class AddServices : System.Web.UI.UserControl
    {
        private TransportModel db = new TransportModel();

        public static List<Services> ServicesList;
        public static string selectedvalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // make custom ValidationGroup>>><<<<
                string vgId = Guid.NewGuid().ToString();
                Services_Name.ValidationGroup = vgId;
                RequiredFieldValidator1.ValidationGroup = vgId;
                ValidationSummary1.ValidationGroup = vgId;
                Save.ValidationGroup = vgId;

                // get all subStore For frist time
                ServicesList = new List<Services>();
                ServicesList = db.Services.ToList();
            }
            selectedvalue = ServicesListhtml.SelectedValue;
            BindSubStoreDrop();
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = ServicesList;
            GridView1.DataBind();
        }
        protected void BindSubStoreDrop()
        {
            ServicesListhtml.DataSource = ServicesList;
            ServicesListhtml.DataBind();
            ServicesListhtml.DataTextField = "Name";
            ServicesListhtml.DataValueField = "ID";
            ServicesListhtml.DataBind();
        }
        protected Services ServicesModel()
        {
            Services Services = new Services();
            if (ServicesIdHid.Value != "0")
            {
                Services.Id = Convert.ToInt32(ServicesIdHid.Value);
            }
            if (Services_Name.Text != "")
            {
                Services.Name = Services_Name.Text;
            }
            Services.LoginID = ExtendedMethod.LoginedUser.Id;
            return Services;
        }
        protected void ClearSearchModel()
        {
            ServicesIdHid.Value = "";
            Services_Name.Text = "";
        }
        public string text
        {
            get
            {
                return selectedvalue;
            }
            set
            {
                ServicesListhtml.SelectedValue = value;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Services Services = ServicesModel();
            if (Services.Id == 0)
            {
                try
                {
                    db.Services.Add(Services);
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
                    db.Services.AddOrUpdate(Services);
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
            List<Services> gvr = (List<Services>)GridView1.DataSource;
            Services ServicesEdit = gvr[index];
            ServicesIdHid.Value = ServicesEdit.Id.ToString();
            Services_Name.Text = ServicesEdit.Name;
        }

        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<Services> gvr = (List<Services>)GridView1.DataSource;
            int deleted_index = gvr[index].Id;
            var deletedServices = db.Services.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.Services.Remove(deletedServices);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}