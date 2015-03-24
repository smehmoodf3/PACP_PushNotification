<%@ Page Language="C#" AutoEventWireup="true" CodeFile="alerts.aspx.cs" Inherits="alert" %>

<%@ Register Src="~/UserControls/MenuControl.ascx" TagPrefix="uc1" TagName="MenuControl" %>
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
            <header></header>
            <uc1:MenuControl runat="server" ID="MenuControl" />
            <section id="content">
      <div class="left"><h2>Alerts:</h2></div>
      <div class="right" style="width:200px;"><input name="newalert" type="button" value="New Alert" class="green" onclick="location.href = 'newalert.aspx'" /></div>
      
      <div>
          <br />
          <br />
          <br />
      <asp:GridView ID="gvAlerts" runat="server" AutoGenerateColumns="False" 
              CellPadding="5" PageSize="30" onrowcommand="gvAlerts_RowCommand" 
              onrowdatabound="gvAlerts_RowDataBound" DataKeyNames="Id" DataMember="Id">
          <Columns>
              <asp:BoundField HeaderText="S No" >
              <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:BoundField>
              <asp:BoundField DataField="Title" HeaderText="Title" >
              <ItemStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" />
              </asp:BoundField>
              <asp:BoundField DataField="Description" HeaderText="Description" >
              <ItemStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle" />
              </asp:BoundField>
              <asp:BoundField DataField="Language" HeaderText="Language" >
              <ItemStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" />
              </asp:BoundField>
              <asp:TemplateField HeaderText="Image" >
              <ItemTemplate>
              <asp:Image ID="Image" runat="server" ImageUrl='<%#Eval("Image")%>' Width="50" Height="50"></asp:Image>
              </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Manage" >
              <ItemTemplate>
              <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Manage"  CommandArgument='<%#Container.DataItemIndex %>' >Manage Alerts</asp:LinkButton>
              <asp:Label ID="lblId" runat="server" Text='<%#Eval("Id") %>' Visible = "false"></asp:Label>
              </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
          </Columns>
      
      </asp:GridView>

      </div>
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
