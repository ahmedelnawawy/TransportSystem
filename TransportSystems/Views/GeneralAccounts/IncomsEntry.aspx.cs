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

namespace TransportSystems.Views.Reports
{
    public partial class IncomsEntry : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public class EntryGrd
        {
            public int ID { set; get; }
            public float Value { set; get; }
            public string Description { set; get; }
            public bool EntryState { set; get; }
            public int EntryID { set; get; }
            public int SubID { set; get; }
        }
        public static List<EntryGrd> EntryGrd_List;
        public SubAccount Sub_Account;
        public KhznaMoved khazna_Moved;
        public static List<KhznaMoved> Khazna_Moved_List;
        public Entry entry { set; get; }
        public List<Entry> Entry_List { set; get; }
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
            ToDateTxt.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            AccountDropID.DataSource = db.SubAccount.ToList().Where(o => o.MainAccount_id == 1103).ToList();//  Sub_Account.GetSubAccount_ByUpID(1103);
            AccountDropID.DataTextField = "name";
            AccountDropID.DataValueField = "ID";
            AccountDropID.DataBind();
            EntryGrdV.DataSource = null;
            EntryGrdV.DataBind();
            SaveBtn.Visible = false;

            Khazna_Moved_List = new List<KhznaMoved>();
            NewEntryTxtID.Text = "";
            TotalText.Text = "";
            TotalSum = 0;
        }
        protected void SaerchBtn_Click(object sender, EventArgs e)
        {
            RsltTxt.Text = "";
            SaveBtn.Visible = true;
            TotalSum = 0;
            khazna_Moved = new KhznaMoved();
            entry = new Entry();
            EntryGrd_List = new List<EntryGrd>();
            //entry.maxid();

            int maxid =(int)db.Entry.ToList().Max(o=>o.EntryID)+1; //entry.MoveID;
            Khazna_Moved_List = new List<KhznaMoved>();
            ToDateTxt.Text= ReformateDateFromPicker(ToDateTxt.Text);
            Khazna_Moved_List = db.KhznaMoved.ToList().Where(o=>o.AccountID== int.Parse(AccountDropID.SelectedValue)&
            o.state==false&o.Date<=ExtendedMethod.FormatDate(ToDateTxt.Text)).ToList();
            // khazna_Moved.GetAllKhznaMovedByKhaznaIDAndStateAndDate(int.Parse(AccountDropID.SelectedValue),
                // 0, ToDateTxt.Text);


            int i = 0;
            foreach (var move in Khazna_Moved_List)
            {
                if (i == 0)
                {
                    if (move.EntryState == false)
                    {
                        SaveBtn.Enabled = true;
                        i++;
                    }
                    else
                        SaveBtn.Enabled = false;

                }

                EntryGrd_List.Add(new EntryGrd()
                {
                    EntryID =(int) move.EntryID,
                    EntryState =(bool) move.EntryState,
                    Description = move.Description,
                    ID = move.ID,
                    Value = float.Parse(move.Value.ToString()),
                    SubID = int.Parse(move.AccountID.ToString()),

                });
            }


            NewEntryTxtID.Text = maxid.ToString();
            EntryGrdV.DataSource = EntryGrd_List;
            EntryGrdV.DataBind();
        }

        public static float TotalSum = 0;
        protected void EntryGrdV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // GridViewRow row = (GridViewRow)EntryGrdV.Rows[e.Row.RowIndex];

            // CheckBox EntryState = (CheckBox)e.Row.FindControl("EntryState") as CheckBox;//(CheckBox)row.Cells[6].Controls[0];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("EntryState");
                Label value = (Label)e.Row.FindControl("Value");
                Label lblSatatus = (Label)e.Row.FindControl("lblStatusCol");

                TotalSum += float.Parse(value.Text.ToString());
                TotalText.Text = TotalSum.ToString();

                if (EntryGrd_List[e.Row.RowIndex].EntryState == false)
                {
                    chk.Checked = false;
                }
                else
                {
                    chk.Checked = true;
                }

            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            Entry_List = new List<Entry>();
            entry = new Entry();
            khazna_Moved = new KhznaMoved();
            Entry_List.Add(new Entry()
            {
                EntryID = int.Parse(NewEntryTxtID.Text),
                SubAccount_id = int.Parse(AccountDropID.SelectedValue),
                Date = DateTime.Parse(ToDateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG")),
                description = "قيد جديد",
                status = "مدين",
                value = decimal.Parse(TotalText.Text),
                RecordID = 0
            });
            foreach (var Daan in EntryGrd_List)
            {
                Entry_List.Add(new Entry()
                {
                    SubAccount_id = int.Parse(Daan.SubID.ToString()),
                    Date = DateTime.Parse(ToDateTxt.Text, CultureInfo.CreateSpecificCulture("ar-EG")),
                    description = Daan.Description,
                    status = "دائن",
                    value = decimal.Parse(Daan.Value.ToString()),
                    EntryID = int.Parse(NewEntryTxtID.Text),
                    RecordID = Daan.ID,
                    EntryType = "قيد مقبوضات"
                });
            }

            foreach (var k in Khazna_Moved_List)
            {
                k.EntryState = true;
                k.EntryID = int.Parse(NewEntryTxtID.Text);
                k.LoginID = ExtendedMethod.LoginedUser.Id;
            }


            db.Entry.AddRange(Entry_List);
            //entry.Operations("Add", Entry_List);
            foreach(var en in Khazna_Moved_List)
            {
                en.LoginID =ExtendedMethod.LoginedUser.Id;
                db.KhznaMoved.AddOrUpdate(en);
            }
            db.SaveChanges();
            //khazna_Moved.Operations("Edit", Khazna_Moved_List);
            RsltTxt.Text = "تمت الحفظ بنجاح";
            RsltTxt.ForeColor = System.Drawing.Color.Green;
            Initialize_Page();
        }
    }
}