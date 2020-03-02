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
    public partial class AddCars : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<Cars> SearchCarsList = new List<Cars>();
        List<Cars> CarsList = new List<Cars>();
        Cars myCars = new Cars();

        public static List<Colors> ColorsList = new List<Colors>();
        Colors myColors = new Colors();

        public static List<City> CityList = new List<City>();
        City myCity = new City();

        public static List<CarType> CarTypeList = new List<CarType>();
        CarType myCarType = new CarType();

        public static List<TrafficDepartment> TrafficDepartmentList = new List<TrafficDepartment>();
        TrafficDepartment myTrafficDepartment = new TrafficDepartment();

        public static List<SubAccount> SubAccountList = new List<SubAccount>();
        SubAccount mySubAccount = new SubAccount();

        public static string  SelectedColorId = "", SelectedCityId = "", SelectedCarTypeId = "", SelectedTrafDepId = "", SelectedSubAccountId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Set_initial();
            }
            SelectedColorId = AddColorListhtml.SelectedValue;
            SelectedCityId = AddCityListhtml.SelectedValue;
            SelectedCarTypeId = AddCarTypeListhtml.SelectedValue;
            SelectedTrafDepId = AddTrafDepListhtml.SelectedValue;
            SelectedSubAccountId = AddSupplierListhtml.SelectedValue;
            CarsList = db.Cars.ToList();
            BindGridList();
        }
        protected void Set_initial()
        {
            //make custom ValidationGroup >>><<<<
            string vgId = Guid.NewGuid().ToString();

            CarNoTxt.ValidationGroup = vgId;
            RequiredFieldValidator1.ValidationGroup = vgId;

            LicenceNOTxt.ValidationGroup = vgId;
            RequiredFieldValidator2.ValidationGroup = vgId;

            LicenceDateTxt.ValidationGroup = vgId;
            RequiredFieldValidator3.ValidationGroup = vgId;

            LicencePeriodTxt.ValidationGroup = vgId;
            RequiredFieldValidator4.ValidationGroup = vgId;

            AlertPeriodTxt.ValidationGroup = vgId;
            RequiredFieldValidator5.ValidationGroup = vgId;

            ValidationSummary1.ValidationGroup = vgId;
            Save.ValidationGroup = vgId;

            //Colors  (For Adding will be forced to choose)
            ColorsList = db.Colors.ToList();
            BindDropDownList(ColorsList, AddColorListhtml);

            //City  (For Adding will be forced to choose)
            CityList = db.City.ToList();
            BindDropDownList(CityList, AddCityListhtml);

            //CarType  (For Adding will be forced to choose)
            CarTypeList = db.CarType.ToList();
            BindDropDownList(CarTypeList, AddCarTypeListhtml);

            //(For Adding will be forced to choose)
            TrafficDepartmentList = db.TrafficDepartment.ToList();
            BindDropDownList(TrafficDepartmentList, AddTrafDepListhtml);
            // (For search will be optional to choose)
            //List<TrafficDepartment> TempTrafDeplist = new List<TrafficDepartment>();
            //TempTrafDeplist.Add(new TrafficDepartment { Name = "-- select TrafficDepartment --", Id = 0 });
            //TempTrafDeplist.AddRange(TrafficDepartmentList);
            //TrafDepSearchListtxt.DataSource = TempTrafDeplist;
            //TrafDepSearchListtxt.DataTextField = "Name";
            //TrafDepSearchListtxt.DataValueField = "Id";
            //TrafDepSearchListtxt.DataBind();
            //subAccount  (For Adding will be forced to choose)
            SubAccountList = db.SubAccount.Where(sub => sub.UpAccount == "2103").ToList();
            BindDropDownList(SubAccountList, AddSupplierListhtml);
            // Initial AlertPeriod
            AlertPeriodTxt.Text = "90";
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
            GridView1.DataSource = CarsList;
            GridView1.DataBind();
        }
        protected void Save_Click(object sender, EventArgs e)
        {
            Cars Car = CarsModel();
            if (Car.id == 0)
            {
                try
                {
                    db.Cars.Add(Car);
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
                    db.Cars.AddOrUpdate(Car);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                }
            }
            ClearDriverModel();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        protected Cars CarsModel()
        {
            Cars cars = new Cars();

            if (CarsIdHid.Value != "0") { cars.id = Convert.ToInt32(CarsIdHid.Value); }

            if (CarNoTxt.Text != "") { cars.CarNo = CarNoTxt.Text; }

            if (LicenceNOTxt.Text != "") { cars.LicenceNO = LicenceNOTxt.Text; }

            if (LicenceDateTxt.Text != "") { cars.LicenceDate =DateTime.Parse(LicenceDateTxt.Text,CultureInfo.CreateSpecificCulture("ar-EG")); }

            if (LicencePeriodTxt.Text != "") { cars.LicencePeriod = int.Parse(LicencePeriodTxt.Text); }

            //Auto Calculate LicenseEndDate
            var date =DateTime.Parse(cars.LicenceDate.ToString()).Year+"-"+ DateTime.Parse(cars.LicenceDate.ToString()).Month+"-"+ DateTime.Parse(cars.LicenceDate.ToString()).Day;
            DateTime newyears = /*Convert.ToInt32(date[0])*/DateTime.Parse(cars.LicenceDate.ToString()).AddYears(int.Parse(cars.LicencePeriod.ToString()));
            cars.LicenseEndDate =DateTime.Parse(newyears.Year+ "-"+ DateTime.Parse(cars.LicenceDate.ToString()).Month + "-" + DateTime.Parse(cars.LicenceDate.ToString()).Day,CultureInfo.CreateSpecificCulture("ar-EG"));

            if (AlertPeriodTxt.Text != "") { cars.AlertPeriod = Convert.ToInt32(AlertPeriodTxt.Text); }

            if (SelectedColorId != "") { cars.ColorId = Convert.ToInt32(SelectedColorId); }

            if (SelectedCityId != "") { cars.CityId = Convert.ToInt32(SelectedCityId); }

            if (SelectedCarTypeId != "") { cars.CarTypeId = Convert.ToInt32(SelectedCarTypeId); }

            if (SelectedTrafDepId != "") { cars.TrafficDepID = Convert.ToInt32(SelectedTrafDepId); }

            if (SelectedSubAccountId != "") { cars.SubAccId = Convert.ToInt32(SelectedSubAccountId); }
            cars.LoginID = ExtendedMethod.LoginedUser.Id;
            return cars;
        }
        protected void ClearDriverModel()
        {
            CarsIdHid.Value = "0";
            CarNoTxt.Text = "";
            LicenceNOTxt.Text = "";
            LicenceDateTxt.Text = "";
            LicencePeriodTxt.Text = "";
            // Initial AlertPeriod
            AlertPeriodTxt.Text = "90";
            //lists initial
            AddColorListhtml.SelectedIndex = 0;
            AddCityListhtml.SelectedIndex = 0;
            AddCarTypeListhtml.SelectedIndex = 0;
            AddTrafDepListhtml.SelectedIndex = 0;
            AddSupplierListhtml.SelectedIndex = 0;

        }

        protected void edit_Command(object sender, CommandEventArgs e)
        {
            Cars temCar = new Cars();
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchCarsList.Count > 0)
            { temCar = SearchCarsList[index]; }
            else { temCar = CarsList[index]; }
            try
            {
                CarsIdHid.Value = temCar.id.ToString();
                CarNoTxt.Text = temCar.CarNo;
                LicenceNOTxt.Text = temCar.LicenceNO;
                LicenceDateTxt.Text =ExtendedMethod.ParseDateToString(DateTime.Parse(temCar.LicenceDate.ToString()));
                LicencePeriodTxt.Text =temCar.LicencePeriod.ToString();
                AlertPeriodTxt.Text = temCar.AlertPeriod.ToString();

                AddColorListhtml.SelectedValue = temCar.ColorId.ToString();
                AddCityListhtml.SelectedValue = temCar.CityId.ToString();
                AddCarTypeListhtml.SelectedValue = temCar.CarTypeId.ToString();
                AddTrafDepListhtml.SelectedValue = temCar.TrafficDepID.ToString();
                AddSupplierListhtml.SelectedValue = temCar.SubAccId.ToString();
            }
            catch (Exception Exec)
            {

            }

            SearchCarsList = new List<Cars>();
            UpdatePanel3.Update();
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int deleted_index = 0;
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchCarsList.Count > 0)
            { deleted_index = Convert.ToInt32(SearchCarsList[index].id); }
            else { deleted_index = Convert.ToInt32(CarsList[index].id); }

            var deletedDriver = db.Cars.Where(SS => SS.id == deleted_index).FirstOrDefault();
            db.Cars.Remove(deletedDriver);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            CarsList = new List<Cars>();
            myCars = new Cars();
            myCars = SearchModel();

            var CarNo = new SqlParameter();
            var LicenceNO = new SqlParameter();
            
            //
            if (myCars.CarNo != null)
            {
                CarNo = new SqlParameter("@CarNo", myCars.CarNo.Trim());
            }
            else { CarNo = new SqlParameter("@CarNo", DBNull.Value); }
            //
            if (myCars.LicenceNO != null)
            {
                LicenceNO = new SqlParameter("@LicenceNO", myCars.LicenceNO.Trim());
            }
            else { LicenceNO = new SqlParameter("@LicenceNO", DBNull.Value); }

            CarsList = db.Database
                .SqlQuery<Cars>("SP_Cars @CarNo , @LicenceNO ", CarNo, LicenceNO).ToList();
            foreach(var item in CarsList)
            {
                item.Colors = db.Colors.Where(col => col.id == item.ColorId).FirstOrDefault();
                item.City = db.City.Where(c => c.Id == item.CityId).FirstOrDefault();
                item.CarType = db.CarType.Where(c => c.Id == item.CarTypeId).FirstOrDefault();
                item.TrafficDepartment = db.TrafficDepartment.Where(c => c.Id == item.TrafficDepID).FirstOrDefault();
                item.SubAccount = db.SubAccount.Where(c => c.ID == item.SubAccId).FirstOrDefault();
            }

            SearchCarsList = CarsList;
            BindGridList();
            ClearSearchModel();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            SearchCarsList = new List<Cars>();
            ClearSearchModel();
            myCars = new Cars();
            CarsList = new List<Cars>();
            CarsList = db.Cars.ToList();
            BindGridList();
        }
        protected Cars SearchModel()
        {
            Cars cars = new Cars();

            if (CarNoSearchtxt.Text != "")
            { cars.CarNo = CarNoSearchtxt.Text; }

            if (LicenceNOSearchtxt.Text != "")
            { cars.LicenceNO = LicenceNOSearchtxt.Text; }

            return cars;
        }
        protected void ClearSearchModel()
        {
            CarNoSearchtxt.Text = "";
            LicenceNOSearchtxt.Text = "";
        }
    }
}