using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MenuControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"]== null)
        {
            Response.Redirect("login.aspx",false );
        }
        if (!IsPostBack)
        {
            Session["ar"] = string.Empty;
            Session["en"] = string.Empty;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("login.aspx",false );
    }
    protected void imgArabic_Click(object sender, ImageClickEventArgs e)
    {
        Session["ar"] = "ar";
        Session["en"] = string.Empty;
        //fillEditor();
    }
    protected void imgEnglish_Click(object sender, ImageClickEventArgs e)
    {
        Session["ar"] = string.Empty;
        Session["en"] = "en";
        //fillEditor();
    }
}