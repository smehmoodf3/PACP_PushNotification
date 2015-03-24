<%@ WebHandler Language="C#" Class="ImageHandler_" %>

using System;
using System.Web;
using PacpService;
////using Pacp;
using System.IO;
public class ImageHandler_ : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        int Id =0;
        if ( context.Request.QueryString["Id"]!= null)
        {
             Id = Convert.ToInt32(context.Request.QueryString["Id"]);
        }
        PacpClient sClient = new PacpClient();
        ClsAlerts altObj = sClient.GetAlertById(Id);
        string ImagePath = altObj.Image;
        byte[] image = null;

        //PRISIONER_PERSONAL_DTLS obj = PersonalDltsClient.FindBy(id).ToList().FirstOrDefault();
       // ImagePath = obj.IMAGE_LEFT;
        string FilePath = context.Request.PhysicalApplicationPath;

        if (string.IsNullOrEmpty(ImagePath))
        {
            FilePath += @"images/Default.jpg";
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader bs = new BinaryReader(fs);
            image = bs.ReadBytes((int)fs.Length);
        }
        else
        {
            try
            {
                FilePath = context.Request.PhysicalApplicationPath;
                FilePath += ImagePath;
                FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                BinaryReader bs = new BinaryReader(fs);
                image = bs.ReadBytes((int)fs.Length);
            }
            catch
            {
                // string FilePath = Request.PhysicalApplicationPath;
                FilePath += @"images/Default.jpg";
                FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                BinaryReader bs = new BinaryReader(fs);
                image = bs.ReadBytes((int)fs.Length);
            }
        }

        context.Response.BinaryWrite((Byte[])image);
        
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}