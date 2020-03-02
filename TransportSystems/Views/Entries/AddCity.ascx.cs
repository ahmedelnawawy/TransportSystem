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
    public partial class AddCity : System.Web.UI.UserControl
    {
        private TransportModel db = new TransportModel();

        public static List<City> CityList;
        public static string selectedvalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // make custom ValidationGroup>>><<<<
                string vgId = Guid.NewGuid().ToString();
                City_Name.ValidationGroup = vgId;
                RequiredFieldValidator1.ValidationGroup = vgId;
                ValidationSummary1.ValidationGroup = vgId;
                Save.ValidationGroup = vgId;

                // get all subStore For frist time
                CityList = new List<City>();
                CityList = db.City.ToList();
            }
            selectedvalue = CityListhtml.SelectedValue;
            BindSubStoreDrop();
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = CityList;
            GridView1.DataBind();
        }
        protected void BindSubStoreDrop()
        {
            CityListhtml.DataSource = CityList;
            CityListhtml.DataBind();
            CityListhtml.DataTextField = "Name";
            CityListhtml.DataValueField = "ID";
            CityListhtml.DataBind();
        }
        protected City CityModel()
        {
            City City = new City();
            if (CityIdHid.Value != "0")
            {
                City.Id = Convert.ToInt32(CityIdHid.Value);
            }
            if (City_Name.Text != "")
            {
                City.Name = City_Name.Text;
            }
            City.LoginID = ExtendedMethod.LoginedUser.Id; 
            return City;
        }
        protected void ClearSearchModel()
        {
            CityIdHid.Value = "";
            City_Name.Text = "";
        }
        public string text
        {
            get
            {
                return selectedvalue;
            }
            set
            {
                CityListhtml.SelectedValue = value;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            City City = CityModel();
            if (City.Id == 0)
            {
                try
                {
                    db.City.Add(City);
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
                    db.City.AddOrUpdate(City);
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
            List<City> gvr = (List<City>)GridView1.DataSource;
            City CityEdit = gvr[index];
            CityIdHid.Value = CityEdit.Id.ToString();
            City_Name.Text = CityEdit.Name;
        }

        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<City> gvr = (List<City>)GridView1.DataSource;
            int deleted_index = gvr[index].Id;
            var deletedCity = db.City.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.City.Remove(deletedCity);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}