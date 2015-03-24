using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PacpService;
using System.ServiceModel.Description;
//using Pacp;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMsg.Text = string.Empty;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        PacpClient cltObj = new PacpClient();
     //   cltObj.Endpoint.Behaviors.Add(new WebHttpBehavior());
        clsUser usObj = cltObj.GetUserByNamePwd(txtUname.Text, txtPwd.Text, "User");

        if (usObj.UserName == null )
        {
            lblMsg.Text = "Invalid User Name / Password";
        }
        else
        {
            Session["UserId"] = usObj.Id;
            Response.Redirect("Index.aspx", false);
        }
    }
}