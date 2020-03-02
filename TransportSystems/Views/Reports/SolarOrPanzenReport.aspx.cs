using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;

namespace TransportSystems.Views.Reports
{
    public partial class SolarOrPanzenReport : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<Cars> CarsList = new List<Cars>();
        Cars myCars = new Cars();
        //
        public static List<Services> ServicesList = new List<Services>();
        Services myServices = new Services();
        //
        public static List<Solar> SearchSolarList = new List<Solar>();
        List<Solar> SolarList = new List<Solar>();
        Solar mySolar = new Solar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Set_initial();
                BindGridList();
            }
        }
        protected void Set_initial()
        {
            //
            SolarList = db.Solar.ToList();
            // Search Service List
            ServicesList = db.Services.Where(item => item.price.ToString() != "").ToList();
            List<Services> TempServiceslist = new List<Services>();
            TempServiceslist.Add(new Services { Name = "-- select Service Name --", Id = 0 });
            TempServiceslist.AddRange(ServicesList);
            ServiceIDListTxt.DataSource = TempServiceslist;
            ServiceIDListTxt.DataValueField = "Id";
            ServiceIDListTxt.DataTextField = "Name";
            ServiceIDListTxt.DataBind();
            // Search Car List
            CarsList = db.Cars.ToList();
            List<Cars> TempCarslist = new List<Cars>();
            TempCarslist.Add(new Cars { CarNo = "-- select Car No --", id = 0 });
            TempCarslist.AddRange(CarsList);
            CarNoListtxt.DataSource = TempCarslist;
            CarNoListtxt.DataValueField = "id";
            CarNoListtxt.DataTextField = "CarNo";
            CarNoListtxt.DataBind();
        }
        protected void Search_Click(object sender, EventArgs e)
        {
            SolarList = new List<Solar>();
            mySolar = new Solar();

            var FromDateparam = new SqlParameter();
            var ToDateparam = new SqlParameter();
            var CarIDparam = new SqlParameter();
            var ServiceIDparam = new SqlParameter();
            //From Date
            if (!string.IsNullOrEmpty(FromDate.Text))
            {
                FromDateparam = new SqlParameter("@FromDate", FromDate.Text.Trim());
            }
            else { FromDateparam = new SqlParameter("@FromDate", DBNull.Value); }
            // To date
            if (!string.IsNullOrEmpty(ToDate.Text))
            {
                ToDateparam = new SqlParameter("@ToDate", ToDate.Text.Trim());
            }
            else { ToDateparam = new SqlParameter("@ToDate", DBNull.Value); }
            //Car List
            if (!string.IsNullOrEmpty(CarNoListtxt.SelectedValue) && CarNoListtxt.SelectedValue != "0")
            {
                CarIDparam = new SqlParameter("@CarID", CarNoListtxt.SelectedValue);
            }
            else { CarIDparam = new SqlParameter("@CarID", DBNull.Value); }
            //Service List
            if (!string.IsNullOrEmpty(ServiceIDListTxt.SelectedValue) && ServiceIDListTxt.SelectedValue != "0")
            {
                ServiceIDparam = new SqlParameter("@ServiceID", ServiceIDListTxt.SelectedValue);
            }
            else { ServiceIDparam = new SqlParameter("@ServiceID", DBNull.Value); }

            ///////////////////// Get Items
            SolarList = db.Database
                .SqlQuery<Solar>("SP_Solar @FromDate , @ToDate , @CarID, @ServiceID", FromDateparam, ToDateparam, CarIDparam, ServiceIDparam).ToList();

            /////////////////
            foreach (var item in SolarList)
            {
                item.Cars = db.Cars.Where(c => c.id == item.CarID).FirstOrDefault();
                item.Cars.CarType = db.CarType.Where(c => c.Id == item.Cars.CarTypeId).FirstOrDefault();
                item.Driver = db.Driver.Where(c => c.Id == item.DriverID).FirstOrDefault();
                item.Services = db.Services.Where(c => c.Id == item.ServiceID).FirstOrDefault();
            }
            SearchSolarList = SolarList;
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = SolarList;
            GridView1.DataBind();
        }
        protected void NewSearch_Click(object sender, EventArgs e)
        {
            FromDate.Text = "";
            ToDate.Text = "";
            ServiceIDListTxt.SelectedIndex = 0;
            CarNoListtxt.SelectedIndex = 0;
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void ExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=تقرير استعاضة سولار او بنزين.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GridView1.RenderControl(hw);
                    string style = "<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}