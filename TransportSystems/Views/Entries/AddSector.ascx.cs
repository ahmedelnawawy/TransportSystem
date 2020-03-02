using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportSystems.EntityModel;
using TransportSystems.EntityModel.Extended;

namespace TransportSystems.Views.Entries
{
    public partial class AddSector : System.Web.UI.UserControl
    {
        private TransportModel db = new TransportModel();

        public static bool flageoper = false;
        Sector mySector = new Sector();
        List<Sector> Sectorlist;
        //User curuser = new User();
        public static string selectedvalue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // make custom ValidationGroup>>><<<<
                string vgId = Guid.NewGuid().ToString();
                Cat_Name.ValidationGroup = vgId;
                RequiredFieldValidator4.ValidationGroup = vgId;
                ValidationSummary1.ValidationGroup = vgId;
                Save_Cat.ValidationGroup = vgId;
            }
            selectedvalue = DropMatList.SelectedValue;
            Sectorlist = (List<Sector>)db.Sector.ToList();
            DropMatList.DataSource = Sectorlist;
            DropMatList.DataBind();
            DropMatList.DataTextField = "Name";
            DropMatList.DataValueField = "ID";
            DropMatList.DataBind();
            BindGridList();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = Sectorlist;
            GridView1.DataBind();
        }
        protected Sector SectorModel()
        {

            Sector Sectortemp = new Sector();
            if (IdHid.Value != "")
            {
                Sectortemp.id = Convert.ToInt32(IdHid.Value);
            }
            if (Cat_Name.Text != "")
            {
                Sectortemp.name = Cat_Name.Text;
            }
            Sectortemp.LoginID =  ExtendedMethod.LoginedUser.Id;
            
            return Sectortemp;
        }
        protected void ClearModel()
        {
            IdHid.Value = "";
            Cat_Name.Text = "";
        }
        protected void Save_Cat_Click(object sender, EventArgs e)
        {
            mySector = SectorModel();
            if (mySector.id == 0)
            {
                db.Sector.Add(mySector);
            }
            else
            {
                db.Sector.AddOrUpdate(mySector);
            }
            try {
                db.SaveChanges();
            }catch(Exception ex)
            {

            }
                ClearModel();
            Page_Load(Save_Cat, null);
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<Sector> gvr = (List<Sector>)GridView1.DataSource;
            int deleted_index = gvr[index].id;
            db.Sector.Remove(gvr[index]);
            db.SaveChanges();
            // mySector.Operations("Delete", deleted_index);
            Page_Load(Save_Cat, null);
        }
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<Sector> gvr = (List<Sector>)GridView1.DataSource;
            Sector SectorEdit = gvr[index];
            IdHid.Value = SectorEdit.id.ToString();
            Cat_Name.Text = SectorEdit.name;
        }
        public string text
        {
            get
            {
                return selectedvalue;
            }
            set
            {
                DropMatList.SelectedValue = value;
            }
        }
        public string ValidationGroup
        {
            get
            {
                return Save_Cat.ValidationGroup;
            }
            set
            {
                // make custom ValidationGroup>>><<<<
                Cat_Name.ValidationGroup = value;
                RequiredFieldValidator4.ValidationGroup = value;
                ValidationSummary1.ValidationGroup = value;
            }
        }
    }
}