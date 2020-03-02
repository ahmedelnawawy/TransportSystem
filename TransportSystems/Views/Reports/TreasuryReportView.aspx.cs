using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AdsSysWeb.Models.LmsaEntitiesDB;


namespace TransportSystems.Views.Reports
{
    public partial class TreasuryReportView : System.Web.UI.Page
    {
        public class Treasury
        {
            public int OperationID { set; get; }
            public string OperationType { set; get; }
            public float BalanceBefor { set; get; }
            public float BalanceAfter { set; get; }
            public float Credit_Movement { set; get; }
            public float InDebt_Movement { set; get; }
        }
        public List<Treasury> Treasury_List;
        public SubAccountManul Sub_Account;
        public KhznaMovedManul Khazna_Moved;

        protected void Page_Load(object sender, EventArgs e)
        {
            Treasury_List = new List<Treasury>();

            if (!Page.IsPostBack)
            {
                Initialize_Page();
            }

        }

        public void Initialize_Page()
        {
            Sub_Account = new SubAccountManul();
            FromDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            ToDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            KhaznaDropID.DataSource = Sub_Account.GetSubAccount_ByUpID(1103);
            KhaznaDropID.DataTextField = "name";
            KhaznaDropID.DataValueField = "ID";
            KhaznaDropID.DataBind();

            TreasuryGrd.DataSource = null;
            TreasuryGrd.DataBind();

        }

        static float sum = 0;
        public void Calc_Operation()
        {
            Sub_Account = new SubAccountManul();
            Khazna_Moved = new KhznaMovedManul();
            float KhaznaStartBalance = float.Parse(Sub_Account.GetSubAccount_ByID(int.Parse(KhaznaDropID.SelectedValue)).ElementAtOrDefault(0).ABalance.ToString());
            double MonyIn, MonyOut = 0;
            List<KhznaMovedManul> Khazna_Moved_List = new List<KhznaMovedManul>();
            Khazna_Moved_List = Khazna_Moved.GetAllKhznaMoved();
            Treasury_List = new List<Treasury>();
            String operationTypeN = "";
            sum = KhaznaStartBalance;
            int counter = 0;
            foreach (var operation in Khazna_Moved_List)
            {

                if (operation.state == false)
                {
                    operationTypeN = "مستلم";

                    Treasury_List.Add(new Treasury()
                    {
                        OperationID = operation.ID,
                        OperationType = operationTypeN,
                        BalanceBefor = sum,
                        BalanceAfter = float.Parse(operation.Value.ToString()) + sum,
                        Credit_Movement = 0,
                        InDebt_Movement = float.Parse(operation.Value.ToString())
                    });
                    sum = sum + float.Parse(operation.Value.ToString());
                }
                else {
                    operationTypeN = "صرف";
                    float s1 = 0;

                    s1 = sum - float.Parse(operation.Value.ToString());
                    Treasury_List.Add(new Treasury()
                    {
                        OperationID = operation.ID,
                        OperationType = operationTypeN,
                        BalanceBefor = sum,
                        BalanceAfter = s1,
                        Credit_Movement = float.Parse(operation.Value.ToString()),
                        InDebt_Movement = 0
                    });
                    sum = s1;
                }

                //  sum += float.Parse(operation.Value.ToString());
                counter++;
            }

            TreasuryGrd.DataSource = Treasury_List;
            TreasuryGrd.DataBind();
        }

        //البحث
        protected void SaerchBtn_Click(object sender, EventArgs e)
        {
            Calc_Operation();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=تقرير خزينة.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    TreasuryGrd.RenderControl(hw);
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