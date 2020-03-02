<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemsCard.aspx.cs" Inherits="TransportSystems.Views.Reports.ItemsCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       
     <script type="text/javascript">
        $(function () {
            $("#MainContent_txtfrom").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            $("#MainContent_txtto").datepicker({
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
    <div  id="prtg" dir="rtl" class="kt-portlet mb-0">
            
           
             <h1 style="text-align:center;color:red;"><u>كارتة صنف</u></h1>
           
     
              <div class="kt-portlet__body p-2 pb-0">
                   <div class="row">
                <div class="form-group col-md-6">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
الصمف                    </label>
                    <asp:TextBox ID="search" placeholder="البحث باسم او كود الصنف" name="search"  style="display: inline-block;margin-right: 3%;" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                             <asp:DropDownList ID="drpitems"  ClientIDMode="Static" CssClass="form-control" runat="server"    style="display:inline-block"></asp:DropDownList>               

                </div>
                       </div>
                  <div class="row">
                       <div class="form-group col-md-3">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
من                    </label>
                <asp:textbox runat="server" ID="txtfrom" CssClass="form-control"></asp:textbox>
</div>
                      <div class="form-group col-md-6">

                           <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
الى                    </label>
                <asp:textbox runat="server" ID="txtto" CssClass="form-control"></asp:textbox>


                </div>


        </div>
    <div class="form-group">
        <div class="NotToPrint text-center">
                
                <asp:Button ID="Button1" runat="server" Text="بحث" CssClass="btn btn-success" OnClick="Button1_Click" />
             <input id="PrintBtn" type="button" class="btn btn-primary" runat="server" onclick="PrintGridDataCustom('prtg')" value="طباعة" />
                 <asp:Button ID="Button3" runat="server" Text="اكسل" CssClass="btn btn-warning" OnClick="Button3_Click"  />
            </div>

    </div>
                  <div class="form-group">
                      <asp:GridView ID="GridView1"  runat="server" BackColor="White" 
                     BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                           GridLines="Horizontal" CssClass="table table-striped table-sm mb-0 text-center"
                           AutoGenerateColumns="False"
                           OnDataBinding="GridView1_DataBinding" OnRowDataBound="GridView1_RowDataBound" 
                          ShowFooter="True" Width="100%">
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
                        <asp:TemplateField HeaderText="رصيد سابق">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("prbalance") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="وارد">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("imquantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="صادر">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("exquantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="رصيد حالى">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("afbalance") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تاريخ">
                            <ItemTemplate>
                     <asp:Label ID="Label5" CssClass="form-control" runat="server" Text='<%#DateTime.Parse( Eval("date").ToString()).Year+"-"+DateTime.Parse( Eval("date").ToString()).Month+"-"+DateTime.Parse( Eval("date").ToString()).Day %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="بيان">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("subid") %>' Visible="False"></asp:Label>
                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("AccountName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="النوع">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("RType") %>'></asp:Label>
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
   



    

</div>
</asp:Content>
