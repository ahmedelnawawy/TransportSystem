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
    public partial class Region : System.Web.UI.Page
    {
        private TransportModel db = new TransportModel();

        public static List<FromRegion> FromRegionList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // make custom ValidationGroup>>><<<<
                string vgId = Guid.NewGuid().ToString();

                FromRegion_Name.ValidationGroup = vgId;
                RequiredFieldValidator1.ValidationGroup = vgId;

                LatitudeTxt.ValidationGroup = vgId;

                LongitudeTxt.ValidationGroup = vgId;

                ValidationSummary1.ValidationGroup = vgId;
                Save.ValidationGroup = vgId;

                // get all subStore For frist time
                FromRegionList = new List<FromRegion>();
                FromRegionList = db.FromRegion.ToList();
            }
            BindGridList();
        }
        protected void BindGridList()
        {
            GridView1.DataSource = FromRegionList;
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridList();
        }
        protected FromRegion FromRegionModel()
        {
            FromRegion FromRegion = new FromRegion();

            if (FromRegionIdHid.Value != "0"){ FromRegion.Id = Convert.ToInt32(FromRegionIdHid.Value);}

            if (FromRegion_Name.Text != ""){FromRegion.Name = FromRegion_Name.Text;}
            
            if (txtlat.Value != ""){FromRegion.Latitude = txtlat.Value; }

            if (txtlong.Value != "") { FromRegion.Longitude = txtlong.Value; }
            FromRegion.LoginID = ExtendedMethod.LoginedUser.Id;
            return FromRegion;
        }
        protected void ClearModel()
        {
            FromRegionIdHid.Value = "0";
            FromRegion_Name.Text = "";
            LatitudeTxt.Text = "";
            LongitudeTxt.Text = "";
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            FromRegion FromRegion = FromRegionModel();
            if (FromRegion.Id == 0)
            {
                try
                {
                    db.FromRegion.Add(FromRegion);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                }
            }
            else
            {
                try
                {
                    db.FromRegion.AddOrUpdate(FromRegion);
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                }
            }
            ClearModel();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        protected void edit_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<FromRegion> gvr = (List<FromRegion>)GridView1.DataSource;
            FromRegion FromRegionEdit = gvr[index];
            FromRegionIdHid.Value = FromRegionEdit.Id.ToString();
            FromRegion_Name.Text = FromRegionEdit.Name;
            LatitudeTxt.Text = FromRegionEdit.Latitude;
            LongitudeTxt.Text = FromRegionEdit.Longitude;
        }
        protected void delete_Command(object sender, CommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            List<FromRegion> gvr = (List<FromRegion>)GridView1.DataSource;
            int deleted_index = gvr[index].Id;
            var deletedFromRegion = db.FromRegion.Where(SS => SS.Id == deleted_index).FirstOrDefault();
            db.FromRegion.Remove(deletedFromRegion);
            db.SaveChanges();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}