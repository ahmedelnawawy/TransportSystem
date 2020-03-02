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
using static AdsSysWeb.Models.LmsaEntitiesDB;

namespace TransportSystems.Views.DailyMovements
{
    public partial class MonyOut : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<Driver> DriverList = new List<Driver>();
        Driver myDriver = new Driver();

        public static List<Cars> CarsList = new List<Cars>();
        Cars myCars = new Cars();
        //
        Solar MySolar;
        List<Solar> SolarList;
        ///////
        KhznaMoved Khzna_Moved;
        SubAccount Sub_Account;
        BankMoved Bank_Moved;
        List<KhznaMoved> Khzna_Moved_List;
        List<BankMoved> Bank_Moved_List;
        Entry Khzna = new Entry();
        List<Entry> KhznaL = new List<Entry >();
        List<Entry> BankL = new List<Entry  >();
        Entry Bank = new Entry();


        public static float OldValue;
        public static Entry OldEntry;
        public static string OldCheq;
        public static string OldBankName;
        public static string OldDate = "";
        public static string OldDescription;
        public static int OldKhznaID;
        public static int OldBankID;
        static bool EditFlag = false;
        static List<KhznaMoved> Khzna_Moved_List_static;
        static List<BankMoved> Bank_Moved_List_static;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initializ_Page();
            }
        }

        public void ClearSolarDiv()
        {
            SolarTypeDrop.DataSource = db.Services.ToList().Where(o => o.Id == 5 || o.Id == 6).ToList();
            SolarTypeDrop.DataTextField = "Name";
            SolarTypeDrop.DataValueField = "Id";
            SolarTypeDrop.DataBind();
            SolarRowID.Visible = false;
           // SolarRowID.Style.Add("display", "none");
            SolarTotalTxt.Text = "0";
            SolarQtyTxt.Text = "";
            CurrentReadTxt.Text = "";
            LastReadTxt.Text = "";
            AverageTxt.Text = "";
            DistanceTxt.Text = "";
            Notes.InnerText = "";

        }

        //سعر الخدمة
        public decimal GetServicePrice(int ServiceID)
        {
            Decimal ServicePrice =Math.Round(decimal.Parse(db.Services.ToList().FirstOrDefault(s => s.Id == ServiceID).price.ToString()), 2);
            return ServicePrice;

        }
        //المبلغ/سعر الخدمة
        public void SolarQtyEquation()
        {
            decimal SolarTotal =Math.Round(decimal.Parse(SolarTotalTxt.Text),2);
            int serviceID =int.Parse( SolarTypeDrop.SelectedValue);
            if (SolarTotal > 1)
            {
                SolarQtyTxt.Text = (Math.Round(SolarTotal / GetServicePrice(serviceID), 2)).ToString();
            }   
        } 
        //المسافة المقطوعة=القراءة الحالية-القراءة السابقة
        public void FinishedDistanceEquation()
        {
            decimal CurrentReading = Math.Round(decimal.Parse(CurrentReadTxt.Text), 2);
            decimal LastReading = Math.Round(decimal.Parse(LastReadTxt.Text), 2);
            decimal Distance = Math.Round(CurrentReading - LastReading);
            DistanceTxt.Text = Math.Round(Distance, 2).ToString();
        }
        //المعدل=المسافة المقطوعة/كمية السولار
        public void AverageEquation()
        {
            if (decimal.Parse(SolarTotalTxt.Text) > 1)
            {
                decimal Distance = decimal.Parse(DistanceTxt.Text);
                decimal SolarQty = decimal.Parse(SolarQtyTxt.Text);
                decimal Average =Distance/SolarQty;
                AverageTxt.Text = Math.Round(Average, 2).ToString();
            }
            else
            {
                AverageTxt.Text = "0";
            }
            }
        public void Initializ_Page()
        {
            // بخفي ال شيك بومكس بتاع مركز التكلفه خالص في الشيك
            FinancialPos_td.Visible = true;
            SolarRowID.Visible = true;
            //
            ReceivedDate.Text= DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            //
            CarsList = db.Cars.ToList();
            CarsListTxt.DataSource = CarsList;
            CarsListTxt.DataValueField = "Id";
            CarsListTxt.DataTextField = "CarNo";
            CarsListTxt.DataBind();
            //
            DriverList = db.Driver.ToList();
            DriverListTxt.DataSource = DriverList;
            DriverListTxt.DataValueField = "Id";
            DriverListTxt.DataTextField = "Name";
            DriverListTxt.DataBind();

            //financial Position initial
            FinancialPos.Checked = false;
            ClearSolarDiv();
            Khzna_Moved = new KhznaMoved();
            Sub_Account = new SubAccount();
            MonyTypeDrop.SelectedIndex = 0;
            //رقم الاذن
            int MoveID = db.KhznaMoved.ToList().Where(o => o.state == true).Max(k => k.ID) + 1;// Khzna_Moved.maxid(0);
            //OperationID.Enabled = false;
            OperationID.Text = MoveID.ToString();
            //القيمة
            Value.Text = "";
            //من
            FromSubAccountsID.DataSource = db.SubAccount.ToList().Where(o => o.MainAccount_id != 1103).ToList();// Sub_Account.GetAllSubNotKhazna();//.Select(c=>c.name);
            FromSubAccountsID.DataValueField = "ID";
            FromSubAccountsID.DataTextField = "name";
            FromSubAccountsID.DataBind();
            //الى
            FromKhaznaRow.Visible = true;
            FromKhaznaDrop.DataSource = db.SubAccount.ToList().Where(o => o.MainAccount_id == 1103).ToList();//Sub_Account.GetSubAccount_ByUpID(1103);
            FromKhaznaDrop.DataValueField = "ID";
            FromKhaznaDrop.DataTextField = "name";
            FromKhaznaDrop.DataBind();
            //الملاحظات
            DescTxt.Text = "";
            //التاريخ
            ReceivedDate.Text = ""; /*DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;*/
            ChecqRow.Visible = false;
            // BankNameRow.Visible = false;
            SarfDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            SarfDateRow.Visible = false;

            Khzna_Moved_List_static = db.KhznaMoved.ToList().Where(k => k.state == true).ToList();// Khzna_Moved.GetKhznaMovedBystate(0);
            if (Khzna_Moved_List_static.Count > 0)
            {
                MonyInSearchBtn.Enabled = true;
                MonyInsForSerachDrop.DataSource = Khzna_Moved_List_static.Select(KM => KM.ID);
                MonyInsForSerachDrop.DataBind();
            }
            else
            {
                MonyInSearchBtn.Enabled = false;
            }
            DeleteMonyInBtn.Enabled = false;
            ChequeNoTxt.Text = "";
            FromKhaznaDrop.SelectedIndex = 0;
            EditFlag = false;
            //CheckForPrivilage();
        }
        //public privilage _Privilage;
        //public Rol_PrivFT _RolePriv;

        //public void CheckForPrivilage()
        //{
        //    _Privilage = new privilage();
        //    _RolePriv = new Rol_PrivFT();
        //    _Privilage = _Privilage.GetprivilageByName("اذن صرف نقدية / شيك").FirstOrDefault();
        //    _RolePriv = _RolePriv.GetAllRol_PrivFTBY_PrivID(_Privilage.ID, LoginedUser.RolID).FirstOrDefault();
        //    if (_RolePriv != null)
        //    {
        //        if (_RolePriv.AddFlag || _RolePriv.AllFlag)
        //        {
        //            Button1.Visible = true;
        //        }
        //        else
        //        {
        //            Button1.Visible = false;
        //        }
        //        if (_RolePriv.SearchFlag || _RolePriv.AllFlag)
        //        {
        //            MonyInSearchBtn.Visible = true;
        //            FirstPurchaseBtn.Visible = true;
        //            NextPurchaseBtn.Visible = true;
        //            PrevPurchaseBtn.Visible = true;
        //            LastPurchaseBtn.Visible = true;
        //        }
        //        else
        //        {
        //            MonyInSearchBtn.Visible = false;
        //            FirstPurchaseBtn.Visible = false;
        //            NextPurchaseBtn.Visible = false;
        //            PrevPurchaseBtn.Visible = false;
        //            LastPurchaseBtn.Visible = false;
        //        }
        //        if (_RolePriv.EditFlag || _RolePriv.AllFlag)
        //        {
        //            EditFlagPrev = true;
        //        }
        //        else
        //        {
        //            EditFlagPrev = false;
        //        }
        //        if (_RolePriv.DeleteFlag || _RolePriv.AllFlag)
        //        {
        //            DeleteMonyInBtn.Visible = true;
        //        }
        //        else
        //        {
        //            DeleteMonyInBtn.Visible = false;
        //        }
        //    }
        //}
        public static bool EditFlagPrev;
        protected void MonyTypeDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sub_Account = new SubAccount();
            if (MonyTypeDrop.SelectedIndex == 1)
            {
                FinancialPos_td.Visible = false;
                SolarRowID.Visible = false;
                EditFlag = false;
                Bank_Moved = new BankMoved();
                int MoveID = db.BankMoved.ToList().Where(b => b.state == false).Max(o => o.ID) + 1;
                OperationID.Text = MoveID.ToString();
                //من
                FromSubAccountsID.DataSource = db.SubAccount.ToList().Where(s => s.MainAccount_id != 1121).ToList();//  Sub_Account.GetAllSubNotIncomsCheq(); //.Select(c => c.name);
                FromSubAccountsID.DataValueField = "ID";
                FromSubAccountsID.DataTextField = "name";
                FromSubAccountsID.DataBind();

                ChecqRow.Visible = true;
                //BankNameRow.Visible = true;
                SarfDateRow.Visible = true;
                FromKhaznaRow.Visible = true;
                FromKhaznaDrop.DataSource = db.SubAccount.ToList().Where(s => s.MainAccount_id == 1105);// Sub_Account.GetSubAccount_ByUpID(1105/*1121*/);
                FromKhaznaDrop.DataValueField = "ID";
                FromKhaznaDrop.DataTextField = "name";
                FromKhaznaDrop.DataBind();

                //ملأ ال Drop down list للبحث
                Bank_Moved_List_static = db.BankMoved.ToList().Where(b => b.state == true).ToList();// Bank_Moved.GetBankID_state(0);
                if (Bank_Moved_List_static.Count > 0)
                {
                    MonyInSearchBtn.Enabled = true;
                    MonyInsForSerachDrop.DataSource = Bank_Moved_List_static.Select(KM => KM.ID);
                    MonyInsForSerachDrop.DataBind();
                }
                else
                {
                    MonyInSearchBtn.Enabled = false;
                }
            }
            else
            {
                Initializ_Page();
            }
        }

        public string Validate_Data_Mony()
        {
            if (Value.Text == "" && Value.Text != "0")
            {
                return "من فضلك قم بادخال القيمة وتكون اكبر من الصفر";
            }
            if (ReceivedDate.Text == "" || SarfDateTxt.Text == "")
                return "من فضلك قم بادخال التاريخ";

            return "";
        }
        public string Validate_Data_Cheq()
        {
            if (Value.Text == "" && Value.Text != "0")
            {
                return "من فضلك قم بادخال القيمة وتكون اكبر من الصفر";
            }
            if (ReceivedDate.Text == "" || SarfDateTxt.Text == "")
                return "من فضلك قم بادخال التاريخ";
            if (ChequeNoTxt.Text == "")
                return "من فضلك ادخل رقم الشيك";

            return "";
        }
        public void AddMony()
        {
            // الي ليه استات ترو في الجدول بتاع ال خزنه هو الاستلام الصرف بياخد فلس
            ReceivedDate.Text = ReformateDateFromPicker(ReceivedDate.Text);

            Khzna_Moved = new KhznaMoved();
            Sub_Account = new SubAccount();

            Khzna_Moved_List = new List<KhznaMoved>();
            Entry entry = new Entry();
            List<Entry> Entry_List = new List<Entry>();

            if (EditFlag == false)
            {
                var response = "";
                Khzna_Moved.state = true;
                Khzna_Moved.ID = int.Parse(OperationID.Text);
                Khzna_Moved.Value = decimal.Parse(Value.Text);
                Khzna_Moved.AccountID = int.Parse(FromSubAccountsID.SelectedValue);
                Khzna_Moved.TreasuryID = int.Parse(FromKhaznaDrop.SelectedValue);
                Khzna_Moved.Description = DescTxt.Text;
                Khzna_Moved.Date = DateTime.Parse(ReceivedDate.Text, CultureInfo.CreateSpecificCulture("ar-EG")); ///////////
                if (FinancialPos.Checked == true)
                {
                    Khzna_Moved.FinancialPostitionType = "سيارة";
                    Khzna_Moved.FinancialPostitionId = int.Parse(CarsListTxt.SelectedValue);
                    response = AddSolarObject(Khzna_Moved.ID);
                    if (!String.IsNullOrEmpty(response))
                    {
                        try
                        {
                            Khzna_Moved_List.Add(Khzna_Moved);
                            db.KhznaMoved.Add(Khzna_Moved);
                            db.SaveChanges();
                            AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                            AddErrorTxt.Text = "تمت الاضافة بنجاح";
                        }
                        catch (Exception e)
                        {
                            AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                            AddErrorTxt.Text = "حدث خطاء اثناء الاضافة لحركة الخزنه";
                        }
                    }
                    else
                    {
                        AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                        AddErrorTxt.Text = "Something Wrong will Adding Solar";
                    }
                }
            }
            else
            {
                Khzna_Moved = db.KhznaMoved.ToList().Where(k => k.ID == int.Parse(OperationID.Text) & k.state == true).FirstOrDefault();// Khzna_Moved.GetKhznaMoved_ByID(int.Parse(OperationID.Text), 1).ElementAtOrDefault(0);
                // هنا بيجدد القيود  او بيعدلها
                if (Khzna_Moved.EntryState == true)
                {
                    entry = db.Entry.ToList().Where(e => e.EntryID == Khzna_Moved.EntryID & e.SubAccount_id == int.Parse(FromSubAccountsID.SelectedValue) & e.status == "مدين" & e.RecordID == Khzna_Moved.ID).FirstOrDefault();

                    //    entry = entry.GetAllEntryByRecIDAndStateAndAccountID(int.Parse(FromSubAccountsID.SelectedValue),
                    //      Khzna_Moved.ID, Khzna_Moved.EntryID, "مدين").ElementAtOrDefault(0);
                    //Entry_List.Add(entry);

                    //لانه اذن استلام فالخزنة مدينة
                    Khzna = db.Entry.ToList().Where(e => e.EntryID == Khzna_Moved.EntryID & e.SubAccount_id == int.Parse(FromKhaznaDrop.SelectedValue) & e.status == "دائن" & e.RecordID == Khzna_Moved.ID).FirstOrDefault();

                   // Khzna = entry.GetAllEntryByRecIDAndStateAndAccountID(int.Parse(FromKhaznaDrop.SelectedValue),
                     //   Khzna_Moved.ID, Khzna_Moved.EntryID, "دائن").ElementAtOrDefault(0);
                    //فى الحالة(تغير التاريخ او الخزنة) دى هنحذفه من القيود
                    //وتغير حالة القيد فى الخزنة
                    //ونقلل اجمالى القيد
                    if (OldEntry.Date != DateTime.Parse(ReceivedDate.Text) || OldKhznaID != Khzna_Moved.TreasuryID ||
                        OldEntry.SubAccount_id != int.Parse(FromSubAccountsID.SelectedValue))
                    {
                        Khzna_Moved_List.Add(Khzna_Moved);
                        Khzna_Moved_List.Where(k => k.state == true & k.ID == entry.RecordID).ElementAtOrDefault(0).EntryState = false;
                        Khzna_Moved_List.Where(k => k.state == true & k.ID == entry.RecordID).ElementAtOrDefault(0).EntryID = 0;

                        Khzna.value -= entry.value;//Khzna_Moved_List.Where(k => k.state == false & k.ID == entry.RecordID).ElementAtOrDefault(0).Value;
                        KhznaL.Add(Khzna);
                        Entry_List.Add(entry);
                        if (Khzna.value == 0)
                        {
                            KhznaL.Add(Khzna);
                            db.Entry.Remove(Khzna);

                        }
                        else
                        {
                            //if (EditFlagPrev)
                            //{
                            db.Entry.AddOrUpdate(Khzna);
                            db.Entry.Remove(entry);
                            db.SaveChanges();
                            //}
                        }
                    }
                    else//غير كدا (اللى هو تغير القيمة اوالبيان او الحساب الدائن)هانعمل مجرد تعديل على القيد
                    {

                        entry.value = decimal.Parse(Value.Text);// Khzna_Moved.Value;
                        entry.description = DescTxt.Text;
                        Khzna.value -= decimal.Parse(OldValue.ToString());
                        Khzna.value += entry.value;
                        //   Khzna.value += Khzna_Moved_List.Where(k => k.state == false & k.ID == entry.RecordID).ElementAtOrDefault(0).Value;
                        Entry_List.Add(Khzna);

                        Entry_List.Add(entry);
                        //if (EditFlagPrev)
                        entry.LoginID = ExtendedMethod.LoginedUser.Id;
                        db.Entry.AddOrUpdate(entry);
                        db.SaveChanges();
                    }
                }
                // هنا بيتعمل مع الحركه بتاعت الخزنه والسولر
                Khzna_Moved.ID = int.Parse(OperationID.Text);
                Khzna_Moved.AccountID = int.Parse(FromSubAccountsID.SelectedValue);
                Khzna_Moved.Value = decimal.Parse(Value.Text);
                Khzna_Moved.Date = DateTime.Parse(ReceivedDate.Text, CultureInfo.CreateSpecificCulture("ar-EG"));///////////
                Khzna_Moved.Description = DescTxt.Text;
                Khzna_Moved.TreasuryID = int.Parse(FromKhaznaDrop.SelectedValue);
                var response = "";
                if (FinancialPos.Checked == true)
                {
                    Khzna_Moved.FinancialPostitionType = "سيارة";
                    Khzna_Moved.FinancialPostitionId = int.Parse(CarsListTxt.SelectedValue);
                    response = AddSolarObject(Khzna_Moved.ID);
                }
                if (!String.IsNullOrEmpty(response))
                {
                    try
                    {
                        Khzna_Moved.LoginID = ExtendedMethod.LoginedUser.Id;
                        db.KhznaMoved.AddOrUpdate(Khzna_Moved);
                        Khzna_Moved_List.Add(Khzna_Moved);
                        db.SaveChanges();
                        AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                        AddErrorTxt.Text = "تمت التعديل بنجاح";
                    }
                    catch (Exception e)
                    {
                        AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                        AddErrorTxt.Text = "حدث خطاء اثناء تعديل لحركة الخزنه";
                    }
                }
                else
                {
                    AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                    AddErrorTxt.Text = "Something Wrong will Editing Solar";
                }
                //Khzna_Moved_List.Add(Khzna_Moved);
                //if (EditFlagPrev)
                //{
                
                //}
            }
        }
        public void AddCheq()
        {
            Bank_Moved = new BankMoved();
            Bank_Moved_List = new List<BankMoved>();
            Sub_Account = new SubAccount();
            Entry entry = new Entry();
            List<Entry> Entry_List = new List<Entry>();

            if (EditFlag == false)
            {
                Bank_Moved.ID = int.Parse(OperationID.Text);
                Bank_Moved.state = true;
                Bank_Moved.Description = DescTxt.Text;
                Bank_Moved.Value = Decimal.Parse(Value.Text);
                Bank_Moved.Date = DateTime.Parse(ReceivedDate.Text, CultureInfo.CreateSpecificCulture("ar-EG"));
                Bank_Moved.ChequeNo = ChequeNoTxt.Text;
                Bank_Moved.SarfDate = DateTime.Parse(SarfDateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG"));
                Bank_Moved.AccountID = int.Parse(FromSubAccountsID.SelectedItem.Value);
                Bank_Moved.DocID = int.Parse(FromKhaznaDrop.SelectedItem.Value);
                Bank_Moved.BankName = FromKhaznaDrop.SelectedItem.Text;
                if (FinancialPos.Checked == true)
                {
                    //Bank_Moved.FinancialPostitionType = FinancialPosTypeList.SelectedValue;
                    //Bank_Moved.FinancialPostitionId = int.Parse(AddRelatednumList.SelectedValue);
                }
                Bank_Moved_List.Add(Bank_Moved);

                db.BankMoved.Add(Bank_Moved);
                db.SaveChanges();
                AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                AddErrorTxt.Text = "تمت الاضافة بنجاح";
            }
            else
            {
                Bank_Moved = db.BankMoved.ToList().Where(o => o.state == true & o.ID == int.Parse(OperationID.Text)).FirstOrDefault();// Bank_Moved.GetBankMoving_ByID(int.Parse(OperationID.Text), 0).ElementAtOrDefault(0);
                if (Bank_Moved.EntryState == true)
                {
                    entry = db.Entry.ToList().Where(e => e.EntryID == Bank_Moved.EntryID &
                   e.SubAccount_id == int.Parse(Bank_Moved.AccountID.ToString()) &
                   e.status == "مدين" & e.RecordID == Bank_Moved.ID).FirstOrDefault();

                    //entry = entry.GetAllEntryByRecIDAndStateAndAccountID(int.Parse(Bank_Moved.AccountID.ToString()),
                    //    Bank_Moved.ID, Bank_Moved.EntryID, "مدين").ElementAtOrDefault(0);
                    //Entry_List.Add(entry);

                    //لانه اذن استلام فالخزنة مدينة
                    Bank = db.Entry.ToList().Where(e => e.EntryID == Bank_Moved.EntryID &
                   e.SubAccount_id == int.Parse(FromKhaznaDrop.SelectedValue) &
                   e.status == "دائن" & e.RecordID == Bank_Moved.ID).FirstOrDefault();

                    //Bank = entry.GetAllEntryByRecIDAndStateAndAccountID(int.Parse(FromKhaznaDrop.SelectedValue),
                    //    Bank_Moved.ID, Bank_Moved.EntryID, "دائن").ElementAtOrDefault(0);
                    //فى الحالة(تغير التاريخ او الخزنة) دى هنحذفه من القيود
                    //وتغير حالة القيد فى الخزنة
                    //ونقلل اجمالى القيد
                    if (OldEntry.Date != DateTime.Parse(ReceivedDate.Text) || OldBankID != Bank_Moved.DocID ||
                        OldEntry.SubAccount_id != int.Parse(FromSubAccountsID.SelectedItem.Value))
                    {
                        Bank_Moved_List.Add(Bank_Moved);
                        Bank_Moved_List.Where(k => k.state == true & k.ID == entry.RecordID).ElementAtOrDefault(0).EntryState = false;
                        Bank_Moved_List.Where(k => k.state == true & k.ID == entry.RecordID).ElementAtOrDefault(0).EntryID = 0;

                        Bank.value -= entry.value;//Khzna_Moved_List.Where(k => k.state == false & k.ID == entry.RecordID).ElementAtOrDefault(0).Value;
                        BankL.Add(Bank);
                        Entry_List.Add(entry);
                        if (Bank.value == 0)
                        {

                            BankL.Add(Bank);
                            db.Entry.Remove(Bank);
                            db.SaveChanges();
                        }
                        else
                        {
                            db.Entry.AddOrUpdate(Bank);
                            //    entry.Operations("Edit", BankL);
                            db.Entry.Remove(entry);
                            db.SaveChanges();
                            //if (EditFlagPrev)
                            //{

                            //}
                        }
                    }
                    else//غير كدا (اللى هو تغير القيمة اوالبيان او الحساب الدائن)هانعمل مجرد تعديل على القيد
                    {
                        entry.value = decimal.Parse(Value.Text);// Khzna_Moved.Value;
                        entry.description = DescTxt.Text;
                        Bank.value -= decimal.Parse(OldValue.ToString());
                        Bank.value += entry.value;
                        //   Khzna.value += Khzna_Moved_List.Where(k => k.state == false & k.ID == entry.RecordID).ElementAtOrDefault(0).Value;
                        Entry_List.Add(Bank);

                        Entry_List.Add(entry);
                        //if (EditFlagPrev)
                        db.Entry.AddOrUpdate(Bank);
                        db.Entry.AddOrUpdate(entry);
                        db.SaveChanges();
                    }
                }
                Bank_Moved.ID = int.Parse(OperationID.Text);
                Bank_Moved.AccountID = int.Parse(FromSubAccountsID.SelectedValue);
                Bank_Moved.Value = decimal.Parse(Value.Text);
                Bank_Moved.Date = DateTime.Parse(ReceivedDate.Text);///////////
                Bank_Moved.Description = DescTxt.Text;
                Bank_Moved.DocID = int.Parse(FromKhaznaDrop.SelectedValue);
                if (FinancialPos.Checked == true)
                {
                    //Bank_Moved.FinancialPostitionType = FinancialPosTypeList.SelectedValue;
                    //Bank_Moved.FinancialPostitionId = int.Parse(AddRelatednumList.SelectedValue);
                }
                Bank_Moved.ChequeNo = ChequeNoTxt.Text;
                Bank_Moved.BankName = FromKhaznaDrop.SelectedItem.Value;// SelectedValue;
                Bank_Moved_List.Add(Bank_Moved);
                //if (EditFlagPrev)
                //{
                db.BankMoved.AddOrUpdate(Bank_Moved);
                db.SaveChanges();

                AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                    AddErrorTxt.Text = "تم التعديل بنجاح";
                //}
            }
        }

        public string AddSolarObject(int ID)
        {
            // علشان يعمل ابديت للقديم في حالة في قديم لو مفيش يضيف
            var oldSolarItem = new Solar();
            MySolar = new Solar();
            oldSolarItem = db.Solar.Where(item => item.KhaznaBankID == ID).FirstOrDefault();
            if(oldSolarItem != null)
            {
                MySolar = oldSolarItem;
            }
            MySolar.CarID = int.Parse(CarsListTxt.SelectedValue);
            MySolar.DriverID = int.Parse(DriverListTxt.SelectedValue);
            MySolar.LastReading = Math.Round(float.Parse(LastReadTxt.Text),2);
            MySolar.CurrentReading = Math.Round(float.Parse(CurrentReadTxt.Text),2);
            MySolar.Date =DateTime.Parse(ReceivedDate.Text,CultureInfo.CreateSpecificCulture("ar-EG"));
            MySolar.Total = decimal.Parse(SolarTotalTxt.Text);
            MySolar.SolarQty = Math.Round(float.Parse(SolarQtyTxt.Text),2);
            MySolar.Average = Math.Round(float.Parse(AverageTxt.Text),2);
            MySolar.Distance = Math.Round(float.Parse(DistanceTxt.Text),2);
            MySolar.Notes = Notes.InnerText;
            MySolar.ServiceID = int.Parse(SolarTypeDrop.SelectedValue);
            MySolar.KhaznaBankID = ID;
            try
            {
                db.Solar.AddOrUpdate(MySolar);
                db.SaveChanges();
                return "Solar Added successfully";
            }
            catch(Exception e)
            {
                return "";
            }
        }
        protected void AddProductBtn_Click(object sender, EventArgs e)
        {
            if (MonyTypeDrop.SelectedIndex == 0)//نقدى
            {
                string Valid = Validate_Data_Mony();
                if (Valid == "")
                {
                    AddMony();
                    Initializ_Page();
                }
                else
                {
                    AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                    AddErrorTxt.Text = Valid;
                }
            }
            else//شيك
            {
                string Valid = Validate_Data_Mony();
                if (Valid == "")
                {
                    AddCheq();
                    Initializ_Page();
                }
                else
                {
                    AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                    AddErrorTxt.Text = Valid;

                }
            }
        }

        protected void MonyInsForSerachDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        void searchFn(int ID)
        {
            EditFlag = true;

            Khzna_Moved = new KhznaMoved();
            Bank_Moved = new BankMoved();
            Sub_Account = new SubAccount();
            OldEntry = new Entry();
            if (MonyTypeDrop.SelectedIndex == 0)//نقدى
            {
                Khzna_Moved = db.KhznaMoved.Where(k => k.ID == ID & k.state == true).FirstOrDefault();//  Khzna_Moved.GetKhznaMoved_ByID(ID, 0).ElementAt(0);

                OperationID.Text = Khzna_Moved.ID.ToString();

                Value.Text = String.Format("{0:0.00}", Khzna_Moved.Value);

                FromSubAccountsID.Items.Clear();
                FromSubAccountsID.DataBind();
                FromSubAccountsID.DataSource = db.SubAccount.ToList().Where(s => s.ID == int.Parse(Khzna_Moved.AccountID.ToString()));//  Sub_Account.GetSubAccount_ByID(int.Parse(Khzna_Moved.AccountID.ToString()));
                FromSubAccountsID.DataValueField = "ID";
                FromSubAccountsID.DataTextField = "name";
                FromSubAccountsID.DataBind();

                string khaznaNaname = FromKhaznaDrop.SelectedValue; //Sub_Account.GetSubAccount_ByID(int.Parse(Khzna_Moved.TreasuryID.ToString())).ElementAtOrDefault(0).name;
                FromKhaznaDrop.SelectedValue = khaznaNaname;
                DescTxt.Text = Khzna_Moved.Description;
                ReceivedDate.Text = ExtendedMethod.ParseDateToString((DateTime)Khzna_Moved.Date);
                if (Khzna_Moved.FinancialPostitionId !=0 && Khzna_Moved.FinancialPostitionType !="")
                {
                    Find_FinancialPosInfo(Khzna_Moved.ID);
                }
                DeleteMonyInBtn.Enabled = true;

                OldKhznaID = int.Parse(FromKhaznaDrop.SelectedValue);
                OldEntry.Date = DateTime.Parse(ReceivedDate.Text, CultureInfo.CreateSpecificCulture("ar-EG"));
                OldEntry.description = DescTxt.Text;
                OldEntry.SubAccount_id = Int64.Parse(Khzna_Moved.AccountID.ToString());

                OldValue = float.Parse(Value.Text);
                OldDescription = DescTxt.Text;
            }
            else//شيك
            {
                Bank_Moved = db.BankMoved.Where(b => b.ID == ID & b.state == true).FirstOrDefault();// Bank_Moved.GetBankMoving_ByID(ID, 0).ElementAt(0);
                if (Bank_Moved.FinancialPostitionId != 0 && Bank_Moved.FinancialPostitionType != "")
                {
                    //add_Edit_financialPosInfo((int)Bank_Moved.FinancialPostitionId, Bank_Moved.FinancialPostitionType);
                }
                OperationID.Text = Bank_Moved.ID.ToString();
                Value.Text = String.Format("{0:0.00}", Bank_Moved.Value);
                //FromSubAccountsID.Items.Clear();
                //FromSubAccountsID.DataBind();
                //FromSubAccountsID.Items.Add(Sub_Account.GetSubAccount_ByID(int.Parse(Bank_Moved.AccountID.ToString())).ElementAt(0).name.ToString());
                FromSubAccountsID.SelectedItem.Text = db.SubAccount.ToList().Where(s => s.ID == int.Parse(Bank_Moved.AccountID.ToString())).FirstOrDefault().name;// Sub_Account.GetSubAccount_ByID(int.Parse(Bank_Moved.AccountID.ToString())).ElementAt(0).name;
                DescTxt.Text = Bank_Moved.Description;
                ReceivedDate.Text = Bank_Moved.Date.ToString();
                SarfDateTxt.Text = Bank_Moved.SarfDate.ToString();
                DeleteMonyInBtn.Enabled = true;
                ChecqRow.Visible = true;
                //BankNameRow.Visible = true;
                FromKhaznaDrop.SelectedItem.Value = Bank_Moved.DocID.ToString();
                ChequeNoTxt.Text = Bank_Moved.ChequeNo;
                //    BankDropID.SelectedValue =FRo //Bank_Moved.BankName;
                DeleteMonyInBtn.Enabled = true;
                // OldDate = SarfDateTxt.Text;
                OldEntry.Date = DateTime.Parse(SarfDateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG"));
                OldCheq = ChequeNoTxt.Text;
                OldBankName = FromKhaznaDrop.SelectedItem.Text;//.SelectedValue;
                OldBankID = int.Parse(FromKhaznaDrop.SelectedValue);
                OldEntry.SubAccount_id = int.Parse(FromSubAccountsID.SelectedItem.Value);

                OldValue = float.Parse(Value.Text);
                OldDescription = DescTxt.Text;
            }
        }
        protected void Find_FinancialPosInfo(int KhaznaId)
        {
            MySolar = new Solar();
            MySolar = db.Solar.Where(item => item.KhaznaBankID == KhaznaId).FirstOrDefault();
            if(MySolar != null)
            {
                FinancialPos.Checked = true;
                SolarRowID.Visible = true;

                // Solar Part Info
                CarsListTxt.SelectedValue = MySolar.CarID.ToString();
                DriverListTxt.SelectedValue = MySolar.DriverID.ToString();
                LastReadTxt.Text = MySolar.LastReading.ToString();
                CurrentReadTxt.Text = MySolar.CurrentReading.ToString();
                SolarTotalTxt.Text = MySolar.Total.ToString();
                SolarQtyTxt.Text = MySolar.SolarQty.ToString();
                AverageTxt.Text = MySolar.Average.ToString();
                DistanceTxt.Text = MySolar.Distance.ToString();
                Notes.InnerText = MySolar.Notes.ToString();
                SolarTypeDrop.SelectedValue = MySolar.ServiceID.ToString();
            }
            else
            {
                AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                AddErrorTxt.Text = "حدث خطاء اثناء البحث عن حركة السولار ";
            }
        }
        protected void MonyInSearchBtn_Click(object sender, EventArgs e)
        {
            searchFn(int.Parse(MonyInsForSerachDrop.SelectedValue));
        }
        //Reset
        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            Initializ_Page();
            AddErrorTxt.ForeColor = System.Drawing.Color.Green;
            AddErrorTxt.Text = "";
        }
        //الاول
        protected void FirstPurchaseBtn_Click(object sender, EventArgs e)
        {
            AddErrorTxt.Text = "";
            try
            {
                if (MonyTypeDrop.SelectedIndex == 0)
                {
                    searchFn(Khzna_Moved_List_static.FirstOrDefault().ID);
                }
                else
                {
                    searchFn(Bank_Moved_List_static.FirstOrDefault().ID);
                }
                NextPurchaseBtn.Enabled = true;
                DeleteMonyInBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                AddErrorTxt.Text = "لايوجد اذونات";
                AddErrorTxt.ForeColor = System.Drawing.Color.Red;
            }
        }
        //التالى
        protected void NextPurchaseBtn_Click(object sender, EventArgs e)
        {
            AddErrorTxt.Text = "";
            try
            {
                if (MonyTypeDrop.SelectedIndex == 0)
                {
                    searchFn(Khzna_Moved_List_static.Where(l => l.ID == int.Parse(OperationID.Text)).ElementAt(0).ID + 1);
                }
                else
                {
                    searchFn(Bank_Moved_List_static.Where(l => l.ID == int.Parse(OperationID.Text)).ElementAt(0).ID + 1);
                }
                PrevPurchaseBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                AddErrorTxt.Text = "لايوجد اذونات";
                AddErrorTxt.ForeColor = System.Drawing.Color.Red;
            }
        }
        //الاخير
        protected void LastPurchaseBtn_Click(object sender, EventArgs e)
        {
            AddErrorTxt.Text = "";
            try
            {
                if (MonyTypeDrop.SelectedIndex == 0)
                {
                    searchFn(Khzna_Moved_List_static.LastOrDefault().ID);
                }
                else
                {
                    searchFn(Bank_Moved_List_static.LastOrDefault().ID);
                }
                PrevPurchaseBtn.Enabled = true;
                DeleteMonyInBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                AddErrorTxt.Text = "لايوجد اذونات";
                AddErrorTxt.ForeColor = System.Drawing.Color.Red;
            }
        }
        //السابق
        protected void PrevPurchaseBtn_Click(object sender, EventArgs e)
        {
            AddErrorTxt.Text = "";
            try
            {
                if (MonyTypeDrop.SelectedIndex == 0)
                {
                    searchFn(Khzna_Moved_List_static.Where(l => l.ID == int.Parse(OperationID.Text)).ElementAt(0).ID - 1);
                }
                else
                {
                    searchFn(Bank_Moved_List_static.Where(l => l.ID == int.Parse(OperationID.Text)).ElementAt(0).ID - 1);
                }
                NextPurchaseBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                AddErrorTxt.Text = "لايوجد اذونات";
                AddErrorTxt.ForeColor = System.Drawing.Color.Red;
            }
        }
        //مسح
        protected void DeleteMonyInBtn_Click(object sender, EventArgs e)
        {
            Khzna_Moved = new KhznaMoved();
            Bank_Moved = new BankMoved();
            Bank_Moved_List = new List<BankMoved>();
            Khzna_Moved_List = new List<KhznaMoved>();
            Entry entry = new Entry();
            List<Entry> Entry_List = new List<Entry>();
            if (MonyTypeDrop.SelectedIndex == 0)//نقدى
            {
                Khzna_Moved = db.KhznaMoved.ToList().Where(k => k.ID == int.Parse(OperationID.Text) & k.state == true).FirstOrDefault();// Khzna_Moved.GetKhznaMoved_ByID(int.Parse(OperationID.Text), 0).ElementAt(0);
                Khzna_Moved_List = new List<KhznaMoved>();
                Khzna_Moved_List.Add(Khzna_Moved);
                if (Khzna_Moved.EntryState == true)
                {
                    entry = db.Entry.ToList().Where(k => k.SubAccount_id == int.Parse(FromSubAccountsID.SelectedValue) &
k.RecordID == Khzna_Moved.ID & k.status == "مدين" & k.EntryID == Khzna_Moved.EntryID).FirstOrDefault();

                    //entry = entry.GetAllEntryByRecIDAndStateAndAccountID(int.Parse(FromSubAccountsID.SelectedValue),
                    //    Khzna_Moved.ID, Khzna_Moved.EntryID, "مدين").ElementAtOrDefault(0);
                    //Entry_List.Add(entry);

                    //لانه اذن استلام فالخزنة مدينة
                    Khzna = db.Entry.ToList().Where(b => b.status == "دائن" & b.SubAccount_id == int.Parse(FromKhaznaDrop.SelectedValue) &
                   b.EntryID == Khzna_Moved.EntryID & b.RecordID == Khzna_Moved.ID).FirstOrDefault();
                    //Khzna = entry.GetAllEntryByRecIDAndStateAndAccountID(int.Parse(FromKhaznaDrop.SelectedValue),
                    //    Khzna_Moved.ID, Khzna_Moved.EntryID, "دائن").ElementAtOrDefault(0);

                    Khzna.value -= entry.value;//Khzna_Moved_List.Where(k => k.state == false & k.ID == entry.RecordID).ElementAtOrDefault(0).Value;
                    if (Khzna.value == 0)
                    {
                        KhznaL.Add(Khzna);
                        db.Entry.Remove(Khzna);
                    }
                    else
                    {
                        KhznaL.Add(Khzna);
                        Entry_List.Add(entry);
                        db.Entry.AddOrUpdate(Khzna);
                        db.Entry.Remove(entry);
                    }
                    db.KhznaMoved.RemoveRange(Khzna_Moved_List);
                    db.SaveChanges();
                    Initializ_Page();

                    AddErrorTxt.Text = "تم المسح";
                }
                else//شيك
                {
                    Bank_Moved = db.BankMoved.ToList().Where(b => b.ID == int.Parse(OperationID.Text) & b.state == true).FirstOrDefault();// Bank_Moved.GetBankMoving_ByID(int.Parse(OperationID.Text), 0).ElementAt(0);
                    Bank_Moved_List = new List<BankMoved>();
                    Bank_Moved_List.Add(Bank_Moved);

                    if (Bank_Moved.EntryState == true)
                    {
                        entry = db.Entry.ToList().Where(o => o.EntryID == Bank_Moved.EntryID &
                o.SubAccount_id == int.Parse(Bank_Moved.AccountID.ToString()) &
                o.status == "مدين" & o.RecordID == Bank_Moved.ID).FirstOrDefault();

                        //entry = entry.GetAllEntryByRecIDAndStateAndAccountID(int.Parse(FromSubAccountsID.SelectedValue),
                        //    Bank_Moved.ID, Bank_Moved.EntryID, "مدين").ElementAtOrDefault(0);
                        //Entry_List.Add(entry);

                        //لانه اذن استلام فالخزنة مدينة
                        Bank = db.Entry.ToList().Where(o => o.EntryID == Bank_Moved.EntryID &
                  o.SubAccount_id == int.Parse(FromKhaznaDrop.SelectedValue) &
                  o.status == "دائن" & o.RecordID == Bank_Moved.ID).FirstOrDefault();

                        //Bank = entry.GetAllEntryByRecIDAndStateAndAccountID(int.Parse(FromKhaznaDrop.SelectedValue),
                        //    Bank_Moved.ID, Bank_Moved.EntryID, "دائن").ElementAtOrDefault(0);

                        Bank.value -= entry.value;//Khzna_Moved_List.Where(k => k.state == false & k.ID == entry.RecordID).ElementAtOrDefault(0).Value;
                        if (Bank.value == 0)
                        {
                            BankL.Add(Bank);
                            db.Entry.RemoveRange(BankL);
                        }
                        else
                        {
                            BankL.Add(Bank);
                            Entry_List.Add(entry);
                            db.Entry.AddOrUpdate(Bank);
                            db.Entry.RemoveRange(Entry_List);
                        }
                        db.BankMoved.RemoveRange(Bank_Moved_List);
                        db.SaveChanges();
                        AddErrorTxt.Text = "تم المسح";

                    }
                    Initializ_Page();
                }
            }
        }
        protected void FinancialPos_CheckedChanged(object sender, EventArgs e)
        {
            SolarRowID.Visible = FinancialPos.Checked;
            if (!String.IsNullOrEmpty(Value.Text))
                if (FinancialPos.Checked)
                {
                    SolarTotalTxt.Text = Math.Round(decimal.Parse(Value.Text), 2).ToString();
                    AllSolarEquations();
                }
        }
        //معادلات السولار مجمعة
        public void AllSolarEquations()
        {
            if (!string.IsNullOrEmpty(LastReadTxt.Text) && !string.IsNullOrEmpty(CurrentReadTxt.Text))
            {
                SolarQtyEquation();
                FinishedDistanceEquation();
                AverageEquation();
            }
        }
        //القراءة الحالية
        protected void CurrentReadTxt_TextChanged(object sender, EventArgs e)
        {
            AddErrorTxt.Text = "";
            if (int.Parse(CurrentReadTxt.Text)> int.Parse(LastReadTxt.Text))
            {
                AllSolarEquations();
            }else
            {
                AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                AddErrorTxt.Text = "Current Value must be more Than The Last Read .... !";
            }
        }
        //القراءة السابقة
        protected void LastReadTxt_TextChanged(object sender, EventArgs e)
        {
            AllSolarEquations();
        }
        //عند تغيير نوع الطاقة
        protected void SolarTypeDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            AllSolarEquations();
        }
        protected void Value_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(Value.Text))
            if (FinancialPos.Checked)
            {
                SolarTotalTxt.Text = Math.Round(decimal.Parse(Value.Text), 2).ToString();
            }
            AllSolarEquations();
        }
    }
}