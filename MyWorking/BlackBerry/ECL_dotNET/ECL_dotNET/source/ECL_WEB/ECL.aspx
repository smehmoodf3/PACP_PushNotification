<%@ Page language="c#" Codebehind="ECL.aspx.cs" AutoEventWireup="true" Inherits="ECL.ECL" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ECL</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body bgColor="white" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<INPUT id="Submit1" style="Z-INDEX: 106; LEFT: 168px; WIDTH: 120px; POSITION: absolute; TOP: 520px; HEIGHT: 24px"
				tabIndex="12" type="submit" value="Submit" name="Submit1" runat="server">
			<DIV language="C#" id="Div3" style="Z-INDEX: 107; LEFT: 480px; WIDTH: 321px; POSITION: absolute; TOP: 96px; HEIGHT: 146px"
				tabIndex="20" align="left" runat="server" ms_positioning="GridLayout">&nbsp;<asp:label id="Browser_Push" style="Z-INDEX: 111; LEFT: 0px; POSITION: absolute; TOP: 8px"
					runat="server" ToolTip="Only For Browser Push" Width="72px" Height="24px" BackColor="LightGray">Push Type</asp:label><asp:dropdownlist id="Browser_List" style="Z-INDEX: 111; LEFT: 72px; POSITION: absolute; TOP: 8px"
					tabIndex="3" runat="server" Width="96px" Height="56px" BackColor="Transparent" ForeColor="Black">
					<asp:ListItem Value="Browser-Channel" Selected="True">Channel</asp:ListItem>
					<asp:ListItem Value="Browser-Channel-Delete">Channel Delete</asp:ListItem>
					<asp:ListItem Value="Message">Message</asp:ListItem>
				</asp:dropdownlist><asp:label id="ChName" style="Z-INDEX: 111; LEFT: 0px; POSITION: absolute; TOP: 64px" runat="server"
					ToolTip="Only For a Channel Browser Push and is the name that will appear on the ribbon" Width="96px" Height="24px"
					BackColor="LightGray">Channel Name</asp:label><asp:label id="RootAddress" style="Z-INDEX: 111; LEFT: 0px; POSITION: absolute; TOP: 120px"
					runat="server" ToolTip="The location where channel icons are held and where ECL.html is stored. Usually its the address of your web server."
					Width="168px" Height="24px" BackColor="LightGray" ForeColor="Black">Web Context Root http://</asp:label><asp:textbox id="ContextAddress" style="Z-INDEX: 110; LEFT: 168px; POSITION: absolute; TOP: 120px"
					tabIndex="5" runat="server" Width="144px" Height="24px" BackColor="White" ForeColor="Black">localhost/ecl</asp:textbox><asp:textbox id="ChannelName" style="Z-INDEX: 106; LEFT: 96px; POSITION: absolute; TOP: 64px"
					tabIndex="4" runat="server" Width="120px" Height="24px" BackColor="White" ForeColor="Black">ECL</asp:textbox></DIV>
			<asp:radiobuttonlist id="Options" style="Z-INDEX: 105; LEFT: 184px; POSITION: absolute; TOP: 136px" runat="server"
				Width="176px" Height="24px" RepeatDirection="Horizontal" CellPadding="1" CellSpacing="1" AutoPostBack="True">
				<asp:ListItem Value="NONPAP">Non Reliable</asp:ListItem>
				<asp:ListItem Value="PAP" Selected="True">Reliable</asp:ListItem>
			</asp:radiobuttonlist><asp:label id="DeviceInfo" style="Z-INDEX: 104; LEFT: 176px; POSITION: absolute; TOP: 448px"
				runat="server" ToolTip="Found in user.txt, please follow the same format" Width="88px" Height="24px" BackColor="LightGray">List of Users</asp:label><INPUT id="File1" style="Z-INDEX: 103; LEFT: 176px; WIDTH: 264px; POSITION: absolute; TOP: 472px; HEIGHT: 22px"
				type="file" size="24" name="File1" runat="server" language="C#" tabIndex="1">
			<asp:panel id="Panel1" style="Z-INDEX: 102; LEFT: 176px; POSITION: absolute; TOP: 88px" runat="server"
				Width="632px" Height="152px" BackColor="Transparent" BorderStyle="Ridge">
				<P>&nbsp;
					<asp:label id="Label9" runat="server" BackColor="LightGray" Height="24px" Width="112px" ToolTip="Reliable method provides a record for each push">Push Method</asp:label></P>
				<P>&nbsp;</P>
				<P>&nbsp;
					<asp:label id="Label1" runat="server" BackColor="LightGray" Height="24px" Width="64px" ToolTip="For custom catcher, please make sure ECL client is loaded on the device">Push To: </asp:label></P>
				<P>
					<asp:radiobuttonlist id="PushType" tabIndex="2" runat="server" BackColor="Transparent" Height="16px"
						Width="200px" ForeColor="Black" AutoPostBack="True" CellSpacing="1" CellPadding="1" RepeatDirection="Horizontal">
						<asp:ListItem Value="Channel" Selected="True">Browser</asp:ListItem>
						<asp:ListItem Value="Catcher">Custom Catcher</asp:ListItem>
					</asp:radiobuttonlist></P>
			</asp:panel><asp:panel id="Panel2" style="Z-INDEX: 101; LEFT: 176px; POSITION: absolute; TOP: 264px" runat="server"
				Width="633px" Height="112px" BorderStyle="Ridge">&nbsp; 
<DIV language="C#" id="Div5" style="WIDTH: 624px; POSITION: relative; HEIGHT: 128px"
					tabIndex="19" align="left" noWrap runat="server" ms_positioning="GridLayout">
					<asp:textbox id="PushID" style="Z-INDEX: 104; LEFT: 312px; POSITION: absolute; TOP: 8px" tabIndex="7"
						runat="server" BackColor="White" Height="24px" Width="136px" ForeColor="Black"></asp:textbox>
					<asp:textbox id="DelBTimestamp" style="Z-INDEX: 103; LEFT: 176px; POSITION: absolute; TOP: 56px"
						tabIndex="8" runat="server" BackColor="White" Height="24px" Width="160px" ForeColor="Black">YYYY-MM-DD:hh-mm-ss</asp:textbox>
					<asp:textbox id="DelATimestamp" style="Z-INDEX: 102; LEFT: 176px; POSITION: absolute; TOP: 88px"
						tabIndex="9" runat="server" BackColor="White" Height="24px" Width="160px" ForeColor="Black">YYYY-MM-DD:hh-mm-ss</asp:textbox>
					<asp:dropdownlist id="Priority" style="Z-INDEX: 101; LEFT: 408px; POSITION: absolute; TOP: 56px" tabIndex="10"
						runat="server" BackColor="Transparent" Height="56px" Width="96px" ForeColor="Black">
						<asp:ListItem Value="high" Selected="True">High</asp:ListItem>
						<asp:ListItem Value="medium">Medium</asp:ListItem>
						<asp:ListItem Value="low">Low</asp:ListItem>
					</asp:dropdownlist>
					<asp:dropdownlist id="DelMethod" style="Z-INDEX: 100; LEFT: 472px; POSITION: absolute; TOP: 88px"
						tabIndex="11" runat="server" BackColor="Transparent" Height="16px" Width="104px" ForeColor="Black">
						<asp:ListItem Value="unconfirmed" Selected="True">Unconfrimed</asp:ListItem>
						<asp:ListItem Value="confirmed">Confirmed</asp:ListItem>
					</asp:dropdownlist>
					<asp:label id="Label3" style="Z-INDEX: 106; LEFT: 232px; POSITION: absolute; TOP: 8px" runat="server"
						BackColor="LightGray" Height="24px" Width="64px" ToolTip="A unique ID that the record is updated with, if not provided a random one is generated. Multiple pushes under one ID will not work."
						ForeColor="Black">Push ID</asp:label>
					<asp:label id="Label4" style="Z-INDEX: 107; LEFT: 8px; POSITION: absolute; TOP: 56px" runat="server"
						BackColor="LightGray" Height="24px" Width="168px" ToolTip="The push must be delivered before this date or it will be dropped. Please follow the exact format as indicated"
						ForeColor="Black">Delivery Before Timestamp</asp:label>
					<asp:label id="Label5" style="Z-INDEX: 108; LEFT: 8px; POSITION: absolute; TOP: 88px" runat="server"
						BackColor="LightGray" Height="24px" Width="168px" ToolTip="The push must be delivered after this date or it will be dropped. Please follow the exact format as indicated"
						ForeColor="Black">Delivery After Timestamp</asp:label>
					<asp:label id="Label6" style="Z-INDEX: 109; LEFT: 344px; POSITION: absolute; TOP: 56px" runat="server"
						BackColor="LightGray" Height="24px" Width="56px" ToolTip="Only comes into effect if multiple pushes occur to a given device concurrently"
						ForeColor="Black">Priority</asp:label>
					<asp:label id="Label7" style="Z-INDEX: 110; LEFT: 344px; POSITION: absolute; TOP: 88px" runat="server"
						BackColor="LightGray" Height="24px" Width="120px" ToolTip="Used to provide an application layer confirmation, by default its unconfirmed"
						ForeColor="Black">Delivery Method</asp:label></DIV></asp:panel>
			<asp:checkbox id="ReliableParameters" style="Z-INDEX: 108; LEFT: 176px; POSITION: absolute; TOP: 272px"
				runat="server" ToolTip="Following options are only for reliable push" Width="208px" Height="24px"
				AutoPostBack="True" Text="Optional Reliable Parameters" tabIndex="6"></asp:checkbox>
			<IMG id="IMG1" style="Z-INDEX: 109; LEFT: 8px; WIDTH: 192px; POSITION: absolute; TOP: 8px; HEIGHT: 64px"
				height="64" alt="" src="blackberry_logo.jpg" width="192" runat="server">
			<DIV dataFormatAs="text" style="DISPLAY: inline; FONT-WEIGHT: bold; FONT-SIZE: 20pt; Z-INDEX: 110; LEFT: 320px; WIDTH: 352px; COLOR: blue; POSITION: absolute; TOP: 16px; HEIGHT: 64px; FONT-VARIANT: small-caps"
				align="center" ms_positioning="FlowLayout">Emergency Contact List</DIV>
			<asp:Button id="PushResult" style="Z-INDEX: 111; LEFT: 312px; POSITION: absolute; TOP: 520px"
				runat="server" Width="144px" Text="Reliable Push Result"></asp:Button></form>
	</body>
</HTML>
