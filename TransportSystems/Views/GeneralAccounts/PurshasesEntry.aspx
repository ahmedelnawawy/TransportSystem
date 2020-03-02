<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurshasesEntry.aspx.cs" Inherits="TransportSystems.Views.GeneralAccounts.PurshasesEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_FromDateTxt").datepicker({
                dateFormat: 'yy-mm-dd'
            });
           
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

      <h1 style="text-align:center;color:red">قيد مشتريات يوم</h1>
   <table  class="table table-bordered" dir="rtl" style="float:right;margin:20px;width: 75%;">
       <tr class="row right ">
                <td class="row text-danger ">
                    <asp:label runat="server" style="font-size:18px;" text="فى"></asp:label>
                </td>
                <td>
             <asp:textbox id="FromDateTxt" class="form-control"  runat="server"></asp:textbox>
                </td>
            </tr>
      <tr class="row right">
            <td class="row text-danger ">
                    <asp:label runat="server" style="font-size:18px;" text="النوع"></asp:label>
                </td>
           <td>
               <asp:DropDownList ID="BillTypeDrop" CssClass="form-control" runat="server">
                   <asp:ListItem Value="true">شراء</asp:ListItem>
                   <asp:ListItem Value="false">مرتجع شراء</asp:ListItem>
               </asp:DropDownList>
           </td>
       </tr>
      
        
       
           
       <tr>
      <td style="text-align:center" colspan="3">
           
           <asp:Button ID="SaerchBtn" CssClass="btn btn-success" style="font-size:21px;" runat="server" Text="بحث" OnClick="SaerchBtn_Click"  />
          <asp:Button ID="SaveBtn" CssClass="btn btn-success" style="font-size:21px;" runat="server" Text="عمل قيد" OnClick="SaveBtn_Click"   />
       </td>
                    </tr>
       <tr>
      <td style="text-align:center" colspan="3">
          <asp:Label ID="addErrorTxt" runat="server" Text=""></asp:Label>
          </td>
                    </tr> 
       
      <tr>

          <td colspan="3"> 
<asp:GridView ID="AccountGrd" style="width:100%"  runat="server" BackColor="White"
     BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="False" ShowFooter="True">
     <Columns>
      <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رقم الحركة">
                        <ItemTemplate>
                            <asp:Label ID="OperationID" CssClass="form-control"   runat="server"  Text='<%# Eval("moveID") %>'></asp:Label>
                             </ItemTemplate>

<HeaderStyle CssClass="text-center"></HeaderStyle>
          </asp:TemplateField>
      
         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="مشتريات">
                        <ItemTemplate>
                            <asp:Label ID="IndebtBalance" CssClass="form-control"   runat="server"  Text='<%# double.Parse( Eval("SValue").ToString()).ToString("#.00") %>'></asp:Label>
                             </ItemTemplate>

<HeaderStyle CssClass="text-center"></HeaderStyle>
          </asp:TemplateField>

             
               
          
           <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رقم القيد">
                        <ItemTemplate>
                            <asp:Label ID="OperationDate" CssClass="form-control"   runat="server"  Text='<%# int.Parse( Eval("KeadNo").ToString()) %>'></asp:Label>
                             </ItemTemplate>

<HeaderStyle CssClass="text-center"></HeaderStyle>
               </asp:TemplateField>

         
          
         
         
          
         </Columns>
    

    <FooterStyle BackColor="White" ForeColor="#333333" />
    <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="White" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F7F7F7" />
    <SortedAscendingHeaderStyle BackColor="#487575" />
    <SortedDescendingCellStyle BackColor="#E5E5E5" />
    <SortedDescendingHeaderStyle BackColor="#275353" />
</asp:GridView>
              </td>
          </tr>

       </table>

</asp:Content>
