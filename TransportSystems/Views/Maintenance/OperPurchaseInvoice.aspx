<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OperPurchaseInvoice.aspx.cs" Inherits="TransportSystems.Views.Maintenance.OperPurchaseInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         $(function () {
             $("#MainContent_InvoiceDate").datepicker({
                 dateFormat: 'yy-mm-dd'
             });
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="kt-portlet">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                 <h2 class="kt-portlet__head-title" style="color: orangered; font-family: 'Times New Roman', Times, serif; font-size: large">  
                     <%=Resources.Store.PurchaseInvoice %>
                </h2>
            </div>
        </div>
        <div class="kt-portlet__body">
            <div class="row">
                <%-- InvoiceNo --%>
                <div class="form-group col-md-3">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: larger; display: inline">
                        <%=Resources.Store.InvoiceNo%>
                    </label>
                    <asp:TextBox ID="InvoiceNo" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <%-- InvoiceDate --%>
                <div class="form-group col-md-3">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: larger; display: inline">
                        <%=Resources.Store.InvoiceDate%>
                    </label>
                    <asp:TextBox ID="InvoiceDate" type='text' runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
                <%-- vendors --%>
                <div class="form-group col-md-3">
                    <label class="mr-5" style="font-family: 'Times New Roman', Times, serif; font-size: larger; display: inline">
                        <%=Resources.Store.vendors %>
                    </label>
                    <asp:DropDownList ID="vendorsListtxt" runat="server"
                        CssClass="browser-default custom-select form-control" Width="100%">
                    </asp:DropDownList>
                </div>
                <%-- PurchaseType --%>
                <div class="form-group col-md-3" style="border:1px #dddddd solid;border-radius: 25px;">
                     <label style="display: inline";font-family: 'Times New Roman', Times, serif; font-size: larger><%=Resources.Store.PurchaseType %></label>
                     <div class="custom-control custom-radio mr-2" style="display: inline-block">
                         <asp:RadioButton ID="Purchase" GroupName="PurchaseType" runat="server" Checked="true" />
                         <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger" for="VoidedState"><%=Resources.Store.Purchase %></label>
                     </div>
                     <div class="custom-control custom-radio mr-2" style="display: inline-block">
                         <asp:RadioButton ID="Discarded" GroupName="PurchaseType" runat="server" />
                         <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger" for="VoidedState"><%=Resources.Store.Discarded %></label>
                     </div>
                </div>
            </div>
             <%-- products part --%>
             <div class="table-responsive">
                 <table class="table table-striped table-sm text-center">
                     <thead class="thead-light">
                         <tr style="font-size: larger; font-style: italic;">
                             <th style="color: #c40f57"><%=Resources.Store.FilterbyProductName %></th>
                             <th style="color: #000000"><%=Resources.Store.ProductName %></th>
                             <th style="color: #000000"><%=Resources.Store.ProductPrice %></th>
                             <th style="color: #000000"><%=Resources.Store.Qty %></th>
                             <th style="color: #000000"><%=Resources.Store.Insert %></th>
                         </tr>
                     </thead>
                     <tbody class="text-center">
                         <asp:HiddenField ID="HidenIdpurchasInvDe" runat="server" Value="0" />
                         <tr class="ng-star-inserted">
                             <td width="25%">
                                 <asp:TextBox ID="ProdNameFilt" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="ProdNameFilt_TextChanged"
                                     placeholder="<%$Resources:Store,FilterbyProductNamePH%>"></asp:TextBox>
                             </td>
                             <td width="25%">
                                 <asp:DropDownList ID="ProductListtxt" runat="server" AutoPostBack="true" 
                                     CssClass="browser-default custom-select form-control" Width="100%">
                                 </asp:DropDownList>
                             </td>
                             <td width="25%">
                                 <asp:TextBox TextMode="Number" ID="ProductPrice" runat="server" CssClass="form-control" min="0"
                                     placeholder="<%$Resources:Store,ProductPricePH%>"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                     ControlToValidate="ProductPrice" ValidationGroup="PurchaseGroup"
                                     CssClass="alert-danger" ErrorMessage="<%$Resources:Store,ProductPriceEM%>"></asp:RequiredFieldValidator>
                             </td>
                             <td width="25%">
                                 <asp:TextBox TextMode="Number" ID="Qty" runat="server" CssClass="form-control" min="0"
                                     placeholder="<%$Resources:Store,Qty%>"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                     ControlToValidate="Qty" ValidationGroup="PurchaseGroup"
                                     CssClass="alert-danger" ErrorMessage="<%$Resources:Store,QtyEM%>"></asp:RequiredFieldValidator>
                             </td>
                             <td>
                                 <asp:Button ID="Save_prodcut" runat="server" Text="<%$Resources:Store,Insert%>"
                                     CssClass="btn btn-success" OnClick="Save_prodcut_Click" />
                             </td>
                         </tr>
                         <tr>
                             <button disabled="disabled" class="btn btn-success white mt-2 ng-star-inserted">إضافة منتج</button>
                         </tr>
                     </tbody>
                 </table>
             </div>
             <div class="kt-section mb-0 mt-2">
                 <h3 class="kt-section__title mb-0">
                     <%=Resources.Store.Details %>
                 </h3>
             </div>
             <asp:GridView ID="GridView1" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                         AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                         runat="server" CssClass="table table-striped table-sm mb-0 text-center" GridLines="Horizontal">
                         <FooterStyle BackColor="White" ForeColor="#333333"></FooterStyle>
                         <HeaderStyle Font-Bold="True" CssClass="thead-light" ForeColor="White"></HeaderStyle>
                         <PagerStyle HorizontalAlign="Center" BackColor="#336666" ForeColor="White"></PagerStyle>
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
                             <asp:BoundField DataField="Product.name" HeaderText="<%$ Resources:Store,ProductName%>" />
                             <asp:BoundField DataField="ProductPrice" HeaderText="<%$ Resources:Store,ProductPrice%>" />
                             <asp:BoundField DataField="Qty" HeaderText="<%$ Resources:Store,Qty%>" />
                             <asp:BoundField DataField="PricePerRecord" HeaderText="<%$ Resources:Store,PricePerRecord%>" />
                             <asp:TemplateField>
                                 <HeaderTemplate>
                                     <%=Resources.Store.Tools%>
                                 </HeaderTemplate>
                                 <ItemTemplate>
                                     <asp:LinkButton ID="edit_product" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm" runat="server"
                                         CausesValidation="False" CommandArgument="<%# Container.DataItemIndex %>" OnCommand="edit_product_Command">
                                                            <div>
                                                                <i class="flaticon-edit-1"></i>
                                                            </div>
                                     </asp:LinkButton>
                                     <%--OnCommand="delete_Command"--%>
                                     <asp:LinkButton ID="delete" CssClass="btn btn-default btn-pill btn-sm btn-icon btn-icon-md custom-sm"
                                         OnClientClick="return confirm('Are you sure you would like Remove This Element?');"
                                         runat="server" CausesValidation="False" CommandArgument="<%# Container.DataItemIndex %>" OnCommand="delete_Command">
                                                            <i class="flaticon-delete"></i>
                                     </asp:LinkButton>
                                 </ItemTemplate>
                             </asp:TemplateField>
                         </Columns>
                         <EmptyDataTemplate>No Rows Available</EmptyDataTemplate>
                     </asp:GridView>
        </div>
        <div class="col-md-12 row " style="border: 1px dashed #ff6a00; border-radius: 25px; padding: 10px;">
            <div class="form-group col-md-3 ">
                <label class="mr-5 text-center" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
                    <%=Resources.Store.Total %>
                </label>
                <asp:TextBox ID="Total" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group col-md-3">
                <label class="mr-5 text-center" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
                    الــقيمة المـسددة
                </label>
                <div class="form-control col-md-12">
                    <asp:CheckBox ID="paymentMethod" runat="server" AutoPostBack="true" Checked="false"
                        OnCheckedChanged="paymentMethod_CheckedChanged" />
                     <label class="mr-5 text-center" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline;color:#ff6a00">
                        هل يوجد قيمة سداد
                    </label>
                </div>
            </div>
            <div id="PaymentValueId" runat="server" class="form-group col-md-3">
                <label class="mr-5 text-center" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
                    <%=Resources.Store.PaymentValue %>
                </label>
                <asp:TextBox ID="PaymentValue" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div  id="TreasuryId" runat="server" class="form-group col-md-3">
                <label class="mr-5" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
                    <%=Resources.Store.Treasury %>
                </label>
                <asp:DropDownList ID="TreasuryListtxt" runat="server"
                    CssClass="browser-default custom-select form-control" Width="100%">
                </asp:DropDownList>
            </div>
        </div>
        <div style="display: block" class="text-center">
            <asp:Label ID="AddErrorTxt" runat="server" Text="" Width="100%"></asp:Label>
        </div>
        <div class="text-center mt-3">
              <asp:Button ID="Save_Invoice" runat="server" Text="<%$Resources:Store,Save%>" CssClass="btn btn-success" OnClick="Save_Invoice_Click" />
              <asp:Button ID="close_Save_Invoice" runat="server" Text="<%$Resources:Store,Close%>" CssClass="btn btn-warning" OnClick="close_Save_Invoice_Click" />
          </div>
    </div>
</asp:Content>
