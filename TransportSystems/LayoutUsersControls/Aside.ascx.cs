using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using TransportSystems.EntityModel;
using TransportSystems.Views.DashBoard;

namespace TransportSystems.LayoutUsersControls
{
    public partial class Aside : System.Web.UI.UserControl
    {
        private static TransportModel db = new TransportModel();


        public static MenuItemCollection menuItems;
        public static List<string> ItemIcon = new List<string> { "flaticon2-protection", "glyphicon glyphicon-user", "glyphicon glyphicon-tasks", "glyphicon glyphicon-wrench", "la la-money", "glyphicon glyphicon-calendar", "flaticon-list"};
        public static privilage _Privilage;
        public static List<privilage> PrivilageList;
        protected void Page_Load(object sender, EventArgs e)
        {
            Menu_Start();
            BuildAsideMenu();
        }
        public static void Menu_Start()
        {
            menuItems = new MenuItemCollection();
            // First LI Main Page
            menuItems.Add(new MenuItem() { NavigateUrl = "../Views/Default.aspx", Text = "الشاشه الرئيسيه", Value = "1001" });
            // 2nd LI is Ul الادخلات
            int counter = 1;
            menuItems.Add(new MenuItem() { Text = "الأدخــلات", Value = "100" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/LookUpsViews", Text = "مــلــحـقات الـمـدخلات", Value = "1" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/ChartOfAccountsView", Text = "دليل الحسابات", Value = "2" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/AddDriver", Text = "أضافه سائق", Value = "3" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/AddCars", Text = "أضافه سيارة", Value = "4" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/AddCarMaintenance", Text = "مـتابعة الـصـيـانـة", Value = "5" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/AddProduct", Text = "أضافة صنف", Value = "6" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/AddClient", Text = "أضافة عميل", Value = "7" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/AddSupplier", Text = "أضافة مورد", Value = "8" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/Region", Text = "أضافة منطقه", Value = "9" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Entries/AddListPrice", Text = "قائمه اسعار", Value = "10" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "أضافه مصروفات نثريه", Value = "11" });
            // 3th LI is Ul الحركة اليوميه
            counter++;
            menuItems.Add(new MenuItem() { Text = "الحركة اليوميه", Value = "101" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/DailyMovements/MonyIn", Text = "سند قبض:نقدي/شيك", Value = "12" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/DailyMovements/MonyOut", Text = "سند صرف:نقدي/شيك", Value = "13" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/DailyMovements/AddTransportCommand", Text = "أمر نقل", Value = "14" });
            // 4th LI is Ul الـــصـيـانـة
            counter++;
            menuItems.Add(new MenuItem() { Text = "الـــصـيـانـة", Value = "102" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Maintenance/ListingPurchaseInvoice", Text = "أذن اسـتـلام بــضـاعـة", Value = "15" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Maintenance/ListingSalesInvoice", Text = "أذن صــرف بــضـاعـة", Value = "16" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "حــركـة الـصـيـانـة", Value = "17" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Maintenance/AddCarChangeRateOnDis", Text = "معدل التغيير والمسافة المقطوعه", Value = "41" });

            // 5th LI is Ul لوحه التحكم
            counter++;
            menuItems.Add(new MenuItem() { Text = "لوحه التحكم", Value = "103" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/DashBoard/AddUser", Text = "أضافة مستخدم", Value = "18" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/DashBoard/AddRolePrivilege", Text = "الصلاحيات", Value = "19" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "مراقبه المستخدم", Value = "20" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "ترحيل سنوي", Value = "21" });

            // 6th LI is Ul حسابات عامة
            counter++;
            menuItems.Add(new MenuItem() { Text = "حسابات عامة", Value = "104" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/GeneralAccounts/IncomsEntry", Text = "قيد مقبوضات", Value = "22" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/GeneralAccounts/OutcomsEntry", Text = "قيد مدفوعات", Value = "23" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/GeneralAccounts/PurshasesEntry", Text = "قيد المشتريات", Value = "24" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/GeneralAccounts/DiaryEntryView", Text = "قيد يوميه", Value = "25" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/GeneralAccounts/OstazAccount", Text = "حساب استاذ", Value = "26" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/GeneralAccounts/AssistantOstazAccount", Text = "حساب استاذ مساعد", Value = "27" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/GeneralAccounts/BalanceReview", Text = "ميزان مراجعه", Value = "28" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "حساب مركز مالي", Value = "29" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "حساب دخل", Value = "30" });
            // 7th LI is Ul حسابات عامة
            counter++;
            menuItems.Add(new MenuItem() { Text = "تقارير", Value = "105" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Reports/ItemsCard", Text = "كارتة صنف", Value = "31" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Reports/ClientOperationsView", Text = "كشف حساب عميل", Value = "32" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Reports/SupplierOperationsView", Text = "كشف حساب مورد", Value = "33" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "ارصده عملاء", Value = "34" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "ارصده موردين", Value = "35" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Reports/TrasnportOrderReport", Text = "تقارير بأوامر النقل", Value = "36" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Reports/CarMaintenanceCost", Text = "تقارير صيانه السيارت", Value = "37" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "حسابات يوميه", Value = "38" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "#", Text = "تقارير صيانه سيارة مفصل", Value = "39" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Reports/TreasuryReportView", Text = "تقرير خزينه", Value = "40" });
            menuItems[counter].ChildItems.Add(new MenuItem() { NavigateUrl = "../../Views/Reports/SolarOrPanzenReport", Text = "تقرير اسـتـعـاضـة السولار", Value = "42" });

            Rol_PrivFT RolePriv = new Rol_PrivFT();
            List<Rol_PrivFT> Role_PrivList = new List<Rol_PrivFT>();

            ///////////////////// get Current user name and its Role and all related Role privilege
            var LoginUsername = HttpContext.Current.User.Identity.Name;
            var LogUserRoleID = db.AspUser.Where(user => user.Username == LoginUsername).FirstOrDefault().RoleID;
            Role_PrivList = db.Rol_PrivFT.Where(item => item.Rol_id == LogUserRoleID).ToList();
            ///////////////////////////////////////////////////////////////////////////
            for (int i = 0; i < menuItems.Count; i++)
            {
                try
                {
                    RolePriv = new Rol_PrivFT();
                    RolePriv = Role_PrivList.Where(o => o.Priv_id == int.Parse(menuItems[i].Value)).FirstOrDefault();
                    if (RolePriv != null)
                    {
                        if (RolePriv.AddFlag == true || RolePriv.EditFlag == true || RolePriv.DeleteFlag == true || RolePriv.SearchFlag == true || RolePriv.AllFlag == true)
                        {
                            for (int j = 0; j < menuItems[i].ChildItems.Count; j++)
                            {
                                RolePriv = new Rol_PrivFT();
                                RolePriv = Role_PrivList.Where(o => o.Priv_id == int.Parse(menuItems[i].ChildItems[j].Value)).FirstOrDefault();
                                if (RolePriv == null)
                                {
                                    menuItems[i].ChildItems.Remove(menuItems[i].ChildItems[j]);
                                    j = -1;
                                }
                                else
                                if (RolePriv.AddFlag != true && RolePriv.EditFlag != true && RolePriv.DeleteFlag != true && RolePriv.SearchFlag != true && RolePriv.AllFlag != true)
                                {
                                    menuItems[i].ChildItems.Remove(menuItems[i].ChildItems[j]);
                                    j = -1;
                                }
                            }
                        }
                        else
                        {
                            menuItems.Remove(menuItems[i]);
                            i = -1;
                        }
                    }
                    else
                    {
                        menuItems.Clear();
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            AddRolePrivilege.UpdateMenu = false;
        }
        private void BuildAsideMenu()
        {
            MainUL.Controls.Clear();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuItems[i].ChildItems.Count > 0)
                {
                    createInnerMenu(menuItems[i].NavigateUrl, menuItems[i].Text, menuItems[i].Value, i);
                }
                else
                {
                    CreateMenuItem(menuItems[i].NavigateUrl, menuItems[i].Text, menuItems[i].Value,i);
                }
            }
        }
        private void CreateMenuItem(string NavUrl, string text, string IdVal,int ItrNo)
        {
            // add item to list
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "kt-menu__item");
            li.Attributes.Add("aria-haspopup", "true");
            li.Attributes.Add("id", ("Id" + IdVal));
            MainUL.Controls.Add(li);
            // add anchor to list item
            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("href", NavUrl);
            anchor.Attributes.Add("class", "kt-menu__link");
            li.Controls.Add(anchor);
            //item inner anchor
            HtmlGenericControl item = new HtmlGenericControl("i");
            item.Attributes.Add("class", "kt-menu__link-icon "+ ItemIcon[ItrNo]);
            anchor.Controls.Add(item);
            //span inner anchor
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.Attributes.Add("style", "font-size: 17px");
            span.Attributes.Add("class", "kt-menu__link-text");
            span.InnerText = text;
            anchor.Controls.Add(span);
        }
        private void createInnerMenu(string NavUrl, string text, string IdVal, int ItrNo)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "kt-menu__item  kt-menu__item--submenu");
            li.Attributes.Add("aria-haspopup", "true");
            li.Attributes.Add("data-ktmenu-submenu-toggle", "hover");
            MainUL.Controls.Add(li);
            // add anchor to list item///////////////////////////////////////////////////////////////
            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("href", "javascript:;");
            anchor.Attributes.Add("class", "kt-menu__link kt-menu__toggle");
            li.Controls.Add(anchor);
            //item inner anchor
            HtmlGenericControl item = new HtmlGenericControl("i");
            item.Attributes.Add("class", "kt-menu__link-icon "+ ItemIcon[ItrNo]);
            anchor.Controls.Add(item);
            //span inner anchor
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.Attributes.Add("style", "font-size: 17px");
            span.Attributes.Add("class", "kt-menu__link-text font-weight-bold");
            span.InnerText = text;
            anchor.Controls.Add(span);
            //span inner anchor
            HtmlGenericControl span2 = new HtmlGenericControl("span");
            span2.Attributes.Add("class", "kt-menu__link-badge");
            anchor.Controls.Add(span2);
            //item inner anchor
            HtmlGenericControl item2 = new HtmlGenericControl("i");
            item2.Attributes.Add("class", "kt-menu__ver-arrow la la-angle-right");
            anchor.Controls.Add(item2);
            // add div to list item///////////////////////////////////////////////////////////////
            HtmlGenericControl divSec = new HtmlGenericControl("div");
            divSec.Attributes.Add("class", "kt-menu__submenu");
            li.Controls.Add(divSec);
            //span inner div
            HtmlGenericControl divspan = new HtmlGenericControl("span");
            divspan.Attributes.Add("class", "kt-menu__arrow");
            divSec.Controls.Add(divspan);
            //ul inner div
            HtmlGenericControl UlDv = new HtmlGenericControl("ul");
            UlDv.Attributes.Add("runat", "server");
            UlDv.Attributes.Add("class", "kt-menu__subnav");
            divSec.Controls.Add(UlDv);
            for (int j = 0; j < menuItems[ItrNo].ChildItems.Count; j++)
            {
                CreateInnerMenuItem(
                     menuItems[ItrNo].ChildItems[j].NavigateUrl
                    , menuItems[ItrNo].ChildItems[j].Text
                    , menuItems[ItrNo].ChildItems[j].Value
                    , UlDv);
            }
        }
        private void CreateInnerMenuItem(string NavUrl, string text, string IdVal, HtmlGenericControl innerUl)
        {

            // add item to list
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.Attributes.Add("class", "kt-menu__item");
            li.Attributes.Add("aria-haspopup", "true");
            innerUl.Controls.Add(li);
            // add anchor to list item
            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("href", NavUrl);
            anchor.Attributes.Add("class", "kt-menu__link");
            li.Controls.Add(anchor);
            //span inner anchor
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.Attributes.Add("style", "font-size: 17px");
            span.Attributes.Add("class", "kt-menu__link-text");
            span.InnerText = text;
            anchor.Controls.Add(span);
        }
    }
}