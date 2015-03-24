<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/UserControls/MenuControl.ascx" TagPrefix="uc1" TagName="MenuControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8">
    <title>The Public Authority for Consumer Protection. | Sultanate of Oman</title>
    <link href="styles.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form2" runat="server">
    <div class="page">
        <div id="wrapper">
            <header></header>
            
            <nav>
    <ul class="left">
        <li ><a href="index.aspx">About PACP</a></li>
        <li ><a href="alerts.aspx">Alerts</a></li>
        <li> <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" 
                CausesValidation="False">Logout</asp:LinkButton></li>
    </ul>
    <div class="flags left">
        <asp:ImageButton ID="imgArabic" runat="server"  ImageUrl="~/images/arabic.png" width="25" height="16" alt="Arabic" OnClick="imgArabic_Click"/>
        <asp:ImageButton ID="imgEnglish" runat="server" ImageUrl="~/images/english.png" width="25" height="16" alt="Arabic" OnClick="imgEnglish_Click"/>
        <%--<img src="images/arabic.png" width="25" height="16" alt="Arabic" />
        <img src="images/english.png" width="25" height="16" alt="English" />--%>

    </div>
</nav>
            <section id="content" class="aboutpacp">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" BasePath="" FullPage="true" Height="400px">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                    </td>
                </tr>
                 <tr>
                    <td align="center">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
                </section>
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
