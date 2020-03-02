<%@ EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChartOfAccountsView.aspx.cs" Inherits="TransportSystems.Views.Entries.ChartOfAccountsView" %>
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
     <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h1 class="kt-portlet__head-title" style="text-align: center; color: red; font-size: large;">دليل الحسابات
                </h1>
            </div>
            <div class="kt-portlet__head-toolbar">
                <div class="kt-portlet__head-actions">
                    <asp:Button ID="ResetBtn" CssClass="btn btn-success" runat="server" Text="جديد" OnClick="ResetBtn_Click" />
                </div>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">
            <table class="table table-bordered" dir="rtl">
                <tr class="row right " style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Style="font-size: 18px;" Text="الحساب الرئيسى"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="RootDrop" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="RootDrop_SelectedIndexChanged">
                            <asp:ListItem Value="1">اصول</asp:ListItem>
                            <asp:ListItem Value="2">خصوم</asp:ListItem>
                            <asp:ListItem Value="3">مصروفات</asp:ListItem>
                            <asp:ListItem Value="4">ايرادات</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>

                <tr class="row right " style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Style="font-size: 18px;" Text="المستوى"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="AccountLevelDrop" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="AccountLevelDrop_SelectedIndexChanged">
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>

                <tr class="row right " style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Style="font-size: 18px;" Text="نوع الحساب"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="AccountTypeDrop" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="AccountTypeDrop_SelectedIndexChanged">
                            <asp:ListItem>رئيسى</asp:ListItem>
                            <asp:ListItem>فرعى</asp:ListItem>
                        </asp:DropDownList>

                    </td>
                </tr>

                <tr class="row right " id="UpAccountRow" runat="server" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Style="font-size: 18px;" Text="الحساب الاعلى"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="UpAccountDrop" AutoPostBack="true" CssClass="form-control" runat="server" OnSelectedIndexChanged="UpAccountDrop_SelectedIndexChanged"></asp:DropDownList>

                    </td>
                </tr>

                <tr class="row right " style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Style="font-size: 18px;" Text="كود الحساب"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="AccountIDTxt" CssClass="form-control" disabled runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr class="row right " style=" margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Style="font-size: 18px;" Text="اسم الحساب"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="AccountNameTxt" CssClass="form-control" runat="server"></asp:TextBox>

                    </td>
                </tr>

                <tr class="row right" runat="server" id="DateRow" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Style="font-size: 18px;" Text="التاريخ"></asp:Label>
                    </td>
                    <td>
                       <%-- <cc1:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="DateTxt" format="yyyy-MM-dd"></cc1:calendarextender>--%>
                        <asp:TextBox ID="DateTxt" class="form-control" runat="server"></asp:TextBox>
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
                <div id="IndxPart" runat="server" style="margin: 5px; padding: 5px;">
                    <tr class="row right" style="margin: 5px; padding: 5px;">
                        <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                            <asp:Label runat="server" Style="font-size: 18px;" Text="رقم البطاقة"></asp:Label>
                        </td>
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:TextBox ID="PersonalID" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="row right" style="margin: 5px; padding: 5px;">
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:Label runat="server" Style="font-size: 18px;" Text="العنوان"></asp:Label>
                        </td>
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:TextBox ID="Addess" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="row right" style="margin: 5px; padding: 5px;">
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:Label runat="server" Style="font-size: 18px;" Text="رقم السجل الضريبى"></asp:Label>
                        </td>
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:TextBox ID="Sgl_TaxNO" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="row right" style="margin: 5px; padding: 5px;">
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:Label runat="server" Style="font-size: 18px;" Text="الملف الضريبى"></asp:Label>
                        </td>
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:TextBox ID="TaxDocument" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="row right" style="margin: 5px; padding: 5px;">
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:Label runat="server" Style="font-size: 18px;" Text="المأمورية التابع لها"></asp:Label>
                        </td>
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:TextBox ID="maamoriaTxt" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="row right" style="margin: 5px; padding: 5px;">
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:Label runat="server" Style="font-size: 18px;" Text="البريد الالكترونى"></asp:Label>
                        </td>
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:TextBox ID="EmailTxt" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="row right" style="margin: 5px; padding: 5px;">
                        <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                            <asp:Label runat="server" Style="font-size: 18px;" Text="رقم الموبايل"></asp:Label>
                        </td>
                        <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                            <asp:TextBox ID="MobileNoTxt" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </div>
                <tr class="row text-center" style="margin: 5px; padding: 5px;">
                    <td>
                        <asp:Button ID="SaveBtn" CssClass="btn btn-success" runat="server" Text="حفظ" OnClick="SaveBtn_Click" />

                        <asp:Label ID="AddErrorTxt" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <input type="text" id="search" class="form-control" placeholder="البحث باسم او كود الحساب" name="search" style="margin: 10px; width: 165px;" onkeyup="filter('AccountNoForSearchDrop')" />

                        <asp:DropDownList ID="AccountNoForSearchDrop" CssClass="form-control" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="AccountNoForSearchDrop_SelectedIndexChanged"></asp:DropDownList>
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
    </div>
</asp:Content>
