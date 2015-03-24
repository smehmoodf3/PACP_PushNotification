<%@ Page language="c#" Codebehind="PAPQuery.aspx.cs" AutoEventWireup="true" Inherits="PAP.PAPQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ECLResult</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<DIV language="C#" id="Div5" style="Z-INDEX: 102; LEFT: 336px; WIDTH: 177px; POSITION: absolute; TOP: 232px; HEIGHT: 40px"
				tabIndex="19" align="left" noWrap runat="server" ms_positioning="GridLayout"><asp:textbox id="PushID" style="Z-INDEX: 115; LEFT: 72px; POSITION: absolute; TOP: 16px" runat="server"
					Width="104px"></asp:textbox><asp:label id="Label2" style="Z-INDEX: 115; LEFT: 16px; POSITION: absolute; TOP: 16px" runat="server"
					Width="56px" Height="24px">Push ID</asp:label></DIV>
			<DIV language="C#" id="Div2" style="Z-INDEX: 108; LEFT: 336px; WIDTH: 177px; POSITION: absolute; TOP: 232px; HEIGHT: 40px"
				tabIndex="19" align="left" noWrap runat="server" ms_positioning="GridLayout"><asp:textbox id="InputDate" style="Z-INDEX: 115; LEFT: 72px; POSITION: absolute; TOP: 16px" runat="server"
					Width="104px">MM/DD/YYYY</asp:textbox><asp:label id="Label3" style="Z-INDEX: 115; LEFT: 16px; POSITION: absolute; TOP: 16px" runat="server"
					Width="56px" Height="24px" ToolTip="Leave blank to search all records.">Date</asp:label></DIV>
			&nbsp;&nbsp;
			<asp:label id="Label1" style="Z-INDEX: 101; LEFT: 336px; POSITION: absolute; TOP: 72px" runat="server"
				Width="139px" Height="23px">Search Push Type:</asp:label><INPUT id="Submit1" style="Z-INDEX: 103; LEFT: 336px; WIDTH: 112px; POSITION: absolute; TOP: 296px; HEIGHT: 24px"
				type="submit" value="Search..." name="Submit1" runat="server">
			<DIV id="DIV1" style="DISPLAY: inline; FONT-SIZE: 20pt; Z-INDEX: 104; LEFT: 283px; WIDTH: 346px; COLOR: blue; POSITION: absolute; TOP: 17px; HEIGHT: 41px; FONT-VARIANT: small-caps"
				align="center" runat="server" ms_positioning="FlowLayout">Reliable Push Query</DIV>
			<asp:radiobuttonlist id="SearchType" style="Z-INDEX: 105; LEFT: 344px; POSITION: absolute; TOP: 104px"
				runat="server" Width="136px" Height="104px" AutoPostBack="True">
				<asp:ListItem Value="All" Selected="True">All</asp:ListItem>
				<asp:ListItem Value="Successful">Successful</asp:ListItem>
				<asp:ListItem Value="Unsuccessful">Unsuccessful</asp:ListItem>
				<asp:ListItem Value="Specific">Specific</asp:ListItem>
			</asp:radiobuttonlist><asp:datagrid id="MyDataGrid" style="Z-INDEX: 106; LEFT: 200px; POSITION: absolute; TOP: 352px"
				runat="server" Width="536px" Height="104px"></asp:datagrid><INPUT id="Submit2" style="Z-INDEX: 107; LEFT: 472px; WIDTH: 96px; POSITION: absolute; TOP: 296px; HEIGHT: 24px"
				type="submit" value="Home" name="Submit2" runat="server">
			<asp:Image id="Image1" style="Z-INDEX: 109; LEFT: 16px; POSITION: absolute; TOP: 16px" runat="server"
				Width="184px" Height="48px" ImageUrl="file:///C:\Inetpub\wwwroot\PAP\blackberry_logo.jpg"></asp:Image></form>
	</body>
</HTML>
