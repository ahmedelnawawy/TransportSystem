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
    public partial class AddCarType : System.Web.UI.UserControl
    {
        private TransportModel db = new TransportModel();

        public static List<CarType> CarTypeList;
        public static string selectedvalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // make custom ValidationGroup>>><<<<
                string vgId = Guid.NewGuid().ToString();
                CarType_Name.ValidationGroup = vgId;
                RequiredFieldValidator1.ValidationGroup = vgId;
                ValidationSummary1.ValidationGroup = vgId;
                Save.ValidationGroup = vgId;

                // get all subStore For frist time
                CarTypeList = new List<CarType>();
                CarTypeList = db.CarType.ToList();
            }
            selectedvalue = CarTypeListhtml.SelectedValue;
            BindSubStoreDrop();
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = CarTypeList;
            GridView1.DataBind();
        }
        protected void BindSubStoreDrop()
        {
            CarTypeListhtml.DataSource = CarTypeList;
            CarTypeListhtml.DataBind();
            CarTypeListhtml.DataTextField = "Name";
            CarTypeListhtml.DataValueField = "ID";
            CarTypeListhtml.DataBind();
        }
        protected CarType CarTypeModel()
        {
            CarType CarType = new CarType();
            if (CarTypeIdHid.Value != "0")
            {
                CarType.Id = Convert.ToInt32(CarTypeIdHid.Value);
            }
            if (CarType_Name.Text != "")
            {
                CarType.Name = CarType_Name.Text;
            }
            CarType.LoginID = ExtendedMethod.LoginedUser.Id;
            return CarType;
        }
        protected void ClearSearchModel()
        {
            CarTypeIdHid.Value = "";
            CarType_Name.Text = "";
        }
        public string text
        {
            get
            {
                return selectedvalue;
            }
            set
            {
                CarTypeListhtml.SelectedValue = value;
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            CarType CarType = CarTypeModel();
            if (CarType.Id == 0)
            {
                try
                {
                    db.CarType.Add(CarType);
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
                    db.CarType.AddOrUpdate(CarType);
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
            List<CarType> gvr = (List<CarType>)GridView1.DataSource;
            CarType CarTypeEdit = gvr[index];
            CarTypeIdHid.Value = CarTypeEdit.Id.ToString();
            CarType_Name.Text = CarTypeEdit.Name;
        }

        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<CarType> gvr = (List<CarType>)GridView1.DataSource;
            int deleted_index = gvr[index].Id;
            var deletedCarType = db.CarType.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.CarType.Remove(deletedCarType);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}