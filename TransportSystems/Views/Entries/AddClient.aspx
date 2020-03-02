<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClient.aspx.cs" Inherits="TransportSystems.Views.Entries.AddClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#MainContent_DateTxt").datepicker({
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h2 class="kt-portlet__head-title" style="font-family: 'Times New Roman', Times, serif; font-size: larger">شاشة العملاء
                </h2>
            </div>
            <div class="kt-portlet__head-toolbar">
                <div class="kt-portlet__head-actions">
                    <asp:Button ID="ResetBtn" CssClass="btn btn-success" runat="server" Text="جديد" OnClick="ResetBtn_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="kt-portlet__body p-2 pb-0">
        <table id="prtg" class="table table-bordered" dir="rtl" >
            <%-- sub acc table details --%>
            <tr class="row right " style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="كود الحساب"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="AccountIDTxt" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="row right " style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="اسم الـــعــمـيـل"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="AccountNameTxt" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <%-- index table details --%>
            <tr class="row right " id="Tr1" runat="server" style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="الـــفـئـة"></asp:Label>
                </td>
                <td class="col-md-2">
                    <asp:DropDownList ID="ClientCategoryList" CssClass="form-control" runat="server">
                        <asp:ListItem>شـــخـــص</asp:ListItem>
                        <asp:ListItem>شـــركـــات مباشــر</asp:ListItem>
                         <asp:ListItem>مكاتب دعايــة</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="row right " id="Tr3" runat="server" style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="نوع العميل"></asp:Label>
                </td>
                <td class="col-md-2">
                    <asp:DropDownList ID="ClentTypelist" CssClass="form-control" runat="server">
                        <asp:ListItem>مميز</asp:ListItem>
                        <asp:ListItem>غير مميز</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="row right " id="Tr2" runat="server" style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="أســـم الــمــبـيـعـات"></asp:Label>
                </td>
                <td class="col-md-2">
                    <asp:DropDownList ID="SalesUesrId" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="row right " style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="اسم الــشـخـص الـمسـؤل"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ResponsiblePersonName" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="row right " style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="رقـم الــشـخـص الـمسـؤل"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ResponsiblePersonPhone" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="row right " style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="رقـم أخــــر"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="AnotherResponsiblePersonPhone" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="row right" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Style="font-size: 18px;" Text="البريد الالكترونى"></asp:Label>
                    </td>

                    <td class="row text-danger " style="margin: 5px; padding: 5px;">
                        <asp:TextBox ID="EmailTxt" CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
            <tr class="row right" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Style="font-size: 18px;" Text="العنوان"></asp:Label>
                    </td>
                    <td class="row text-danger " style="margin: 5px; padding: 5px;">
                        <asp:TextBox ID="Addess" CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
            <tr class="row right" style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="رقم التسجيل الضريبى"></asp:Label>
                </td>
                <td class="row text-danger " style="margin: 5px; padding: 5px;">
                    <asp:TextBox ID="Sgl_TaxNO" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="row right" style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="الـــســجـل الــتـجـاري"></asp:Label>
                </td>
                <td class="row text-danger " style="margin: 5px; padding: 5px;">
                    <asp:TextBox ID="CommercialDocument" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="row right" style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="الملف الضريبى"></asp:Label>
                </td>
                <td class="row text-danger " style="margin: 5px; padding: 5px;">
                    <asp:TextBox ID="TaxDocument" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
             <%-- sub acc table details --%>
            <tr class="row right" runat="server" id="DateRow" style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="تـــاريخ أضافة عــمــيل"></asp:Label>
                </td>
                <td>
                    <%--<cc1:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="DateTxt" format="yyyy-MM-dd"></cc1:calendarextender>--%>
                    <asp:TextBox ID="DateTxt" class="form-control" AutoCompleteType="Disabled" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="row right " id="AccountStateRow" runat="server" style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="حالة الحساب"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="AccountStateDrop" CssClass="form-control" runat="server">
                        <asp:ListItem>دائن</asp:ListItem>
                        <asp:ListItem>مدين</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="row right " id="AccountBalanceRow" runat="server" style="margin: 5px; padding: 5px;">
                <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                    <asp:Label runat="server" Style="font-size: 18px;" Text="الرصيد"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="BalanceTxt" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="row right text-center" style="margin: 5px; padding: 5px;">
                <td>
                    <asp:Button ID="SaveBtn" CssClass="btn btn-success" runat="server" Text="حفظ" OnClick="SaveBtn_Click" />

                    <asp:Label ID="AddErrorTxt" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="AccountNoForSearchDrop" CssClass="form-control" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="SearchBtn" CssClass="btn btn-success" runat="server" Text="بحث" OnClick="SearchBtn_Click" />
                </td>
                <td>
                    <asp:Button ID="FirstBtn" CssClass="btn btn-success" runat="server" Text="الاول" OnClick="FirstBtn_Click" />
                    <asp:Button ID="NextBtn" CssClass="btn btn-success" runat="server" Text="التالى" OnClick="NextBtn_Click" />
                    <asp:Button ID="PrevBtn" CssClass="btn btn-success" runat="server" Text="السابق" OnClick="PrevBtn_Click" />
                    <asp:Button ID="LastBtn" CssClass="btn btn-success" runat="server" Text="الاخير" OnClick="LastBtn_Click" />
                    <asp:Button ID="Delete" CssClass="btn btn-success" runat="server" Text="حذف" OnClick="Delete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
