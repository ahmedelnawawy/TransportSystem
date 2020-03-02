using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.GeneralAccounts
{
    public partial class PurshasesEntry : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public Entry entry;
        public List<Entry> Entry_List;
        public List<PurchaseInvoice> LSales = new List<PurchaseInvoice>();
        public PurchaseInvoice Sale = new PurchaseInvoice();
        public class SalesResults
        {
            public string moveID { set; get; }
            public decimal SValue { set; get; }
            public bool State { set; get; }
            public int KeadNo { set; get; }
            public int subAccountID { set; get; }//رقم الحساب

        }
        public static int FalseCounter = 0;

        public static List<SalesResults> Result_lst = new List<SalesResults>();
        public SalesResults Result = new SalesResults();
        public static bool EditFlag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FromDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                EditFlag = false;
                FalseCounter = 0;
                Result_lst.Clear();
                TaxFlag = false;
                OldKeadID = 0;

            }
        }
        protected void SaerchBtn_Click(object sender, EventArgs e)
        {
            FromDateTxt.Text = ReformateDateFromPicker(FromDateTxt.Text);
            FalseCounter = 0;
            TaxFlag = false;
            OldKeadID = 0;

            entry = new Entry();
            Entry_List = new List<Entry>();

            Result_lst.Clear();
            LSales = db.PurchaseInvoice.ToList().Where(o => o.InvoiceDate <= ExtendedMethod.FormatDate(FromDateTxt.Text) &
            o.PurchaseType == bool.Parse(BillTypeDrop.SelectedValue)
                 ).ToList();
          //  Sale.GetAllPurchaseInvoiceInDate(DateTime.Parse(FromDateTxt.Text,
            //    CultureInfo.CreateSpecificCulture("ar-EG")).ToString(),
              //  bool.Parse(BillTypeDrop.SelectedValue));//Sale.GetAllPurchaseInvoice(FromDateTxt.Text, FromDateTxt .Text,BillTypeDrop.SelectedValue);
            foreach (var tt in LSales)
            {
                if (tt.KeadNo == 0)
                {
                    FalseCounter++;
                }
                if (tt.PurchaseType ==true)
                {
                    if ( tt.KeadNo != 0)
                    {
                            TaxFlag = true;
                            OldKeadID =(int) tt.KeadNo;

                       entry= db.Entry.FirstOrDefault(o => o.EntryID== int.Parse(OldKeadID.ToString()));
                            //entry = entry.GetAllEntryByID(int.Parse(OldKeadID.ToString())).FirstOrDefault();
                            Entry_List.Add(entry);
                        db.Entry.RemoveRange(Entry_List);
                        //  entry.Operations("Delete", Entry_List);
                        db.SaveChanges();

                    }

                    Result_lst.Add(new SalesResults
                    {
                        moveID =tt.Id,
                        SValue =decimal.Parse(tt.Total.ToString()),
                        KeadNo =(int) tt.KeadNo,
                        subAccountID = int.Parse(tt.SubAccountId.ToString())
                    });
                }
                else
                {
                    if (tt.KeadNo != 0)
                    {
                            TaxFlag = true;
                            OldKeadID =(int) tt.KeadNo;
                        entry = db.Entry.FirstOrDefault(o=>o.EntryID== int.Parse(OldKeadID.ToString()));// entry.GetAllEntryByID(int.Parse(OldKeadID.ToString())).FirstOrDefault();
                            Entry_List.Add(entry);
                        //entry.Operations("Delete", Entry_List);
                        db.Entry.RemoveRange(Entry_List);
                        db.SaveChanges();
                    }
                    // SNet = tt.Value_LE+tt.Tax_Added-tt.CommercialTax, SValue =tt.Value_LE
                    Result_lst.Add(new SalesResults
                    {
                        moveID = tt.Id,
                        SValue = (decimal)tt.Total,
                        KeadNo =(int) tt.KeadNo,
                        subAccountID = int.Parse(tt.SubAccountId.ToString())
                    });
                }

            }
            AccountGrd.DataSource = Result_lst;
            AccountGrd.DataBind();
            if (Result_lst.Count > 0)
            {
                AccountGrd.FooterRow.Cells[0].Text = "اجماليات";
                AccountGrd.FooterRow.Cells[1].Text = Result_lst.Sum(c => c.SValue).ToString("#.00");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(),
                    "تنبيه", "alert('لا يوجد نتائج');", true);

            }
        }
        public static string ReformateDateFromPicker(string date)
        {
            var alldate = date.Split('-');
            return alldate[0] + "-" + alldate[1] + "-" + alldate[2];
        }

        public static bool TaxFlag = false;
        public static int OldKeadID = 0;
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            FromDateTxt.Text = ReformateDateFromPicker(FromDateTxt.Text);
            entry = new Entry();
            Entry_List = new List<Entry>();
            Sale = new PurchaseInvoice();
            LSales = new List<PurchaseInvoice>();
            int RecordID = 0;
            int EntryIDInEditMode = 0;
            int MaxEntrID;
            if (!TaxFlag)
            {
                //entry.maxid();
                 MaxEntrID = (int) db.Entry.ToList().Max(o=>o.EntryID)+1;
               // MaxEntrID =MoveID;
            }
            else
            {
                MaxEntrID = OldKeadID;


            }
            decimal Mab3at = 0, Khasm = 0, khasmarba7tgary = 0, AddedValueTax = 0;

            foreach (var grdrow in Result_lst)
            {
                // entry.maxRecordID(entry.MoveID);
                 RecordID = (int) db.Entry.ToList().Where(o => o.EntryID == MaxEntrID).Max(s=>s.RecordID)+1;
                // = RecordID;

                Mab3at += grdrow.SValue;
                
                //حالة التعديل
                if (EditFlag == true && FalseCounter != Result_lst.Count)
                {
                    if (grdrow.KeadNo != 0)
                        EntryIDInEditMode = grdrow.KeadNo;
                    else
                        grdrow.KeadNo = Result_lst.ElementAtOrDefault(0).KeadNo;

                    Entry_List.Add(new Entry()
                    {
                        EntryID = grdrow.KeadNo,
                        RecordID =int.Parse( grdrow.moveID),
                        EntryType = " قيد" + BillTypeDrop.SelectedItem.Text,
                        SubAccount_id = grdrow.subAccountID,//  العميل دائن يقيمة البضاعه
                        value = grdrow.SValue ,
                        status = "دائن",
                        Date = DateTime.Parse(FromDateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG")),
                        description = "المورد دائن يقيمة البضاعه",

                    });
                }
                else {

                    Entry_List.Add(new Entry()
                    {
                        EntryID = MaxEntrID,
                        RecordID =int.Parse( grdrow.moveID),
                        EntryType = " قيد" + BillTypeDrop.SelectedItem.Text,
                        SubAccount_id = grdrow.subAccountID,//  العميل دائن يقيمة البضاعه
                        value = grdrow.SValue ,
                        status = "دائن",
                        Date = DateTime.Parse(FromDateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG")),
                        description = "المورد دائن يقيمة البضاعه",

                    });

                }
           

                RecordID++;



                Sale = db.PurchaseInvoice.FirstOrDefault(o => o.PurchaseType == bool.Parse(BillTypeDrop.SelectedValue) &
                 o.KeadNo == int.Parse(grdrow.moveID));
                 //   Sale.GetPOByOrderID(grdrow.moveID,bool.Parse( BillTypeDrop.SelectedValue));
                //  Sale.KeadNo = MaxEntrID;
                if (FalseCounter == Result_lst.Count)//جديد
                {
                    Sale.KeadNo = MaxEntrID;
                }
                else
                {
                    Sale.KeadNo = grdrow.KeadNo;

                }
                LSales.Add(Sale);
            }
            /////////////////End Loop
            if (EditFlag == true && FalseCounter != Result_lst.Count)
            {
                if (Mab3at > 0)
                    Entry_List.Add(new Entry()
                    {
                        EntryID = EntryIDInEditMode,
                        RecordID = 0,
                        EntryType = " قيد" + BillTypeDrop.SelectedItem.Text,
                        SubAccount_id = 32010002,//حساب المشتريات
                        value = Mab3at,
                        status = "مدين",
                        Date = DateTime.Parse(FromDateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG")),
                        description = "حساب المشتريات",


                    });

            }
            else {
                if (Mab3at > 0)
                    Entry_List.Add(new Entry()
                    {
                        EntryID = MaxEntrID,
                        RecordID = 0,
                        EntryType = " قيد" + BillTypeDrop.SelectedItem.Text,
                        SubAccount_id = 32010002,//حساب المبيعات
                        value = Mab3at,
                        status = "مدين",
                        Date = DateTime.Parse(FromDateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG")),
                        description = "حساب المشتريات",


                    });
            }
            
            
            ///////////////////////




            if (EditFlag == true && FalseCounter != Result_lst.Count)
            {
                foreach(var en in Entry_List)
                {
                    en.LoginID = ExtendedMethod.LoginedUser.Id;
                    db.Entry.AddOrUpdate();
                }
                db.SaveChanges();
              //  entry.Operations("Edit", Entry_List);
                addErrorTxt.Text = "تم الحفظ بنجاح";

            }
            else
            {
                db.Entry.AddRange(Entry_List);
                db.SaveChanges();
              //  entry.Operations("Add", Entry_List);
                addErrorTxt.Text = "تمت الاضافة بنجاح";
            }
            addErrorTxt.ForeColor = System.Drawing.Color.Green;
            foreach(var s in LSales)
            {
               // s.UserID = ExtendedMethod.LoginedUser.Id;
                db.PurchaseInvoice.AddOrUpdate(s);
            }
            db.SaveChanges();
          //  Sale.Operations("Edit", LSales);
            AccountGrd.DataSource = null;
            AccountGrd.DataBind();

            FromDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            BillTypeDrop.SelectedIndex = 0;
        }
    }
}