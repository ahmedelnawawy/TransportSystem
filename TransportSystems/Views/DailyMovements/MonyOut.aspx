<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonyOut.aspx.cs" Inherits="TransportSystems.Views.DailyMovements.MonyOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#MainContent_ReceivedDate").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            $("#MainContent_SarfDateTxt").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            //On UpdatePanel Refresh.
            $(document).ready(function () {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            $("#MainContent_ReceivedDate").datepicker({
                                dateFormat: 'yy-mm-dd'
                            });
                            $("#MainContent_SarfDateTxt").datepicker({
                                dateFormat: 'yy-mm-dd'
                            });
                        }
                    });
                };
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h1 class="kt-portlet__head-title" style="text-align: center; color: red; font-size: large;">سند صرف نقدية وشيك
                </h1>
            </div>
            <div class="kt-portlet__head-toolbar">
                <div class="kt-portlet__head-actions">
                    <asp:Button ID="ResetBtn" CssClass="btn btn-success" runat="server" Text="جديد" OnClick="ResetBtn_Click" />
                </div>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="table table-bordered" dir="rtl">
                        <tr class="row right " style="margin: 5px; padding: 5px;">
                            <td class="row text-danger col-2  " style="margin: 5px; padding: 5px;">
                                <asp:Label runat="server" Text="نوع التعامل" Style="font-size: 18px;"></asp:Label>
                            </td>
                            <td class="col-3" style="margin: 5px; padding: 5px;">
                                <asp:DropDownList Style="font-size: 21px; padding: 5px;" ID="MonyTypeDrop" 
                                    AutoPostBack="true" class="form-control" runat="server" OnSelectedIndexChanged="MonyTypeDrop_SelectedIndexChanged">
                                    <asp:ListItem>نقدى</asp:ListItem>
                                    <asp:ListItem>شيك</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td runat="server" id="FinancialPos_td" style="margin: 5px; padding: 5px;">
                                <asp:CheckBox ID="FinancialPos" runat="server" AutoPostBack="true" class="form-control" Style="font-size: 18px; padding: 5px; color: #ff6a00;"
                                    Text="هل يوجد مركز التكلفة" OnCheckedChanged="FinancialPos_CheckedChanged" />
                            </td>
                        </tr>
                        <tr class="row" id="RecordIDRow" runat="server" style="margin: 5px; padding: 5px;">
                            <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                                <asp:Label runat="server" Text="رقم الاذن" Style="font-size: 18px;"></asp:Label>
                            </td>
                            <td class="col-6 text-danger">
                                <asp:TextBox ID="OperationID" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="row right" runat="server" id="ValueRow" style="margin: 5px; padding: 5px;">
                            <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                                <asp:Label runat="server" Text="القيمة" Style="font-size: 18px;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Value" class="form-control" AutoPostBack="true" runat="server" OnTextChanged="Value_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="row right" runat="server" id="FromKhaznaRow" style="margin: 5px; padding: 5px;">
                            <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                                <asp:Label runat="server" Text="من" Style="font-size: 18px;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="FromSubAccountsID" class="form-control" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="row right" runat="server" id="SubAccountRow" style="margin: 5px; padding: 5px;">
                            <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                                <asp:Label runat="server" Text="الى" Style="font-size: 18px;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="FromKhaznaDrop" class="form-control" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="row right" style="margin: 5px; padding: 5px;">
                            <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                                <asp:Label runat="server" Text="البيان/ملحوظة" Style="font-size: 18px;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="DescTxt" cols="20" Rows="2" class="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="row right " style="margin: 5px; padding: 5px;">
                            <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                                <asp:Label runat="server" Text="التاريح" Style="font-size: 18px;"></asp:Label>
                            </td>
                            <td>
                                <%--<cc1:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="ReceivedDate" format="yyyy-MM-dd"></cc1:calendarextender>--%>
                                <asp:TextBox ID="ReceivedDate" class="form-control" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="row right" id="SarfDateRow" runat="server" style="margin: 5px; padding: 5px;">
                            <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                                <asp:Label runat="server" Text="تاريخ الصرف" Style="font-size: 18px;"></asp:Label>
                            </td>
                            <td>
                                <%--<cc1:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="SarfDateTxt" format="yyyy-MM-dd"></cc1:calendarextender>--%>
                                <asp:TextBox ID="SarfDateTxt" class="form-control" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="row right" id="ChecqRow" runat="server" style="margin: 5px; padding: 5px;">
                            <td class="row text-danger  col-2" style="margin: 5px; padding: 5px;">
                                <asp:Label runat="server" Text="رقم الشيك" Style="font-size: 18px;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ChequeNoTxt" class="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <div runat="server" id="SolarRowID">
                            <tr class="row text-danger" style="margin: 5px; padding: 5px;">
                                <td class="col-md-12">
                                    <asp:Label runat="server" Text=" اســتـــعــاضـــة  الـــســـولار  :" Style="font-size: 20px;"></asp:Label>
                                </td>
                            </tr>
                            <tr  class="row text-danger" style="margin: 5px; padding: 5px;">
                                <td class="col-3" style="margin: 5px; padding: 5px;">
                                    <asp:Label runat="server" Text=" سيارة  :" Style="font-size: 20px;"></asp:Label>
                                </td>
                                <td id="RelatedFinancialPsList" class="col-3" style="margin: 5px; padding: 5px;">
                                    <asp:DropDownList ID="CarsListTxt" class="form-control" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr  class="row text-danger" style="margin: 5px; padding: 5px;">
                                <td class="col-3" style="margin: 5px; padding: 5px;">
                                    <asp:Label runat="server" Text=" الــســائــق  :" Style="font-size: 20px;"></asp:Label>
                                </td>
                                <td id="DriverListId" class="col-3" style="margin: 5px; padding: 5px;">
                                    <asp:DropDownList ID="DriverListTxt" class="form-control" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="row text-danger" style="margin: 5px; padding: 5px;">
                                <td class="col-md-1">
                                    <asp:Label runat="server" Text="القراءة السابقة" Style="font-size: 18px;"></asp:Label>
                                </td>
                                <td class="col-md-3">
                                    <asp:TextBox ID="LastReadTxt" AutoPostBack="true" class="form-control" runat="server" OnTextChanged="LastReadTxt_TextChanged"></asp:TextBox>
                                </td>
                                <td class="col-md-1">
                                    <asp:Label runat="server" Text="القراءة الحالية" Style="font-size: 18px;"></asp:Label>
                                </td>
                                <td class="col-md-3">
                                    <asp:TextBox ID="CurrentReadTxt" AutoPostBack="true" class="form-control" runat="server" OnTextChanged="CurrentReadTxt_TextChanged"></asp:TextBox>
                                </td>
                                <td class="col-md-1">
                                    <asp:Label runat="server" Text="المسافة المقطوعة" Style="font-size: 18px;"></asp:Label>
                                </td>
                                <td class="col-md-3">
                                    <asp:TextBox ID="DistanceTxt" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="row text-danger" style="margin: 5px; padding: 5px;">
                                <td class="col-md-1">
                                    <asp:Label runat="server" Text="نوع الطاقة" Style="font-size: 18px;"></asp:Label>
                                </td>
                                <td class="col-md-2">
                                    <asp:DropDownList ID="SolarTypeDrop" AutoPostBack="true" class="form-control" runat="server"
                                        OnSelectedIndexChanged="SolarTypeDrop_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td class="col-md-1">
                                    <asp:Label runat="server" Text="المبلغ" Style="font-size: 18px;"></asp:Label>
                                </td>
                                <td class="col-md-2">
                                    <asp:TextBox ID="SolarTotalTxt" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td class="col-md-1">
                                    <asp:Label runat="server" Text="كمية السولار" Style="font-size: 18px;"></asp:Label>
                                </td>
                                <td class="col-md-2">
                                    <asp:TextBox ID="SolarQtyTxt" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td class="col-md-1">
                                    <asp:Label runat="server" Text="المعدل" Style="font-size: 18px;"></asp:Label>
                                </td>
                                <td class="col-md-2">
                                    <asp:TextBox ID="AverageTxt" ReadOnly="true" class="form-control" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="row text-danger" style="margin: 5px; padding: 5px;">
                                <td class="col-md-1">
                                    <asp:Label runat="server" Text="ملحوظات"></asp:Label>
                                </td>
                                <td class="col-md-11">
                                    <textarea runat="server" id="Notes" class="col-11" cols="10" rows="5">
                                    </textarea>
                                </td>
                            </tr>
                        </div>
                        <!--Start Buttons Part-->
                        <tr>
                            <td colspan="1" style="text-align: center">
                                <asp:Button ID="Button1" Style="font-size: 25px; width: 75%;" class="btn btn-success" runat="server" Text="حفظ" OnClick="AddProductBtn_Click" />
                                <asp:Label ID="AddErrorTxt" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="1" style="text-align: center">
                                <asp:DropDownList ID="MonyInsForSerachDrop" CssClass="form-control" Style="display: inline-block" runat="server" OnSelectedIndexChanged="MonyInsForSerachDrop_SelectedIndexChanged"></asp:DropDownList>
                                <asp:Button ID="MonyInSearchBtn" Style="font-size: 25px; float: left; display: inline-block" CssClass="btn btn-success" runat="server" Text="بحث" OnClick="MonyInSearchBtn_Click" />
                            </td>
                            <td>
                                <asp:Button ID="FirstPurchaseBtn" CssClass="btn btn-success" Style="font-size: 18px; display: inline-block" runat="server" Text="الاول" OnClick="FirstPurchaseBtn_Click" />
                                <asp:Button ID="NextPurchaseBtn" CssClass="btn btn-success" Style="font-size: 18px; display: inline-block" Enabled="false" runat="server" Text="التالى" OnClick="NextPurchaseBtn_Click" />
                                <asp:Button ID="PrevPurchaseBtn" CssClass="btn btn-success" Style="font-size: 18px; display: inline-block" Enabled="false" runat="server" Text="السابق" OnClick="PrevPurchaseBtn_Click" />
                                <asp:Button ID="LastPurchaseBtn" CssClass="btn btn-success" Style="font-size: 18px; display: inline-block" runat="server" Text="الاخير" OnClick="LastPurchaseBtn_Click" />
                            </td>
                            <td>
                                <asp:Button ID="DeleteMonyInBtn" CssClass="btn btn-success" Style="font-size: 25px; display: inline-block" Enabled="false" runat="server" Text="مسح" OnClick="DeleteMonyInBtn_Click" />
                            </td>
                        </tr>
                        <!--End Buttons Part-->
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
