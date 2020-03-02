<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonyIn.aspx.cs" Inherits="TransportSystems.Views.DailyMovements.MonyIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_ReceivedDate").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            $("#MainContent_SarfDateTxt").datepicker({
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h1 class="kt-portlet__head-title" style="text-align: center; color: red; font-size: large;">سند قبض نقدية وشيك
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
                <tr class="row" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="نوع التعامل"></asp:Label>
                    </td>
                    <td class="col-3" style="margin: 5px; padding: 5px;">
                        <asp:DropDownList Style="font-size: 21px; padding: 5px;" ID="MonyTypeDrop" 
                            AutoPostBack="true" class="form-control" runat="server" OnSelectedIndexChanged="MonyTypeDrop_SelectedIndexChanged">
                            <asp:ListItem>نقدى</asp:ListItem>
                            <asp:ListItem>شيك</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td style="margin: 5px; padding: 5px;">
                       <asp:CheckBox ID="FinancialPos" runat="server" AutoPostBack="true" class="form-control" Style="font-size: 18px; padding: 5px; color:#ff6a00;" 
                           Text="هل يوجد مركز التكلفة" OnCheckedChanged="FinancialPos_CheckedChanged" />  
                    </td>
                </tr>
                <tr class="row " runat="server" id="TklfaRow" style="margin: 10px; padding: 5px;">
                    <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="نوع مركز التكلفة"></asp:Label>
                    </td>
                    <td class="col-3" style="margin: 5px; padding: 5px;">
                         <asp:DropDownList Style="font-size: 21px; padding: 5px;" ID="FinancialPosTypeList" 
                            AutoPostBack="true" class="form-control" runat="server" OnSelectedIndexChanged="FinancialPosTypeList_SelectedIndexChanged">
<%--                            <asp:ListItem>سائق</asp:ListItem>--%>
                            <asp:ListItem>عربة</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     <td id="RelatedFinancialPsList" class="col-3" style="margin: 5px; padding: 5px;">
                          <asp:DropDownList ID="AddRelatednumList" class="form-control" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr class="row " id="RecordIDRow" runat="server" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="رقم الاذن"></asp:Label>
                    </td>
                    <td class="col-6 text-danger">
                        <asp:TextBox ID="OperationID" class="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr class="row right" runat="server" id="ValueRow" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="القيمة"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Value" class="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr class="row right" runat="server" id="SubAccountRow" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="الى"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="FromSubAccountsID" class="form-control" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr class="row right" runat="server" id="ToKhaznaDropRow" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2" style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="من"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ToKhaznaDrop" class="form-control" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr class="row right" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="البيان/ملحوظة"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="DescTxt" cols="20" Rows="2" class="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr class="row right " style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="التاريح"></asp:Label>
                    </td>
                    <td>
                        <%--<cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="ReceivedDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>--%>
                        <asp:TextBox ID="ReceivedDate" class="form-control" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr class="row right" id="SarfDateRow" runat="server" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="تاريخ الصرف"></asp:Label>
                    </td>
                    <td>
                        <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="SarfDateTxt" Format="yyyy-MM-dd"></cc1:CalendarExtender>--%>
                        <asp:TextBox ID="SarfDateTxt" class="form-control" AutoCompleteType="Disabled" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr class="row right" id="ChecqRow" runat="server" style="margin: 5px; padding: 5px;">
                    <td class="row text-danger col-2 " style="margin: 5px; padding: 5px;">
                        <asp:Label runat="server" Text="رقم الشيك"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="ChequeNoTxt" class="form-control" runat="server"></asp:TextBox>
                    </td>
                </tr>
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
        </div>
    </div>
</asp:Content>
