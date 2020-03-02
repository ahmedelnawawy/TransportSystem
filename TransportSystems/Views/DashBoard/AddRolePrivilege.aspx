<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRolePrivilege.aspx.cs" Inherits="TransportSystems.Views.DashBoard.AddRolePrivilege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#<%=PrivilageGridID.ClientID %>').Scrollable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table cellspacing="15" id="prtg" dir="rtl" class="table table-bordered" style="width: 100%;">
        <tr>
            <td colspan="3">
                <h1 style="text-align: center; color: red">شاشة الوظائف</h1>
                <asp:Button ID="ResetBtn" CssClass="btn btn-success" runat="server" Text="جديد" OnClick="ResetBtn_Click" />
            </td>
        </tr>
        <tr class="row right text-center">
            <td>
                <asp:Button ID="SaveBtn" CssClass="btn btn-success" runat="server" Text="حفظ" OnClick="SaveBtn_Click" />
                <asp:Label ID="AddErrorTxt" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="RoleNoForSearchDrop" CssClass="form-control" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="SearchBtn" CssClass="btn btn-success" runat="server" Text="بحث" OnClick="SearchBtn_Click" />
            </td>
        </tr>
        <td colspan="4">
            <table cellspacing="0" cellpadding="4" style="background-color: White; border-color: #336666; border-width: 3px; border-style: Double; width: 100%; border-collapse: collapse;">
                <tr style="color: White; background-color: #336666; font-weight: bold;">
                    <th class="text-center" scope="col">الشاشة</th>
                    <th class="text-center" scope="col">الاضافة</th>
                    <th class="text-center" scope="col">الحذف</th>
                    <th class="text-center" scope="col">التعديل</th>
                    <th class="text-center" scope="col">البحث</th>
                    <th class="text-center" scope="col">الكل</th>
                </tr>
            </table>
        </td>
        </tr>
        <tr>
            <td colspan="4">
                <div style="width: 100%; height: 500px; overflow: scroll">
                    <asp:GridView ID="PrivilageGridID"
                        runat="server" BackColor="White" BorderColor="#336666"
                        BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                        GridLines="Horizontal" AutoGenerateColumns="False" Width="100%" OnRowDataBound="PrivilageGridID_RowDataBound" OnPageIndexChanging="PrivilageGridID_PageIndexChanging" ShowHeader="False">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="الشاشة" ItemStyle-Width="27%">
                                <ItemTemplate>
                                    <asp:Label ID="PrivilageID" CssClass="text-center hidden" Text='<%# Eval("PrivilageID") %>' runat="server" />
                                    <asp:Label ID="RolPrivFKLBl" CssClass="text-center hidden" Text='<%# Eval("RolPrivFK") %>' runat="server" />
                                    <asp:Label ID="TitleTxt" CssClass="text-center" Text='<%# Eval("Title") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="الاضافة">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Addchbx" CssClass="text-center" Checked='<%# Eval("AddFlag") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="21%" HeaderText="الحذف">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Deletechbx" CssClass="text-center" Checked='<%# Eval("DeleteFlag") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="التعديل">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Editchbx" CssClass="text-center" Checked='<%# Eval("EditFlag") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="البحث">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Searchchbx" CssClass="text-center" Checked='<%# Eval("SearchFlag") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-Width="7%" HeaderText="الكل">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Allchbx" CssClass="text-center" Checked='<%# Eval("AllFlag") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333"></FooterStyle>
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White"></HeaderStyle>
                        <PagerStyle HorizontalAlign="Center" BackColor="#336666" ForeColor="White"></PagerStyle>
                        <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                        <SortedAscendingCellStyle BackColor="#F7F7F7"></SortedAscendingCellStyle>
                        <SortedAscendingHeaderStyle BackColor="#487575"></SortedAscendingHeaderStyle>
                        <SortedDescendingCellStyle BackColor="#E5E5E5"></SortedDescendingCellStyle>
                        <SortedDescendingHeaderStyle BackColor="#275353"></SortedDescendingHeaderStyle>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
