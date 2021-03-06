﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.Reports
{
    public partial class SupplierOperationsView : System.Web.UI.Page
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
        public KhznaMoved Khazna_Moved;
        public BankMoved Bank_Moved;
        //public List<SaleOrders> Sales_Order_List;
        // public SaleOrders Sale_Order;
        public PurchaseInvoice Purchase_Invoice;
        public List<PurchaseInvoice> Purchase_Invoice_List;

        public List<Treasury> Treasury_List;
        public List<KhznaMoved> Khazna_Moved_List;
        public List<BankMoved> Bank_Moved_List;
        AccountOperationsGrd Account_Operations;
        List<AccountOperationsGrd> Account_Operations_List;
        List<KhznaMoved> Account_Mony_Operations_List;
        public SubAccount Account;
        

        static float sum = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Initialize_Page();
            }

        }
        public static string ReformateDateFromPicker(string date)
        {
            var alldate = date.Split('-');
            return alldate[0] + "-" + alldate[1] + "-" + alldate[2];
        }

        public void Initialize_Page()
        {
            Sub_Account = new SubAccount();
            FromDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            ToDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            AccountDropID.DataSource = db.SubAccount.ToList().Where(s => s.MainAccount_id == 2103).ToList(); //Sub_Account.GetSubAccount_ByUpID(2103);
            AccountDropID.DataTextField = "name";
            AccountDropID.DataValueField = "ID";
            AccountDropID.DataBind();
            SuppliersOperationsGrd.Visible = false;

        }
        public static float attemptValue = 0;
        
        public void Calc_Operation()
        {
            attemptValue = 0;
            Account = new SubAccount();
            Khazna_Moved_List = new List<KhznaMoved>();
            Bank_Moved_List = new List<BankMoved>();
            Purchase_Invoice_List = new List<PurchaseInvoice>();
            Khazna_Moved = new KhznaMoved();
            Bank_Moved = new BankMoved();
            Purchase_Invoice = new PurchaseInvoice();

            // Khazna_Moved_List = Khazna_Moved.GetKhznaMoved_ByAccountIDByDate(int.Parse(AccountDropID.SelectedValue),FromDateTxt.Text,ToDateTxt.Text);
            // Bank_Moved_List = Bank_Moved.BankMoved_Between_Date_ForAccount(FromDateTxt.Text,ToDateTxt.Text, int.Parse(AccountDropID.SelectedValue));

            Khazna_Moved_List = db.KhznaMoved.ToList().Where(o => o.AccountID == int.Parse(AccountDropID.SelectedValue)).ToList();
            // Khazna_Moved_List = Khazna_Moved.GetKhznaMoved_ByAccountID(int.Parse(AccountDropID.SelectedValue));

            Bank_Moved_List = db.BankMoved.ToList().Where(o => o.AccountID == int.Parse(AccountDropID.SelectedValue)).ToList();
            Purchase_Invoice_List = db.PurchaseInvoice.ToList().Where(p => p.SubAccountId == int.Parse(AccountDropID.SelectedValue)).ToList();
                //Purchase_Invoice.GetPurchaseInvoiceBySubID(int.Parse(AccountDropID.SelectedValue));


            SubAccount sub_Account = new SubAccount();
            sub_Account = db.SubAccount.ToList().FirstOrDefault(s => s.ID == int.Parse(AccountDropID.SelectedValue));//  sub_Account.GetSubAccount_ByID(int.Parse(AccountDropID.SelectedValue)).ElementAtOrDefault(0);
            float Account_Sum = float.Parse(sub_Account.ABalance.ToString());
            Account_Operations = new AccountOperationsGrd();
            AccountOperations_List = new List<AccountOperationsGrd>();
            Account_Mony_Operations_List = new List<KhznaMoved>();
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

                Account_Mony_Operations_List.Add(new KhznaMoved
                {
                    ID = Khazna.ID,
                    state = Khazna.state == true ? false : true,
                    Value = Khazna.Value,
                    Description = Khazna.Description,
                    AccountID = Khazna.AccountID,
                    Date = Khazna.Date
                });


            }

            //كل العمليات الواقعة على التعاملات البنكية 
            foreach (var bank in Bank_Moved_List)
            {
                Account_Mony_Operations_List.Add(new KhznaMoved
                {
                    ID = bank.ID,
                    state = bank.state == true ? false : true,
                    Value = bank.Value,
                    Description = bank.Description,
                    AccountID = bank.AccountID,
                    Date = bank.Date
                });
            }


            //فواتير الشراء 
            foreach (var Pur in Purchase_Invoice_List)
            {
                Account_Mony_Operations_List.Add(new KhznaMoved
                {
                    ID =int.Parse(Pur.Id),
                    state =(bool) Pur.PurchaseType,
                    Value =decimal.Parse(Pur.Total.ToString()),
                    Description = Pur.PurchaseType==true?"شراء":"مرتجع شراء",
                    AccountID = Pur.SubAccountId,
                    Date =  DateTime.Parse(Pur.InvoiceDate.ToString(), CultureInfo.CreateSpecificCulture("ar-EG"))
                });
            }
            List<TransportCommand> TransportComandList = new List<TransportCommand>();
            TransportComandList = db.TransportCommand.ToList();
            //اوامر النقل
            foreach (var Pur in TransportComandList)
            {
                Account_Mony_Operations_List.Add(new KhznaMoved
                {
                    ID = Pur.Id,
                    state = true,
                    Value = decimal.Parse(Pur.TotalTransportPrice.ToString()),
                    Description = "امر نقل",
                    AccountID = Pur.SubAccVendorId,
                    Date = ExtendedMethod.FormatDate(ExtendedMethod.ParseDateToString( DateTime.Parse(Pur.TransportCommandTime.ToString())))
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


            string fromdate = ReformateDateFromPicker(FromDateTxt.Text);// DateTime.Parse(FromDateTxt.Text).Year + "-" + DateTime.Parse(FromDateTxt.Text).Month + "-" + DateTime.Parse(FromDateTxt.Text).Day;
            string Todate = ReformateDateFromPicker(ToDateTxt.Text);//DateTime.Parse(ToDateTxt.Text).Year + "-" + DateTime.Parse(ToDateTxt.Text).Month + "-" + DateTime.Parse(ToDateTxt.Text).Day;
            var mydate = DateTime.Parse(fromdate, CultureInfo.CreateSpecificCulture("ar-EG"));
            var mydateTo = DateTime.Parse(Todate, CultureInfo.CreateSpecificCulture("ar-EG"));

            LastGrd.Add(FinalGrd.Where(o => o.OperationDate < mydate).LastOrDefault());
            if (LastGrd.Last() == null)
            {
                LastGrd.Remove(LastGrd.Last());
            }
            LastGrd.AddRange(FinalGrd.Where(o => o.OperationDate >= mydate & o.OperationDate <= mydateTo));
            AccountGrd.DataSource = LastGrd;// FinalGrd.Where(o => o.OperationDate < DateTime.Parse(fromdate)).LastOrDefault() && o.OperationDate >= DateTime.Parse(fromdate) & o.OperationDate <= DateTime.Parse(Todate) ); //Account_Operations_List.Where(o=>o.OperationDate >= DateTime.Parse(FromDateTxt.Text) &  o.OperationDate <= DateTime.Parse(ToDateTxt.Text));
            AccountGrd.DataBind();

            GridView1.DataSource = LastGrd;
            GridView1.DataBind();
            Textbox1.Text = FromDateTxt.Text;
            Textbox2.Text = ToDateTxt.Text;
            Dropdownlist1.Text = AccountDropID.SelectedItem.Text;
        }
        public void ConcatenationLists(List<KhznaMoved> Account_Mony_Operations_List, List<AccountOperationsGrd> Account_Operations_List, float Account_Sum)
        {
            // int i = 0;
            //   float sum = 0;

            foreach (var operation in Account_Mony_Operations_List)
            {
                string date = DateTime.Parse(operation.Date.ToString()).Year + "-" + DateTime.Parse(operation.Date.ToString()).Month + "-" + DateTime.Parse(operation.Date.ToString()).Day;
                // sum = Account_Sum > float.Parse(operation.Value.ToString()) ? Account_Sum - float.Parse(operation.Value.ToString()) : float.Parse(operation.Value.ToString()) - Account_Sum;

                // Account_Sum
                if (operation.state == true)//مستلم  المورد دائن
                {
                    Account_Operations_List.Add(new AccountOperationsGrd()
                    {
                        OperationID = operation.ID,
                        OperationDate = DateTime.Parse(date, CultureInfo.CreateSpecificCulture("ar-EG")),
                        Description = operation.Description,
                        CreditBalance = 0,
                        IndebtBalance = 0,
                        Credit_Movement = float.Parse(operation.Value.ToString()),
                        InDebt_Movement = 0,


                    });
                }
                else//مصروف  المورد مدين
                {
                    Account_Operations_List.Add(new AccountOperationsGrd()
                    {
                        OperationID = operation.ID,
                        OperationDate = DateTime.Parse(date, CultureInfo.CreateSpecificCulture("ar-EG")),
                        Description = operation.Description,
                        CreditBalance = 0,
                        IndebtBalance = 0,
                        Credit_Movement = 0,
                        InDebt_Movement = float.Parse(operation.Value.ToString()),

                    });
                }

            }
            Account_Operations_List = Account_Operations_List.OrderBy(o => o.OperationDate).ToList();

        }

        protected void SaerchBtn_Click(object sender, EventArgs e)
        {

            Calc_Operation();


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=كشف حساب مورد.xls");
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