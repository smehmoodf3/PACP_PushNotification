using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PacpService;
//using Pacp;
public partial class index : System.Web.UI.Page
{
    PacpClient clntobj;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("login.aspx", false);
        }
        if (!IsPostBack)
        {
            Session["ar"] = Session["ar"] == null ? string.Empty : Session["ar"];
            Session["en"] = Session["en"] == null ? string.Empty : Session["en"];
            fillEditor();
            lblMsg.Text = string.Empty;
        }
    }

    private void fillEditor()
    {
        clntobj = new PacpClient();
        ClsAbout abtObj = clntobj.GetAboutById(1);
        string st = Session["ar"].ToString();
        string st1 = Session["en"].ToString();
        FCKeditor1.Value = Session["en"] == string.Empty ? (Session["ar"] == string.Empty ? HttpUtility.HtmlDecode(abtObj.Text) : HttpUtility.HtmlDecode(abtObj.AText)) : (abtObj != null ? HttpUtility.HtmlDecode(abtObj.Text) : string.Empty);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        clntobj = new PacpClient();

        ClsAbout dbObj = clntobj.GetAboutById(1);

        ClsAbout abtObj = new ClsAbout();
        abtObj.Id = 1;
        string st2 = Session["ar"].ToString();
        string st1 = Session["en"].ToString();
        if (Session["en"] != string.Empty)
        {
            abtObj.Text = Server.HtmlEncode(FCKeditor1.Value);
            abtObj.AText  = dbObj .AText ;
        }
        else if (Session["ar"] != string.Empty)
        {
            abtObj.Text = dbObj.Text;
            abtObj.AText = Server.HtmlEncode(FCKeditor1.Value);
        }
        else {
            abtObj.Text = dbObj.Text;
            abtObj.Text = Server.HtmlEncode(FCKeditor1.Value);
        }

        string st = clntobj.UpdateAbout(abtObj);
        lblMsg.Text = st == "Updated" ? "Saved Successfully":"Saving Failed";
        fillEditor();
    }
    protected void imgArabic_Click(object sender, ImageClickEventArgs e)
    {
        Session["ar"] = "ar";
        Session["en"] = string.Empty;
        fillEditor();
    }
    protected void imgEnglish_Click(object sender, ImageClickEventArgs e)
    {
        Session["ar"] = string.Empty;
        Session["en"] = "en";
        fillEditor();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx", false);
    }
}