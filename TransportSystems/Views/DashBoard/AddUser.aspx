<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="TransportSystems.Views.DashBoard.AddUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add User</title>
    <%-- All required Filles --%>
    <link href="../../Content/LoginJSCss/bootstrap.min1.css" rel="stylesheet" />
    <script src="../../Content/LoginJSCss/bootstrap.min1.js"></script>
    <script src="../../Content/LoginJSCss/jquery.min321.js"></script>
    <link href="../../Content/LoginJSCss/all.css" rel="stylesheet" />
    <script src="../../Content/LoginJSCss/all.js"></script>
    <link href="../../Content/LoginJSCss/AddUserCss.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <div class="card bg-light">
            <article class="card-body mx-auto" style="max-width: 400px;">
                <h4 class="card-title mt-3 text-center HeaderColor">Create Account</h4>
                <p class="text-center HeaderColor">Add New User To System</p>
                <form runat="server">
                     <!-- User Name -->
                    <div class="form-group input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fa fa-user"></i></span>
                        </div>
                        <asp:TextBox ID="Username" runat="server" CssClass="form-control" placeholder="Username" type="text"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Username" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,UsernameEM%>">*</asp:RequiredFieldValidator>
                    </div>
                    <!-- AddRoleListTxt -->
                    <div class="form-group input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fa fa-building"></i></span>
                        </div>
                        <asp:DropDownList ID="AddRoleListTxt" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <!-- Email -->
                    <div class="form-group input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fa fa-envelope"></i></span>
                        </div>
                        <asp:TextBox ID="Email" runat="server" CssClass="form-control" placeholder="Email address" type="email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,EmailEM%>">*</asp:RequiredFieldValidator>
                    </div>
                    <!-- Phone -->
                    <div class="form-group input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fa fa-phone"></i></span>
                        </div>
                        <asp:TextBox ID="Phone" runat="server" CssClass="form-control" placeholder="Phone number" type="text"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Phone" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,PhoneEMR%>">*</asp:RequiredFieldValidator>
                    </div>
                    <!-- Password -->
                    <div class="form-group input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fa fa-lock"></i></span>
                        </div>
                        <asp:TextBox ID="Password" runat="server" CssClass="form-control" placeholder="Create password" type="password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Password" SetFocusOnError="true"
                                            CssClass="alert-danger ml-4" ErrorMessage="<%$Resources:Store,PasswordEM%>">*</asp:RequiredFieldValidator>
                    </div>
                    <%-- Errors Div --%>
                    <div class="text-center" style="background-color: white">
                        <asp:Label ID="LoginError" runat="server" ForeColor="Red" CssClass="text-danger"></asp:Label>
                    </div>
                    <!-- form-group// -->
                    <div class="form-group">
                        <asp:Button ID="SignIn" runat="server" Text="Create Account" CssClass="btn btn-primary btn-block" OnClick="SignIn_Click" />
                    </div>
                    <div class="form-group">
                        <asp:Button ID="ClosePageReg" runat="server" Text="Back to Main Page" CssClass="btn btn-warning btn-block" OnClick="ClosePageReg_Click" />
                    </div>
                    <div class="form-group text-center">
                       <asp:ValidationSummary ID="ValidationSummary1" runat="server" BackColor="White"  ForeColor="Red" ValidationGroup="ProdGroup"
                                            DisplayMode="List" Font-Names="Impact" />
                    </div>
                </form>
            </article>
        </div>
    </div>
</body>
</html>
