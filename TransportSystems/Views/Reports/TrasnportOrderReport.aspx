﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrasnportOrderReport.aspx.cs" Inherits="TransportSystems.Views.Reports.TrasnportOrderReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
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
    <div  id="prtg" dir="rtl" class="kt-portlet mb-0">
            
           
             <h1 style="text-align:center;color:red;"><u>تقرير اذونات نقل</u></h1>
           
     
              <div class="kt-portlet__body p-2 pb-0">
                   <div class="row">
                <div class="form-group col-md-3">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
من                   </label>
                    <asp:TextBox ID="FromDateTxt" style="display: inline-block;margin-right: 3%;" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                       <div class="form-group col-md-6">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
الى                    </label>
                    <asp:TextBox ID="ToDateTxt" style="display: inline-block;margin-right: 3%;" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                       </div>
              

                
                  <div class="row">

                  <div class="form-group text-center col-md-12 NotToPrint">
                <asp:Button ID="SaerchBtn" CssClass="btn btn-success" Style="font-size: 21px;" runat="server" Text="بحث" OnClick="SaerchBtn_Click" />
                <input id="PrintBtn" type="button" class="btn btn-success" style="font-size: 21px;" onclick="PrintGridDataCustom('prtg')" value="طباعة" />
                <asp:Button ID="Button3" runat="server" Text="اكسل" Style="font-size: 21px;" class="btn btn-success" OnClick="Button3_Click" />
            

                  </div>
                      </div>
                  <div class="row">
                      <div class="form-group col-md-12">
                      <asp:GridView ID="TransportGrd" Style="width: 100%" runat="server" BackColor="White" 
                     BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
                           GridLines="Horizontal" CssClass="table table-striped table-sm mb-0 text-center" AutoGenerateColumns="False" Width="100%">
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
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رقم الحركة">
                            <ItemTemplate>
                                <asp:Label ID="OrderID" CssClass="form-control" runat="server" Text='<%# Eval("id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="اسم العميل">
                            <ItemTemplate>
                                <asp:Label ID="ClientNameLbl" CssClass="form-control" runat="server" Text='<%# Eval("ClientName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="متعهد النقل">
                            <ItemTemplate>
                                <asp:Label ID="VendorNameLbl" CssClass="form-control" runat="server" Text='<%# Eval("VendorName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="سيارة">
                            <ItemTemplate>
                                <asp:Label ID="CarName" CssClass="form-control" runat="server" Text='<%# Eval("CarName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="المنتج">
                            <ItemTemplate>
                                <asp:Label ID="ProductName" CssClass="form-control" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="من منطقة">
                            <ItemTemplate>
                                <asp:Label ID="FromRegon" CssClass="form-control" runat="server" Text='<%# Eval("FromRegon") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                                  <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="الى منطقة">
                            <ItemTemplate>
                                <asp:Label ID="ToRegon" CssClass="form-control" runat="server" Text='<%# Eval("ToRegon") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="الكمية">
                            <ItemTemplate>
                                <asp:Label ID="Qty" CssClass="form-control" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="قيمة النقل">
                            <ItemTemplate>
                                <asp:Label ID="Total" CssClass="form-control" runat="server" Text='<%# Eval("Total") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="طريقة الدفع">
                            <ItemTemplate>
                                <asp:Label ID="PaymentMethod" CssClass="form-control" runat="server" Text='<%# Eval("PaymentMethod") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="التاريخ">
                            <ItemTemplate>
                                <asp:Label ID="OperationDate" CssClass="form-control" runat="server" Text='<%#DateTime.Parse( Eval("Date").ToString()).Year+"-"+DateTime.Parse( Eval("Date").ToString()).Month+"-"+DateTime.Parse( Eval("Date").ToString()).Day %>'></asp:Label>
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

    </div>


   
</asp:Content>
