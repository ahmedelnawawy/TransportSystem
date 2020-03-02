<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCarMaintenance.aspx.cs" Inherits="TransportSystems.Views.Entries.AddCarMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .remember {
            color: orange;
        }

            .remember input {
                width: 20px;
                height: 20px;
                margin-left: 15px;
                margin-right: 5px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h2 class="kt-portlet__head-title" style="color: orangered; font-family: 'Times New Roman', Times, serif; font-size: large"><%=Resources.Store.CarMaintenance %>
                </h2>
            </div>
            <div class="kt-portlet__head-toolbar">
                <div class="kt-portlet__head-actions">
                    <a class="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" title="Add" data-target="#CarMaintenanceModal" data-toggle="modal">
                        <i class="flaticon2-add-1"></i>
                    </a>
                </div>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">
            <div class="row col-md-12">
                <%-- CarsSearchList --%>
                <div class="form-group col-md-4" style="width: 30%; display: inline-block;">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger">
                        <%=Resources.Store.CarNo%></label>
                    <asp:DropDownList ID="CarsSearchListtxt" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <%-- ServicesSearchList --%>
                <div class="form-group col-md-4" style="width: 30%; display: inline-block;">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger">
                        <%=Resources.Store.ServiceName%></label>
                    <asp:DropDownList ID="ServicesSearchListtxt" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="text-center mt-2">
                <asp:Button ID="Search" runat="server" Text="<%$ Resources:Store,Search%>" CssClass="btn btn-success round mr-1" OnClick="Search_Click" CausesValidation="False" />
                <%--OnClick="Search_Click"--%>
                <asp:Button ID="NewSearch" runat="server" Text="<%$ Resources:Store,NewSearch%>" CssClass="btn btn-success round mr-1" OnClick="NewSearch_Click" CausesValidation="False" />
                <%--OnClick="NewSearch_Click"--%>
            </div>
            <div class="table-responsive mt-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                            runat="server" CssClass="table table-striped text-center table-sm mb-0" GridLines="Horizontal">
                            <FooterStyle BackColor="White" ForeColor="#333333"></FooterStyle>
                            <HeaderStyle Font-Bold="True" CssClass="thead-light" ForeColor="White"></HeaderStyle>
                            <PagerStyle HorizontalAlign="Center" BackColor="#dfe1ea" ForeColor="#DDD"></PagerStyle>
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                            <SortedAscendingCellStyle BackColor="#F7F7F7"></SortedAscendingCellStyle>
                            <SortedAscendingHeaderStyle BackColor="#487575"></SortedAscendingHeaderStyle>
                            <SortedDescendingCellStyle BackColor="#E5E5E5"></SortedDescendingCellStyle>
                            <SortedDescendingHeaderStyle BackColor="#275353"></SortedDescendingHeaderStyle>
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        #
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Cars.CarNo" HeaderText="<%$ Resources:Store,CarNo%>" />
                                <asp:BoundField DataField="Services.Name" HeaderText="<%$ Resources:Store,ServiceName%>" />
                                <asp:BoundField DataField="ChangeRate" HeaderText="<%$ Resources:Store,ChangeRate%>" />
                                <asp:BoundField DataField="AlertRate" HeaderText="<%$ Resources:Store,AlertRate%>" />
                                <asp:TemplateField HeaderText="<%$ Resources:Store,PurchaseType%>">
                                    <ItemTemplate>
                                        <%# Convert.ToBoolean(Eval("HaveChangeRate")) ? "لها معدل تغيير" : "ليس لها"%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Resources.Store.Tools%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="edit" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" runat="server"
                                            CausesValidation="False" CommandArgument="<%# Container.DataItemIndex %>" OnCommand="edit_Command">
                                            <div data-target="#CarMaintenanceModal" data-toggle="modal">
                                                <i class="flaticon-edit-1"></i>
                                            </div>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="delete" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm"
                                            OnClientClick="return confirm('Are you sure you would like Remove This Element?');"
                                            runat="server" CausesValidation="False" OnCommand="delete_Command"
                                            CommandArgument="<%# Container.DataItemIndex %>">
                                            <i class="flaticon-delete"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>No Rows Available</EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <%--Modal of Add--%>
    <div class="modal fade " id="CarMaintenanceModal" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 style="color: #ff6a00"><%=Resources.Store.AddCarMaintenance%> </h4>
                    <button type="button" id="modal" class="close pull-right" data-dismiss="modal" aria-label="Close" value="Refresh Page" onclick="window.location.reload();">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div>
                        <div style="width: 100%">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:HiddenField ID="IdHid" runat="server" Value="0" />
                                    <%-- CarsList --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.CarNo%></label>
                                        <asp:DropDownList ID="AddCarsListhtml" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <%-- ServicesList --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.ServiceName%></label>
                                        <asp:DropDownList ID="AddServicesListhtml" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <%-- ChangeRate --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.ChangeRate%></label>
                                        <asp:TextBox ID="ChangeRateTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <%-- AlertRate --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AlertRate%></label>
                                        <asp:TextBox ID="AlertRateTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <%-- Have Change Rate --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group text-center">
                                        <div class="row align-items-center remember " style="border: 1px solid #d4cbcb; font-size: 15px; padding: 10px 0px; border-radius: 5px;">
                                            <asp:CheckBox ID="HaveChangeRate" runat="server" AutoPostBack="true" OnCheckedChanged="HaveChangeRate_CheckedChanged"/>
                                            <label style="font-family: 'Times New Roman', Times, serif; font-size: larger;width: 30%;">لها معدل تغيير</label>
                                        </div>
                                    </div>
                                    <%-- Save & Close Btn --%>
                                    <div class="text-center">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert-danger" ValidationGroup="ProdGroup"
                                            DisplayMode="List" Font-Names="Impact" />
                                        <br />
                                        <asp:Button ID="Save" runat="server" Text="<%$Resources:Store,Save%>" CssClass="btn btn-success" OnClick="Save_Click" />
                                        <button type="button" class="btn btn-warning" data-dismiss="modal" value="Refresh Page" onclick="window.location.reload();"
                                            aria-label="Close">
                                            <i class="la la-close"></i><%=Resources.Store.Close%></button>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
