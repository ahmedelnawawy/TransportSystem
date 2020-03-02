using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;
using TransportSystems.LayoutUsersControls;

namespace TransportSystems.Views.DashBoard
{
    public partial class AddRolePrivilege : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public Role _Role;
        public List<Role> RoleList;
        public List<Rol_PrivFT> RolPrivList = new List<Rol_PrivFT>();
        public Rol_PrivFT _RolPriv;
        public static bool EditFalg;
        public static bool UpdateMenu;

        public static List<PrGrd> grdList;
        public static Dictionary<int, string> PrivlageNameDict;

        [Serializable]
        public class PrGrd
        {
            public string Title { set; get; }
            public bool AddFlag { set; get; }
            public bool DeleteFlag { set; get; }
            public bool EditFlag { set; get; }
            public bool SearchFlag { set; get; }
            public bool AllFlag { set; get; }
            public int PrivilageID { set; get; }
            public int RolPrivFK { set; get; }
        }

        Label PrevID, Title, RolPrivFK;
        CheckBox Add, Edit, Search, Delete, All;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Initialize_Page();
            }
            if(PrivilageGridID.Rows.Count>0)
            {
                int RolID = int.Parse(RoleNoForSearchDrop.SelectedValue);
                foreach (GridViewRow row in PrivilageGridID.Rows)
                {
                    Add = (CheckBox)row.FindControl("Addchbx") as CheckBox;
                    Edit = (CheckBox)row.FindControl("Editchbx") as CheckBox;
                    Search = (CheckBox)row.FindControl("Searchchbx") as CheckBox;
                    Delete = (CheckBox)row.FindControl("Deletechbx") as CheckBox;
                    All = (CheckBox)row.FindControl("Allchbx") as CheckBox;
                    Title = (Label)row.FindControl("TitleTxt") as Label;
                    PrevID = (Label)row.FindControl("PrivilageID") as Label;
                    _RolPriv = new Rol_PrivFT();

                    if (EditFalg)// RolPrivFK
                    {
                        RolPrivFK = (Label)row.FindControl("RolPrivFKLBl") as Label;
                        _RolPriv.ID = int.Parse(RolPrivFK.Text);
                    }
                    _RolPriv.AddFlag = Add.Checked;
                    _RolPriv.EditFlag = Edit.Checked;
                    _RolPriv.DeleteFlag = Delete.Checked;
                    _RolPriv.SearchFlag = Search.Checked;
                    _RolPriv.AllFlag = All.Checked;
                    _RolPriv.Rol_id = RolID;
                    if (All.Checked)
                    {
                        _RolPriv.AddFlag = true;
                        _RolPriv.EditFlag = true;
                        _RolPriv.DeleteFlag = true;
                        _RolPriv.SearchFlag = true;
                    }
                    _RolPriv.Priv_id = int.Parse(PrevID.Text.ToString());
                    RolPrivList.Add(_RolPriv);
                }
            }
        }
        public void Initialize_Page()
        {
            EditFalg = false;
            AddErrorTxt.Text = "";
            AddErrorTxt.ForeColor = System.Drawing.Color.Red;
            RoleNoForSearchDrop.SelectedIndex = 0;
            _Role = new Role();
            RoleList = new List<Role>();
            RoleList = db.Role.ToList();
            RoleNoForSearchDrop.DataSource = RoleList;
            RoleNoForSearchDrop.DataTextField = "name";
            RoleNoForSearchDrop.DataValueField = "ID";
            RoleNoForSearchDrop.DataBind();
            _RolPriv = new Rol_PrivFT();
            RolPrivList = new List<Rol_PrivFT>();

            privilage _Priv = new privilage();
            List<privilage> PrivList = new List<privilage>();
            grdList = new List<PrGrd>();
            PrivList = db.privilage.ToList();
            foreach (var p in PrivList)
            {
                grdList.Add(new PrGrd()
                {
                    Title = p.Title,
                    AddFlag = false,
                    AllFlag = false,
                    DeleteFlag = false,
                    EditFlag = false,
                    SearchFlag = false,
                    PrivilageID = p.ID
                });
            }
            PrivilageGridID.DataSource = grdList;
            PrivilageGridID.DataBind();
            privilage _Privilage = new privilage();
            PrivlageNameDict = new Dictionary<int, string>();
            foreach (var p in db.privilage.ToList())
            {
                PrivlageNameDict.Add(p.ID, p.Title);
            }


        }

        protected void PrivilageGridID_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "50px");
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            int RolID = int.Parse(RoleNoForSearchDrop.SelectedValue);
            _RolPriv = new Rol_PrivFT();
            try
            {
                if (!EditFalg)
                {
                    Rol_PrivFT rp = new Rol_PrivFT();
                    var RoleID = int.Parse(RoleNoForSearchDrop.SelectedValue);
                    rp = db.Rol_PrivFT.Where(item => item.Rol_id == RoleID).FirstOrDefault();
                    if (rp == null)
                    {
                        foreach(var item in RolPrivList)
                        {
                            item.LoginID = ExtendedMethod.LoginedUser.Id;
                            db.Rol_PrivFT.Add(item);
                            db.SaveChanges();
                        }
                        Initialize_Page();
                        AddErrorTxt.Text = "تم الحفظ بنجاح";
                        AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        AddErrorTxt.Text = "هذه الوظيفة مضاف لها صلاحيات بالفعل الرجاء الضغط على بحث لرؤيتها";
                        AddErrorTxt.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    foreach (var item in RolPrivList)
                    {
                       item.LoginID = ExtendedMethod.LoginedUser.Id;

                        db.Rol_PrivFT.AddOrUpdate(item);
                        db.SaveChanges();
                    }
                    Aside.Menu_Start();
                    Initialize_Page();

                    AddErrorTxt.Text = "تم الحفظ بنجاح";
                    AddErrorTxt.ForeColor = System.Drawing.Color.Green;
                }


            }
            catch (Exception ex)
            {
                AddErrorTxt.Text = "لم يتم الحفظ";
                AddErrorTxt.ForeColor = System.Drawing.Color.Red;
            }

        }

        public void SearchFn(int RoleID)
        {
            UpdateMenu = true;
            AddErrorTxt.Text = "";
            EditFalg = true;
            _RolPriv = new Rol_PrivFT();
            RolPrivList = new List<Rol_PrivFT>();
            privilage _Privilage = new privilage();
            grdList = new List<PrGrd>();
            try
            {
                RolPrivList =db.Rol_PrivFT.Where(item => item.Rol_id == RoleID).ToList() ;
                //foreach (var rp in RolPrivList)
                //{
                //    string _PrivilageName = PrivlageNameDict[rp.Priv_id];
                //    grdList.Add(new PrGrd()
                //    {
                //        Title = _PrivilageName,
                //        AddFlag = rp.AddFlag,
                //        AllFlag = rp.AllFlag,
                //        DeleteFlag = rp.DeleteFlag,
                //        EditFlag = rp.EditFlag,
                //        SearchFlag = rp.SearchFlag,
                //        PrivilageID = rp.Priv_id,
                //        RolPrivFK=rp.ID

                //    });
                //}
                foreach (var rp in PrivlageNameDict)
                {
                    string _PrivilageName = rp.Value;

                    try
                    {
                        Rol_PrivFT RP = RolPrivList.Where(o => o.Priv_id == rp.Key).First();

                        grdList.Add(new PrGrd()
                        {
                            Title = _PrivilageName,
                            AddFlag =Convert.ToBoolean(RP.AddFlag),
                            AllFlag = Convert.ToBoolean(RP.AllFlag),
                            DeleteFlag = Convert.ToBoolean(RP.DeleteFlag),
                            EditFlag = Convert.ToBoolean(RP.EditFlag),
                            SearchFlag = Convert.ToBoolean(RP.SearchFlag),
                            PrivilageID =Convert.ToInt32(RP.Priv_id),
                            RolPrivFK = RP.ID

                        });
                    }
                    catch (Exception ex)
                    {
                        grdList.Add(new PrGrd()
                        {
                            Title = _PrivilageName,
                            AddFlag = false,
                            AllFlag = false,
                            DeleteFlag = false,
                            EditFlag = false,
                            SearchFlag = false,
                            PrivilageID = rp.Key,

                        });

                    }
                }
                PrivilageGridID.DataSource = grdList;
                PrivilageGridID.DataBind();

            }
            catch (Exception ex)
            {

            }
        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                SearchFn(int.Parse(RoleNoForSearchDrop.SelectedValue));

            }
            catch (Exception ex)
            {

            }
        }

        protected void PrivilageGridID_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PrivilageGridID.PageIndex = e.NewPageIndex;
            PrivilageGridID.DataSource = grdList;
            PrivilageGridID.DataBind();
        }

        protected void ResetBtn_Click(object sender, EventArgs e)
        {
            Initialize_Page();
        }
    }
}