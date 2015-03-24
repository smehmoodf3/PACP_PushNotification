<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuControl.ascx.cs" Inherits="MenuControl" %>

<nav>
    <ul class="left">
        <li ><a href="index.aspx" >About PACP</a></li>
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
