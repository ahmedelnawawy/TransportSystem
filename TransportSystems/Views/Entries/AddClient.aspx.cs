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
    public partial class AddClient : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public SubAccount Sub_Account;
        //public User user;
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
            EditFlag = false;

            AccountNameTxt.Text = "";
            BalanceTxt.Text = "";
            string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            DateTxt.Text = date;

            Sub_Account = new SubAccount();
            //  user = new User();

            AccountNoForSearchDrop.DataSource = db.SubAccount.ToList().Where(o => o.MainAccount_id == 1101).ToList();//  Sub_Account.GetAllSubAccountStartBy(1101);
            AccountNoForSearchDrop.DataTextField = "name";
            AccountNoForSearchDrop.DataValueField = "ID";
            AccountNoForSearchDrop.DataBind();

            //اسم المبيعات بيتاخد من ليس اليوزر
            SalesUesrId.DataSource = db.AspUser.ToList(); // user.Get_AllUser();
            SalesUesrId.DataTextField = "Username";
            SalesUesrId.DataValueField = "Id";
            SalesUesrId.DataBind();

            ClearIndxControls();
            setAccoutID();
            //CheckForPrivilage();
        }
        //public privilage _Privilage;
        //public Rol_PrivFT _RolePriv;

        //public void CheckForPrivilage()
        //{
        //    _Privilage = new privilage();
        //    _RolePriv = new Rol_PrivFT();
        //    _Privilage = _Privilage.GetprivilageByName("اضافة عميل").FirstOrDefault();
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
        public static bool EditFlagPrev;
        public void setAccoutID()
        {
            Sub_Account = new SubAccount();
            try
            {
               // Sub_Account.maxid(1101);
                Int64 AccountID =(Int64) db.SubAccount.ToList().Max(o => o.MainAccount_id) + 1;
                if (AccountID == 1)
                {

                    AccountID = int.Parse(1101.ToString() + "0001");
                }
                AccountIDTxt.Text = AccountID.ToString();
                SubAccountManulID = int.Parse(AccountIDTxt.Text);

            }
            catch (Exception ex)
            {
            }
        }

        public static int SubAccountManulID;
        public void ClearIndxControls()
        {
            AccountIDTxt.Text = "";

            ResponsiblePersonName.Text = "";
            ResponsiblePersonPhone.Text = "";
            AnotherResponsiblePersonPhone.Text = "";
            EmailTxt.Text = "";
            Addess.Text = "";
            Sgl_TaxNO.Text = "";
            CommercialDocument.Text = "";
            TaxDocument.Text = "";



        }

        public string CheckIndx()
        {
            //if (Sgl_TaxNO.Text != "")
            //{
            //    if (Sgl_TaxNO.Text.Length < 8)
            //    {
            //        return " يجب ان يكون رقم التسجيل الضريبى مكون من 8 اراقام او اكثر";
            //    }
            //}
            //else
            //{
            //    return "من فضلك ادخل السجل الضريبى";
            //}
            //if (TaxDocument.Text != "")
            //{
            //    if (TaxDocument.Text.Length < 1)
            //    {
            //        return "رقم ملف ضريبى غير صحيح";
            //    }
            //}
            //else
            //{
            //    return "من فضلك ادخل رقم الملف الضريبى";
            //}
            //if (EmailTxt.Text != "")
            //{
            //    if (!IsValidEmail(EmailTxt.Text))
            //    {
            //        return "من فضلك ادخل البريد الالكترونى بشكل صحيح";
            //    }
            //}

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
                    // client SubAccountManul Info
                    Sub_Account.ID = int.Parse(AccountIDTxt.Text);
                    Sub_Account.name = AccountNameTxt.Text;
                    Sub_Account.Level = 4;
                   // Sub_Account.AccountID = Int64.Parse(OldSubAccountManul.ToString());
                    Sub_Account.MainAccount_id = 1101;
                    Sub_Account.BType = AccountStateDrop.SelectedValue;
                    Sub_Account.RegisterDate = DateTime.Parse(DateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG"));
                    Sub_Account.ABalance = decimal.Parse(BalanceTxt.Text == "" ? "0" : BalanceTxt.Text);

                    //LoginedUser.ID;
                    Sub_Account.LoginID =ExtendedMethod.LoginedUser.Id;
                    Sub_Account_List.Add(Sub_Account);

                    // client Index Info
                    PersonalIndx.ClientCategory = ClientCategoryList.SelectedItem.Text;
                    PersonalIndx.ClientType = ClentTypelist.SelectedItem.Text;
                    PersonalIndx.SalesUesrId = Convert.ToInt32(SalesUesrId.SelectedValue);
                    PersonalIndx.ResponsiblePersonName = ResponsiblePersonName.Text;
                    PersonalIndx.ResponsiblePersonPhone = ResponsiblePersonPhone.Text;
                    PersonalIndx.AnotherResponsiblePersonPhone = AnotherResponsiblePersonPhone.Text;
                    PersonalIndx.Email = EmailTxt.Text;
                    PersonalIndx.Address = Addess.Text;
                    PersonalIndx.Sgl_TaxNO = Sgl_TaxNO.Text;
                    PersonalIndx.CommercialDocument = CommercialDocument.Text;
                    PersonalIndx.TaxDocument = TaxDocument.Text;
                    PersonalIndx.Sub_ID = Int64.Parse(AccountIDTxt.Text);

                    PersonalIndxList.Add(PersonalIndx);
                    // not used here
                    //PersonalIndx.MobileNo = MobileNoTxt.Text;
                    //PersonalIndx.PersonalID = PersonalID.Text;
                    //PersonalIndx.Maamria = maamoriaTxt.Text;
                    if (EditFlag == true)
                    {
                          //  Sub_Account.Operations("Edit", Sub_Account_List);
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
                             //   PersonalIndx.Operations("Add", PersonalIndxList);
                                db.Indx.Add(PersonalIndx);
                            }
                            db.SaveChanges();
                            Initialize_Page();
                            AddErrorTxt.Text = "تم الحفظ بنجاح";
                            AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                        
                    }
                    else
                    {
                        db.SubAccount.Add(Sub_Account);
                        db.Indx.Add(PersonalIndx);
                        try {
                            db.SaveChanges();
                        }catch(Exception ex)
                        {

                        }
                        //Sub_Account.Operations("Add", Sub_Account_List);
                        //PersonalIndx.Operations("Add", PersonalIndxList);
                        try {
                            Initialize_Page();
                            AddErrorTxt.Text = "تم الحفظ بنجاح";
                            AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                        }catch(Exception ex)
                        {

                        }
                        }
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
            level =(int) Sub_Account.Level;
            SubAccount Sub_Account1 = db.SubAccount.ToList().Where(s => s.ID == AccountID).FirstOrDefault();// Sub_Account.GetSubAccount_ByID(AccountID).ElementAtOrDefault(0);
            if (Sub_Account1 != null)
            {
                AccountNoForSearchDrop.SelectedValue = Sub_Account1.ID.ToString();
            }
            AccountIDTxt.Text = Sub_Account1.ID.ToString();
            SubAccountManulID = int.Parse(AccountIDTxt.Text);
            AccountNameTxt.Text = Sub_Account1.name;
            DateTxt.Text =ExtendedMethod.ParseDateToString((DateTime)Sub_Account1.RegisterDate);
            AccountStateDrop.SelectedValue = Sub_Account1.BType;
            BalanceTxt.Text = Sub_Account1.ABalance.ToString();
            Indx PersonalIndx = new Indx();
            try
            {
                PersonalIndx = db.Indx.ToList().Where(i=>i.Sub_ID== Int64.Parse(AccountIDTxt.Text)).FirstOrDefault();// PersonalIndx.GetIndx_BySubID(Int64.Parse(AccountIDTxt.Text)).FirstOrDefault();

                if (PersonalIndx.ClientCategory != "") { ClientCategoryList.SelectedItem.Text = PersonalIndx.ClientCategory; }
                if (PersonalIndx.ClientType != "") { ClentTypelist.SelectedItem.Text = PersonalIndx.ClientType; }
                if (PersonalIndx.SalesUesrId.ToString() != "") { SalesUesrId.SelectedValue = PersonalIndx.SalesUesrId.ToString(); }
                ResponsiblePersonName.Text = PersonalIndx.ResponsiblePersonName;
                ResponsiblePersonPhone.Text = PersonalIndx.ResponsiblePersonPhone;
                AnotherResponsiblePersonPhone.Text = PersonalIndx.AnotherResponsiblePersonPhone;
                EmailTxt.Text = PersonalIndx.Email; ;
                Addess.Text = PersonalIndx.Address;
                Sgl_TaxNO.Text = PersonalIndx.Sgl_TaxNO;
                CommercialDocument.Text = PersonalIndx.CommercialDocument;
                TaxDocument.Text = PersonalIndx.TaxDocument;
                PersonalIndx.Sub_ID = Int64.Parse(AccountIDTxt.Text);
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
                        Sub_Account_List.Add(Sub_Account);
                        db.SubAccount.Remove(Sub_Account);
                        db.SaveChanges();
                       // Sub_Account.Operations("Delete", Sub_Account_List);
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