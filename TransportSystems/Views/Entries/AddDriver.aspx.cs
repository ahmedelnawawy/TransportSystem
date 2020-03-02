using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.Entries
{
    public partial class AddDriver : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<Driver> SearchDriverList = new List<Driver>();
        List<Driver> DriverList = new List<Driver>();
        Driver myDriver = new Driver();

        public static List<TrafficDepartment> TrafficDepartmentList = new List<TrafficDepartment>();
        TrafficDepartment myTrafficDepartment = new TrafficDepartment();

        public static string SelectedTrafDepId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Set_initial();
            }
            SelectedTrafDepId = AddTrafDepListhtml.SelectedValue;
            DriverList = db.Driver.ToList();
            BindGridList();
        }
        protected void Set_initial()
        {
            //make custom ValidationGroup >>><<<<
            string vgId = Guid.NewGuid().ToString();

            NameTxt.ValidationGroup = vgId;
            RequiredFieldValidator1.ValidationGroup = vgId;

            AddressTxt.ValidationGroup = vgId;
            RequiredFieldValidator2.ValidationGroup = vgId;

            PhoneTxt.ValidationGroup = vgId;
            RequiredFieldValidator3.ValidationGroup = vgId;
            RegularExpressionValidator1.ValidationGroup = vgId;

            licenseTxt.ValidationGroup = vgId;
            RequiredFieldValidator4.ValidationGroup = vgId;

            LicenceDateTxt.ValidationGroup = vgId;
            RequiredFieldValidator5.ValidationGroup = vgId;

            LicencePeriodTxt.ValidationGroup = vgId;
            RequiredFieldValidator7.ValidationGroup = vgId;

            AlertPeriodTxt.ValidationGroup = vgId;
            RequiredFieldValidator6.ValidationGroup = vgId;

            ValidationSummary1.ValidationGroup = vgId;
            Save.ValidationGroup = vgId;
            //  (For Adding will be forced to choose)
            TrafficDepartmentList = db.TrafficDepartment.ToList();
            AddTrafDepListhtml.DataSource = TrafficDepartmentList;
            AddTrafDepListhtml.DataTextField = "Name";
            AddTrafDepListhtml.DataValueField = "Id";
            AddTrafDepListhtml.DataBind();
            // (For search will be optional to choose)
            //List<TrafficDepartment> TempTrafDeplist = new List<TrafficDepartment>();
            //TempTrafDeplist.Add(new TrafficDepartment { Name = "-- select TrafficDepartment --", Id = 0 });
            //TempTrafDeplist.AddRange(TrafficDepartmentList);
            //TrafDepSearchListtxt.DataSource = TempTrafDeplist;
            //TrafDepSearchListtxt.DataTextField = "Name";
            //TrafDepSearchListtxt.DataValueField = "Id";
            //TrafDepSearchListtxt.DataBind();
            // Initial AlertPeriod
            AlertPeriodTxt.Text = "90";
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = DriverList;
            GridView1.DataBind();
        }
        
        // Searching Part
        protected void Search_Click(object sender, EventArgs e)
        {
            DriverList = new List<Driver>();
            myDriver = new Driver();
            myDriver = SearchModel();

            var Name = new SqlParameter();
            var license = new SqlParameter();
            var phone = new SqlParameter();
            //
            if (myDriver.Name != null)
            {
                Name = new SqlParameter("@name", myDriver.Name.Trim());
            }
            else {  Name = new SqlParameter("@name", DBNull.Value); }
            //
            if (myDriver.license != null)
            {
               license = new SqlParameter("@license", myDriver.license.Trim());
            }
            else {license = new SqlParameter("@license", DBNull.Value); }
            //
            if (myDriver.phone != null)
            {
                 phone = new SqlParameter("@phone", myDriver.phone.Trim());
            }
            else {  phone = new SqlParameter("@phone", DBNull.Value); }

            DriverList = db.Database
                .SqlQuery<Driver>("SP_Driver @name , @license , @phone", Name, license, phone).ToList();

            foreach (var item in DriverList)
            {
                item.TrafficDepartment = db.TrafficDepartment.Where(c => c.Id == item.TrafficDepID).FirstOrDefault();
            }
            SearchDriverList = DriverList;
            BindGridList();
            ClearSearchModel();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            SearchDriverList = new List<Driver>();
            ClearSearchModel();
            myDriver = new Driver();
            DriverList = new List<Driver>();
            DriverList = db.Driver.ToList();
            BindGridList();
        }
        protected Driver SearchModel()
        {
            Driver driver = new Driver();

            if (DrivierNametxt.Text != "")
            { driver.Name = DrivierNametxt.Text; }

            if (LicenseNametxt.Text != "")
            { driver.license = LicenseNametxt.Text; }

            if (phoneNametxt.Text != "")
            { driver.phone = phoneNametxt.Text; }


            return driver;
        }
        protected void ClearSearchModel()
        {
            DrivierNametxt.Text = "";
            LicenseNametxt.Text = "";
            phoneNametxt.Text = "";
        }

        // Operations Part
        protected void Save_Click(object sender, EventArgs e)
        {
            Driver driver = DriverModel();
            if (driver.Id == 0)
            {
                try
                {
                    db.Driver.Add(driver);
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
                    db.Driver.AddOrUpdate(driver);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                }
            }
            ClearDriverModel();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        protected Driver DriverModel()
        {
            Driver driver = new Driver();
            if (IdHid.Value != "0"){driver.Id = Convert.ToInt32(IdHid.Value);}

            if (NameTxt.Text != "") { driver.Name = NameTxt.Text; }

            if (AddressTxt.Text != ""){ driver.Address = AddressTxt.Text; }

            if (PhoneTxt.Text != "") { driver.phone = PhoneTxt.Text; }

            if (licenseTxt.Text != ""){ driver.license = licenseTxt.Text; }

            if (LicenceDateTxt.Text != "") { driver.LicenceDate =ExtendedMethod.FormatDate(LicenceDateTxt.Text); }

            if (LicencePeriodTxt.Text != "") { driver.LicencePeriod =int.Parse( LicencePeriodTxt.Text); }
            //Auto Calculate
            
            var date = DateTime.Parse(driver.LicenceDate.ToString()).Year + "-" + DateTime.Parse(driver.LicenceDate.ToString()).Month + "-" + DateTime.Parse(driver.LicenceDate.ToString()).Day;
            DateTime newyears = /*Convert.ToInt32(date[0])*/DateTime.Parse(driver.LicenceDate.ToString()).AddYears(int.Parse(driver.LicencePeriod.ToString()));
            driver.LicenseEndDate = DateTime.Parse(newyears.Year + "-" + DateTime.Parse(driver.LicenceDate.ToString()).Month + "-" + DateTime.Parse(driver.LicenceDate.ToString()).Day, CultureInfo.CreateSpecificCulture("ar-EG"));



            if (AlertPeriodTxt.Text != "") { driver.AlertPeriod = Convert.ToInt32(AlertPeriodTxt.Text); }

            if (SelectedTrafDepId != "") { driver.TrafficDepID = Convert.ToInt32(SelectedTrafDepId); }
            driver.LoginID= ExtendedMethod.LoginedUser.Id; 
            return driver;
        }
        protected void ClearDriverModel()
        {
            IdHid.Value = "0";
            NameTxt.Text = "";
            AddressTxt.Text = "";
            PhoneTxt.Text = "";
            licenseTxt.Text = "";
            LicenceDateTxt.Text = "";
            LicencePeriodTxt.Text = "";
            // Initial AlertPeriod
            AlertPeriodTxt.Text = "90";

            AddTrafDepListhtml.SelectedIndex = 0;

        }
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            Driver temDriver = new Driver();
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchDriverList.Count > 0)
            { temDriver = SearchDriverList[index]; }
            else { temDriver = DriverList[index]; }
            try
            {
                IdHid.Value = temDriver.Id.ToString();
                NameTxt.Text = temDriver.Name;
                AddressTxt.Text = temDriver.Address;
                PhoneTxt.Text = temDriver.phone;
                licenseTxt.Text = temDriver.license;
                LicenceDateTxt.Text =ExtendedMethod.ParseDateToString(DateTime.Parse(temDriver.LicenceDate.ToString()));
                LicencePeriodTxt.Text = temDriver.LicencePeriod.ToString();
                AlertPeriodTxt.Text = temDriver.AlertPeriod.ToString();
                AddTrafDepListhtml.SelectedValue = temDriver.TrafficDepID.ToString();
            }
            catch(Exception Exec)
            {

            }

            SearchDriverList = new List<Driver>();
            UpdatePanel3.Update();
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int deleted_index = 0;
            int index = Convert.ToInt32(e.CommandArgument);
            if (SearchDriverList.Count > 0)
            { deleted_index = Convert.ToInt32(SearchDriverList[index].Id); }
            else { deleted_index = Convert.ToInt32(DriverList[index].Id); }

            var deletedDriver = db.Driver.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.Driver.Remove(deletedDriver);
            db.SaveChanges();

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}