<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTransportCommand.aspx.cs" Inherits="TransportSystems.Views.DailyMovements.AddTransportCommand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#MainContent_TransCmdDate").datepicker({
                dateFormat: 'yy-mm-dd'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h2 class="kt-portlet__head-title" style="font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.TransportCommand %>
                </h2>
            </div>
            <div class="kt-portlet__head-toolbar">
                <div class="kt-portlet__head-actions">
                    <a class="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" title="Add" data-target="#TransCmdModal" data-toggle="modal">
                        <i class="flaticon2-add-1"></i>
                    </a>
                </div>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">
            <div class="row col-md-12">
                <%-- SubAccClient search list --%>
                <div class="form-group col-md-3">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.ClientName%></label>
                    <asp:DropDownList ID="SubClientSearchListtxt" runat="server" class="form-control" Width="100%">
                    </asp:DropDownList>
                </div>
                <%-- SubAccVendor search list --%>
                <div class="form-group col-md-3">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.SupplierName%></label>
                    <asp:DropDownList ID="SubVendorSearchListtxt" runat="server" class="form-control" Width="100%">
                    </asp:DropDownList>
                </div>
                <%-- Cars search list --%>
                <div class="form-group col-md-3">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.CarNo%></label>
                    <asp:DropDownList ID="CarSearchListtxt" runat="server" class="form-control" Width="100%">
                    </asp:DropDownList>
                </div>
                <%-- Product search list --%>
                <div class="form-group col-md-3">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.ProductType%></label>
                    <asp:DropDownList ID="ProductSearchListtxt" runat="server" class="form-control" Width="100%">
                    </asp:DropDownList>
                </div>
                <%-- FromRegion search list --%>
                <div class="form-group col-md-3">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.RegionFrom%></label>
                    <asp:DropDownList ID="FromRegionSearchListtxt" runat="server" class="form-control" Width="100%">
                    </asp:DropDownList>
                </div>
                <%-- ToRegion search list --%>
                <div class="form-group col-md-3">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.RegionTo%></label>
                    <asp:DropDownList ID="ToRegionSearchListtxt" runat="server" class="form-control" Width="100%">
                    </asp:DropDownList>
                </div>
                <%-- TransportType search list --%>
                <div class="form-group col-md-3">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.TransportType%></label>
                    <asp:DropDownList ID="TransTypeSearchListtxt" runat="server" class="form-control">
                        <asp:ListItem>اختار نوع النقل</asp:ListItem>
                        <asp:ListItem>بالــطــن</asp:ListItem>
                        <asp:ListItem>بالــحــمــولــة</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <%-- PayMeth search list --%>
                <div class="form-group col-md-3">
                    <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.PaymentMethod%></label>
                    <asp:DropDownList ID="PayMethSearchListtxt" runat="server" class="form-control">
                        <asp:ListItem>اختار نوع الدفع</asp:ListItem>
                        <asp:ListItem>نـــقـداً</asp:ListItem>
                        <asp:ListItem>أجــــل</asp:ListItem>
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
                                <asp:BoundField DataField="Id" HeaderText="#" />
                                
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="التاريخ">
                            <ItemTemplate>
                                <asp:Label ID="OperationDate" CssClass="form-control" runat="server" Text='<%#DateTime.Parse( Eval("TransportCommandTime").ToString()).Year+"-"+DateTime.Parse( Eval("TransportCommandTime").ToString()).Month+"-"+DateTime.Parse( Eval("TransportCommandTime").ToString()).Day %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:BoundField DataField="SubAccount.name" HeaderText="<%$ Resources:Store,ClientName%>" />
                                <asp:BoundField DataField="SubAccount1.name" HeaderText="<%$ Resources:Store,SupplierName%>" />
                                <asp:BoundField DataField="Cars.CarNo" HeaderText="<%$ Resources:Store,CarNo%>" />
                                <asp:BoundField DataField="Product.name" HeaderText="<%$ Resources:Store,ProductType%>" />
                                <asp:BoundField DataField="FromRegion.Name" HeaderText="<%$ Resources:Store,RegionFrom%>" />
                                <asp:BoundField DataField="FromRegion1.Name" HeaderText="<%$ Resources:Store,RegionTo%>" />
                                <asp:BoundField DataField="TransportPrice" HeaderText="<%$ Resources:Store,Price%>" />
                                <asp:BoundField DataField="Quantity" HeaderText="<%$ Resources:Store,Qty%>" />
                                <asp:BoundField DataField="TransportType" HeaderText="<%$ Resources:Store,TransportType%>" />
                                <asp:BoundField DataField="TotalTransportPrice" HeaderText="<%$ Resources:Store,TotalTransportPrice%>" />
                                <asp:BoundField DataField="PaymentWay" HeaderText="<%$ Resources:Store,PaymentMethod%>" />
                                <asp:BoundField DataField="TimeOfShipping" HeaderText="<%$ Resources:Store,AtHour%>" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Resources.Store.Tools%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="edit" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" runat="server"
                                            CausesValidation="False" CommandArgument="<%# Container.DataItemIndex %>" OnCommand="edit_Command">
                                            <div data-target="#TransCmdModal" data-toggle="modal">
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
    <div class="modal fade " id="TransCmdModal" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><%=Resources.Store.AddTransportCommand%> </h4>
                    <button type="button" id="modal" class="close pull-right" data-dismiss="modal" aria-label="Close" value="Refresh Page" onclick="window.location.reload();">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div>
                        <div style="width: 100%">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                                <ContentTemplate>
                                    <div style="width: 100%; border: 1px dotted #808080; border-radius: 25px; padding: 10px; margin: 10px;">
                                        <%-- TransCmdID --%>
                                        <div style="width: 39%; display: inline-block;" class="form-group">
                                            <div style="width: 20%; display: inline-block;">
                                                <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.TransCmdID%></label>
                                            </div>
                                            <div style="width: 60%; display: inline-block;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TransCmdTxt" SetFocusOnError="true"
                                                    CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,TransCmdIDEM%>">*</asp:RequiredFieldValidator>
                                                <asp:TextBox ID="TransCmdTxt" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <%-- TransCmdDate --%>
                                        <div style="width: 39%; display: inline-block;" class="form-group">
                                            <div style="width: 20%; display: inline-block;">
                                                <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.DateOfTransport%></label>
                                            </div>
                                            <div style="width: 60%; display: inline-block;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TransCmdDate" SetFocusOnError="true"
                                                    CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,DateEM%>">*</asp:RequiredFieldValidator>
                                                <asp:TextBox ID="TransCmdDate" type='text' runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; padding: 10px; margin: 10px;">
                                        <%-- Client List --%>
                                        <div style="width: 49%; display: inline-block;" class="form-group">
                                            <div style="width: 25%; display: inline-block;">
                                                <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.ClientName%></label>
                                            </div>
                                            <div style="width: 60%; display: inline-block;">
                                                <asp:DropDownList ID="AddClientListhtml" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- Vendor List --%>
                                        <div style="width: 49%; display: inline-block;" class="form-group">
                                            <div style="width: 25%; display: inline-block;">
                                                <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.SupplierName%></label>
                                            </div>
                                            <div style="width: 60%; display: inline-block;">
                                                <asp:DropDownList ID="AddVendorListhtml" runat="server"
                                                     OnSelectedIndexChanged="AddVendorListhtml_SelectedIndexChanged"
                                                    AutoPostBack="true" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- Cars List --%>
                                        <div style="width: 49%; display: inline-block;" class="form-group">
                                            <div style="width: 25%; display: inline-block;">
                                                <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.CarNo%></label>
                                            </div>
                                            <div style="width: 60%; display: inline-block;">
                                                <asp:DropDownList ID="AddCarsListhtml" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%-- Product List --%>
                                        <div style="width: 49%; display: inline-block;" class="form-group">
                                            <div style="width: 25%; display: inline-block;">
                                                <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.ProductType%></label>
                                            </div>
                                            <div style="width: 60%; display: inline-block;">
                                                <asp:DropDownList ID="AddProductListhtml" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 100%; border: 1px solid #808080; border-radius: 25px; padding: 10px; margin: 10px;">
                                        <%-- FromRegion List --%>
                                        <div style="width: 24%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.RegionFrom%></label>
                                            <asp:DropDownList ID="AddFromRegionListhtml" runat="server" OnSelectedIndexChanged="AddFromRegionListhtml_SelectedIndexChanged"
                                                CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <%-- RegionTo List --%>
                                        <div style="width: 24%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.RegionTo%></label>
                                            <asp:DropDownList ID="AddRegionToListhtml" runat="server" OnSelectedIndexChanged="AddRegionToListhtml_SelectedIndexChanged"
                                                AutoPostBack="true" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <%-- TransportPrice --%>
                                        <div style="width: 24%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Price%></label>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TransportPrice" SetFocusOnError="true"
                                                CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,PriceEM%>">*</asp:RequiredFieldValidator>

                                            <asp:TextBox ID="TransportPrice" TextMode="Number" min="0" runat="server" OnTextChanged="TransportPrice_TextChanged"
                                                CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <%-- Quantity --%>
                                        <div style="width: 24%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Qty%></label>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Quantity" SetFocusOnError="true"
                                                CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,QtyEM%>">*</asp:RequiredFieldValidator>

                                            <asp:TextBox ID="Quantity" TextMode="Number" min="0" OnTextChanged="Quantity_TextChanged"
                                                runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                        <%-- TransportType --%>
                                        <div style="width: 24%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.TransportType%></label>
                                            <asp:DropDownList ID="AddTransportTypeListhtml" runat="server"
                                                AutoPostBack="true" class="form-control" OnSelectedIndexChanged="AddTransportTypeListhtml_SelectedIndexChanged">
                                                <asp:ListItem>بالــطــن</asp:ListItem>
                                                <asp:ListItem>بالــحــمــولــة</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <%-- TotalTransportPrice --%>
                                        <div style="width: 24%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.TotalTransportPrice%></label>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Quantity" SetFocusOnError="true"
                                                CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,TotalTransportPriceEM%>">*</asp:RequiredFieldValidator>

                                            <asp:TextBox ID="TotalTransportPrice" TextMode="Number" min="0" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <%-- PaymentMethod --%>
                                        <div style="width: 24%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.PaymentMethod%></label>
                                            <asp:DropDownList ID="AddPaymentMethodListhtml" runat="server"
                                                AutoPostBack="true" class="form-control" OnSelectedIndexChanged="AddPaymentMethodListhtml_SelectedIndexChanged">
                                                <asp:ListItem>نـــقـداً</asp:ListItem>
                                                <asp:ListItem>أجــــل</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <%-- Time --%>
                                        <div style="width: 24%; display: inline-block;" class="form-group">
                                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AtHour%></label>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="AtHour" SetFocusOnError="true"
                                                CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,AtHourEM%>">*</asp:RequiredFieldValidator>

                                            <asp:TextBox ID="AtHour" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div style="display: block" class="text-center">
                                        <asp:Label ID="AddErrorTxt" runat="server" Text="" Width="100%"></asp:Label>
                                    </div>
                                    <div class="text-center">
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
