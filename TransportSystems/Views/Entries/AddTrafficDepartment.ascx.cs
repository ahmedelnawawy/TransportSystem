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
    public partial class AddTrafficDepartment : System.Web.UI.UserControl
    {
        private TransportModel db = new TransportModel();

        public static List<TrafficDepartment> TrafficDepartmentList;
        public static string selectedvalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // make custom ValidationGroup>>><<<<
                string vgId = Guid.NewGuid().ToString();
                TrafficDepartment_Name.ValidationGroup = vgId;
                RequiredFieldValidator1.ValidationGroup = vgId;
                ValidationSummary1.ValidationGroup = vgId;
                Save.ValidationGroup = vgId;

                // get all subStore For frist time
                TrafficDepartmentList = new List<TrafficDepartment>();
                TrafficDepartmentList = db.TrafficDepartment.ToList();
            }
            selectedvalue = TrafficDepartmentListhtml.SelectedValue;
            BindSubStoreDrop();
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = TrafficDepartmentList;
            GridView1.DataBind();
        }
        protected void BindSubStoreDrop()
        {
            TrafficDepartmentListhtml.DataSource = TrafficDepartmentList;
            TrafficDepartmentListhtml.DataBind();
            TrafficDepartmentListhtml.DataTextField = "Name";
            TrafficDepartmentListhtml.DataValueField = "ID";
            TrafficDepartmentListhtml.DataBind();
        }
        protected TrafficDepartment TrafficDepartmentModel()
        {
            TrafficDepartment TrafficDepartment = new TrafficDepartment();
            if (TrafficDepartmentIdHid.Value != "0")
            {
                TrafficDepartment.Id = Convert.ToInt32(TrafficDepartmentIdHid.Value);
            }
            if (TrafficDepartment_Name.Text != "")
            {
                TrafficDepartment.Name = TrafficDepartment_Name.Text;
            }
            TrafficDepartment.LoginID = ExtendedMethod.LoginedUser.Id;
            return TrafficDepartment;
        }
        protected void ClearSearchModel()
        {
            TrafficDepartmentIdHid.Value = "";
            TrafficDepartment_Name.Text = "";
        }
        public string text
        {
            get
            {
                return selectedvalue;
            }
            set
            {
                TrafficDepartmentListhtml.SelectedValue = value;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            TrafficDepartment TrafficDepartment = TrafficDepartmentModel();
            if (TrafficDepartment.Id == 0)
            {
                try
                {
                    db.TrafficDepartment.Add(TrafficDepartment);
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
                    db.TrafficDepartment.AddOrUpdate(TrafficDepartment);
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
            List<TrafficDepartment> gvr = (List<TrafficDepartment>)GridView1.DataSource;
            TrafficDepartment TrafficDepartmentEdit = gvr[index];
            TrafficDepartmentIdHid.Value = TrafficDepartmentEdit.Id.ToString();
            TrafficDepartment_Name.Text = TrafficDepartmentEdit.Name;
        }

        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<TrafficDepartment> gvr = (List<TrafficDepartment>)GridView1.DataSource;
            int deleted_index = gvr[index].Id;
            var deletedTrafficDepartment = db.TrafficDepartment.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.TrafficDepartment.Remove(deletedTrafficDepartment);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}