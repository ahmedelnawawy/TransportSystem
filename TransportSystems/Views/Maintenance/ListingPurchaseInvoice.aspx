<%@ EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListingPurchaseInvoice.aspx.cs" Inherits="TransportSystems.Views.Maintenance.ListingPurchaseInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         $(function () {
             $("#MainContent_InvoiceDate").datepicker({
                 dateFormat: 'yy-mm-dd'
             });
             $("#MainContent_DateTotxt").datepicker({
                 dateFormat: 'yy-mm-dd'
             });
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                 <h2 class="kt-portlet__head-title" style="color: orangered; font-family: 'Times New Roman', Times, serif; font-size: large">  
                     <%=Resources.Store.PurchaseInvoice %>
                </h2>
            </div>
            <div class="kt-portlet__head-toolbar">
                <div class="kt-portlet__head-actions">
                    <a class="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" title="Add" href="../Maintenance/OperPurchaseInvoice.aspx"> 
                        <i class="flaticon2-add-1"></i>
                    </a>
                </div>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">
            <div class="row">
                <div class="form-group col-md-3">
                    <label class="mr-5" style="font-family: 'Times New Roman', Times, serif; font-size: larger; display: inline">
                        <%=Resources.Store.vendors %>
                    </label>
                    <asp:DropDownList ID="vendorsListtxt" runat="server"
                        CssClass="browser-default custom-select form-control" Width="100%">
                    </asp:DropDownList>
                </div>
                <div class="form-group col-md-3">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: larger; display: inline">
                        <%=Resources.Store.InvoiceNo%>
                    </label>
                    <asp:TextBox ID="InvoiceNo" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-3">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: larger; display: inline">
                        <%=Resources.Store.InvoiceDate%>
                    </label>
                    <asp:TextBox ID="InvoiceDate" type='text' runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <div class="form-group col-md-3" style="border:1px #dddddd solid;border-radius: 25px;">
                    <label style="display: inline";font-family: 'Times New Roman', Times, serif; font-size: larger><%=Resources.Store.PurchaseType %></label>
                    <br>
                    <div class="custom-control custom-radio mr-2" style="display: inline-block">
                        <asp:RadioButton ID="Purchase" GroupName="PurchaseType" runat="server" Checked="False" />
                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger" for="VoidedState"><%=Resources.Store.Purchase %></label>
                    </div>
                    <div class="custom-control custom-radio mr-2" style="display: inline-block">
                        <asp:RadioButton ID="Discarded" GroupName="PurchaseType" runat="server" Checked="False"/>
                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger" for="VoidedState"><%=Resources.Store.Discarded %></label>
                    </div>
                    <div class="custom-control custom-radio mr-2" style="display: inline-block">
                        <asp:RadioButton ID="RadioButton1" GroupName="PurchaseType" runat="server" Checked="True"/>
                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger" for="VoidedState">الـــكــل</label>
                    </div>
                 </div>
            </div>
            <div style="display: block" class="text-center">
                <asp:Label ID="AddErrorTxt" runat="server" Text="" Width="100%"></asp:Label>
            </div>
            <div class="text-center mt-2">
                <asp:Button ID="Search" runat="server" Text="<%$ Resources:Store,Search%>" CssClass="btn btn-success round mr-1" OnClick="Search_Click" CausesValidation="False" ValidationGroup="SearchGroup" />
                <asp:Button ID="NewSearch" runat="server" Text="<%$ Resources:Store,NewSearch%>" CssClass="btn btn-success round mr-1" OnClick="NewSearch_Click" CausesValidation="False" />
            </div>
            <div class="table-responsive mt-2">
                <asp:GridView ID="GridView1" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                    runat="server" CssClass="table table-striped table-sm mb-0 text-center" GridLines="Horizontal">
                    <FooterStyle BackColor="White" ForeColor="#333333"></FooterStyle>
                    <HeaderStyle Font-Bold="True" CssClass="thead-light" ForeColor="White"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Center" BackColor="#dddddd" ForeColor="black"></PagerStyle>
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
                        <asp:BoundField DataField="AspUser.Username" HeaderText="<%$ Resources:Store,Username%>" />
                        <asp:BoundField DataField="SubAccount.name" HeaderText="<%$ Resources:Store,Supplier%>" />
                        <asp:BoundField DataField="InvoiceDate" HeaderText="<%$ Resources:Store,InvoiceDate%>" />
                        <asp:TemplateField HeaderText="<%$ Resources:Store,PurchaseType%>">
                            <ItemTemplate>
                                 <%# Convert.ToBoolean(Eval("PurchaseType")) ? "مٌورد" : "مرتجع"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PaymentValue" HeaderText="<%$ Resources:Store,PaymentValue%>" />
                        <asp:BoundField DataField="PaymentMethod" HeaderText="<%$ Resources:Store,Indebtedness%>" />
                        <asp:BoundField DataField="Total" HeaderText="<%$ Resources:Store,Total%>" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%=Resources.Store.Tools%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="edit" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" runat="server"
                                    CausesValidation="False" CommandArgument="<%# Container.DataItemIndex %>" OnCommand="edit_Command">
                                             <i class="flaticon-edit-1"></i>
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
            </div>
        </div>
    </div>
</asp:Content>
