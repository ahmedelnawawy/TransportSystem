using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;


namespace TransportSystems.Views.Reports
{
    public partial class TrasnportOrderReport : System.Web.UI.Page
    {
        public TransportModel db = new TransportModel();

        public static List<SubAccount> _SubAccountList = new List<SubAccount>();
        public static List<Cars> _CarList = new List<Cars>();
        public static List<Product> ProductList = new List<Product>();
        public class TransportReport
        {
            public int Id { set; get; }
            public string ClientName { set; get; }
            public string VendorName { set; get; }
            public string CarName { set; get; }
            public string ProductName { set; get; }
            public string FromRegon { set; get; }
            public string ToRegon { set; get; }
            public float Qty { set; get; }
            public float Total { set; get; }
            public string PaymentMethod { set; get; }
            public DateTime Date { set; get; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize_Page();
            }

        }
        public void Initialize_Page (){

            FromDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            ToDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            _SubAccountList = db.SubAccount.ToList();
            _CarList = db.Cars.ToList();

            TransportGrd.DataSource = null;
            TransportGrd.DataBind();
        }

        public  List<TransportReport> TransportCommanDateSearch(string FromDate,string ToDate)
        {
            List<TransportCommand> TransportCommandList = db.TransportCommand.ToList().Where(t=>t.TransportCommandTime>=DateTime.Parse( FromDate, CultureInfo.CreateSpecificCulture("ar-EG")) &
            t.TransportCommandTime <= DateTime.Parse(ToDate, CultureInfo.CreateSpecificCulture("ar-EG"))).ToList();
            List<TransportReport> ReportList = new List<TransportReport>();

            foreach (var command in TransportCommandList)
            {
                ReportList.Add(new TransportReport {
                    Id=command.Id,
                    ClientName=_SubAccountList.Where(s=>s.ID==command.SubAccClientId).FirstOrDefault().name,
                    VendorName= _SubAccountList.Where(s => s.ID == command.SubAccVendorId).FirstOrDefault().name,
                    CarName=_CarList.Where(c=>c.id==command.CarId).FirstOrDefault().CarType.Name,
                    ProductName=command.Product.name,// ProductList.Where(p=>p.ID==command.ProductId).FirstOrDefault().name,
                    FromRegon=command.FromRegion.Name,
                    ToRegon=command.FromRegion1.Name,
                    Qty=(float)command.Quantity,
                    Total =(float)command.TotalTransportPrice,
                    PaymentMethod=command.PaymentWay,
                    Date=command.TransportCommandTime
                   
                });

            }

            return ReportList;


        }
        protected void SaerchBtn_Click(object sender, EventArgs e)
        {
            TransportGrd.DataSource = TransportCommanDateSearch(FromDateTxt.Text,ToDateTxt.Text);
            TransportGrd.DataBind();
            

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=كارتة صنف.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    TransportGrd.RenderControl(hw);
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