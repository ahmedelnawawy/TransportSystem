<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="TransportSystems.Views.Entries.AddProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h2 class="kt-portlet__head-title" style="font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Products %>
                </h2>
            </div>
            <div class="kt-portlet__head-toolbar">
                <div class="kt-portlet__head-actions">
                    <a class="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" title="Add" data-target="#ProductModal" data-toggle="modal">
                        <i class="flaticon2-add-1"></i>
                    </a>
                </div>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">
           <%-- Searching Part --%>
            <div class="row col-md-12">
                <div class="form-group col-md-4">
                    <label class="mr-4" style="font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Name %></label>
                    <asp:TextBox ID="ProductNametxt" runat="server" CssClass="form-control" Style="display: inline" CausesValidation="false"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label class="mr-2" style="display: inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.CategoryName%></label>
                    <asp:DropDownList ID="SectorSearchListtxt" runat="server"
                        CssClass="browser-default custom-select" Width="100%">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="text-center mt-2">
                <asp:Button ID="Search" runat="server" Text="<%$ Resources:Store,Search%>" CssClass="btn btn-success round mr-1" OnClick="Search_Click" CausesValidation="False"/>
                <%--OnClick="Search_Click"--%>
                <asp:Button ID="NewSearch" runat="server" Text="<%$ Resources:Store,NewSearch%>" CssClass="btn btn-success round mr-1" OnClick="NewSearch_Click" CausesValidation="False" />
                <%--OnClick="NewSearch_Click"--%>
            </div>
            <div class="table-responsive mt-2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                            runat="server" CssClass="table table-striped table-sm mb-0" GridLines="Horizontal">
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
                                <asp:BoundField DataField="name" HeaderText="<%$ Resources:Store,ProductName%>" />
                                <asp:BoundField DataField="PPrice" HeaderText="<%$ Resources:Store,PPrice%>" />
                                <asp:BoundField DataField="SPrice" HeaderText="<%$ Resources:Store,SPrice%>" />
                                <asp:BoundField DataField="MinBalance" HeaderText="<%$ Resources:Store,MinBalance%>" />
                                <asp:BoundField DataField="Description" HeaderText="<%$ Resources:Store,Description%>" />
                                <asp:BoundField DataField="CurrentBalance" HeaderText="<%$ Resources:Store,CurrentBalance%>" />
                                 <asp:BoundField DataField="Sector.Name" HeaderText="<%$ Resources:Store,CategoryName%>" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Resources.Store.Tools%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="edit" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" runat="server"
                                            CausesValidation="False" CommandArgument="<%# Container.DataItemIndex %>" OnCommand="edit_Command">
                                            <div data-target="#ProductModal" data-toggle="modal">
                                                <i class="flaticon-edit-1"></i>
                                            </div>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="delete" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm"
                                            runat="server" CausesValidation="False" OnClientClick="return confirm('Are you sure you would like Remove This Element?');"
                                            OnCommand="delete_Command" CommandArgument="<%# Container.DataItemIndex %>">
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
              <%-- Searching Part --%>
        </div>
    </div>
    <%--Modal of Add--%>
    <div class="modal fade " id="ProductModal" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4><%=Resources.Store.AddProduct%> </h4>
                    <button type="button" id="modal" class="close pull-right" data-dismiss="modal" aria-label="Close" value="Refresh Page" onclick="window.location.reload();">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div>
                        <div style="width: 100%">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                     <%-- ProductID --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display:inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.ProductID%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ProductIDTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,ProductIDEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="ProductIDTxt" runat="server" CssClass="form-control" placeholder="<%$Resources:Store,ProductIDPH%>"></asp:TextBox>
                                    </div>
                                    <%-- name --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display:inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Name%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NameTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,NameEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="NameTxt" runat="server" CssClass="form-control" placeholder="<%$Resources:Store,NamePH%>"></asp:TextBox>
                                    </div>
                                    <%-- Pprice --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display:inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.PPrice%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PPriceTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,PPriceEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="PPriceTxt" TextMode="Number" min="0" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <%-- Sprice --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display:inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.SPrice%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="SPriceTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,SPriceEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="SPriceTxt" TextMode="Number" min="0" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <%-- Min Balance --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display:inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.MinBalance%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="MinBalanceTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,MinBalanceEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="MinBalanceTxt" TextMode="Number" min="0" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <%-- Description --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display:inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Description%></label>
                                        <asp:TextBox ID="DescriptionTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <%-- CurrentBalance --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display:inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.CurrentBalance%></label>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="CurrentBalanceTxt" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,CurrentBalanceEM%>">*</asp:RequiredFieldValidator>

                                        <asp:TextBox ID="CurrentBalanceTxt" TextMode="Number" min="0" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <%-- Store List --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display:inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.StoreName%></label>
                                        <asp:DropDownList ID="AddStoreListhtml" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <%-- AddSector --%>
                                    <div style="width: 30%; display: inline-block;" class="form-group">
                                        <label style="display:inline;font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.CategoryName%></label>
                                        <asp:DropDownList ID="AddSectorListhtml" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div style="display:block" class="text-center">
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
