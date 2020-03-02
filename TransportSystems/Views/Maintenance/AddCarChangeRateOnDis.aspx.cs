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

namespace TransportSystems.Views.Maintenance
{
    public partial class AddCarChangeRateOnDis : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<CarChangeRateOnDis> SearchCarChangeRateOnDisList = new List<CarChangeRateOnDis>();
        List<CarChangeRateOnDis> CarChangeRateOnDisList = new List<CarChangeRateOnDis>();
        CarChangeRateOnDis myCarChangeRateOnDis = new CarChangeRateOnDis();

        public static List<Cars> CarsList = new List<Cars>();
        Cars myCars = new Cars();

        public static List<Services> ServicesList = new List<Services>();
        Services myServices = new Services();

        public static string SelectedCarId = "", SelectedServiceId = "";

        public static bool EditFlage = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Set_initial();
            }
            SelectedCarId = AddCarsListhtml.SelectedValue;
            SelectedServiceId = AddServicesListhtml.SelectedValue;

            CarChangeRateOnDisList = db.CarChangeRateOnDis.ToList();
            BindGridList();
        }
        protected void Set_initial()
        {
            
            //make custom ValidationGroup >>><<<<
            string vgId = Guid.NewGuid().ToString();

            Description.ValidationGroup = vgId;

            //before
            Before.ValidationGroup = vgId;
            RequiredFieldValidator1.ValidationGroup = vgId;
            //
            DateBefore.ValidationGroup = vgId;
            RequiredFieldValidator2.ValidationGroup = vgId;
            //
            AtHourBefore.ValidationGroup = vgId;
            //After
            After.ValidationGroup = vgId;
            RequiredFieldValidator3.ValidationGroup = vgId;
            //
            DateAfter.ValidationGroup = vgId;
            RequiredFieldValidator4.ValidationGroup = vgId;
            //
            AtHourAfter.ValidationGroup = vgId;
            //Btn And summary
            ValidationSummary1.ValidationGroup = vgId;
            Save.ValidationGroup = vgId;

            //CarsList  (For Adding will be forced to choose)
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

            //Services  (For Adding will be forced to choose)
            ServicesList = db.Services.ToList();
            BindDropDownList(ServicesList, AddServicesListhtml);
            // (For search will be optional to choose)
            List<Services> TempServiceslist = new List<Services>();
            TempServiceslist.Add(new Services { Name = "-- select Services --", Id = 0 });
            TempServiceslist.AddRange(ServicesList);
            BindDropDownList(TempServiceslist, ServicesSearchListtxt);

            Before.Text = "0";
            DateBefore.Text = ExtendedMethod.GetDateToday();
            //
            After.Text = "0";
            DateAfter.Text = ExtendedMethod.GetDateToday();
            //Disable After Part Untill edit
            EditFlage = false;
            After.Enabled = false;
            DateAfter.Enabled = false;
            AtHourAfter.Enabled = false;
            //enable Before Part Untill edit
            Before.Enabled = true;
            DateBefore.Enabled = true;
            AtHourBefore.Enabled = true;
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
            GridView1.DataSource = CarChangeRateOnDisList;
            GridView1.DataBind();
        }
        public void ShowMessage(string message)
        {
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("function(){");
            //sb.Append("alert('");
            //sb.Append(message);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString(),true);
            //Response.Write("<script type='text/javascript'>alert('" + message + "');</script>");
            AddErrorTxt.Text = message;
            AddErrorTxt.ForeColor = System.Drawing.Color.Red;
        }
        protected double IsReachedChangeRate(int CarId, int ServiceId)
        {
            var CarChangeRateOnDisList = db.CarChangeRateOnDis.Where(carcha => carcha.CarId == CarId && carcha.ServiceId == ServiceId && carcha.State == false).ToList();

            var SumOfBefore = CarChangeRateOnDisList.Select(item => item.Before).Sum();
            var SumOfAfter = CarChangeRateOnDisList.Select(item => item.After).Sum();
            return SumOfAfter - SumOfBefore; //Return Cuurent Dis
        }
        // Operation Part
        protected void Save_Click(object sender, EventArgs e)
        {
            CarChangeRateOnDis carChangeRateOnDis = CarChangeRateOnDisModel();

            // to have change rate and alert rate values
            var carmaintainceItem = db.CarMaintenance.Where(carmain => carmain.CarId == carChangeRateOnDis.CarId && carmain.ServiceId == carChangeRateOnDis.ServiceId).FirstOrDefault();
            ////////////
            var Dis = IsReachedChangeRate(carChangeRateOnDis.CarId, carChangeRateOnDis.ServiceId);
            // low htb2a bra al change rate

            if (carChangeRateOnDis.Id == 0)
            {
                if (Dis <= Convert.ToDouble(carmaintainceItem.ChangeRate))
                {
                    try
                    {
                        db.CarChangeRateOnDis.Add(carChangeRateOnDis);
                        db.SaveChanges();
                        ClearModel();
                        Page.Response.Redirect(Page.Request.Url.ToString(), false);
                    }
                    catch (Exception except)
                    {
                    }
                }
                else
                {
                    ShowMessage("This Movement Will be More than Change Rate can not Added");
                }
            }
            else
            {
                try
                {
                    db.CarChangeRateOnDis.AddOrUpdate(carChangeRateOnDis);
                    db.SaveChanges();
                    ClearModel();
                    Page.Response.Redirect(Page.Request.Url.ToString(), false);
                }
                catch (Exception except)
                {
                }
            }
        }
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            EditFlage = true;
            //enable After Part Untill edit
            After.Enabled = true;
            DateAfter.Enabled = true;
            AtHourAfter.Enabled = true;
            //Disable Before Part Untill edit
            Before.Enabled = false;
            DateBefore.Enabled = false;
            AtHourBefore.Enabled = false;

            CarChangeRateOnDis temCarChangeRateOndis = new CarChangeRateOnDis();
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchCarChangeRateOnDisList.Count > 0)
            { temCarChangeRateOndis = SearchCarChangeRateOnDisList[index]; }
            else { temCarChangeRateOndis = CarChangeRateOnDisList[index]; }
            try
            {
                IdHid.Value = temCarChangeRateOndis.Id.ToString();
                Description.Text = temCarChangeRateOndis.Description;

                AddCarsListhtml.SelectedValue = temCarChangeRateOndis.CarId.ToString();
                AddServicesListhtml.SelectedValue = temCarChangeRateOndis.ServiceId.ToString();

                Before.Text = temCarChangeRateOndis.Before.ToString();
                DateBefore.Text =ExtendedMethod.ParseDateToString(DateTime.Parse(temCarChangeRateOndis.DateBefore.ToString()));
                AtHourBefore.Text = temCarChangeRateOndis.AtHourBefore;

                After.Text = temCarChangeRateOndis.After.ToString();
                DateAfter.Text = ExtendedMethod.ParseDateToString(DateTime.Parse(temCarChangeRateOndis.DateAfter.ToString()));
                AtHourAfter.Text = temCarChangeRateOndis.AtHourAfter;
            }
            catch (Exception Exec)
            {
            }
            SearchCarChangeRateOnDisList = new List<CarChangeRateOnDis>();
            UpdatePanel3.Update();
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int deleted_index = 0;
            int index = Convert.ToInt32(e.CommandArgument);

            if (SearchCarChangeRateOnDisList.Count > 0)
            { deleted_index = Convert.ToInt32(SearchCarChangeRateOnDisList[index].Id); }
            else { deleted_index = Convert.ToInt32(CarChangeRateOnDisList[index].Id); }

            var deletedItem = db.CarChangeRateOnDis.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.CarChangeRateOnDis.Remove(deletedItem);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        protected CarChangeRateOnDis CarChangeRateOnDisModel()
        {
            CarChangeRateOnDis carChangeRateOnDis = new CarChangeRateOnDis();

            if (IdHid.Value != "0") { carChangeRateOnDis.Id = Convert.ToInt32(IdHid.Value); }

            if (Description.Text != "") { carChangeRateOnDis.Description = Description.Text; }

            if (SelectedCarId != "") { carChangeRateOnDis.CarId = Convert.ToInt32(SelectedCarId); }

            if (SelectedServiceId != "") { carChangeRateOnDis.ServiceId = Convert.ToInt32(SelectedServiceId); }
            //Before
            if (Before.Text != "") { carChangeRateOnDis.Before = Convert.ToDouble(Before.Text); }

            if (DateBefore.Text != "") { carChangeRateOnDis.DateBefore =ExtendedMethod.FormatDate(DateBefore.Text); }

            if (AtHourBefore.Text != "") { carChangeRateOnDis.AtHourBefore = AtHourBefore.Text; }
            //After
            if (After.Text != "") { carChangeRateOnDis.After = Convert.ToDouble(After.Text); }

            if (DateAfter.Text != "") { carChangeRateOnDis.DateAfter = ExtendedMethod.FormatDate(DateAfter.Text); }

            if (AtHourAfter.Text != "") { carChangeRateOnDis.AtHourAfter = AtHourAfter.Text; }

            carChangeRateOnDis.LoginID = ExtendedMethod.LoginedUser.Id;

            return carChangeRateOnDis;
        }
        protected void ClearModel()
        {
            IdHid.Value = "0";
            Description.Text = "";

            AddCarsListhtml.SelectedIndex = 0;
            AddServicesListhtml.SelectedIndex = 0;

            Before.Text = "0";
            DateBefore.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            AtHourBefore.Text = "";

            After.Text = "0";
            DateAfter.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            AtHourAfter.Text = "";
        }
        // Search Part
        protected void Search_Click(object sender, EventArgs e)
        {
            CarChangeRateOnDisList = new List<CarChangeRateOnDis>();
            myCarChangeRateOnDis = new CarChangeRateOnDis();
            myCarChangeRateOnDis = SearchModel();

            var CarId = new SqlParameter();
            var ServiceId = new SqlParameter();

            //
            if (myCarChangeRateOnDis.CarId != 0)
            {
                CarId = new SqlParameter("@CarId", myCarChangeRateOnDis.CarId);
            }
            else { CarId = new SqlParameter("@CarId", DBNull.Value); }
            //
            if (myCarChangeRateOnDis.ServiceId != 0)
            {
                ServiceId = new SqlParameter("@ServiceId", myCarChangeRateOnDis.ServiceId);
            }
            else { ServiceId = new SqlParameter("@ServiceId", DBNull.Value); }

            CarChangeRateOnDisList = db.Database
                .SqlQuery<CarChangeRateOnDis>("SP_CarChangeRateOnDis @CarId , @ServiceId", CarId, ServiceId).ToList();

            foreach (var item in CarChangeRateOnDisList)
            {
                item.Cars = db.Cars.Where(c => c.id == item.CarId).FirstOrDefault();
                item.Services = db.Services.Where(c => c.Id == item.ServiceId).FirstOrDefault();
            }
            SearchCarChangeRateOnDisList = CarChangeRateOnDisList;
            BindGridList();
            ClearSearchModel();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            SearchCarChangeRateOnDisList = new List<CarChangeRateOnDis>();
            ClearSearchModel();
            myCarChangeRateOnDis = new CarChangeRateOnDis();
            CarChangeRateOnDisList = new List<CarChangeRateOnDis>();
            CarChangeRateOnDisList = db.CarChangeRateOnDis.ToList();
            BindGridList();
        }
        protected CarChangeRateOnDis SearchModel()
        {
            CarChangeRateOnDis carChangeRateOnDis = new CarChangeRateOnDis();
            if (CarsSearchListtxt.SelectedValue != "0") { carChangeRateOnDis.CarId = Convert.ToInt32(CarsSearchListtxt.SelectedValue); }
            if (ServicesSearchListtxt.SelectedValue != "0") { carChangeRateOnDis.ServiceId = Convert.ToInt32(ServicesSearchListtxt.SelectedValue); }
            return carChangeRateOnDis;
        }

        protected void After_TextChanged(object sender, EventArgs e)
        {
            int myCarId=0, myServiceID =0;

            if (SelectedCarId != "") { myCarId = Convert.ToInt32(SelectedCarId); }

            if (SelectedServiceId != "") { myServiceID = Convert.ToInt32(SelectedServiceId); }

            // to have change rate and alert rate values
            var carmaintainceItem = db.CarMaintenance.Where(carmain => carmain.CarId == myCarId && carmain.ServiceId == myServiceID).FirstOrDefault();

            var Dis = IsReachedChangeRate(myCarId, myServiceID);

            var currentAfter = Convert.ToDouble(After.Text);

            if ((Dis + currentAfter) >= Convert.ToDouble(carmaintainceItem.ChangeRate))
            {
                ShowMessage("You Now More Than Change Rate Of this Car To This Service");
            }
            else if ((Dis + Convert.ToDouble(After.Text)) >= Convert.ToDouble(carmaintainceItem.AlertRate))
            {
                ShowMessage("You  Now Reach alert Rate to this Car Of this service");
            }
            else
            {
                ShowMessage("");
            }
        }
        protected void Before_TextChanged(object sender, EventArgs e)
        {
            int myCarId = 0, myServiceID = 0;

            if (SelectedCarId != "") { myCarId = Convert.ToInt32(SelectedCarId); }

            if (SelectedServiceId != "") { myServiceID = Convert.ToInt32(SelectedServiceId); }

            // to have change rate and alert rate values
            var carmaintainceItem = db.CarMaintenance.Where(carmain => carmain.CarId == myCarId && carmain.ServiceId == myServiceID).FirstOrDefault();

            if (carmaintainceItem != null)
            {
                var Dis = IsReachedChangeRate(myCarId, myServiceID);
                if (Dis >= Convert.ToDouble(carmaintainceItem.ChangeRate))
                {
                    ShowMessage("You can not Add this Distance becuuse you will break change rate to this car");
                }
                else if (Dis >= Convert.ToDouble(carmaintainceItem.AlertRate))
                {
                    ShowMessage("You  will Reach alert Rate to this Car Of this service");
                }
                else
                {
                    ShowMessage("");
                }
            }
            else
            {
                ShowMessage("This service Not Availble to this car");
            }
        }

        protected void ClearSearchModel()
        {
            CarsSearchListtxt.SelectedIndex = 0;
            ServicesSearchListtxt.SelectedIndex = 0;
        }
    }
}