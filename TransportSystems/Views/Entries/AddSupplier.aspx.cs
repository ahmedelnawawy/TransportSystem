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
    public partial class AddSupplier : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();
        public SubAccount Sub_Account;
        public static bool EditFlag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Initialize_Page();
            }

        }
        public void Initialize_Page()
        {
            AccountNameTxt.Text = "";
            BalanceTxt.Text = "";
            EditFlag = false;
            string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            DateTxt.Text = date;
            Sub_Account = new SubAccount();

            AccountNoForSearchDrop.DataSource = db.SubAccount.ToList().Where(s=>s.MainAccount_id==2103);// Sub_Account.GetAllSubAccountStartBy(2103);
            AccountNoForSearchDrop.DataTextField = "name";
            AccountNoForSearchDrop.DataValueField = "ID";
            AccountNoForSearchDrop.DataBind();
            ClearIndxControls();
            setAccoutID();
            CheckForPrivilage();
        }
        //public privilage _Privilage;
        //public Rol_PrivFT _RolePriv;

        public void CheckForPrivilage()
        {
            //_Privilage = new privilage();
            //_RolePriv = new Rol_PrivFT();
            //_Privilage = _Privilage.GetprivilageByName("اضافة مورد").FirstOrDefault();
            //_RolePriv = _RolePriv.GetAllRol_PrivFTBY_PrivID(_Privilage.ID, LoginedUser.RolID).FirstOrDefault();
            //if (_RolePriv != null)
            //{
            //    if (_RolePriv.AddFlag || _RolePriv.AllFlag)
            //    {
            //        SaveBtn.Visible = true;
            //    }
            //    else
            //    {
            //        SaveBtn.Visible = false;

            //    }
            //    if (_RolePriv.SearchFlag || _RolePriv.AllFlag)
            //    {
            //        SearchBtn.Visible = true;
            //        FirstBtn.Visible = true;
            //        NextBtn.Visible = true;
            //        PrevBtn.Visible = true;
            //        LastBtn.Visible = true;

            //    }
            //    else
            //    {
            //        SearchBtn.Visible = false;
            //        FirstBtn.Visible = false;
            //        NextBtn.Visible = false;
            //        PrevBtn.Visible = false;
            //        LastBtn.Visible = false;
            //    }
            //    if (_RolePriv.EditFlag || _RolePriv.AllFlag)
            //    {
            //        EditFlagPrev = true;
            //    }
            //    else
            //    {
            //        EditFlagPrev = false;

            //    }
            //    if (_RolePriv.DeleteFlag || _RolePriv.AllFlag)
            //    {
            //        Delete.Visible = true;
            //    }
            //    else
            //    {
            //        Delete.Visible = false;
            //    }

            //}
        }
        public static bool EditFlagPrev;
        public void setAccoutID()
        {
            Sub_Account = new SubAccount();
            try
            {
                Int64 AccountID = (Int64)db.SubAccount.ToList().Max(o => o.MainAccount_id) + 1;
                if (AccountID == 1)
                {

                    AccountID = int.Parse(2103.ToString() + "0001");
                }
                AccountIDTxt.Text =AccountID.ToString();
                SubAccountManulID = int.Parse(AccountIDTxt.Text);

            }
            catch (Exception ex)
            {
            }
        }

        public static int SubAccountManulID;
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

        public string CheckIndx()
        {
            if (Sgl_TaxNO.Text != "")
            {
                if (Sgl_TaxNO.Text.Length < 8)
                {
                    return " يجب ان يكون رقم التسجيل الضريبى مكون من 8 اراقام او اكثر";
                }
            }
            else
            {
                return "من فضلك ادخل السجل الضريبى";
            }

            if (TaxDocument.Text != "")
            {
                if (TaxDocument.Text.Length < 1)
                {
                    return "رقم ملف ضريبى غير صحيح";
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

            if (EmailTxt.Text != "")
            {
                if (!IsValidEmail(EmailTxt.Text))
                {
                    return "من فضلك ادخل البريد الالكترونى بشكل صحيح";
                }
            }

            return "";
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
        List<SubAccount> Sub_Account_List;
        Indx PersonalIndx;
        List<Indx> PersonalIndxList;
        public static int OldSubAccountManul;

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            Sub_Account = new SubAccount();
            Sub_Account_List = new List<SubAccount>();
            PersonalIndx = new Indx();
            PersonalIndxList = new List<Indx>();
            AccountIDTxt.Text = SubAccountManulID.ToString();
            if (AccountNameTxt.Text != "")
            {
                string IndxFlag = CheckIndx();
                if (IndxFlag == "")
                {
                    Sub_Account.ID = int.Parse(AccountIDTxt.Text);
                    Sub_Account.name = AccountNameTxt.Text;
                    //Sub_Account.AccountID = Int64.Parse(OldSubAccountManul.ToString());

                    Sub_Account.MainAccount_id = 2103;
                    Sub_Account.BType = AccountStateDrop.SelectedValue;
                    Sub_Account.RegisterDate = DateTime.Parse(DateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG"));
                    Sub_Account.ABalance = decimal.Parse(BalanceTxt.Text == "" ? "0" : BalanceTxt.Text);
                    Sub_Account.Level = 4;
                    //LoginedUser.ID
                    Sub_Account.LoginID = ExtendedMethod.LoginedUser.Id;
                    Sub_Account_List.Add(Sub_Account);

                    PersonalIndx.Address = Addess.Text;
                    PersonalIndx.Email = EmailTxt.Text;
                    PersonalIndx.MobileNo = MobileNoTxt.Text;
                    PersonalIndx.PersonalID = PersonalID.Text;
                    PersonalIndx.Maamria = maamoriaTxt.Text;
                    PersonalIndx.Sgl_TaxNO = Sgl_TaxNO.Text;
                    PersonalIndx.Sub_ID = Int64.Parse(AccountIDTxt.Text);
                    PersonalIndx.TaxDocument = TaxDocument.Text;
                    PersonalIndxList.Add(PersonalIndx);




                    if (EditFlag == true)
                    {
                        db.SubAccount.AddOrUpdate(Sub_Account);
                        try
                        {
                            Int64 IndxID = db.Indx.ToList().Where(i => i.Sub_ID == int.Parse(AccountIDTxt.Text)).FirstOrDefault().ID;// PersonalIndx.GetIndx_BySubID(int.Parse(AccountIDTxt.Text)).FirstOrDefault().ID;
                            PersonalIndx.ID = IndxID;
                            // PersonalIndx.Operations("Edit", PersonalIndxList);
                            db.Indx.AddOrUpdate(PersonalIndx);
                        }
                        catch (Exception ex)
                            {
                            db.Indx.Add(PersonalIndx);

                        }

                    }
                    else
                    {
                        db.SubAccount.Add(Sub_Account);
                        db.Indx.Add(PersonalIndx);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    Initialize_Page();
                    AddErrorTxt.Text = "تم الحفظ بنجاح";
                    AddErrorTxt.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    AddErrorTxt.Text = IndxFlag;
                    AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                }
            }

        }

        public void SearchFn(int AccountID)
        {
            EditFlag = true;
            AddErrorTxt.Text = "";
            Sub_Account = new SubAccount();
            Sub_Account_List = new List<SubAccount>();
            int level = 4;
            Sub_Account = db.SubAccount.ToList().Where(s => s.ID == AccountID).FirstOrDefault();//  Sub_Account.GetSubAccount_ByID(AccountID).ElementAtOrDefault(0);
            level = (int)Sub_Account.Level;
            SubAccount Sub_Account1 = db.SubAccount.ToList().Where(s => s.ID == AccountID).FirstOrDefault();// Sub_Account.GetSubAccount_ByID(AccountID).ElementAtOrDefault(0);
            if (Sub_Account1 != null)
            {
                AccountNoForSearchDrop.SelectedValue = Sub_Account1.ID.ToString();
            }
            AccountIDTxt.Text = Sub_Account1.ID.ToString();
            SubAccountManulID = int.Parse(AccountIDTxt.Text);
            AccountNameTxt.Text = Sub_Account1.name;
            DateTxt.Text = ExtendedMethod.ParseDateToString((DateTime)Sub_Account1.RegisterDate);
            AccountStateDrop.SelectedValue = Sub_Account1.BType;
            BalanceTxt.Text = Sub_Account1.ABalance.ToString();
            Indx PersonalIndx = new Indx();
            try
            {
                PersonalIndx = db.Indx.ToList().Where(i => i.Sub_ID == Int64.Parse(AccountIDTxt.Text)).FirstOrDefault();// PersonalIndx.GetIndx_BySubID(Int64.Parse(AccountIDTxt.Text)).FirstOrDefault();
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

            OldSubAccountManul = SubAccountManulID;


        }

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
                Sub_Account = new SubAccount();
                Sub_Account_List = new List<SubAccount>();
                AccountIDTxt.Text = SubAccountManulID.ToString();

                int AccountID = int.Parse(AccountIDTxt.Text);
                try
                {
                    Sub_Account = db.SubAccount.ToList().Where(s => s.ID == AccountID).FirstOrDefault();// Sub_Account.GetSubAccount_ByID(AccountID).ElementAtOrDefault(0);
                    if (Sub_Account != null)
                    {
                        db.SubAccount.Remove(Sub_Account);
                        db.SaveChanges();
                    }
                    AddErrorTxt.Text = "تم الحذف";
                    AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                    Initialize_Page();

                }
                catch (Exception ex)
                {
                    AddErrorTxt.Text = "حدث خطأ لا يمكن الحذف";
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
    }
}