<%@  EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDriver.aspx.cs" Inherits="TransportSystems.Views.Entries.AddDriver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_LicenceDateTxt").datepicker({
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h2 class="kt-portlet__head-title" style="color: orangered; font-family: 'Times New Roman', Times, serif; font-size: large"><%=Resources.Store.Driver %>
                </h2>
            </div>
            <div class="kt-portlet__head-toolbar">
                <div class="kt-portlet__head-actions">
                    <a class="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" title="Add" data-target="#DriverModal" data-toggle="modal">
                        <i class="flaticon2-add-1"></i>
                    </a>
                </div>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">
            <div class="row col-md-12">
                <div class="form-group col-md-4" style="width: 30%; display: inline-block;">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Name %></label>
                    <asp:TextBox ID="DrivierNametxt" runat="server" CssClass="form-control" CausesValidation="false"></asp:TextBox>
                </div>
                <div class="form-group col-md-4" style="width: 30%; display: inline-block;">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.License %></label>
                    <asp:TextBox ID="LicenseNametxt" runat="server" CssClass="form-control" CausesValidation="false"></asp:TextBox>
                </div>
                <div class="form-group col-md-4" style="width: 30%; display: inline-block;">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Phone %></label>
                    <asp:TextBox ID="phoneNametxt" runat="server" CssClass="form-control" CausesValidation="false"></asp:TextBox>
                </div>
                <%-- <div class="form-group col-md-4" style="width: 30%; display: inline-block;">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.TrafficDepartmentName%></label>
                    <asp:DropDownList ID="TrafDepSearchListtxt" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>--%>
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
                                <asp:BoundField DataField="Name" HeaderText="<%$ Resources:Store,Name%>" />
                                <asp:BoundField DataField="Address" HeaderText="<%$ Resources:Store,Address%>" />
                                <asp:BoundField DataField="phone" HeaderText="<%$ Resources:Store,Phone%>" />
                                <asp:BoundField DataField="license" HeaderText="<%$ Resources:Store,License%>" />
                                <asp:BoundField DataField="LicenceDate" HeaderText="<%$ Resources:Store,LicenceDate%>" />
                                <asp:BoundField DataField="LicencePeriod" HeaderText="<%$ Resources:Store,LicencePeriod%>" />
                                <asp:BoundField DataField="LicenseEndDate" HeaderText="<%$ Resources:Store,LicenseEndDate%>" />
                                <asp:BoundField DataField="AlertPeriod" HeaderText="<%$ Resources:Store,AlertPeriod%>" />
                                <asp:BoundField DataField="TrafficDepartment.Name" HeaderText="<%$ Resources:Store,TrafficDepartmentName%>" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Resources.Store.Tools%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="edit" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" runat="server"
                                            CausesValidation="False" CommandArgument="<%# Container.DataItemIndex %>" OnCommand="edit_Command">
                                            <div data-target="#DriverModal" data-toggle="modal">
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
    <div class="modal fade " id="DriverModal" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 style="color: #ff6a00"><%=Resources.Store.AddDriver%> </h4>
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
                                    <%-- name --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Name%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,NameEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="NameTxt" runat="server" CssClass="form-control " placeholder="<%$Resources:Store,NamePH%>"></asp:TextBox>
                                    </div>
                                    <%-- address --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Address%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="AddressTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,AddressEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="AddressTxt" runat="server" CssClass="form-control" placeholder="<%$Resources:Store,AddressPH%>"></asp:TextBox>
                                    </div>
                                    <%-- phone --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Phone%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="PhoneTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,PhoneEMR%>">*</asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PhoneTxt"
                                            CssClass="alert-danger" ErrorMessage="<%$Resources:Store,PhoneEMV%>" ValidationExpression="(201)[0-9]{9}">*</asp:RegularExpressionValidator>

                                        <asp:TextBox ID="PhoneTxt" runat="server" CssClass="form-control" placeholder="<%$Resources:Store,PhonePH%>"></asp:TextBox>
                                    </div>
                                    <%-- license --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.License%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="licenseTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-5" ErrorMessage="<%$Resources:Store,LicenseEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="licenseTxt" runat="server" CssClass="form-control" placeholder="<%$Resources:Store,LicensePH%>"></asp:TextBox>
                                    </div>
                                    <%-- LicenceDate --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.LicenceDate%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="LicenceDateTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,LicenceDateEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="LicenceDateTxt" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="<%$Resources:Store,LicenceDatePH%>"></asp:TextBox>
                                    </div>
                                    <%-- LicencePeriod --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.LicencePeriod%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="LicencePeriodTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,LicencePeriodEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="LicencePeriodTxt" runat="server" CssClass="form-control" placeholder="<%$Resources:Store,LicencePeriodPH%>"></asp:TextBox>
                                    </div>
                                    <%-- AlertPeriod --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AlertPeriod%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="AlertPeriodTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,AlertPeriodEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="AlertPeriodTxt" runat="server" CssClass="form-control" placeholder="<%$Resources:Store,AlertPeriodPH%>"></asp:TextBox>
                                    </div>
                                    <%-- TrafficDepartmentList --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.TrafficDepartmentName%></label>
                                        <asp:DropDownList ID="AddTrafDepListhtml" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

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
