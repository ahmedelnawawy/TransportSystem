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
    public partial class AssistantOstazAccount : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public class AccountOperationsGrd
        {
            public int OperationID { set; get; }
            public float IndebtBalance { set; get; }
            public float CreditBalance { set; get; }
            public float Credit_Movement { set; get; }
            public float InDebt_Movement { set; get; }
            public DateTime OperationDate { set; get; }
            public string Description { set; get; }
            public float ABalance { set; get; }
            public float LastBalance { set; get; }
            public bool state { set; get; }
            public string rstate { set; get; }

        }
        public List<AccountOperationsGrd> AccountOperations_List;
        public SubAccount Sub_Account;
        public Entry Khazna_Moved;

        public List<Entry> Khazna_Moved_List;

        AccountOperationsGrd Account_Operations;
        List<AccountOperationsGrd> Account_Operations_List;
        List<Entry> Account_Mony_Operations_List;

        public SubAccount Account;
        public MainAccount mainaccount = new MainAccount();
        List<AccountOperationsGrd> FinalGrd1 = new List<AccountOperationsGrd>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Initialize_Page();
            }
        }
        public static List<MainAccount> MainAccountList = new List<MainAccount>();
        public static List<SubAccount> SubAccountList = new List<SubAccount>();
        public static List<AccountsContainer> AllAccountList = new List<AccountsContainer>();

        public class AccountsContainer {
            public Int64 ID { set; get; }
            public string name { set; get; }
        }
        public void FillDrops() {
           

            //MainAccountList = db.MainAccount.Where(o => o.ID.ToString().StartsWith(Dropdownlist1.SelectedValue)&o.ID!=Int64.Parse(Dropdownlist1.SelectedValue)).ToList();
            //SubAccountList = db.SubAccount.Where(o => o.ID.ToString().StartsWith(Dropdownlist1.SelectedValue)).ToList();
            //foreach (var m in MainAccountList)
            //{
            //    AllAccountList.Add(new AssistantOstazAccount.AccountsContainer() { ID = m.ID, name = m.name });
            //}
            //foreach (var s in SubAccountList)
            //{
            //    AllAccountList.Add(new AssistantOstazAccount.AccountsContainer() { ID = s.ID, name = s.name });
            //}
            AccountDropID.DataSource = db.SubAccount.ToList().Where(o=>o.ID.ToString().StartsWith(Dropdownlist1.SelectedValue)).ToList();
            AccountDropID.DataTextField = "name";
            AccountDropID.DataValueField = "ID";
            AccountDropID.DataBind();

        }
        public void Initialize_Page()
        {
            Sub_Account = new SubAccount();
            ToDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            Dropdownlist1.DataSource = db.MainAccount.ToList();// mainaccount.GetAllMain();
            Dropdownlist1.DataTextField = "name";
            Dropdownlist1.DataValueField = "ID";
            Dropdownlist1.DataBind();
            FillDrops();
        }
        public void Calc_Operation()
        {
            Account = new SubAccount();
            Khazna_Moved_List = new List<Entry>();

            Khazna_Moved = new Entry();


            Khazna_Moved_List = db.Entry.ToList().Where(o=>o.SubAccount_id== int.Parse(AccountDropID.SelectedValue)&
           o.Date<= ExtendedMethod.FormatDate(ToDateTxt.Text) ).ToList();
            //Khazna_Moved.GetEntry_ByAccountIDByDate(int.Parse(AccountDropID.SelectedValue), ToDateTxt.Text);


            SubAccount sub_Account = new SubAccount();
            sub_Account = db.SubAccount.ToList().FirstOrDefault(o => o.ID == int.Parse(AccountDropID.SelectedValue)); 
               // sub_Account.GetSubAccount_ByID(int.Parse(AccountDropID.SelectedValue)).ElementAtOrDefault(0);
            float Account_Sum = float.Parse(sub_Account.ABalance.ToString());
            Account_Operations = new AccountOperationsGrd();
            AccountOperations_List = new List<AccountOperationsGrd>();
            Account_Mony_Operations_List = new List<Entry>();
            Account_Operations_List = new List<AccountOperationsGrd>();
            List<AccountOperationsGrd> FinalGrd = new List<AccountOperationsGrd>();

            //  DateTime.Parse(operation.Date.ToString()).Year
            //DateTime RD= DateTime.ParseExact(sub_Account.RegisterDate.ToString(), "yyyy-MM-dd", null);
            if (sub_Account.BType == "مدين")
            {
                FinalGrd.Add(new AccountOperationsGrd()
                {
                    OperationID = 0,
                    OperationDate =(DateTime)sub_Account.RegisterDate,

                    CreditBalance = 0,
                    Credit_Movement = 0,
                    IndebtBalance = float.Parse(sub_Account.ABalance.ToString()),
                    //InDebt_Movement = float.Parse(sub_Account.ABalance.ToString()),
                    Description = "رصيد افتتاحى",

                });
            }
            else
            {
                FinalGrd.Add(new AccountOperationsGrd()
                {
                    OperationID = 0,
                    OperationDate =(DateTime) sub_Account.RegisterDate,

                    CreditBalance = 0,
                    Credit_Movement = 0,
                    IndebtBalance = -float.Parse(sub_Account.ABalance.ToString()),
                    //InDebt_Movement = float.Parse(sub_Account.ABalance.ToString()),
                    Description = "رصيد افتتاحى",

                });
            }



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


            Account_Mony_Operations_List.OrderByDescending(a => a.Date);

            //Concatination
            foreach (var operation in Account_Mony_Operations_List)
            {
                string date = DateTime.Parse(operation.Date.ToString()).Year + "-" + DateTime.Parse(operation.Date.ToString()).Month + "-" + DateTime.Parse(operation.Date.ToString()).Day;
                float sum = Account_Sum > float.Parse(operation.value.ToString()) ? Account_Sum - float.Parse(operation.value.ToString()) : float.Parse(operation.value.ToString()) - Account_Sum;

                // Account_Sum
                if (operation.status == "دائن")//مستلم 
                {
                    Account_Operations_List.Add(new AccountOperationsGrd()
                    {
                        OperationID = operation.ID,
                        OperationDate = ExtendedMethod.FormatDate( date),
                        Description = operation.description,
                        CreditBalance = sum,
                        IndebtBalance = 0,
                        Credit_Movement = float.Parse(operation.value.ToString()),
                        InDebt_Movement = 0,
                        state = false,
                        ABalance = float.Parse(sub_Account.ABalance.ToString())



                    });
                }
                else//مصروف
                {
                    Account_Operations_List.Add(new AccountOperationsGrd()
                    {
                        OperationID = operation.ID,
                        OperationDate = ExtendedMethod.FormatDate(date),
                        Description = operation.description,
                        CreditBalance = 0,
                        IndebtBalance = sum,
                        Credit_Movement = 0,
                        InDebt_Movement = float.Parse(operation.value.ToString()),
                        state = true,
                        ABalance = float.Parse(sub_Account.ABalance.ToString())

                    });
                }
            }
            Account_Operations_List = Account_Operations_List.OrderBy(o => o.OperationDate).ToList();
            string Todate = ReformateDateFromPicker(ToDateTxt.Text); //DateTime.Parse(ToDateTxt.Text).Year + "-" + DateTime.Parse(ToDateTxt.Text).Month + "-" + DateTime.Parse(ToDateTxt.Text).Day;

            FinalGrd.AddRange(Account_Operations_List.Where(o => o.OperationDate <= ExtendedMethod.FormatDate(Todate)));
            ////FinalGrd.AddRange(Account_Operations_List.Where(o => o.OperationDate >= DateTime.Parse(fromdate) & o.OperationDate <= DateTime.Parse(Todate)));
            //FinalGrd.Add(new AccountOperationsGrd()
            //{
            //    CreditBalance = FinalGrd.Sum(g => g.CreditBalance),
            //    Credit_Movement = FinalGrd.Sum(g => g.Credit_Movement),
            //    IndebtBalance = FinalGrd.Sum(g => g.IndebtBalance),
            //    InDebt_Movement = FinalGrd.Sum(g => g.InDebt_Movement),
            //    OperationID = 0,
            //    Description = "",
            //    OperationDate = FinalGrd.LastOrDefault().OperationDate
            //});

            float lastBalance = FinalGrd.FirstOrDefault().state == true ? (FinalGrd.FirstOrDefault().CreditBalance) + (FinalGrd.Sum(c => c.Credit_Movement)) - (FinalGrd.Sum(c => c.InDebt_Movement)) :
                (FinalGrd.FirstOrDefault().IndebtBalance) + (FinalGrd.Sum(c => c.InDebt_Movement)) - (FinalGrd.Sum(c => c.Credit_Movement));
            if (sub_Account.BType == "مدين")
            {
                Account_Operations = new AccountOperationsGrd()
                {
                    CreditBalance = FinalGrd.Sum(g => g.CreditBalance),
                    Credit_Movement = FinalGrd.Sum(g => g.Credit_Movement),
                    IndebtBalance = FinalGrd.Sum(g => g.IndebtBalance),
                    InDebt_Movement = FinalGrd.Sum(g => g.InDebt_Movement),
                    OperationID = 0,
                    Description = AccountDropID.SelectedItem.Text,
                    OperationDate =FinalGrd.LastOrDefault().OperationDate,
                    ABalance = float.Parse(sub_Account.ABalance.ToString()),
                    LastBalance = lastBalance

                };
            }
            else
            {
                Account_Operations = new AccountOperationsGrd()
                {
                    CreditBalance = FinalGrd.Sum(g => g.CreditBalance),
                    Credit_Movement = FinalGrd.Sum(g => g.Credit_Movement),
                    IndebtBalance = FinalGrd.Sum(g => g.IndebtBalance),
                    InDebt_Movement = FinalGrd.Sum(g => g.InDebt_Movement),
                    OperationID = 0,
                    Description = AccountDropID.SelectedItem.Text,
                    OperationDate = FinalGrd.LastOrDefault().OperationDate,
                    ABalance = -float.Parse(sub_Account.ABalance.ToString()),
                    LastBalance = lastBalance

                };
            }

            FinalGrd.Clear();
            FinalGrd.Add(Account_Operations);
            FinalGrd1.Add(Account_Operations);
            AccountGrd.DataSource = FinalGrd1; //Account_Operations_List.Where(o=>o.OperationDate >= DateTime.Parse(FromDateTxt.Text) &  o.OperationDate <= DateTime.Parse(ToDateTxt.Text));
            AccountGrd.DataBind();


        }
        public static string ReformateDateFromPicker(string date)
        {
            var alldate = date.Split('-');
            return alldate[0] + "-" + alldate[1] + "-" + alldate[2];
        }

        protected void SaerchBtn_Click(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                try
                {
                    AccountDropID.SelectedIndex = 0;
                    for (int i = 0; i <= AccountDropID.Items.Count - 1; i++)
                    {
                        Calc_Operation();
                        if (AccountDropID.SelectedIndex != AccountDropID.Items.Count - 1)
                        {
                            AccountDropID.SelectedIndex += 1;
                        }

                    }
                }
                catch (Exception ex) {
                    RsltTxt.Text = "ازل اختيار الكل لانه لا يوجد حسابات تحت خذا الحساب";
                    RsltTxt.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                Calc_Operation();
            }

            if (AccountGrd.Rows.Count > 0)
            {
                AccountGrd.FooterRow.Cells[0].Text = "اجماليات";
                AccountGrd.FooterRow.Cells[1].Text = FinalGrd1.Sum(c => c.ABalance).ToString();
                AccountGrd.FooterRow.Cells[2].Text = FinalGrd1.Sum(c => c.InDebt_Movement).ToString();
                AccountGrd.FooterRow.Cells[3].Text = FinalGrd1.Sum(c => c.Credit_Movement).ToString();
                AccountGrd.FooterRow.Cells[4].Text = FinalGrd1.Sum(c => c.LastBalance).ToString();

            }

        }

        protected void Dropdownlist1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                //Sub_Account = new SubAccount();
                //AccountDropID.DataSource = db.SubAccount.ToList().Where(s => s.MainAccount_id == int.Parse(Dropdownlist1.SelectedValue)).ToList(); 
                //    //Sub_Account.GetSubAccount_ByUpID(int.Parse(Dropdownlist1.SelectedValue));
                //AccountDropID.DataTextField = "name";
                //AccountDropID.DataValueField = "ID";
                //AccountDropID.DataBind();
                FillDrops();
            }
            catch (Exception ex)
            {

            };
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=كشف حساب استاذ مساعد.xls");
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