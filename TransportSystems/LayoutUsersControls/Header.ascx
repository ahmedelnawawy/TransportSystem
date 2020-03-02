<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="TransportSystems.LayoutUsersControls.Header" %>
<!-- begin:: Aside Logo in header -->
<div class="kt-header__brand kt-grid__item  " id="kt_header_brand" style="background:#123">
    <div class="kt-header__brand-logo">
    </div>
</div>
<!-- end:: Aside Logo in header -->
<div style="width:400px;margin-top:20px;height:90px;">
    <h2 style="background:#ffffff;border-radius:10px;height:40px;text-align:center;padding-top:3px"> شركه عبد الاخر للنقل البري</h2>
</div>
<div class="kt-header__topbar">
    <%if (IsAuthenticated){%>
        <h3 class="kt-header__title">
             <asp:Button ID="LogOut" runat="server" Text="خـــروج" CssClass="btn btn-outline-warning btn-block" OnClick="LogOut_Click" />
        </h3>
    <%}%>
    <!--end: User bar -->
</div>
<!-- end:: Header Topbar -->