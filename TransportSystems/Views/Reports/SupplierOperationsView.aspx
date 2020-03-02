<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierOperationsView.aspx.cs" Inherits="TransportSystems.Views.Reports.SupplierOperationsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#MainContent_FromDateTxt").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            $("#MainContent_ToDateTxt").datepicker({
                dateFormat: 'yy-mm-dd'
            });

        });
        function PrintGridData(prtg) {
    document.getElementById(prtg).style.visibility = "visible";
    var prtGrid = document.getElementById(prtg);//<%--'<%=prtg.ClientID %>');--%>

    prtGrid.border = 2;
    var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
    prtwin.document.write(prtGrid.outerHTML);
    prtwin.document.close();
    prtwin.focus();
    prtwin.print();
    prtwin.close();
    prtGrid.style.visibility = 'hidden';

}
//دى للى هيطبع بشكل مباشر
function PrintGridDataCustom(prtg) {
    // document.getElementById(prtg).style.visibility = "visible";
    var divsToHide = document.getElementsByClassName("NotToPrint");
    for (var i = 0; i < divsToHide.length; i++) {
        //  divsToHide[i].style.visibility = "hidden"; // or
        divsToHide[i].style.display = "none"; // depending on what you're doing
    }

    var prtGrid = document.getElementById(prtg);//<%--'<%=prtg.ClientID %>');--%>

    prtGrid.border = 2;
    var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
    prtwin.document.write(prtGrid.outerHTML);
    prtwin.document.close();
    prtwin.focus();
    prtwin.print();
    prtwin.close();
    for (var i = 0; i < divsToHide.length; i++) {
        //  divsToHide[i].style.visibility = "visible"; // or
        divsToHide[i].style.display = ""; // depending on what you're doing
    }
}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <h1 style="text-align:center;color:red">كشف حساب مورد</h1>
   <table  class="table table-bordered" id="prtg" dir="rtl" style="float:right;margin:20px;width: 75%;">
       <tr class="row right ">
                <td class="row text-danger ">
                    <asp:label runat="server" style="font-size:18px;" text="من"></asp:label>
                </td>
                <td>
             <asp:textbox id="FromDateTxt" class="form-control"  runat="server"></asp:textbox>
                </td>
            </tr>
    <tr class="row right">
                <td class="row text-danger ">
                    <asp:label runat="server" style="font-size:18px;" text="الى"></asp:label>
                </td>
                <td>
             <asp:textbox id="ToDateTxt" class="form-control"  runat="server"></asp:textbox>
                </td>
            </tr>
        
       <tr class="row right ">
                <td class="row text-danger ">
                 <asp:label runat="server"  style="font-size:18px;" text="حساب"></asp:label>
                </td>
                <td>
                <asp:dropdownlist style="font-size:21px;padding:5px;" id="AccountDropID"  class="form-control"  runat="server"> 
                </asp:dropdownlist> 

                </td>
            </tr>
           
       <tr>
      <td style="text-align:center" class="NotToPrint" colspan="3">
           
           <asp:Button ID="SaerchBtn" CssClass="btn btn-success" style="font-size:21px;" runat="server" Text="بحث" OnClick="SaerchBtn_Click"  />
      
             <input id="PrintBtn" type="button" class="btn btn-success" style="font-size:21px;" runat="server" onclick="PrintGridDataCustom('prtg')" value="طباعة" />
                 <asp:Button ID="Button3" runat="server" Text="اكسل" style="font-size:21px;" class="btn btn-success" OnClick="Button3_Click"  />          

            </td>
                    </tr>
       
      <tr>

          <td colspan="3"> 
<asp:GridView ID="AccountGrd" style="width:100%"  runat="server" BackColor="White"
     BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="False">
     <Columns>
      <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رقم الحركة">
                        <ItemTemplate>
                            <asp:Label ID="OperationID" CssClass="form-control"   runat="server"  Text='<%# Eval("OperationID") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>
      
         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رصيد مدين">
                        <ItemTemplate>
                            <asp:Label ID="IndebtBalance" CssClass="form-control"   runat="server"  Text='<%# Eval("IndebtBalance") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>

             
         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رصيد دائن">
                        <ItemTemplate>
                            <asp:Label ID="CreditBalance" CssClass="form-control"   runat="server"  Text='<%# Eval("CreditBalance") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="حركة مدينة">
                        <ItemTemplate>
                            <asp:Label ID="InDebt_MovementLbl" CssClass="form-control"   runat="server"  Text='<%# Eval("InDebt_Movement") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField> 
         
           <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="حركة دائنة">
                        <ItemTemplate>
                            <asp:Label ID="Credit_MovementLbl" CssClass="form-control"   runat="server"  Text='<%# Eval("Credit_Movement") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>


           <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="التاريخ">
                        <ItemTemplate>
                                <asp:Label ID="OperationDate" CssClass="form-control" runat="server" Text='<%#DateTime.Parse( Eval("OperationDate").ToString()).Year+"-"+DateTime.Parse( Eval("OperationDate").ToString()).Month+"-"+DateTime.Parse( Eval("OperationDate").ToString()).Day %>'></asp:Label>
                             </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="البيان">
                        <ItemTemplate>
                            <asp:Label ID="description" CssClass="form-control"   runat="server"  Text='<%# Eval("description") %>'></asp:Label>
                             </ItemTemplate>
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
    <!--Start Print Part-->

    <table id="SuppliersOperationsGrd"  runat="server" class="table table-bordered" dir="rtl" style="float:right;margin:20px;width: 75%;">
       <tr class="row right ">
                <td class="row text-danger ">
                    <asp:label runat="server" style="font-size:18px;" text="من"></asp:label>
                </td>
                <td>
             <asp:textbox id="Textbox1" class="form-control"  runat="server"></asp:textbox>
                </td>
            </tr>
    <tr class="row right">
                <td class="row text-danger ">
                    <asp:label runat="server" style="font-size:18px;" text="الى"></asp:label>
                </td>
                <td>
             <asp:textbox id="Textbox2" class="form-control"  runat="server"></asp:textbox>
                </td>
            </tr>
        
       <tr class="row right ">
                <td class="row text-danger ">
                 <asp:label runat="server"  style="font-size:18px;" text="حساب"></asp:label>
                </td>
                <td>
                <asp:Label style="font-size:21px;padding:5px;" id="Dropdownlist1"  class="form-control"  runat="server"> 
                </asp:Label> 

                </td>
            </tr>
           
       <tr>
      <td style="text-align:center" colspan="3">
          
       </td>
                    </tr>
       
      <tr>

          <td colspan="3"> 
<asp:GridView ID="GridView1" style="width:100%"  runat="server" BackColor="White"
     BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="False">
     <Columns>
      <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رقم الحركة">
                        <ItemTemplate>
                            <asp:Label ID="OperationID" CssClass="form-control"   runat="server"  Text='<%# Eval("OperationID") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>
      
         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رصيد مدين">
                        <ItemTemplate>
                            <asp:Label ID="IndebtBalance" CssClass="form-control"   runat="server"  Text='<%# Eval("IndebtBalance") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>

             
         <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رصيد دائن">
                        <ItemTemplate>
                            <asp:Label ID="CreditBalance" CssClass="form-control"   runat="server"  Text='<%# Eval("CreditBalance") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="حركة مدينة">
                        <ItemTemplate>
                            <asp:Label ID="InDebt_MovementLbl" CssClass="form-control"   runat="server"  Text='<%# Eval("InDebt_Movement") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField> 
         
           <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="حركة دائنة">
                        <ItemTemplate>
                            <asp:Label ID="Credit_MovementLbl" CssClass="form-control"   runat="server"  Text='<%# Eval("Credit_Movement") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>


           <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="التاريخ">
                        <ItemTemplate>
                                <asp:Label ID="OperationDate" CssClass="form-control" runat="server" Text='<%#DateTime.Parse( Eval("OperationDate").ToString()).Year+"-"+DateTime.Parse( Eval("OperationDate").ToString()).Month+"-"+DateTime.Parse( Eval("OperationDate").ToString()).Day %>'></asp:Label>
                             </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="البيان">
                        <ItemTemplate>
                            <asp:Label ID="description" CssClass="form-control"   runat="server"  Text='<%# Eval("description") %>'></asp:Label>
                             </ItemTemplate>
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
