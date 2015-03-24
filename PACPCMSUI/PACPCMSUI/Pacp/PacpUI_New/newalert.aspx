<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newalert.aspx.cs" Inherits="newalert" %>

<%@ Register Src="~/usercontrols/menucontrol.ascx" TagPrefix="uc" TagName="MenuControl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <script src="scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <%--<script src="scripts/DateValidator.js" type="text/javascript"></script>--%>
    <script src="scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
    <%--<script src="scripts/jquery-ui-1.8.7.min.js" type="text/javascript"></script>--%>
    <script src="scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="jquery-ui.css" rel="stylesheet" type="text/css" />
    <meta charset="utf-8">
    <title>The Public Authority for Consumer Protection. | Sultanate of Oman</title>
    <link href="styles.css" rel="stylesheet" type="text/css" />
    <script>
        function Validate() {
            //alert('as');
            $("#dvlang").html('');
            $("#dvTitle").html('');
            $("#dvupload").html('');
            $("#dvDate").html('');
            $("#dvDescription").html('');
            var IsRequired = true;
            if ($("#ddlLang").val() == '0') {
                var $myDiv = $('#dvlang');
                if ($myDiv.length) {
                    $myDiv.html("Select Language");
                }
                else {
                    $("#ddlLang").after("<span id='dvlang' class='error'>Select Language</span>");
                }
                IsRequired = false;
            }
            if ($("#txtTitle").val() == '') {
                var $myDiv = $('#dvTitle');
                if ($myDiv.length) {
                    $myDiv.html("Enter Title");
                }
                else {
                    $("#txtTitle").after("<span id='dvTitle' class='error'>Enter Title</span>");
                }
                isrequired = false;
            }
            Checkfiles();
            //            if ($("#fUpload").length) {
            //                var $myDiv = $('#dvupload');
            //                if ($myDiv.length) {
            //                    $myDiv.html("select image to upload");
            //                }
            //                else {
            //                    $("#fUpload").after("<span id='dvupload' class='error'>select image to upload</span>");
            //                }
            //                isrequired = false;
            //            }
            if ($("#txtDate").val() == '') {
                var $myDiv = $('#dvDate');
                if ($myDiv.length) {
                    $myDiv.html("Enter Date");
                }
                else {
                    $(".ui-datepicker-trigger").after("<span id='dvDate' class='error'>Enter Date</span>");
                }
                isrequired = false;
            }
            if ($("#txtDescription").val() == '') {
                var $myDiv = $('#dvDescription');
                if ($myDiv.length) {
                    $myDiv.html("Enter Description");
                }
                else {
                    $("#txtDescription").after("<span id='dvDescription' class='error'>Enter Description</span>");
                }
                isrequired = false;
            }
            //            //var isrequired = true;
            //            if ($("#ddlLang").val() == '0') {
            //                $("#dvlang").html('');
            //                $("#ddlLang").after("<span id='dvlang' class='error'>Select Language</span>");
            //                //alert("please select language");
            //                isrequired = false;
            //            }
            //            if ($("#txtTitle").val() == '') {
            //                $("#dvTitle").html('');
            //                $("#txtTitle").after("<span id='dvTitle' class='error'>Enter Title</span>");
            //                isrequired = false;
            //            }
            //            if ($(".ui-datepicker-trigger").val() == '') {
            //                $("#dvDate").html('');
            //                $(".ui-datepicker-trigger").after("<span id='dvDate' class='error'>Enter Date</span>");
            //                isrequired = false;
            //            }
            //            if ($("#txtDescription").val() == '') {
            //                $("#dvDescription").html('');
            //                $("#txtDescription").after("<span id='dvDescription' class='error'>Enter Description</span>");
            //                isrequired = false;
            //            }
            //alert("please select language");
            return isrequired;
        }
        $(function () {
            $("#txtDate").datepicker({
                showOn: "button",
                buttonImage: "images/date.png",
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy"

            });
        });
    </script>
    <script language="javascript">
        function Checkfiles() {
            var fup = document.getElementById('fUpload');
            var fileName = fup.value;
            if (fileName == '') {
                $("#fUpload").after("<div id='dvupload' class='error'>select image to upload</div>");
            }
            else {
                var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
                if (ext == "gif" || ext == "GIF" || ext == "JPEG" || ext == "jpeg" || ext == "jpg" || ext == "JPG" || ext == "png" || ext == "PNG") {
                    return true;
                }
                else {
                    $("#fUpload").after("<div id='dvupload' class='error'>please upload Gif or Jpg images only</div>");
                    fup.focus();
                    return false;
                }
            }

        }
    </script>
</head>
<body>
    <form id="form2" runat="server">
    <div class="page">
        <div id="wrapper">
            <header></header>
            <uc:MenuControl runat="server" ID="ucMenuControl" />
            <section id="content" class="loginform" style="width: 878px !important;">

            <table width="60%" border="0" cellpadding="0" cellspacing="0">
                 <tr>
                        <td> <asp:Label ID="lblLanguage" runat="server" Text="Language"></asp:Label></td>
                        <td> 
                            <asp:DropDownList ID="ddlLang" runat="server" Width="125px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">English</asp:ListItem>
                                <asp:ListItem Value="2">Arabic</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                      <tr>
                        <td> <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label></td>
                        <td> <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
                           
                          </td>
                    </tr>
                      <tr>
                        <td> <asp:Label ID="lblImage" runat="server" Text="Image"></asp:Label></td>
                        <td> <input type="file" id = "fUpload" runat="server" style="margin-top:25px;"/>
                        <div style="float:right"> <asp:Image ID="imgAlt" 
                                runat="server" Height="80px" Width="100px" /></div>
                       
                          </td>
                          
                        
                    </tr>
                      <tr>
                        <td> <asp:Label ID="lblDate" runat="server" Text="Date" ></asp:Label>
                        
                        </td>
                        <td> <asp:TextBox ID="txtDate" runat="server" CssClass="date"></asp:TextBox>
                            
                          </td>
                    </tr>
                     <tr>
                        <td> <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td>
                        <td> <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                           
                         </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td> <asp:Button ID="Button1" runat="server" Text="Save" onclick="btnSave_Click" OnClientClick="return Validate();"></asp:Button></td>
                        
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td> <asp:Label ID="lblMsg" runat="server" CssClass="success" ForeColor="green"></asp:Label></td>
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
</body>d
</html>
