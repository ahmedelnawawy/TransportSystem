<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarMaintenanceCost.aspx.cs" Inherits="TransportSystems.Views.Reports.CarMaintenanceCost" %>

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
    <div id="prtg" class="kt-portlet mb-0">
        <h1 style="text-align: center; color: red;"><u>تكلفة صيانة سيارة</u></h1>
        <div class="kt-portlet__body p-2 pb-0">
            <div class="row">
                <div class="form-group col-md-3">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
                        من                  
                    </label>
                    <asp:TextBox ID="FromDateTxt" Style="display: inline-block; margin-right: 3%;" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-6">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
                        الى                   
                    </label>
                    <asp:TextBox ID="ToDateTxt" Style="display: inline-block; margin-right: 3%;" type='text' runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-12">
                    <label class="mr-3" style="font-family: 'Times New Roman', Times, serif; font-size: large; display: inline">
                        حساب                   
                    </label>
                    <asp:DropDownList Style="font-size: 21px; padding: 5px; display: inline-block;" ID="AccountDropID" class="form-control" runat="server">
                    </asp:DropDownList>

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
                    <asp:GridView ID="AccountGrd" Style="width: 100%" runat="server" BackColor="White"
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
                                    <asp:Label ID="OperationID" CssClass="form-control" runat="server" Text='<%# Eval("OperationID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رصيد مدين">
                                <ItemTemplate>
                                    <asp:Label ID="IndebtBalance" CssClass="form-control" runat="server" Text='<%# Eval("IndebtBalance") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="رصيد دائن">
                                <ItemTemplate>
                                    <asp:Label ID="CreditBalance" CssClass="form-control" runat="server" Text='<%# Eval("CreditBalance") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="حركة مدينة">
                                <ItemTemplate>
                                    <asp:Label ID="InDebt_MovementLbl" CssClass="form-control" runat="server" Text='<%# Eval("InDebt_Movement") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="حركة دائنة">
                                <ItemTemplate>
                                    <asp:Label ID="Credit_MovementLbl" CssClass="form-control" runat="server" Text='<%# Eval("Credit_Movement") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="التاريخ">
                                <ItemTemplate>
                                    <asp:Label ID="OperationDate" CssClass="form-control" runat="server" Text='<%#DateTime.Parse( Eval("OperationDate").ToString()).Year+"-"+DateTime.Parse( Eval("OperationDate").ToString()).Month+"-"+DateTime.Parse( Eval("OperationDate").ToString()).Day %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="البيان">
                                <ItemTemplate>
                                    <asp:Label ID="description" CssClass="form-control" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="نوع الحركة">
                                <ItemTemplate>
                                    <asp:Label ID="OperationType" CssClass="form-control" runat="server" Text='<%# Eval("OperationType") %>'></asp:Label>
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
