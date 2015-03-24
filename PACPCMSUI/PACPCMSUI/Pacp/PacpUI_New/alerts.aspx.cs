using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PacpService;
////using Pacp;
using System.IO;
public partial class alert : System.Web.UI.Page
{
    PacpClient clntobj;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["altId"] = null;
            fillAlerts();
        }
    }

    private void fillAlerts()
    {
        clntobj = new PacpClient();
        int Id = Session["UserId"] != null? Convert.ToInt32(Session["UserId"]):0;
        gvAlerts.DataSource = clntobj.GetAllAlerts().Where (s=>s.UserId == Id );
        gvAlerts.DataBind();
    }
    protected void gvAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType==  DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = (e.Row.DataItemIndex + 1).ToString();
            //string ImagePath = e.Row.Cells[4].Text;
            Image imgAlt = (Image)e.Row.Cells[4].FindControl("Image");
            string ImagePath = imgAlt.ImageUrl;
            byte[] image = null;

            //PRISIONER_PERSONAL_DTLS obj = PersonalDltsClient.FindBy(id).ToList().FirstOrDefault();
            //ImagePath = obj.IMAGE_LEFT;
            string FilePath = string.Empty;// Request.PhysicalApplicationPath;

            if (string.IsNullOrEmpty(ImagePath))
            {
                FilePath += "images/Default.jpg";
                //FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                //BinaryReader bs = new BinaryReader(fs);
                //image = bs.ReadBytes((int)fs.Length);
            }
            else
            {
                try
                {
                    //FilePath = Request.PhysicalApplicationPath;
                    FilePath += ImagePath.Replace("\\","/");
                    //FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    //BinaryReader bs = new BinaryReader(fs);
                    //image = bs.ReadBytes((int)fs.Length);
                }
                catch
                {
                    // string FilePath = Request.PhysicalApplicationPath;
                    FilePath += "images/Default.jpg";
                    //FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    //BinaryReader bs = new BinaryReader(fs);
                    //image = bs.ReadBytes((int)fs.Length);
                }
            }
            imgAlt.ImageUrl = FilePath;
        }
    }
    protected void gvAlerts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName=="Manage")
        {
            //object st = e.CommandArgument;
            int rowId = e.CommandArgument != null ? Convert.ToInt32(e.CommandArgument) : 0;
            Label lblId =(Label) gvAlerts.Rows[rowId].FindControl("lblId");
            Session["altId"] = Convert.ToInt32 (lblId.Text);
            Response.Redirect("newalert.aspx");
        }
    }

}