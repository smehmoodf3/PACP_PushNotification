using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PacpService;
//using Pacp;
public partial class register_form : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMsg.Text = string.Empty;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        PacpClient cltObj = new PacpClient();
        clsUser usObj = new clsUser();
        usObj.Name = txtName.Text.Trim();
        usObj.UserName = txtUseName.Text.Trim();
        usObj.Password = txtPwd.Text.Trim();
        usObj.Email = txtEmail.Text.Trim();
        usObj.UserType = "User";
        var x = cltObj.GetAllUsers().Where (s=>s.UserName == txtUseName.Text).FirstOrDefault();

        if (x == null)
        {
            string st = cltObj.InsertUser(usObj);

            if (st == "Inserted")
            {
                lblMsg.Text = "Registered Successfully";
                ClearControls();
            }
            else
            {
                lblMsg.Text = "Registeration Failed";
            }
        }
        else
        {
            lblMsg.Text = "User Name Is Already Exist ";
        }
        
    }

    private void ClearControls()
    {
       txtName.Text = string.Empty ;
       txtUseName.Text=string.Empty ;
       txtPwd.Text=string .Empty ;
       txtEmail.Text=string .Empty ;
    }
}