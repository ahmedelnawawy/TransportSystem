<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCarChangeRateOnDis.aspx.cs" Inherits="TransportSystems.Views.Maintenance.AddCarChangeRateOnDis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_DateBefore").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            $("#MainContent_DateAfter").datepicker({
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h2 class="kt-portlet__head-title" style="color: orangered; font-family: 'Times New Roman', Times, serif; font-size: large"><%=Resources.Store.CarChangeRateOnDis %>
                </h2>
            </div>
            <div class="kt-portlet__head-toolbar">
                <div class="kt-portlet__head-actions">
                    <a class="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" title="Add" data-target="#CarChangeRateOnDisModal" data-toggle="modal">
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
                                <asp:BoundField DataField="Before" HeaderText="<%$ Resources:Store,Before%>" />
                                <asp:BoundField DataField="DateBefore" HeaderText="<%$ Resources:Store,Date%>" />
                                <asp:BoundField DataField="AtHourBefore" HeaderText="<%$ Resources:Store,AtHour%>" />
                                <asp:BoundField DataField="After" HeaderText="<%$ Resources:Store,After%>" />
                                <asp:BoundField DataField="DateAfter" HeaderText="<%$ Resources:Store,Date%>" />
                                <asp:BoundField DataField="AtHourAfter" HeaderText="<%$ Resources:Store,AtHour%>" />
                                <asp:BoundField DataField="Description" HeaderText="<%$ Resources:Store,Description%>" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Resources.Store.Tools%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="edit" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" runat="server"
                                            CausesValidation="False" CommandArgument="<%# Container.DataItemIndex %>" OnCommand="edit_Command">
                                            <div data-target="#CarChangeRateOnDisModal" data-toggle="modal">
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
    <div class="modal fade " id="CarChangeRateOnDisModal" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 style="color: #ff6a00"><%=Resources.Store.AddCarChangeRateOnDis%> </h4>
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
                                    <div style="border: 1px dotted #808080 ; border-radius:15px;padding:5px;margin:5px">
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
                                        <%-- Description --%>
                                        <div style="width: 30%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Description%></label>

                                            <asp:TextBox ID="Description" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="border: 1px dotted #808080 ; border-radius:15px;padding:5px;margin:5px">
                                        <%-- Before --%>
                                        <div style="width: 30%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Before%></label>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Before" SetFocusOnError="true"
                                                CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,BeforeEM%>">*</asp:RequiredFieldValidator>

                                            <asp:TextBox ID="Before" runat="server" AutoPostBack="true" OnTextChanged="Before_TextChanged"
                                                CssClass="form-control" Text="number"></asp:TextBox>
                                        </div>
                                        <%-- DateBefore --%>
                                        <div style="width: 30%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Date%></label>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DateBefore" SetFocusOnError="true"
                                                CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,DateEM%>">*</asp:RequiredFieldValidator>

                                            <asp:TextBox ID="DateBefore" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <%-- AtHourBefore --%>
                                        <div style="width: 30%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AtHour%></label>
                                            <asp:TextBox ID="AtHourBefore" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="border: 1px dotted #808080; border-radius: 15px; padding: 5px; margin: 5px">
                                        <%-- After --%>
                                        <div style="width: 30%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.After%></label>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="After" SetFocusOnError="true"
                                                CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,AfterEM%>">*</asp:RequiredFieldValidator>

                                            <asp:TextBox ID="After" runat="server" AutoPostBack="true" OnTextChanged ="After_TextChanged"
                                                CssClass="form-control" Text="number"></asp:TextBox>
                                        </div>
                                        <%-- DateAfter --%>
                                        <div style="width: 30%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Date%></label>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DateAfter" SetFocusOnError="true"
                                                CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,DateEM%>">*</asp:RequiredFieldValidator>

                                            <asp:TextBox ID="DateAfter" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <%-- AtHourAfter --%>
                                        <div style="width: 30%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AtHour%></label>

                                            <asp:TextBox ID="AtHourAfter" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="text-center">
                                        <asp:Label ID="AddErrorTxt" runat="server" Text="" Width="100%"></asp:Label>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert-danger"
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

