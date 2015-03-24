<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8">
    <title>The Public Authority for Consumer Protection. | Sultanate of Oman</title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
    <div class="page">
        <div id="wrapper">
            <div class="loginlogo">
                <img src="images/loginlogo.png" width="226" height="208"></div>
            <div class="loginform">
                <form action="" method="get">
                <label>
                    Username</label>
                <asp:TextBox ID="txtUname" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter User Name"
                    SetFocusOnError="true" ControlToValidate="txtUname" Display="Dynamic" 
                    CssClass="error"></asp:RequiredFieldValidator>
                    <br />
                <label>
                    Password</label>
                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Password"
                    SetFocusOnError="true" ControlToValidate="txtPwd" Display="Dynamic" 
                    CssClass="error"></asp:RequiredFieldValidator>
                    <br />
                <asp:Button ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" class="green" />
                <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>
                </form>
            </div>
            <div class="register">
                <h2>
                    Create an Account</h2>
                <p style="width: 700px;" class="left">
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh
                    euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.</p>
                    <a href ="register-form.aspx" class="right create white_button">Create Account</a>
                <%--<input name="" type="button" value="Create Account" class="right create">--%>
            </div>
        </div>
        <div class="footer-wrapper">
            <footer id="footer">
      <span>Copyright © The Public Authority for Consumer Protection.<br>
        Sultanate of Oman</span>
      </footer>
        </div>
    </div>
    </form>
</body>
</html>
