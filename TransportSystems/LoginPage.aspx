<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="TransportSystems.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logain Page</title>
    <link href="Content/LoginJSCss/CssOFF.css" rel="stylesheet" />
    <link href="Content/LoginJSCss/bootstrap.min1.css" rel="stylesheet" />
    <link href="Content/LoginJSCss/all.css" rel="stylesheet" />
    <script src="Content/LoginJSCss/all.js"></script>
</head>
<body>
    <div class="container">
        <div class="d-flex justify-content-center h-100">
            <div class="card">
                <div class="card-header">
                    <h3>Sign In</h3>
                    <div class="d-flex justify-content-end social_icon">
                        <span><i class="fab  fa-apple"></i></span>
                    </div>
                </div>
                <div class="card-body">
                    <form runat="server">
                        <div class="input-group form-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-user"></i></span>
                            </div>
                            <asp:TextBox ID="Username" runat="server" CssClass="form-control" placeholder="username" type="text"></asp:TextBox>
                        </div>
                        <div class="input-group form-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-key"></i></span>
                            </div>
                            <asp:TextBox ID="Password" runat="server" CssClass="form-control" placeholder="password" type="password"></asp:TextBox>
                        </div>
                        <div class="row align-items-center remember">
                            <input runat="server" id="RememberMe" type="checkbox">Remember Me
                        </div>
                        <div class="form-group text-center mt-3">
                            <asp:Button ID="Login" runat="server" Text="Login" CssClass="btn  login_btn" OnClick="Login_Click" />
                        </div>
                    </form>
                </div>
                <div class="card-footer ">
                    <div class="text-center" style="background-color: white">
                        <asp:Label ID="LoginError" runat="server" ForeColor="Red" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="d-flex justify-content-center links">
                        welcome On Board
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
