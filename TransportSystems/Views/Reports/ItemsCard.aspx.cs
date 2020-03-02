using ConnectDataBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static AdsSysWeb.Models.LmsaEntitiesDB;

namespace TransportSystems.Views.Reports
{
    public partial class ItemsCard : System.Web.UI.Page
    {
        ConnectDataBase.FillFunction fun1 = new ConnectDataBase.FillFunction();
        ConnectFunction fun = new ConnectFunction();
        InventoryReports.CardOfCategory coc = new AdsSysWeb.Models.LmsaEntitiesDB.InventoryReports.CardOfCategory();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtfrom.Text = DateTime.Now.Year - 1 + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                txtto.Text = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                fun1.FillDrop(drpitems, "product", "ID", "Name");
            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            List<InventoryReports.CardOfCategory> lcard = new List<InventoryReports.CardOfCategory>();

            InventoryReports inv = new InventoryReports();
            lcard = coc.imports(drpitems.SelectedValue, DateTime.Parse(txtfrom.Text, CultureInfo.CreateSpecificCulture("ar-EG")), DateTime.Parse(txtto.Text, CultureInfo.CreateSpecificCulture("ar-EG")));
            GridView1.DataSource = lcard;
            GridView1.DataBind();
            if (lcard.Count > 0)
            {
                coc = inv.Card_CalcQuntity(lcard);
                GridView1.FooterRow.Cells[1].Text = coc.imquantity.ToString();
                GridView1.FooterRow.Cells[2].Text = coc.exquantity.ToString();
                GridView1.FooterRow.Cells[0].Text = "اجماليات";
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //    Label lblname=null;
            //    Label lblid = null;
            //   try
            //    {
            //        //GridViewRow currentrow = (GridViewRow)(sender as Control).Parent.Parent;
            //         lblid = (Label)e.Row.FindControl("Label6");// currentrow.FindControl("Label6");
            //        lblname  = (Label)e.Row.FindControl("Label7");// currentrow.FindControl("Label7");

            //        lblname.Text = fun.FireSql("select name from SubAccount where id=" + Int64.Parse(lblid.Text) + "").ToString() + "" + coc.RType ;
            //        if (lblname.Text=="")
            //        {
            //            lblname.Text = "رصيد افتتاحى";
            //        }
            //    } 
            //    catch(Exception ex)
            //    {
            //        lblid = (Label)e.Row.FindControl("Label6");// currentrow.FindControl("Label6");

            //        lblname = (Label)e.Row.FindControl("Label7");
            //        lblname.Text = fun.FireSql("select name from SubStore where ID=" + Int64.Parse(lblid.Text) + "").ToString() + "" + coc.RType;


            //    };
        }

        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=كارتة صنف.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            {
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GridView1.RenderControl(hw);
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



    }
}