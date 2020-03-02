<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Aside.ascx.cs" Inherits="TransportSystems.LayoutUsersControls.Aside" %>
<!-- begin:: Aside -->
<button class="kt-aside-close " id="kt_aside_close_btn"><i class="la la-close"></i></button>
<div class="kt-aside  kt-aside--fixed  kt-grid__item kt-grid kt-grid--desktop kt-grid--hor-desktop" id="kt_aside">
    <!-- begin:: Aside Menu -->
    <div class="kt-aside-menu-wrapper kt-grid__item kt-grid__item--fluid" id="kt_aside_menu_wrapper">
        <div style="background: #123" id="kt_aside_menu" class="kt-aside-menu " data-ktmenu-vertical="1" data-ktmenu-scroll="1" data-ktmenu-dropdown-timeout="500">
            <ul runat="server" id="MainUL" class="kt-menu__nav">
            </ul>
        </div>
    </div>
    <!-- end:: Aside Menu -->
</div>
