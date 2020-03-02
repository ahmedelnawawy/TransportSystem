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

namespace TransportSystems.Views.Entries
{
    public partial class ChartOfAccountsView : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static bool EditFlag = false;
        public MainAccount Main_Account;
        public SubAccount Sub_Account;
        public Indx PersonalIndx;
        public List<Indx> PersonalIndxList;
        List<MainAccount> Main_Account_List;
        List<SubAccount> Sub_Account_List;
        public static int subAccountID = 0;
        public static int OldSubAccount;
        public static bool EditFlagPrev;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Initialize_Page();
                MakeAccountID();
            }
        }
        public List<MainAccount> GetAllMainStartBy(int prefix, int level)
        {
            List<MainAccount> temp = new List<MainAccount>();
            string select = "";
            if (level == 1)
            {
                level = 2;
                select = "select * from MainAccount where ID like'" + prefix + "%' and Level=" + level;
            }
            else
            {
                select = "select * from MainAccount where UpAccount like'" + prefix + "%' and Level=" + level;
            }
            temp = db.Database.SqlQuery<MainAccount>(select).ToList();
            return temp;
        }
        public List<SubAccount> GetAllSubAccountStartBy(int prefix, int level)
        {
            string select = "select * from SubAccount where UpAccount like'" + prefix + "%' and Level = " + level;
            return db.Database.SqlQuery<SubAccount>(select).ToList();
        }
        public void Initialize_Page()
        {
            IndxPart.Visible = false;
            EditFlag = false;
            string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            DateTxt.Text = date;
            AccountNameTxt.Text = "";
            DateRow.Visible = false;
            Main_Account = new MainAccount();
            Main_Account_List = new List<MainAccount>();
            Sub_Account = new SubAccount();
            UpAccountDrop.DataSource = GetAllMainStartBy(1, int.Parse(AccountLevelDrop.SelectedValue));
            UpAccountDrop.DataTextField = "name";
            UpAccountDrop.DataValueField = "ID";
            UpAccountDrop.DataBind();

            Dictionary<int, string> AccountsDictionary = new Dictionary<int, string>();
            foreach (var M in db.MainAccount.ToList())
            {
                AccountsDictionary.Add(int.Parse(M.ID.ToString()), M.name);
            }
            foreach (var S in db.SubAccount.ToList())
            {
                AccountsDictionary.Add(int.Parse(S.ID.ToString()), S.name);
            }
            AccountNoForSearchDrop.DataSource = AccountsDictionary;
            AccountNoForSearchDrop.DataTextField = "Value";
            AccountNoForSearchDrop.DataValueField = "Key";
            AccountNoForSearchDrop.DataBind();
            RootDrop.SelectedIndex = 0;
            AccountLevelDrop.SelectedIndex = 0;
            AccountTypeDrop.SelectedIndex = 0;
            long AccountID = 1;
            try
            {
                if (int.Parse(AccountLevelDrop.SelectedValue) == 2)
                {
                    var up = int.Parse(RootDrop.SelectedValue);
                    AccountID = db.MainAccount.Where(ma => ma.UpAccount == up).Select(i=>i.ID).Max()+1;
                    
                    UpAccountRow.Visible = false;
                }
                else
                {
                    var up = int.Parse(UpAccountDrop.SelectedValue);
                    AccountID = db.MainAccount.Where(ma => ma.UpAccount == up).Select(i => i.ID).Max() + 1;
                }

                AccountIDTxt.Text = AccountID.ToString();
                subAccountID = int.Parse(AccountIDTxt.Text);

            }
            catch (Exception ex)
            {

            }
            if (int.Parse(AccountLevelDrop.SelectedValue) < 4)
            {
                AccountStateRow.Visible = false;
                AccountBalanceRow.Visible = false;
            }
            else
            {
                AccountStateRow.Visible = true;
                AccountBalanceRow.Visible = true;
            }

            //Fill Search Drop Down List
            FillSearchDropDown();
            ClearIndxControls();

            //CheckForPrivilage();
        }
        //public privilage _Privilage;
        //public Rol_PrivFT _RolePriv;

        //public void CheckForPrivilage()
        //{
        //    _Privilage = new privilage();
        //    _RolePriv = new Rol_PrivFT();
        //    _Privilage = _Privilage.GetprivilageByName("دليل الحسابات").FirstOrDefault();
        //    _RolePriv = _RolePriv.GetAllRol_PrivFTBY_PrivID(_Privilage.ID, LoginedUser.RolID).FirstOrDefault();
        //    if (_RolePriv != null)
        //    {
        //        if (_RolePriv.AddFlag || _RolePriv.AllFlag)
        //        {
        //            SaveBtn.Visible = true;
        //        }
        //        else
        //        {
        //            SaveBtn.Visible = false;

        //        }
        //        if (_RolePriv.SearchFlag || _RolePriv.AllFlag)
        //        {
        //            SearchBtn.Visible = true;
        //            FirstBtn.Visible = true;
        //            NextBtn.Visible = true;
        //            PrevBtn.Visible = true;
        //            LastBtn.Visible = true;

        //        }
        //        else
        //        {
        //            SearchBtn.Visible = false;
        //            FirstBtn.Visible = false;
        //            NextBtn.Visible = false;
        //            PrevBtn.Visible = false;
        //            LastBtn.Visible = false;
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
        //            Delete.Visible = true;
        //        }
        //        else
        //        {
        //            Delete.Visible = false;
        //        }

        //    }

        //}
        public void ClearIndxControls()
        {
            Addess.Text = "";
            EmailTxt.Text = "";
            MobileNoTxt.Text = "";
            PersonalID.Text = "";
            maamoriaTxt.Text = "";
            Sgl_TaxNO.Text = "";
            AccountIDTxt.Text = "";
            TaxDocument.Text = "";

        }
        public void CommonInitializeFunction()
        {
            Main_Account = new MainAccount();
            Main_Account_List = new List<MainAccount>();
            Sub_Account_List = new List<SubAccount>();
            Sub_Account = new SubAccount();
            int Root = int.Parse(RootDrop.SelectedValue);
            int level = int.Parse(AccountLevelDrop.SelectedValue);
            string AccountType = AccountTypeDrop.SelectedValue;
            if (level < 4)
            {
                if (level == 2)
                {
                    UpAccountRow.Visible = false;
                }
                else
                {

                    UpAccountRow.Visible = true;

                }
                AccountType = "رئيسى";
                AccountTypeDrop.SelectedIndex = 0;
            }
            else if (level != 2)
            {
                UpAccountRow.Visible = true;
            }
            if (AccountType == "رئيسى" && int.Parse(AccountLevelDrop.SelectedValue) < 4)
            {
                long AccountID;
                DateRow.Visible = false;
                UpAccountDrop.DataSource = GetAllMainStartBy(int.Parse(RootDrop.SelectedValue), int.Parse(AccountLevelDrop.SelectedValue) - 1);
                UpAccountDrop.DataTextField = "name";
                UpAccountDrop.DataValueField = "ID";
                UpAccountDrop.DataBind();
                try
                {
                    if (int.Parse(AccountLevelDrop.SelectedValue) == 2)
                    {
                        var up = int.Parse(RootDrop.SelectedValue);
                        AccountID = db.MainAccount.Where(ma => ma.UpAccount == up).Select(i => i.ID).Max() + 1;
                    }
                    else
                    {
                        var up = int.Parse(UpAccountDrop.SelectedValue);
                        AccountID = db.MainAccount.Where(ma => ma.UpAccount == up).Select(i => i.ID).Max() + 1;
                    }

                    if (AccountID.ToString().Length < 2)
                    {
                        AccountIDTxt.Text = UpAccountDrop.SelectedValue + "01";
                    }
                    else
                    {
                        AccountIDTxt.Text = AccountID.ToString();
                    }
                    subAccountID = int.Parse(AccountIDTxt.Text);

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                DateRow.Visible = true;
                AccountTypeDrop.SelectedIndex = 1;
                if (int.Parse(AccountLevelDrop.SelectedValue) - 1 >= 4)
                {
                    UpAccountDrop.DataSource = GetAllSubAccountStartBy(int.Parse(RootDrop.SelectedValue), int.Parse(AccountLevelDrop.SelectedValue) - 1);
                }
                else
                {
                    UpAccountDrop.DataSource = GetAllMainStartBy(int.Parse(RootDrop.SelectedValue), int.Parse(AccountLevelDrop.SelectedValue) - 1);
                }
                UpAccountDrop.DataTextField = "name";
                UpAccountDrop.DataValueField = "ID";
                UpAccountDrop.DataBind();
                long AccountID;
                try
                {
                    
                    AccountID = db.SubAccount.Where(ma => ma.UpAccount == UpAccountDrop.SelectedValue).Select(i => i.ID).Max() + 1;
                    AccountIDTxt.Text = AccountID.ToString();
                    subAccountID = int.Parse(AccountIDTxt.Text);
                }
                catch (Exception ex)
                {
                }

                if (int.Parse(AccountLevelDrop.SelectedValue) < 4 && AccountTypeDrop.SelectedValue == "رئيسى")
                {
                    AccountStateRow.Visible = false;
                    AccountBalanceRow.Visible = false;
                    DateRow.Visible = false;
                }
                else
                {

                    AccountStateRow.Visible = true;
                    AccountBalanceRow.Visible = true;
                    DateRow.Visible = true;
                }

            }
            if (UpAccountDrop.SelectedValue == "1101" || UpAccountDrop.SelectedValue == "2103")
            {
                IndxPart.Visible = true;
            }
            else
            {

                IndxPart.Visible = false;
            }

            //Fill Search Drop Down List

            FillSearchDropDown();
        }
        public void FillSearchDropDown()
        {

        }
        //نوع الحساب
        protected void AccountTypeDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonInitializeFunction();
        }
        //الحساب الاعلى
        protected void UpAccountDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeAccountID();
        }
        public void MakeAccountID()
        {
            //  CommonInitializeFunction();
            Main_Account = new MainAccount();
            Sub_Account = new SubAccount();
            int Root = int.Parse(RootDrop.SelectedValue);
            int level = int.Parse(AccountLevelDrop.SelectedValue);
            string AccountType = AccountTypeDrop.SelectedValue;
            if (level < 4)
            {
                if (level == 2)
                {
                    UpAccountRow.Visible = false;
                }
                else
                {
                    UpAccountRow.Visible = true;
                }
                AccountType = "رئيسى";
                AccountTypeDrop.SelectedIndex = 0;
            }
            else if (level != 2)
            {
                UpAccountRow.Visible = true;
            }
            if (AccountType == "رئيسى" && int.Parse(AccountLevelDrop.SelectedValue) < 4)
            {
                long AccountID;
                try
                {
                    if (int.Parse(AccountLevelDrop.SelectedValue) == 2)
                    {
                        var up = int.Parse(RootDrop.SelectedValue);
                        AccountID = db.MainAccount.Where(ma => ma.UpAccount == up).Select(i => i.ID).Max() + 1;

                    }
                    else
                    {
                        var up = int.Parse(UpAccountDrop.SelectedValue);
                        AccountID = db.MainAccount.Where(ma => ma.UpAccount == up).Select(i => i.ID).Max() + 1;
                    }
                    if (AccountID.ToString().Length < 2)
                    {
                        AccountIDTxt.Text = UpAccountDrop.SelectedValue + "01";
                    }
                    else
                    {
                        AccountIDTxt.Text = AccountID.ToString();
                    }
                    subAccountID = int.Parse(AccountIDTxt.Text);

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                AccountTypeDrop.SelectedIndex = 1;
                long AccountID;
                try
                {
                    AccountID = Convert.ToInt64(db.SubAccount.Where(ma => ma.UpAccount == RootDrop.SelectedValue).Select(i => i.ID).Max() + 1);
                    if (AccountID == 1)
                    {
                        if (level == 4)
                        {
                            AccountID = int.Parse(UpAccountDrop.SelectedValue.ToString() + "0001");
                        }
                        else if (level == 3)
                        {
                            AccountID = int.Parse(UpAccountDrop.SelectedValue.ToString() + "01");

                        }
                        else if (level == 2)
                        {
                            AccountID = int.Parse(UpAccountDrop.SelectedValue.ToString() + "1");

                        }
                    }
                    AccountIDTxt.Text = AccountID.ToString();
                    subAccountID = int.Parse(AccountIDTxt.Text);

                }
                catch (Exception ex)
                {
                }
            }
            if (UpAccountDrop.SelectedValue == "1101" || UpAccountDrop.SelectedValue == "2103")
            {
                IndxPart.Visible = true;
            }
            else
            {

                IndxPart.Visible = false;
            }
        }
        //المستوى
        protected void AccountLevelDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonInitializeFunction();
            //try
            //{
            //    MakeAccountID();
            //}catch(Exception ex)
            //{
            //}
        }

        protected void RootDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonInitializeFunction();
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            AccountIDTxt.Text = subAccountID.ToString();
            Main_Account = new MainAccount();
            Main_Account_List = new List<MainAccount>();
            Sub_Account = new SubAccount();
            Sub_Account_List = new List<SubAccount>();
            PersonalIndx = new Indx();
            PersonalIndxList = new List<Indx>();
            long AccountID;
            //= int.Parse(AccountIDTxt.Text);
            if (AccountNameTxt.Text != "")
            {
                if (int.Parse(AccountLevelDrop.SelectedValue) < 4)
                {
                    Main_Account.ID = int.Parse(AccountIDTxt.Text);
                    AccountID = int.Parse(OldSubAccount.ToString());
                    Main_Account.name = AccountNameTxt.Text;
                    if (int.Parse(AccountLevelDrop.SelectedValue) == 2)
                    {
                        Main_Account.UpAccount = int.Parse(RootDrop.SelectedValue);
                    }
                    else
                    {
                        Main_Account.UpAccount = int.Parse(UpAccountDrop.SelectedValue);
                    }
                    Main_Account.Level = int.Parse(AccountLevelDrop.SelectedValue);
                    //LoginedUser.ID
                    Main_Account.LoginID = ExtendedMethod.LoginedUser.Id;
                    db.MainAccount.AddOrUpdate(Main_Account);
                    db.SaveChanges();

                    Initialize_Page();
                    AddErrorTxt.Text = "تم الحفظ بنجاح";
                }
                else
                {
                    string IndxFlag = "";
                    if (UpAccountDrop.SelectedValue == "1101" || UpAccountDrop.SelectedValue == "2103")
                        IndxFlag = CheckIndx();

                    if (IndxFlag == "")
                    {
                        Sub_Account.ID = int.Parse(AccountIDTxt.Text);
                        Sub_Account.name = AccountNameTxt.Text;
                        AccountID = int.Parse(OldSubAccount.ToString());

                        Sub_Account.MainAccount_id = int.Parse(UpAccountDrop.SelectedValue);
                        Sub_Account.BType = AccountStateDrop.SelectedValue;
                        Sub_Account.RegisterDate = DateTime.Parse(DateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG"));
                        Sub_Account.ABalance = decimal.Parse(BalanceTxt.Text == "" ? "0" : BalanceTxt.Text);
                        Sub_Account.Level = int.Parse(AccountLevelDrop.SelectedValue);
                        //LoginedUser.ID
                        Sub_Account.LoginID = ExtendedMethod.LoginedUser.Id;
                        Sub_Account_List.Add(Sub_Account);
                        PersonalIndx.Sub_ID = int.Parse(AccountIDTxt.Text);

                        if (UpAccountDrop.SelectedValue == "1101" || UpAccountDrop.SelectedValue == "2103")
                        {
                            PersonalIndx.Address = Addess.Text;
                            PersonalIndx.Email = EmailTxt.Text;
                            PersonalIndx.MobileNo = MobileNoTxt.Text;
                            PersonalIndx.PersonalID = PersonalID.Text;
                            PersonalIndx.Maamria = maamoriaTxt.Text;
                            PersonalIndx.Sgl_TaxNO = Sgl_TaxNO.Text;
                            PersonalIndx.TaxDocument = TaxDocument.Text;
                            PersonalIndx.LoginID = ExtendedMethod.LoginedUser.Id;
                        }
                        PersonalIndxList.Add(PersonalIndx);
                        if (EditFlag == true)
                        {
                                db.SubAccount.AddOrUpdate(Sub_Account);
                                db.SaveChanges();
                                try
                                {
                                    var up = int.Parse(AccountIDTxt.Text);
                                    long IndxID = db.Indx.Where(index => index.Sub_ID == up).FirstOrDefault().ID;
                                    PersonalIndx.ID = IndxID;
                                    PersonalIndx.LoginID =ExtendedMethod.LoginedUser.Id;
                                db.Indx.AddOrUpdate(PersonalIndx);
                                    db.SaveChanges();
                                    Initialize_Page();
                                    AddErrorTxt.Text = "تم الحفظ بنجاح";
                                    AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                                }
                                catch (Exception ex)
                                {
                                    db.Indx.AddOrUpdate(PersonalIndx);
                                    db.SaveChanges();
                                    Initialize_Page();
                                    AddErrorTxt.Text = "تم الحفظ بنجاح";
                                    AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                                }
                            
                        }
                        else
                        {
                            db.SubAccount.AddOrUpdate(Sub_Account);
                            db.SaveChanges();
                            db.Indx.AddOrUpdate(PersonalIndx);
                            db.SaveChanges();
                            Initialize_Page();
                            AddErrorTxt.Text = "تم الحفظ بنجاح";
                            AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                        }
                    }
                    else
                    {
                        AddErrorTxt.Text = IndxFlag;
                        AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        public string CheckIndx()
        {
            if (Sgl_TaxNO.Text != "")
            {
                if (Sgl_TaxNO.Text.Length < 9 || Sgl_TaxNO.Text.Length > 9)
                {
                    return "يجب ان يكون رقم السجل الضريبى مكون من 9 اراقام";
                }
            }
            else
            {
                return "من فضلك ادخل السجل الضريبى";
            }

            if (TaxDocument.Text != "")
            {
                if (TaxDocument.Text.Length != 16)
                {
                    return "يجب ان يكون رقم الملف الضريبى مكون من 16 رقم";
                }
            }
            else
            {
                return "من فضلك ادخل رقم الملف الضريبى";
            }
            if (PersonalID.Text != "")
            {
                if (PersonalID.Text.Length != 14)
                {
                    return "يجب ان يكون رقم البطاقة مكون من 14 رقم";
                }
            }
            else
            {
                return "من فضلك ادخل رقم البطاقة";
            }
            if (EmailTxt.Text != "")
            {
                if (!IsValidEmail(EmailTxt.Text))
                {
                    return "من فضلك ادخل البريد الالكترونى بشكل صحيح";
                }
            }
            return "";
        }
        protected void AccountNoForSearchDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        public void SearchFn(int AccountID)
        {
            EditFlag = true;
            AddErrorTxt.Text = "";
            Main_Account = new MainAccount();
            Main_Account_List = new List<MainAccount>();
            Sub_Account = new SubAccount();
            Sub_Account_List = new List<SubAccount>();
            int level = int.Parse(AccountLevelDrop.SelectedValue);
            try
            {
                Main_Account = db.MainAccount.Where(main => main.ID == AccountID).FirstOrDefault();
                level = Convert.ToInt32(Main_Account.Level);
            }
            catch (Exception ex)
            {
                Sub_Account = db.SubAccount.Where(sub => sub.ID == AccountID).FirstOrDefault();
                level = Convert.ToInt32(Sub_Account.Level);
            }
            //int RootID = int.Parse(RootDrop.SelectedValue);
            if (level < 4)//Main Account
            {

                MainAccount Main_Account1 = db.MainAccount.Where(main => main.ID == AccountID).FirstOrDefault();
                if (Main_Account1 != null)
                {
                    AccountNoForSearchDrop.SelectedValue = Main_Account1.ID.ToString();
                    AccountLevelDrop.SelectedValue = Main_Account1.Level.ToString();
                    RootDrop.SelectedValue = Main_Account1.ID.ToString()[0].ToString();
                    AccountLevelDrop_SelectedIndexChanged(this, null);
                }
                if (level != 2)
                    UpAccountDrop.SelectedValue = Main_Account1.UpAccount.ToString();

                AccountIDTxt.Text = Main_Account1.ID.ToString();
                subAccountID = int.Parse(AccountIDTxt.Text);
                AccountNameTxt.Text = Main_Account1.name;


                AccountStateRow.Visible = false;
                AccountBalanceRow.Visible = false;
                DateRow.Visible = false;
            }
            else//SubAccount
            {
                SubAccount Sub_Account1 = db.SubAccount.Where(sub => sub.ID == AccountID).FirstOrDefault();
                if (Sub_Account1 != null)
                {
                    AccountNoForSearchDrop.SelectedValue = Sub_Account1.ID.ToString();
                    AccountLevelDrop.SelectedValue = Sub_Account1.Level.ToString();
                    RootDrop.SelectedValue = Sub_Account1.ID.ToString()[0].ToString();
                    AccountLevelDrop_SelectedIndexChanged(this, null);
                }
                UpAccountDrop.SelectedValue = Sub_Account1.MainAccount_id.ToString();
                AccountIDTxt.Text = Sub_Account1.ID.ToString();
                subAccountID = int.Parse(AccountIDTxt.Text);
                AccountNameTxt.Text = Sub_Account1.name;
                DateTxt.Text = Sub_Account1.RegisterDate.ToString();
                AccountStateDrop.SelectedValue = Sub_Account1.BType;
                BalanceTxt.Text = Sub_Account1.ABalance.ToString();
                Indx PersonalIndx = new Indx();
                try
                {
                    var up = int.Parse(AccountIDTxt.Text);
                    PersonalIndx = db.Indx.Where(index => index.Sub_ID ==up).FirstOrDefault();
                    PersonalID.Text = PersonalIndx.PersonalID;
                    MobileNoTxt.Text = PersonalIndx.MobileNo;
                    EmailTxt.Text = PersonalIndx.Email;
                    Sgl_TaxNO.Text = PersonalIndx.Sgl_TaxNO;
                    TaxDocument.Text = PersonalIndx.TaxDocument;
                    maamoriaTxt.Text = PersonalIndx.Maamria;
                    Addess.Text = PersonalIndx.Address;

                }
                catch (Exception ex)
                {

                }
                if (Sub_Account1.MainAccount_id.ToString() == "1101" || Sub_Account1.MainAccount_id.ToString() == "2103")
                {
                    IndxPart.Visible = true;
                }
                else
                {

                    IndxPart.Visible = false;
                }

                AccountStateRow.Visible = true;
                AccountBalanceRow.Visible = true;
                DateRow.Visible = true;
            }
            OldSubAccount = subAccountID;
        }
        // Search Button
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            SearchFn(int.Parse(AccountNoForSearchDrop.SelectedValue));
        }
        protected void FirstBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(AccountNoForSearchDrop.Items[0].Value);
                SearchFn(id);
            }
            catch (Exception ex)
            {
                AddErrorTxt.Text = "لا يوجد نتائج";
            }
        }
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(AccountNoForSearchDrop.SelectedValue) + 1;
                SearchFn(id);
            }
            catch (Exception ex)
            {
                AddErrorTxt.Text = "لا يوجد نتائج";
            }
        }
        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(AccountNoForSearchDrop.SelectedValue) - 1;
                SearchFn(id);
            }
            catch (Exception ex)
            {
                AddErrorTxt.Text = "لا يوجد نتائج";
            }
        }
        protected void LastBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int indx = AccountNoForSearchDrop.Items.Count - 1;
                int id = int.Parse(AccountNoForSearchDrop.Items[indx].Value.ToString());
                SearchFn(id);
            }
            catch (Exception ex)
            {
                AddErrorTxt.Text = "لا يوجد نتائج";
            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            if (EditFlag == true)
            {
                Main_Account = new MainAccount();
                Main_Account_List = new List<MainAccount>();
                Sub_Account = new SubAccount();
                Sub_Account_List = new List<SubAccount>();
                AccountIDTxt.Text = subAccountID.ToString();
                int AccountID = int.Parse(AccountIDTxt.Text);
                try
                {
                    int level = int.Parse(AccountLevelDrop.SelectedValue);
                    if (level < 4)//Main account
                    {
                        Main_Account = db.MainAccount.Where(main => main.ID == AccountID).FirstOrDefault();
                        if (Main_Account != null)
                        {
                            db.MainAccount.Remove(Main_Account);
                            db.SaveChanges();
                        }
                    }
                    else//SubAccount
                    {
                        Sub_Account = db.SubAccount.Where(sub => sub.ID == AccountID).FirstOrDefault();
                       
                        if (Sub_Account != null)
                        {
                            db.SubAccount.Remove(Sub_Account);
                            db.SaveChanges();
                        }
                    }
                    AddErrorTxt.Text = "تم الحذف";
                    AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                    Initialize_Page();

                }
                catch (Exception ex)
                {
                    AddErrorTxt.Text = " حدث خطأ لا يمكن الحذف من فضلك تأكد من مسح الحسابات الفرعية منه";
                    AddErrorTxt.ForeColor = System.Drawing.Color.Red;

                }
            }
            else
            {
                AddErrorTxt.Text = "من فضلك ابحث عن حساب اولا";
                AddErrorTxt.ForeColor = System.Drawing.Color.Red;

            }
        }
        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            Initialize_Page();
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}