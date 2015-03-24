<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register-form.aspx.cs" Inherits="register_form" %>

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
                    Fullname</label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Enter Name" ControlToValidate ="txtName" CssClass="error"></asp:RequiredFieldValidator>
                <br>
                <label>
                    Email</label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Enter Email" ControlToValidate ="txtEmail" CssClass="error"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="Invalid Email" ControlToValidate="txtEmail" Display="Dynamic" 
                    SetFocusOnError="True" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    CssClass="error"></asp:RegularExpressionValidator>
                <br>
                <label>
                    Username</label>
                <asp:TextBox ID="txtUseName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Enter User Name" ControlToValidate ="txtUseName" CssClass="error"></asp:RequiredFieldValidator>
                <br>
                <label>
                    Password</label>
                <asp:TextBox ID="txtPwd" runat="server" TextMode ="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="Enter Password" ControlToValidate ="txtPwd" CssClass="error"></asp:RequiredFieldValidator>
                <br>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="green" 
                    onclick="btnSubmit_Click" />
                <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>

                Already Member ? <a href ="login.aspx">Log In</a>
                </form>
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
