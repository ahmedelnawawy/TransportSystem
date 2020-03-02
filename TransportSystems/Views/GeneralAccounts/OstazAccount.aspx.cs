using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;
namespace TransportSystems.Views.GeneralAccounts
{
    public partial class OstazAccount : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public class Treasury
        {
            public int OperationID { set; get; }
            public string OperationType { set; get; }
            public float BalanceBefor { set; get; }
            public float BalanceAfter { set; get; }
            public float Credit_Movement { set; get; }
            public float InDebt_Movement { set; get; }
        }
        public class AccountOperationsGrd
        {
            public int OperationID { set; get; }
            public float IndebtBalance { set; get; }
            public float CreditBalance { set; get; }
            public float Credit_Movement { set; get; }
            public float InDebt_Movement { set; get; }
            public DateTime OperationDate { set; get; }
            public string Description { set; get; }
            public string State { set; get; }
        }
        public List<AccountOperationsGrd> AccountOperations_List;
        public SubAccount Sub_Account;
        public Entry Khazna_Moved;


        public List<Treasury> Treasury_List;
        public List<Entry> Khazna_Moved_List;

        AccountOperationsGrd Account_Operations;
        List<AccountOperationsGrd> Account_Operations_List;
        List<Entry> Account_Mony_Operations_List;
        public SubAccount Account;
        static float sum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Initialize_Page();
            }
        }
        public void Initialize_Page()
        {
            Sub_Account = new SubAccount();
            FromDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            ToDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            AccountDropID.DataSource = db.SubAccount.ToList();//.GetAllSub();// GetSubAccount_ByUpID(1101);
            AccountDropID.DataTextField = "name";
            AccountDropID.DataValueField = "ID";
            AccountDropID.DataBind();

        }
        public static float attemptValue = 0;
        public void Calc_Operation()
        {
            attemptValue = 0;
            Account = new SubAccount();
            Khazna_Moved_List = new List<Entry>();

            Khazna_Moved = new Entry();





            Khazna_Moved_List = db.Entry.ToList().Where(o=>o.SubAccount_id== int.Parse(AccountDropID.SelectedValue)&(
            o.Date>= ExtendedMethod.FormatDate(FromDateTxt.Text) & o.Date <= ExtendedMethod.FormatDate(ToDateTxt.Text))).ToList();// Khazna_Moved.GetEntry_ByAccountIDByDate(
               // int.Parse(AccountDropID.SelectedValue), FromDateTxt.Text, ToDateTxt.Text);



            SubAccount sub_Account = new SubAccount();
            sub_Account = db.SubAccount.ToList().FirstOrDefault(o=>o.ID== int.Parse(AccountDropID.SelectedValue));
         //   sub_Account.GetSubAccount_ByID(int.Parse(AccountDropID.SelectedValue)).ElementAtOrDefault(0);
            float Account_Sum = float.Parse(sub_Account.ABalance.ToString());
            Account_Operations = new AccountOperationsGrd();
            AccountOperations_List = new List<AccountOperationsGrd>();
            Account_Mony_Operations_List = new List<Entry>();
            Account_Operations_List = new List<AccountOperationsGrd>();
            List<AccountOperationsGrd> FinalGrd = new List<AccountOperationsGrd>();
            List<AccountOperationsGrd> LastGrd = new List<AccountOperationsGrd>();

            //  DateTime.Parse(operation.Date.ToString()).Year
            //DateTime RD= DateTime.ParseExact(sub_Account.RegisterDate.ToString(), "yyyy-MM-dd", null);

            if (sub_Account.BType == "دائن")
            {
                FinalGrd.Add(new AccountOperationsGrd()
                {
                    OperationID = 0,
                    OperationDate =(DateTime) sub_Account.RegisterDate,

                    CreditBalance = float.Parse(sub_Account.ABalance.ToString()),
                    Credit_Movement = 0,
                    IndebtBalance = 0,
                    InDebt_Movement = 0,
                    Description = "رصيد افتتاحى",
                    State = sub_Account.BType

                });
            }
            else
            {
                FinalGrd.Add(new AccountOperationsGrd()
                {

                    OperationID = 0,
                    OperationDate = (DateTime)sub_Account.RegisterDate,

                    CreditBalance = 0,
                    Credit_Movement = 0,
                    IndebtBalance = float.Parse(sub_Account.ABalance.ToString()),
                    InDebt_Movement = 0,
                    Description = "رصيد افتتاحى",
                    State = sub_Account.BType
                });
            }

            //  LastGrd.Add(FinalGrd[0]);
            //كل العمليات مع الخزنة الخاصة بaccount
            foreach (var Khazna in Khazna_Moved_List)
            {

                Account_Mony_Operations_List.Add(new Entry
                {
                    ID =(int) Khazna.EntryID,
                    status = Khazna.status,
                    value = Khazna.value,
                    description = Khazna.description,
                    SubAccount_id = Khazna.SubAccount_id,
                    Date = Khazna.Date
                });


            }



            // Account_Mony_Operations_List.OrderBy(a => a.Date);


            //Concatination//////////////////////////////////////////////////////////////////////////////////////////////////////////

            ConcatenationLists(Account_Mony_Operations_List, Account_Operations_List, Account_Sum);
            //الشغل على Account_Operations_List
            int i = 1;
            Account_Operations_List = Account_Operations_List.OrderBy(o => o.OperationDate).ToList();
            attemptValue = FinalGrd[0].State == "دائن" ? FinalGrd[0].CreditBalance : FinalGrd[0].IndebtBalance;
            string CurrentState = "";
            foreach (var operation in Account_Operations_List)
            {
                //CurrentState = FinalGrd[i - 1].State == "مدين" ? Math.Sign(attemptValue + operation.InDebt_Movement - operation.Credit_Movement) >= 0 ? "مدين" : "دائن" :
                //     Math.Sign(attemptValue +  operation.Credit_Movement- operation.InDebt_Movement) >= 0 ? "دائن" : "مدين";

                if (FinalGrd[i - 1].State == "مدين")
                {
                    if (operation.InDebt_Movement > 0)
                    {
                        attemptValue = Math.Abs(attemptValue + operation.InDebt_Movement - operation.Credit_Movement);
                        if (attemptValue > 0)
                        {
                            CurrentState = "مدين";
                        }
                        else
                        {
                            CurrentState = "دائن";
                        }
                    }
                    else
                    {
                        attemptValue = (attemptValue - operation.Credit_Movement + operation.InDebt_Movement);
                        if (attemptValue > 0)
                        {
                            CurrentState = "مدين";
                        }
                        else
                        {
                            CurrentState = "دائن";
                            attemptValue = -attemptValue;
                        }
                    }
                }
                else
                {
                    if (operation.Credit_Movement > 0)
                    {
                        attemptValue = Math.Abs(attemptValue + operation.Credit_Movement - operation.InDebt_Movement);
                        if (attemptValue > 0)
                        {
                            CurrentState = "دائن";
                        }
                        else
                        {
                            CurrentState = "مدين";
                        }
                    }
                    else
                    {
                        attemptValue = (attemptValue - operation.InDebt_Movement + operation.Credit_Movement);
                        if (attemptValue > 0)
                        {
                            CurrentState = "دائن";
                        }
                        else
                        {
                            CurrentState = "مدين";
                            attemptValue = -attemptValue;
                        }
                    }
                }
                if (CurrentState == "دائن")
                {


                    FinalGrd.Add(new AccountOperationsGrd()
                    {
                        CreditBalance = attemptValue,
                        Credit_Movement = operation.Credit_Movement,
                        IndebtBalance = operation.IndebtBalance,
                        InDebt_Movement = operation.InDebt_Movement,
                        Description = operation.Description,
                        OperationDate = operation.OperationDate,
                        OperationID = operation.OperationID,
                        State = "دائن"
                    });
                }
                else
                {
                    //if (operation.InDebt_Movement  > 0)
                    //{
                    //    attemptValue = Math.Abs(attemptValue + operation.InDebt_Movement - operation.Credit_Movement);
                    //}
                    //else
                    //{
                    //    attemptValue = Math.Abs(attemptValue + operation.Credit_Movement - operation.InDebt_Movement);
                    //}

                    FinalGrd.Add(new AccountOperationsGrd()
                    {
                        CreditBalance = operation.CreditBalance,
                        Credit_Movement = operation.Credit_Movement,
                        IndebtBalance = attemptValue,// operation.IndebtBalance,
                        InDebt_Movement = operation.InDebt_Movement,
                        Description = operation.Description,
                        OperationDate = operation.OperationDate,
                        OperationID = operation.OperationID,
                        State = "مدين"
                    });
                }



                i++;
            }



            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Account_Operations_List = Account_Operations_List.OrderBy(o => o.OperationDate).ToList();
            string fromdate = ReformateDateFromPicker(FromDateTxt.Text); //DateTime.Parse(FromDateTxt.Text).Year + "-" + DateTime.Parse(FromDateTxt.Text).Month + "-" + DateTime.Parse(FromDateTxt.Text).Day;
            string Todate = ReformateDateFromPicker(ToDateTxt.Text); //DateTime.Parse(ToDateTxt.Text).Year + "-" + DateTime.Parse(ToDateTxt.Text).Month + "-" + DateTime.Parse(ToDateTxt.Text).Day;
            LastGrd.Add(FinalGrd.Where(o => o.OperationDate < ExtendedMethod.FormatDate(fromdate)).LastOrDefault());
            if (LastGrd.Last() == null)
            {
                LastGrd.Remove(LastGrd.Last());
            }
            LastGrd.AddRange(FinalGrd.Where(o => o.OperationDate >= ExtendedMethod.FormatDate(fromdate) & o.OperationDate <= ExtendedMethod.FormatDate(Todate)));

            AccountGrd.DataSource = LastGrd;// FinalGrd.Where(o => o.OperationDate < DateTime.Parse(fromdate)).LastOrDefault() && o.OperationDate >= DateTime.Parse(fromdate) & o.OperationDate <= DateTime.Parse(Todate) ); //Account_Operations_List.Where(o=>o.OperationDate >= DateTime.Parse(FromDateTxt.Text) &  o.OperationDate <= DateTime.Parse(ToDateTxt.Text));
            AccountGrd.DataBind();


        }
        public void ConcatenationLists(List<Entry> Account_Mony_Operations_List, List<AccountOperationsGrd> Account_Operations_List, float Account_Sum)
        {
            // int i = 0;
            //   float sum = 0;

            foreach (var operation in Account_Mony_Operations_List)
            {
                string date = DateTime.Parse(operation.Date.ToString()).Year + "-" + DateTime.Parse(operation.Date.ToString()).Month + "-" + DateTime.Parse(operation.Date.ToString()).Day;
                // sum = Account_Sum > float.Parse(operation.Value.ToString()) ? Account_Sum - float.Parse(operation.Value.ToString()) : float.Parse(operation.Value.ToString()) - Account_Sum;

                // Account_Sum
                if (operation.status == "دائن")//مستلم  العميل دائن
                {
                    Account_Operations_List.Add(new AccountOperationsGrd()
                    {
                        OperationID = operation.ID,
                        OperationDate =ExtendedMethod.FormatDate(date),
                        Description = operation.description,
                        CreditBalance = 0,
                        IndebtBalance = 0,
                        Credit_Movement = float.Parse(operation.value.ToString()),
                        InDebt_Movement = 0,


                    });
                }
                else//مصروف  العميل مدين
                {
                    Account_Operations_List.Add(new AccountOperationsGrd()
                    {
                        OperationID = operation.ID,
                        OperationDate = ExtendedMethod.FormatDate(date),
                        Description = operation.description,
                        CreditBalance = 0,
                        IndebtBalance = 0,
                        Credit_Movement = 0,
                        InDebt_Movement = float.Parse(operation.value.ToString()),

                    });
                }

            }
            Account_Operations_List = Account_Operations_List.OrderBy(o => o.OperationDate).ToList();

        }
        public static string ReformateDateFromPicker(string date)
        {
            var alldate = date.Split('-');
            return alldate[0] + "-" + alldate[1] + "-" + alldate[2];
        }
        protected void SaerchBtn_Click(object sender, EventArgs e)
        {

            Calc_Operation();


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=حساب استاذ.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    AccountGrd.RenderControl(hw);
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