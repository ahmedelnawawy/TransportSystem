<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncomsEntry.aspx.cs" Inherits="TransportSystems.Views.Reports.IncomsEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function () {
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


     <div  id="prtg" class="kt-portlet mb-0">
            
           
             <h1 style="text-align:center;color:red;"><u>قيد مقبوضات</u></h1>
           
     
              <div class="kt-portlet__body p-2 pb-0">
                   <div class="row">
               
                       <div class="form-group col-md-6">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
التاريخ                    </label>
                    <asp:TextBox ID="ToDateTxt" style="display: inline-block;margin-right: 3%;" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                       </div>
              

                  <div class="row">
                      <div class="form-group col-md-12">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
حساب                    </label>
                  
                                 <asp:DropDownList Style="font-size: 21px; padding: 5px;display:inline-block;" ID="AccountDropID" class="form-control" runat="server">
                </asp:DropDownList>

                      </div>

        </div>
                  <div class="row">
                      <div class="form-group col-md-12 text-center">
                     <asp:Button ID="SaerchBtn" CssClass="btn btn-success" style="font-size:21px;" runat="server" Text="بحث" OnClick="SaerchBtn_Click"  />
                                     <asp:Label ID="RsltTxt"  style="font-size:21px;" runat="server" Text=""   />


                      </div>
                  </div>
                  <div class="row">
                      <div class="form-group col-md-12">
                          <asp:GridView ID="EntryGrdV" style="width:100%"  runat="server" BackColor="White" 
     BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" AutoGenerateColumns="False"  CssClass="table table-striped table-sm mb-0 text-center" OnRowDataBound="EntryGrdV_RowDataBound">
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
      <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رقم الاذن">
                        <ItemTemplate>
                            <asp:Label ID="ID" CssClass="form-control"   runat="server"  Text='<%# Eval("ID") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="قيمة العملية">
                        <ItemTemplate>
                            <asp:Label ID="Value" CssClass="form-control"   runat="server"  Text='<%# Eval("Value") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField> 
         
           <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="البيان">
                        <ItemTemplate>
                            <asp:Label ID="Description" CssClass="form-control"   runat="server"  Text='<%# Eval("Description") %>'></asp:Label>
                             </ItemTemplate>
          </asp:TemplateField>


          
                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="حالة القيد">
                        <ItemTemplate>
                     <asp:Label ID="lblStatusCol" Visible="false" CssClass="form-control"    runat="server"  Text='<%# Eval("EntryState") %>'></asp:Label>

                            <asp:CheckBox ID="EntryState" CssClass="form-control"   runat="server" style="font-size:18px"  checked='<%# Eval("EntryState") %>'></asp:CheckBox>
                             </ItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رقم القيد">
                        <ItemTemplate>
                            <asp:Label ID="EntryID" CssClass="form-control"   runat="server"  Text='<%# Eval("EntryID") %>'></asp:Label>
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

                      </div>
                  </div>
                  <div class="row"> <div class="form-group col-md-12">
                                 <asp:Label ID="lbl"  style="font-size:18px" runat="server" Text="الاجمالى"></asp:Label>
                                 <asp:Label ID="TotalText" class="col-md-10" style="font-size:18px" runat="server" Text=""></asp:Label>

                                    </div></div>
                    <div class="row"> <div class="form-group col-md-12">
                                   <asp:Label  runat="server"  style="font-size:18px" Text="رقم القيدالجديد"></asp:Label>
                                   <asp:Label ID="NewEntryTxtID" class="col-md-10" style="font-size:18px" runat="server" Text=""></asp:Label>

                                      </div></div>
                   </div></div>
                    <div class="row"> <div class="form-group col-md-12">
                                   <asp:Button ID="SaveBtn" CssClass="btn btn-success" runat="server" style="font-size:21px" Text="حفظ" OnClick="SaveBtn_Click" />

                                      </div></div>
     

   
    




</asp:Content>
