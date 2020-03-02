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
    public partial class AddColors : System.Web.UI.UserControl
    {
        private TransportModel db = new TransportModel();

        public static List<Colors> ColorsList;
        public static string selectedvalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // make custom ValidationGroup>>><<<<
                string vgId = Guid.NewGuid().ToString();
                Colors_Name.ValidationGroup = vgId;
                RequiredFieldValidator1.ValidationGroup = vgId;
                ValidationSummary1.ValidationGroup = vgId;
                Save.ValidationGroup = vgId;

                // get all subStore For frist time
                ColorsList = new List<Colors>();
                ColorsList = db.Colors.ToList();
            }
            selectedvalue = ColorsListhtml.SelectedValue;
            BindSubStoreDrop();
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = ColorsList;
            GridView1.DataBind();
        }
        protected void BindSubStoreDrop()
        {
            ColorsListhtml.DataSource = ColorsList;
            ColorsListhtml.DataBind();
            ColorsListhtml.DataTextField = "Name";
            ColorsListhtml.DataValueField = "ID";
            ColorsListhtml.DataBind();
        }
        protected Colors ColorsModel()
        {
            Colors Colors = new Colors();
            if (ColorsIdHid.Value != "0")
            {
                Colors.id = Convert.ToInt32(ColorsIdHid.Value);
            }
            if (Colors_Name.Text != "")
            {
                Colors.name = Colors_Name.Text;
            }
            Colors.LoginID = ExtendedMethod.LoginedUser.Id;
            return Colors;
        }
        protected void ClearSearchModel()
        {
            ColorsIdHid.Value = "";
            Colors_Name.Text = "";
        }
        public string text
        {
            get
            {
                return selectedvalue;
            }
            set
            {
                ColorsListhtml.SelectedValue = value;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Colors Colors = ColorsModel();
            if (Colors.id == 0)
            {
                try
                {
                    db.Colors.Add(Colors);
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
                    db.Colors.AddOrUpdate(Colors);
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
            List<Colors> gvr = (List<Colors>)GridView1.DataSource;
            Colors ColorsEdit = gvr[index];
            ColorsIdHid.Value = ColorsEdit.id.ToString();
            Colors_Name.Text = ColorsEdit.name;
        }

        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<Colors> gvr = (List<Colors>)GridView1.DataSource;
            int deleted_index = gvr[index].id;
            var deletedColors = db.Colors.Where(SS => SS.id == deleted_index).FirstOrDefault();
            db.Colors.Remove(deletedColors);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}