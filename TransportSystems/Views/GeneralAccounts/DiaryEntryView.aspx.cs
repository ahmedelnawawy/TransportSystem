using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.GeneralAccounts
{
    public partial class DiaryEntryView : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        SubAccount Sub_Account;
        Entry _entry;
        List<MainAccount> Main_Account_List;
        MainAccount Main_Account;
         List<Entry> Entry_List;
        public static List<DiaryEntrGrd> Diar_Grd_List;
        public class DiaryEntrGrd
        {
            public int RecordID { set; get; }
            public int AccountNO { set; get; }
            public string AccountName { set; get; }
            public float IndebtBalance { set; get; }
            public float CreditBalance { set; get; }
            public string description { set; get; }
        }
        DiaryEntrGrd Diary_Entry_obj;
        public static int IncrementalID = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Initialize_Page();
            }

        }

        public void Initialize_Page()
        {
             sum1credit = 0;
            sumIndebt = 0;
            DiaryGrd.DataSource = null;
            DiaryGrd.DataBind();
            DeleteBtn.Enabled = false;
            SaveBtn.Enabled = false;
            Sub_Account = new SubAccount();
            _entry = new Entry();
            DateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            //AccountNameDrop.DataSource = Sub_Account.GetAllSub().ToList();
            //AccountNameDrop.DataTextField = "name";
            //AccountNameDrop.DataValueField = "id";
            //AccountNameDrop.DataBind();

            Main_Account = new MainAccount();
            Main_Account_List = new List<MainAccount>();
            Sub_Account = new SubAccount();
           
            Dictionary<int, string> AccountsDictionary = new Dictionary<int, string>();
            foreach (var M in db.MainAccount.ToList())
            {
                AccountsDictionary.Add(int.Parse(M.ID.ToString()), M.name);
            }
            foreach (var S in db.SubAccount.ToList())
            {
                AccountsDictionary.Add(int.Parse(S.ID.ToString()), S.name);
            }

            AccountNameDrop.DataSource = AccountsDictionary;
            AccountNameDrop.DataTextField = "Value";
            AccountNameDrop.DataValueField = "Key";
            AccountNameDrop.DataBind();

            Diar_Grd_List = new List<DiaryEntrGrd>();
            IncrementalID = 1;
            //_entry.maxid();
            int MoveID =(int) db.Entry.ToList().Max(o=>o.EntryID)+1;
            EntryNoTxt.Text = MoveID.ToString();
            DiaryGrd.DataSource = null;
            DiaryGrd.DataBind();
            try
            {
                List<Entry> SearchList = new List<Entry>();
                SearchList = db.Entry.ToList();// _entry.GetAllEntryID().ToList();
                EntryNoForSearch.Items.Insert(0, new ListItem("اختر رقم القيد للبحث", String.Empty));
                
                foreach (var item in SearchList)
                {
                    EntryNoForSearch.Items.Add(new ListItem(item.EntryID.ToString(),item.EntryID.ToString()));

                }
                //EntryNoForSearch.DataSource = SearchList;
                EntryNoForSearch.DataTextField = "EntryID";
                EntryNoForSearch.DataValueField = "EntryID";
                EntryNoForSearch.DataBind();

            }
            catch (Exception ex)
            {

            }


            DateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            NextBtn.Enabled = false;
            PrevBtn.Enabled = false;
            FirstBtn.Enabled = true;
            LastBtn.Enabled = true;
            EditFalg = false;
        }

        
        static float sum1credit = 0, sumIndebt = 0;
        protected void addRowBtn_Click(object sender, EventArgs e)
        {


            if (AccountNameDrop.Items.Count > 0 && CreditTxt.Text != "" && Indebt_BalanceTxt.Text != "")
            {

                _entry = new Entry();

                Diary_Entry_obj = new DiaryEntrGrd();
                if (EditFalg == false)
                    Diary_Entry_obj.RecordID = IncrementalID;
                else {
                    int RecordID =(int) db.Entry.ToList().Where(o => o.EntryID == int.Parse(EntryNoTxt.Text)).ToList().Max(r => r.RecordID) + 1;
                    //_entry.maxRecordID((int.Parse(EntryNoTxt.Text)));
                    Diary_Entry_obj.RecordID =RecordID;
                }

                Diary_Entry_obj.AccountNO = int.Parse(AccountNameDrop.SelectedItem.Value);
                Diary_Entry_obj.AccountName = AccountNameDrop.SelectedItem.Text;
                Diary_Entry_obj.CreditBalance = float.Parse(CreditTxt.Text);
                Diary_Entry_obj.IndebtBalance = float.Parse(Indebt_BalanceTxt.Text);
                sum1credit+= float.Parse(CreditTxt.Text);
                sumIndebt+= float.Parse(Indebt_BalanceTxt.Text);
                Diary_Entry_obj.description = DesTxt.Text;
                try
                {
                    Diar_Grd_List.Where(o => o.AccountNO == Diary_Entry_obj.AccountNO).ElementAt(0);
                }
                catch (Exception ex)
                {
                    Diar_Grd_List.Add(Diary_Entry_obj);

                }

                DiaryGrd.DataSource = Diar_Grd_List;
                DiaryGrd.DataBind();

                AccountNameDrop.SelectedIndex = 0;
                CreditTxt.Text = "";
                Indebt_BalanceTxt.Text = "";
                DesTxt.Text = "";
                DescTxt.Text = "";

                CreditTxt.Enabled = true;
                Indebt_BalanceTxt.Enabled = true;
                AccountNOTxt.Text = "";
                IncrementalID++;

                DiaryGrd.FooterRow.Cells[4].Text = sum1credit.ToString();
                DiaryGrd.FooterRow.Cells[3].Text = sumIndebt.ToString();

            }
            else
            {
                AddError.Text = "من فضلك ادخل جميع القيم";
                AddError.ForeColor = System.Drawing.Color.Red;
            }
            SaveBtn.Enabled = true;
        }

        protected void AccountNameDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountNOTxt.Text = AccountNameDrop.SelectedItem.Value;
            Indebt_BalanceTxt.Text = "";
            Indebt_BalanceTxt.Enabled = true;
            CreditTxt.Text = "";
            CreditTxt.Enabled = true;
            DiaryGrd.DataSource = Diar_Grd_List;
            DiaryGrd.DataBind();
        }

        protected void Indebt_BalanceTxt_TextChanged(object sender, EventArgs e)
        {
            CreditTxt.Text = "0";
            CreditTxt.Enabled = false;
        }
        public static string ReformateDateFromPicker(string date)
        {
            var alldate = date.Split('-');
            return alldate[0] + "-" + alldate[1] + "-" + alldate[2];
        }
        protected void CreditTxt_TextChanged(object sender, EventArgs e)
        {
            Indebt_BalanceTxt.Text = "0";
            Indebt_BalanceTxt.Enabled = false;

        }
        public static bool EditFalg = false;
        //Search By Entry ID 
        public void SearchFn(int Entry_ID)
        {
            try { 
            EditFalg = true;
            SaveBtn.Enabled = false;
            DeleteBtn.Enabled = true;
            _entry = new Entry ();
            Entry_List = new List<Entry>();

                Entry_List = db.Entry.ToList().Where(en => en.EntryID == Entry_ID).ToList();//  _entry.GetAllEntryByID(Entry_ID);
            EntryNoTxt.Text = Entry_ID.ToString();
            DateTxt.Text = Entry_List.ElementAtOrDefault(0).Date.ToString();
            DescTxt.Text = Entry_List.ElementAtOrDefault(0).description;
            Diar_Grd_List = new List<DiaryEntrGrd>();
            float SumMaden = 0, SumDaan = 0;
            foreach (var ent in Entry_List)
            {
                float CreditVal = ent.status == "دائن" ? float.Parse(ent.value.ToString()) : 0;
                float IndebtVal = ent.status == "مدين" ? float.Parse(ent.value.ToString()) : 0;
                Diar_Grd_List.Add(new DiaryEntrGrd()
                {
                    AccountNO = int.Parse(ent.SubAccount_id.ToString()),
                    CreditBalance = CreditVal,
                    IndebtBalance = IndebtVal,
                    description = ent.description,
                    RecordID =(int) ent.RecordID,
                    AccountName = AccountNameDrop.Items.FindByValue(ent.SubAccount_id.ToString()).Text

                });
                SumDaan += CreditVal;
                
                SumMaden += IndebtVal;
            }
          //  Diar_Grd_List.OrderByDescending(o => o.IndebtBalance);

            DiaryGrd.DataSource = Diar_Grd_List;
            DiaryGrd.DataBind();
            
            DiaryGrd.FooterRow.Cells[4].Text = SumDaan.ToString("#.00");
            DiaryGrd.FooterRow.Cells[3].Text = SumMaden.ToString("#.00");
            sum1credit = SumDaan;
            sumIndebt = SumMaden;
            }
            catch (Exception ex) { }
        }

        //الحفظ
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
        
            DateTxt.Text = ReformateDateFromPicker(DateTxt.Text);
        
            _entry = new Entry();
            Entry_List = new List<Entry>();
            float CreditSum = 0, IndebtSum = 0;

            foreach (GridViewRow row in DiaryGrd.Rows)
            {
                Label CreditSumlbl = row.FindControl("CreditBalanceLbl") as Label;
                Label IndebtSumlbl = row.FindControl("IndebtBalanceLbl") as Label;
                CreditSum += float.Parse(CreditSumlbl.Text);
                IndebtSum += float.Parse(IndebtSumlbl.Text);

            }

            if (CreditSum != IndebtSum)
            {
                AddError.Text = "يجب ان يكون طرف الدائن مساو لطرف المدين";
            }
            else
            {
                foreach (var entry in Diar_Grd_List)
                {
                    float value = entry.CreditBalance == 0 ? entry.IndebtBalance : entry.CreditBalance;
                    string status = entry.CreditBalance == 0 ? "مدين" : "دائن";

                    if (EditFalg == true)
                    {
                        Entry_List.Add(new Entry()
                        {
                            ID = int.Parse(EntryNoTxt.Text),
                            Date = DateTime.Parse(DateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG")),
                            value = decimal.Parse(value.ToString()),
                            status = status,
                            description = DescTxt.Text == "" ? entry.description : DescTxt.Text,
                            SubAccount_id = entry.AccountNO,
                            EntryID = int.Parse(EntryNoTxt.Text),
                            RecordID = entry.RecordID,




                        });
                    }
                    else {
                        Entry_List.Add(new Entry()
                        {

                            Date = DateTime.Parse(DateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG")),// DateTime.ParseExact(DateTxt.Text,"yyyy-MM-dd", CultureInfo.InvariantCulture),
                            value = decimal.Parse(value.ToString()),
                            status = status,
                            description = DescTxt.Text == "" ? entry.description : DescTxt.Text,
                            SubAccount_id = entry.AccountNO,
                            EntryID = int.Parse(EntryNoTxt.Text),
                            RecordID = entry.RecordID,
                            EntryType = "قيد حر"
                            
                        });
                    }
                }
                if (Entry_List.Count > 1 && Entry_List.Count > 0 && CreditSum == IndebtSum)
                {
                    if (EditFalg == true)
                    {
                        foreach(var en in Entry_List)
                        {
                            en.LoginID = ExtendedMethod.LoginedUser.Id;
                            db.Entry.AddOrUpdate(en);
                          //  _entry.Operations("Edit", Entry_List);
                           // db.Entry.add
                        }
                        //_entry.Operations("Edit", Entry_List);
                    }
                    else {
                        foreach (var en in Entry_List)
                        {
                            db.Entry.Add(en);
                        }
                        //_entry.Operations("Add", Entry_List);
                    }
                    db.SaveChanges();
                    AddError.Text = "تم الحفظ بنجاح";
                    AddError.ForeColor = System.Drawing.Color.Green;
                    Initialize_Page();
                }
                else
                {
                    AddError.Text = "يجب  الا يقل عدد اطراف القيد عن طرفين";
                    AddError.ForeColor = System.Drawing.Color.Red;
                }
            }

        }
        protected void DiaryGrd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label rowindx = (Label)DiaryGrd.Rows[e.RowIndex].FindControl("RowNoLbl");

            Diar_Grd_List.Remove(Diar_Grd_List.Where(o => o.RecordID == int.Parse(rowindx.Text)).ElementAtOrDefault(0));
            DiaryGrd.DataSource = Diar_Grd_List;
            DiaryGrd.DataBind();
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            try {
                SearchFn(int.Parse(EntryNoForSearch.Text));
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "تنبيه", "alert('من فضلك اختر قيمة صحيحة للبحث');",true);
            }
            }
        //الاول
        protected void FirstBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SearchFn(int.Parse(EntryNoForSearch.Items[1].Value));
                NextBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                AddError.Text = "لا يوجد نتائج";
                AddError.ForeColor = System.Drawing.Color.Red;
            }


        }
        //الاخير
        protected void LastBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int lastindx = EntryNoForSearch.Items.Count - 1;
                SearchFn(int.Parse(EntryNoForSearch.Items[lastindx].Value));
                PrevBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                AddError.Text = "لا يوجد نتائج";
                AddError.ForeColor = System.Drawing.Color.Red;
            }

        }
        //التالى
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SearchFn(int.Parse(EntryNoTxt.Text) + 1);
                PrevBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                AddError.Text = "لا يوجد نتائج";
                AddError.ForeColor = System.Drawing.Color.Red;
            }

        }
        //السابق
        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SearchFn(int.Parse(EntryNoTxt.Text) - 1);
                NextBtn.Enabled = true;
            }
            catch (Exception ex)
            {
                AddError.Text = "لا يوجد نتائج";
                AddError.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void DiaryGrd_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DiaryGrd.EditIndex = e.NewEditIndex;
            DiaryGrd.DataSource = Diar_Grd_List;
            DiaryGrd.DataBind();
        }

        protected void DiaryGrd_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)DiaryGrd.Rows[e.RowIndex];
            TextBox DescriptionText = DiaryGrd.Rows[e.RowIndex].FindControl("DescriptionTxt") as TextBox;//(TextBox)row.Cells[1].Controls[0];
            TextBox CreditTxt = DiaryGrd.Rows[e.RowIndex].FindControl("CreditBalanceTxt") as TextBox; //(TextBox)row.Cells[2].Controls[0];
            TextBox IndebtTxt = DiaryGrd.Rows[e.RowIndex].FindControl("IndebtBalanceTxt") as TextBox;// (TextBox)row.Cells[3].Controls[0];
            Label RowNoLbl = DiaryGrd.Rows[e.RowIndex].FindControl("RowNoLbl") as Label;//(TextBox)row.Cells[4].Controls[0];
            Diar_Grd_List.Where(o => o.RecordID == int.Parse(RowNoLbl.Text)).ElementAtOrDefault(0).CreditBalance = float.Parse(CreditTxt.Text);
            Diar_Grd_List.Where(o => o.RecordID == int.Parse(RowNoLbl.Text)).ElementAtOrDefault(0).IndebtBalance = float.Parse(IndebtTxt.Text);
            Diar_Grd_List.Where(o => o.RecordID == int.Parse(RowNoLbl.Text)).ElementAtOrDefault(0).description = DescriptionText.Text;
            DiaryGrd.DataSource = Diar_Grd_List;
            DiaryGrd.DataBind();
            DiaryGrd_RowCancelingEdit(sender, null);
        }

        protected void DiaryGrd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DiaryGrd.EditIndex = -1;
            DiaryGrd.DataSource = Diar_Grd_List;
            DiaryGrd.DataBind();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=قيد يومية.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    DiaryGrd.RenderControl(hw);
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

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            Initialize_Page();
        }



        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (EditFalg == true)
            {
                if (Diar_Grd_List.Count > 0)
                {
                    try
                    {
                        _entry = new Entry();
                        _entry = db.Entry.Where(en => en.EntryID == int.Parse(EntryNoTxt.Text)).FirstOrDefault();// _entry.GetAllEntryByID(int.Parse(EntryNoTxt.Text)).ElementAtOrDefault(0);
                        Entry_List = new List<Entry>();
                        Entry_List.Add(_entry);
                        db.Entry.RemoveRange(Entry_List);
                       // _entry.Operations("Delete", Entry_List);
                        if (_entry.EntryType == "قيد حر")
                        {

                        }
                        else if (_entry.EntryType == "قيد مقبوضات")
                        {
                            KhznaMoved Khzna_Moved = new KhznaMoved();
                            List<KhznaMoved> KhznaMoved_List = new List<KhznaMoved>();
                            KhznaMoved_List = db.KhznaMoved.ToList().Where(o=>o.state==false &o.EntryID==_entry.EntryID).ToList();
                          //  Khzna_Moved.GetAllKhznaMovedByEntryIDAndState(_entry.EntryID, 0);
                            foreach (var k in KhznaMoved_List)
                            {
                                k.EntryID = 0;
                                k.EntryState = false;
                                k.LoginID = ExtendedMethod.LoginedUser.Id;
                                db.KhznaMoved.AddOrUpdate(k);
                            }
                            //Khzna_Moved.Operations("Edit", KhznaMoved_List);

                        }
                        else if (_entry.EntryType == "قيد مدفوعات")
                        {
                            KhznaMoved Khzna_Moved = new KhznaMoved();
                            List<KhznaMoved> KhznaMoved_List = new List<KhznaMoved>();
                            KhznaMoved_List = db.KhznaMoved.ToList().Where(o => o.state == true & o.EntryID == _entry.EntryID).ToList();
                            foreach (var k in KhznaMoved_List)
                            {
                                k.EntryID = 0;
                                k.EntryState = false;
                                k.LoginID = ExtendedMethod.LoginedUser.Id;
                                db.KhznaMoved.AddOrUpdate(k);
                            }
                             //   Khzna_Moved.Operations("Edit", KhznaMoved_List);

                        }
                        else if (_entry.EntryType == "قيد مقبوضات شيك")
                        {

                            BankMoved  Bank_Moved = new BankMoved();
                            List<BankMoved> BankMoved_List = new List<BankMoved>();
                            BankMoved_List = db.BankMoved.ToList().Where(o => o.state == false & o.EntryID == _entry.EntryID).ToList();
                            foreach (var k in BankMoved_List)
                            {
                                k.EntryID = 0;
                                k.EntryState = false;
                                k.LoginID = ExtendedMethod.LoginedUser.Id;
                                db.BankMoved.AddOrUpdate(k);

                            }
                          //  Bank_Moved.Operations("Edit", BankMoved_List);
                        }
                        else if (_entry.EntryType == "قيد مدفوعات شيك")
                        {
                            BankMoved  Bank_Moved = new BankMoved();
                            List<BankMoved> BankMoved_List = new List<BankMoved>();
                            BankMoved_List = db.BankMoved.ToList().Where(o => o.state == true & o.EntryID == _entry.EntryID).ToList();
                            foreach (var k in BankMoved_List)
                            {
                                k.EntryID = 0;
                                k.EntryState = false;
                                k.LoginID = ExtendedMethod.LoginedUser.Id;
                                db.BankMoved.AddOrUpdate(k);
                            }
                            //Bank_Moved.Operations("Edit", BankMoved_List);
                        }
                        db.SaveChanges();
                        AddError.Text = "تم المسح بنجاح ";
                        AddError.ForeColor = System.Drawing.Color.Green;

                    }
                    catch (Exception ex)
                    {
                        AddError.Text = "حدث خطأ لا يمكن المسح ";
                        AddError.ForeColor = System.Drawing.Color.Red;
                    }
                    Initialize_Page();
                }
            }
        }




    }
}

   