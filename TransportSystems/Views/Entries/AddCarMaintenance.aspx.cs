using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.Entries
{
    public partial class AddCarMaintenance : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<CarMaintenance> SearchCarMaintenanceList = new List<CarMaintenance>();
        List<CarMaintenance> CarMaintenanceList = new List<CarMaintenance>();
        CarMaintenance myCarMaintenance = new CarMaintenance();

        public static List<Cars> CarsList = new List<Cars>();
        Cars myCars = new Cars();

        public static List<Services> ServicesList = new List<Services>();
        Services myServices = new Services();

        public static string SelectedCarId = "", SelectedServiceId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Set_initial();
            }
            SelectedCarId = AddCarsListhtml.SelectedValue;
            SelectedServiceId = AddServicesListhtml.SelectedValue;

            CarMaintenanceList = db.CarMaintenance.ToList();
            BindGridList();
        }
        protected void Set_initial()
        {
            //make custom ValidationGroup >>><<<<
            string vgId = Guid.NewGuid().ToString();

            ChangeRateTxt.ValidationGroup = vgId;
            AlertRateTxt.ValidationGroup = vgId;
            HaveChangeRate.ValidationGroup = vgId;

            ValidationSummary1.ValidationGroup = vgId;
            Save.ValidationGroup = vgId;

            //Colors  (For Adding will be forced to choose)
            CarsList = db.Cars.ToList();
            AddCarsListhtml.DataSource = CarsList;
            AddCarsListhtml.DataTextField = "CarNo";
            AddCarsListhtml.DataValueField = "Id";
            AddCarsListhtml.DataBind();
            // (For search will be optional to choose)
            List<Cars> TempCarsListlist = new List<Cars>();
            TempCarsListlist.Add(new Cars { CarNo = "-- select CarNo --", id = 0 });
            TempCarsListlist.AddRange(CarsList);
            CarsSearchListtxt.DataSource = TempCarsListlist;
            CarsSearchListtxt.DataTextField = "CarNo";
            CarsSearchListtxt.DataValueField = "Id";
            CarsSearchListtxt.DataBind();

            //City  (For Adding will be forced to choose)
            ServicesList = db.Services.ToList();
            BindDropDownList(ServicesList, AddServicesListhtml);
            // (For search will be optional to choose)
            List<Services> TempServiceslist = new List<Services>();
            TempServiceslist.Add(new Services { Name = "-- select Services --", Id = 0 });
            TempServiceslist.AddRange(ServicesList);
            BindDropDownList(TempServiceslist, ServicesSearchListtxt);

            ChangeRateTxt.Text = "0";
            AlertRateTxt.Text = "0";
            HaveChangeRate.Checked = true;
            ChangeRateTxt.Enabled = true;
            AlertRateTxt.Enabled = true;
        }
        protected void BindDropDownList(object DataSourceList, object DropdownList)
        {
            DropDownList Droplist = DropdownList as DropDownList;
            Droplist.DataSource = DataSourceList;
            Droplist.DataTextField = "Name";
            Droplist.DataValueField = "Id";
            Droplist.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = CarMaintenanceList;
            GridView1.DataBind();
        }
        //operation Part
        protected void Save_Click(object sender, EventArgs e)
        {
            CarMaintenance carMaintenance = CarsModel();
            if (carMaintenance.Id == 0)
            {
                try
                {
                    db.CarMaintenance.Add(carMaintenance);
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
                    db.CarMaintenance.AddOrUpdate(carMaintenance);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                }
            }
            ClearModel();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        protected CarMaintenance CarsModel()
        {
            CarMaintenance carMaintenance = new CarMaintenance();

            if (IdHid.Value != "0") { carMaintenance.Id = Convert.ToInt32(IdHid.Value); }

            if (ChangeRateTxt.Text != "") { carMaintenance.ChangeRate = ChangeRateTxt.Text; } else { carMaintenance.ChangeRate = "0"; }

            if (AlertRateTxt.Text != "") { carMaintenance.AlertRate = AlertRateTxt.Text; } else { carMaintenance.AlertRate = "0"; }

            carMaintenance.HaveChangeRate = HaveChangeRate.Checked;

            if (SelectedCarId != "") { carMaintenance.CarId = Convert.ToInt32(SelectedCarId); }

            if (SelectedServiceId != "") { carMaintenance.ServiceId = Convert.ToInt32(SelectedServiceId); }
            carMaintenance.LoginID = ExtendedMethod.LoginedUser.Id;

            return carMaintenance;
        }
        protected void ClearModel()
        {
            IdHid.Value = "0";
            ChangeRateTxt.Text = "0";
            AlertRateTxt.Text = "0";
            HaveChangeRate.Checked = true;
            AddCarsListhtml.SelectedIndex = 0;
            AddServicesListhtml.SelectedIndex = 0;
        }
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            CarMaintenance temCarMaintenance = new CarMaintenance();
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchCarMaintenanceList.Count > 0)
            { temCarMaintenance = SearchCarMaintenanceList[index]; }
            else { temCarMaintenance = CarMaintenanceList[index]; }
            try
            {
                IdHid.Value = temCarMaintenance.Id.ToString();

                ChangeRateTxt.Text = temCarMaintenance.ChangeRate;
                AlertRateTxt.Text = temCarMaintenance.AlertRate;
                HaveChangeRate.Checked = temCarMaintenance.HaveChangeRate;
                if(HaveChangeRate.Checked == false)
                {
                    ChangeRateTxt.Enabled = false;
                    AlertRateTxt.Enabled = false;
                }
                AddCarsListhtml.SelectedValue = temCarMaintenance.CarId.ToString();
                AddServicesListhtml.SelectedValue = temCarMaintenance.ServiceId.ToString();
            }
            catch (Exception Exec)
            {
            }
            SearchCarMaintenanceList = new List<CarMaintenance>();
            UpdatePanel3.Update();
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int deleted_index = 0;
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchCarMaintenanceList.Count > 0)
            { deleted_index = Convert.ToInt32(SearchCarMaintenanceList[index].Id); }
            else { deleted_index = Convert.ToInt32(CarMaintenanceList[index].Id); }

            var deletedCarMaintenance = db.CarMaintenance.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.CarMaintenance.Remove(deletedCarMaintenance);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);

        }

        //Search Part
        protected void Search_Click(object sender, EventArgs e)
        {
            CarMaintenanceList = new List<CarMaintenance>();
            myCarMaintenance = new CarMaintenance();
            myCarMaintenance = SearchModel();

            var CarId = new SqlParameter();
            var ServiceId = new SqlParameter();
           
            //
            if (myCarMaintenance.CarId != 0)
            {
                CarId = new SqlParameter("@CarId", myCarMaintenance.CarId);
            }
            else { CarId = new SqlParameter("@CarId", DBNull.Value); }
            //
            if (myCarMaintenance.ServiceId != 0)
            {
                ServiceId = new SqlParameter("@ServiceId", myCarMaintenance.ServiceId);
            }
            else { ServiceId = new SqlParameter("@ServiceId", DBNull.Value); }

            CarMaintenanceList = db.Database
                .SqlQuery<CarMaintenance>("SP_CarMaintenance @CarId , @ServiceId", CarId, ServiceId).ToList();

            foreach (var item in CarMaintenanceList)
            {
                item.Cars = db.Cars.Where(c => c.id == item.CarId).FirstOrDefault();
                item.Services = db.Services.Where(c => c.Id == item.ServiceId).FirstOrDefault();
            }
            SearchCarMaintenanceList = CarMaintenanceList;
            BindGridList();
            ClearSearchModel();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            SearchCarMaintenanceList = new List<CarMaintenance>();
            ClearSearchModel();
            myCarMaintenance = new CarMaintenance();
            CarMaintenanceList = new List<CarMaintenance>();
            CarMaintenanceList = db.CarMaintenance.ToList();
            BindGridList();
        }
        protected CarMaintenance SearchModel()
        {
            CarMaintenance carMaintenance = new CarMaintenance();
            if (CarsSearchListtxt.SelectedValue != "0") { carMaintenance.CarId = Convert.ToInt32(CarsSearchListtxt.SelectedValue); }
            if (ServicesSearchListtxt.SelectedValue != "0") { carMaintenance.ServiceId = Convert.ToInt32(ServicesSearchListtxt.SelectedValue); }
            return carMaintenance;
        }

        protected void HaveChangeRate_CheckedChanged(object sender, EventArgs e)
        {
            if (HaveChangeRate.Checked == true)
            {
                ChangeRateTxt.Enabled = true;
                AlertRateTxt.Enabled = true;
            }
            else
            {
                ChangeRateTxt.Enabled = false;
                AlertRateTxt.Enabled = false;
            }
        }

        protected void ClearSearchModel()
        {
            CarsSearchListtxt.SelectedIndex = 0;
            ServicesSearchListtxt.SelectedIndex = 0;
        }

    }
}