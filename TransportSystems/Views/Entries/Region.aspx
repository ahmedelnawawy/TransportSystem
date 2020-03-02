<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Region.aspx.cs" Inherits="TransportSystems.Views.Entries.Region" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB1FDi62WNs15p_wrf50YmlSiE1HldEH7M&sensor=false"></script>
    <script type="text/javascript" src="https://unpkg.com/location-picker/dist/location-picker.min.js"></script>
    <style type="text/css">
        #map {
            width: 100%;
            height: 480px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h2 class="kt-portlet__head-title" style="color: orangered; font-family: 'Times New Roman', Times, serif; font-size: large"><%=Resources.Store.FromRegion %>
                </h2>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">

            <div style="border: 1px solid #ddd; border-radius: 20px; margin: 10px; padding: 10px;">
                <h2 class="kt-portlet__head-title" style="font-family: 'Times New Roman', Times, serif; font-size: large"><%=Resources.Store.AddFromRegion %>
                </h2>
                <div style="width: 100%">
                    <asp:HiddenField ID="FromRegionIdHid" runat="server" Value="0" />

                    <div class="form-group" style="width: 30%; display: block;">
                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Name%></label>
                        <asp:TextBox ID="FromRegion_Name" runat="server" CssClass="form-control col-md-12" placeholder="<%$Resources:Store,NamePH%>"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FromRegion_Name"
                            CssClass="alert-danger" ErrorMessage="<%$Resources:Store,NameEM%>">*</asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group" style="width: 30%; display: block;">
                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Latitude%></label>
                        <asp:TextBox ID="LatitudeTxt" runat="server" CssClass="form-control col-md-12" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group" style="width: 30%; display: inline-block;">
                        <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.Longitude%></label>
                        <asp:TextBox ID="LongitudeTxt" runat="server" CssClass="form-control col-md-12" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="form-group" style="width: 49%; display: block;">
                        <div id="GetLocation" runat="server" class="btn btn-success" data-target="#CarTypeModal" data-toggle="modal">
                            <%=Resources.Store.GetLocation%>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert-danger" DisplayMode="List" />
                    <br />
                    <asp:Button ID="Save" runat="server" Text="<%$Resources:Store,Save%>" CssClass="btn btn-success" OnClick="Save_Click" />
                </div>
            </div>
            <div class="table-responsive mt-2">
                <asp:GridView ID="GridView1" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                    runat="server" CssClass="table table-striped text-center table-sm mb-0" GridLines="Horizontal">
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
                        <asp:BoundField DataField="Name" HeaderText="<%$ Resources:Store,Name%>" />
                        <asp:BoundField DataField="Latitude" HeaderText="<%$ Resources:Store,Latitude%>" />
                        <asp:BoundField DataField="Longitude" HeaderText="<%$ Resources:Store,Longitude%>" />
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
    <div class="modal" id="CarTypeModal" tabindex="-1">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                </div>
                <div class="modal-body">
                    <%-- Map Div --%>
                    <div id="map" style="border: 5px solid #ff6a00"></div>
                    <%-- Location Div  --%>
                    <div class="form-group row col-md-12 mt-3" style="border: 1px solid #ddd; padding: 10px; margin: 10px;">
                        <div class="form-group col-md-4 offset-md-2">
                            <asp:Label runat="server" Style="font-family: 'Times New Roman', Times, serif; font-size: larger; color: #ff6a00;" Text="<%$Resources:Store,Latitude%>"></asp:Label>
                            <input id="txtlat" runat="server" type="text" class="form-control" style="display: inline" value="0" />
                        </div>
                        <div class="form-group col-md-4">
                            <asp:Label runat="server" Style="font-family: 'Times New Roman', Times, serif; font-size: larger; color: #ff6a00;" Text="<%$Resources:Store,Longitude%>"></asp:Label>
                            <input id="txtlong" runat="server" type="text" class="form-control" style="display: inline" value="0" />
                        </div>
                        <div class="form-group col-md-12">
                            <div class="text-center mt-2 ">
                                <button id="confirmPosition" runat="server" class="btn btn-success" data-dismiss="modal"
                                    aria-label="Close" value="Refresh Page" onclick="Get_Location();">
                                    Confirm Selected Position</button>
                            </div>
                        </div>
                    </div>
                    <%--  <p>On idle position: <span id="onIdlePositionView"></span></p>
                    <p>On click position: <span id="onClickPositionView"></span></p>--%>
                    <script>
                        // Get element references
                        var confirmBtn = document.getElementById('MainContent_confirmPosition');
                        //var onClickPositionView = document.getElementById('onClickPositionView');
                        //var onIdlePositionView = document.getElementById('onIdlePositionView');

                        // Initialize locationPicker plugin
                        var lp = new locationPicker('map', {
                            setCurrentPosition: true, // You can omit this, defaults to true
                        }, {
                            zoom: 15 // You can set any google map options here, zoom defaults to 15
                        });
                        // Listen to button onclick event
                        // Get current location and show it in HTML
                        function Get_Location() {
                            var location = lp.getMarkerPosition();
                            document.getElementById("MainContent_LatitudeTxt").value = location.lat;
                            document.getElementById("MainContent_LongitudeTxt").value = location.lng;
                            //onClickPositionView.innerHTML = 'The chosen location is ' + location.lat + ',' + location.lng;
                        }
                        // Listen to map idle event, listening to idle event more accurate than listening to ondrag event
                        google.maps.event.addListener(lp.map, 'idle', function (event) {
                            // Get current location and show it in HTML
                            var location = lp.getMarkerPosition();
                            //onIdlePositionView.innerHTML = 'The chosen location is ' + location.lat + ',' + location.lng;
                            document.getElementById("MainContent_txtlat").value = location.lat;
                            document.getElementById("MainContent_txtlong").value = location.lng;
                        });
                    </script>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>
