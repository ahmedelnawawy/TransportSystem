<%@ EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LookUpsViews.aspx.cs" Inherits="TransportSystems.Views.Entries.LookUpsViews" %>

<%@ Register Src="~/Views/Entries/SubStores.ascx" TagPrefix="uc1" TagName="SubStores" %>
<%@ Register Src="~/Views/Entries/AddSector.ascx" TagPrefix="uc1" TagName="AddSector" %>
<%@ Register Src="~/Views/Entries/AddCity.ascx" TagPrefix="uc1" TagName="AddCity" %>
<%@ Register Src="~/Views/Entries/AddCarType.ascx" TagPrefix="uc1" TagName="AddCarType" %>
<%@ Register Src="~/Views/Entries/AddColors.ascx" TagPrefix="uc1" TagName="AddColors" %>
<%@ Register Src="~/Views/Entries/AddTrafficDepartment.ascx" TagPrefix="uc1" TagName="AddTrafficDepartment" %>
<%@ Register Src="~/Views/Entries/AddServices.ascx" TagPrefix="uc1" TagName="AddServices" %>
<%@ Register Src="~/Views/DashBoard/AddRole.ascx" TagPrefix="uc1" TagName="AddRole" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="kt-portlet mb-0">
        <div class="kt-portlet__head">
            <div class="kt-portlet__head-label">
                <h2 class="kt-portlet__head-title" style="color: orangered; font-family: 'Times New Roman', Times, serif; font-size: large"><%=Resources.Store.LookUpsViews %>
                </h2>
            </div>
        </div>
        <div class="kt-portlet__body p-2 pb-0">
            <div class="row" style="margin: 10px; padding: 10px;">
                <div class="col-md-4 border-bottom border-right" style="min-height: 100px;margin-top:10px">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AddStore %></label>
                    <uc1:SubStores runat="server" ID="SubStores" />
                </div>
                <div class="col-md-4 border-bottom border-left border-right" style="min-height: 100px;margin-top:10px">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AddCategory %></label>
                    <uc1:AddSector runat="server" ID="AddSector" />
                </div>
                <div class="col-md-4 border-bottom border-left" style="min-height: 100px;margin-top:10px">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AddCity %></label>
                    <uc1:AddCity runat="server" ID="AddCity" />
                </div>
                <%-- 2nd row --%>
                <div class="col-md-4 border-bottom border-right" style="min-height: 100px;margin-top:10px">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AddCarType %></label>
                    <uc1:AddCarType runat="server" ID="AddCarType" />
                </div>
                <div class="col-md-4 border-bottom border-left border-right" style="min-height: 100px;margin-top:10px">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AddColors %></label>
                    <uc1:AddColors runat="server" ID="AddColors" />
                </div>
                <div class="col-md-4 border-bottom border-left" style="min-height: 100px;margin-top:10px">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.TrafficDepartment %></label>
                    <uc1:AddTrafficDepartment runat="server" ID="AddTrafficDepartment" />
                </div>
                <%-- 2nd row --%>
                <div class="col-md-4 border-bottom border-right" style="min-height: 100px;margin-top:10px">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AddServices %></label>
                    <uc1:AddServices runat="server" ID="AddServices" />
                </div>
                <div class="col-md-4 border-bottom border-left border-right" style="min-height: 100px;margin-top:10px">
                    <label style="display: inline; font-family: 'Times New Roman', Times, serif; font-size: larger"><%=Resources.Store.AddRole %></label>
                    <uc1:AddRole runat="server" ID="AddRole" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
