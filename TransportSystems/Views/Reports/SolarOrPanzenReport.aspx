<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SolarOrPanzenReport.aspx.cs" Inherits="TransportSystems.Views.Reports.SolarOrPanzenReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#MainContent_FromDate").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            $("#MainContent_ToDate").datepicker({
                dateFormat: 'yy-mm-dd'
            });
            //On UpdatePanel Refresh.
            $(document).ready(function () {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            $("#MainContent_FromDate").datepicker({
                                dateFormat: 'yy-mm-dd'
                            });
                            $("#MainContent_ToDate").datepicker({
                                dateFormat: 'yy-mm-dd'
                            });
                        }
                    });
                };
            });
        });
        ////// Printing Part
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
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h2 class="kt-portlet__head-title" style="color: orangered; font-family: 'Times New Roman', Times, serif; font-size: large"><%=Resources.Store.SolarOrOilReport %>
                </h2>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row col-md-12">
                        <div class="form-group col-md-3" style="width: 30%; display: inline-block;">
                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.DateFrom %></label>
                            <asp:TextBox ID="FromDate" runat="server" CssClass="form-control" CausesValidation="false"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3" style="width: 30%; display: inline-block;">
                            <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.DateTo %></label>
                            <asp:TextBox ID="ToDate" runat="server" CssClass="form-control" CausesValidation="false"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-3" style="width: 30%; display: inline-block;">
                            <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.CarNo%></label>
                            <asp:DropDownList ID="ServiceIDListTxt" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-3" style="width: 30%; display: inline-block;">
                            <label class="mr-2" style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.CarNo%></label>
                            <asp:DropDownList ID="CarNoListtxt" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="text-center mt-2 NotToPrint">
                        <asp:Button ID="Search" runat="server" Text="<%$ Resources:Store,Search%>" CssClass="btn btn-success round mr-1" OnClick="Search_Click" CausesValidation="False" />
                        <%--OnClick="Search_Click"--%>
                        <asp:Button ID="NewSearch" runat="server" Text="<%$ Resources:Store,NewSearch%>" CssClass="btn btn-success round mr-1" OnClick="NewSearch_Click" CausesValidation="False" />
                        <%--OnClick="NewSearch_Click"--%>
                        <input id="PrintBtn" type="button" class="btn btn-success round mr-1"  onclick="PrintGridDataCustom('prtg')" value="طباعة" />
                        <asp:Button ID="ExportExcel" runat="server" Text="اكسل"  class="btn btn-success round mr-1" OnClick="ExportExcel_Click" />
                    </div>
                    <div class="table-responsive mt-2" runat="server"  id="prtg">
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                            CssClass="table table-striped text-center table-sm mb-0" GridLines="Horizontal">
                            <FooterStyle BackColor="White" ForeColor="#333333"></FooterStyle>
                            <HeaderStyle Font-Bold="True" CssClass="thead-light" ForeColor="red"></HeaderStyle>
                            <PagerStyle HorizontalAlign="Center" BackColor="#dfe1ea" ForeColor="#DDD"></PagerStyle>
                            <RowStyle BackColor="White" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White"></SelectedRowStyle>
                            <SortedAscendingCellStyle BackColor="#F7F7F7"></SortedAscendingCellStyle>
                            <SortedAscendingHeaderStyle BackColor="#487575"></SortedAscendingHeaderStyle>
                            <SortedDescendingCellStyle BackColor="#E5E5E5"></SortedDescendingCellStyle>
                            <SortedDescendingHeaderStyle BackColor="#275353"></SortedDescendingHeaderStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="التاريخ">
                                    <ItemTemplate>
                                        <asp:Label ID="Date"  runat="server" Text='<%#DateTime.Parse( Eval("Date").ToString()).Year+"-"+DateTime.Parse( Eval("Date").ToString()).Month+"-"+DateTime.Parse( Eval("Date").ToString()).Day %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Cars.CarNo" HeaderText="<%$ Resources:Store,CarNo%>" />
                                <asp:BoundField DataField="Cars.CarType.Name" HeaderText="<%$ Resources:Store,CarType%>" />
                                <asp:BoundField DataField="Driver.Name" HeaderText="<%$ Resources:Store,Driver%>" />
                                <asp:BoundField DataField="LastReading" HeaderText="<%$ Resources:Store,LastReading%>" />
                                <asp:BoundField DataField="CurrentReading" HeaderText="<%$ Resources:Store,CurrentReading%>" />
                                <asp:BoundField DataField="Distance" HeaderText="<%$ Resources:Store,Distance%>" />
                                <asp:BoundField DataField="Total" HeaderText="<%$ Resources:Store,Total%>" />
                                <asp:BoundField DataField="SolarQty" HeaderText="<%$ Resources:Store,SolarQty%>" />
                                <asp:BoundField DataField="Average" HeaderText="<%$ Resources:Store,Average%>" />
                                <asp:BoundField DataField="Notes" HeaderText="<%$ Resources:Store,Notes%>" />
                                <asp:BoundField DataField="Services.Name" HeaderText="<%$ Resources:Store,ServiceName%>" />
                            </Columns>
                            <EmptyDataTemplate>No Rows Available</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ExportExcel" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
