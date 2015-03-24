using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PacpService;
using System.Xml;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

//using Pacp;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;

public partial class newalert : System.Web.UI.Page
{
    int altId;
    PacpClient clntobj;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMsg.Text = string.Empty;

            altId = Session["altId"] != null ? fillControls(Convert.ToInt32(Session["altId"])) : 0;
        }
    }
    private int fillControls(int altId)
    {
        clntobj = new PacpClient();

        ClsAlerts altObj = clntobj.GetAlertById(altId);

        if (altObj != null)
        {
            ddlLang.SelectedItem.Text = altObj.Language;
            txtTitle.Text = altObj.Title;
            txtDescription.Text = altObj.Description;
            //fUpload.FileName =altObj.Image;
            //imgAlt.ImageUrl = altObj.Image != string.Empty ? altObj.Image : "images/Default.jpg";
            imgAlt.ImageUrl = altObj.Image != null ? altObj.Image.Replace("\\", "/") : "images/Default.jpg";
            ClsApplication apobj = new ClsApplication();
            txtDate.Text = apobj.DatetoString(altObj.Date);
        }

        return altId;
    }
    public DateTime? DateFormate(string FormateDate, string Formate)
    {
        DateTime? dt = null;
        if (!string.IsNullOrEmpty(FormateDate))
        {
            string input = FormateDate;

            if (Formate.ToUpper() == "DD/MM/YYYY")
            {
                dt = Convert.ToDateTime(Regex.Replace(input, @"\b(?<day>\d{1,2})/(?<month>\d{1,2})/(?<year>\d{2,4})\b", "${day}/${month}/${year}"));
            }

            else if (Formate.ToUpper() == "YYYY/MM/DD")
            {
                dt = Convert.ToDateTime(Regex.Replace(input, @"\b(?<day>\d{1,2})/(?<month>\d{1,2})/(?<year>\d{2,4})\b", "${year}/${month}/${day}"));
            }
            else
            {
                dt = Convert.ToDateTime(Regex.Replace(input, @"\b(?<day>\d{1,2})/(?<month>\d{1,2})/(?<year>\d{2,4})\b", "${month}/${day}/${year}"));
            }

        }
        return dt;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ClsAlerts altObj = new ClsAlerts();
        altObj.Language = ddlLang.SelectedItem.Text;
        altObj.Title = txtTitle.Text;
        altObj.Description = txtDescription.Text;
        //altObj.Date = Convert.ToDateTime(txtDate.Text);
        altObj.Date = DateFormate(txtDate.Text, "");// Convert.ToDateTime(txtDate.Text);
        altObj.UserId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0;
        var image = Request.Files[0];
        if (image.ContentLength > 0)
        {
            int ExtIndex = image.FileName.LastIndexOf('.');
            string fileExt = image.FileName.Remove(0, ExtIndex);

            string savedFileName = Path.Combine("Alerts\\", Guid.NewGuid() + fileExt);
            image.SaveAs(Request.PhysicalApplicationPath + savedFileName);
            altObj.Image = savedFileName;//.Replace("\","");
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(image.InputStream))
            {
                fileData = binaryReader.ReadBytes(image.ContentLength);
            }

            altObj.Content = fileData;
        }
        clntobj = new PacpClient();


        if (Session["altId"] != null)
        {
            altObj.Id = Convert.ToInt32(Session["altId"]);
            string jsonResponse = clntobj.GetUpdateAlertResponse(altObj);

            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic updateAlertResponse = js.DeserializeObject(jsonResponse);
            lblMsg.Text = updateAlertResponse["Message"];
            //lblMsg.Text = clntobj.UpdateAlert(altObj) == "Updated" ? "Alert Saved Successfully" : "Alert Is Not Saved";
            fillControls(altObj.Id);
            Notification note = new Notification();
            note.sendAndroidNotifications(0, updateAlertResponse["Data"]["Title"], updateAlertResponse["Data"]["Description"], updateAlertResponse["Data"]["Language"], updateAlertResponse["Data"]["Id"]);
            note.sendAppleNotifications(0);
        }
        else
        {
            string jsonResponse = clntobj.GetInsertAlertResponse(altObj);
            JavaScriptSerializer js = new JavaScriptSerializer();
            dynamic insertAlertResponse = js.DeserializeObject(jsonResponse);
            lblMsg.Text = insertAlertResponse["Message"];

            Notification note = new Notification();
            note.sendAndroidNotifications(0, insertAlertResponse["Data"]["Title"], insertAlertResponse["Data"]["Description"], insertAlertResponse["Data"]["Language"], insertAlertResponse["Data"]["Id"]);
            note.sendAppleNotifications(0);
            //lblMsg.Text = clntobj.InsertAlert(altObj) == "Inserted" ? "Alert Saved Successfully" : "Alert Is Not Saved";
        }



        //if (Session["altId"] == null)
        //{
        ClearControls();
        //}
    }

    private T Deserialize<T>(string json)
    {
        T returnValue;
        using (var memoryStream = new MemoryStream())
        {
            byte[] jsonBytes = Encoding.UTF8.GetBytes(json);
            memoryStream.Write(jsonBytes, 0, jsonBytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            using (var jsonReader = JsonReaderWriterFactory.CreateJsonReader(memoryStream, Encoding.UTF8, XmlDictionaryReaderQuotas.Max, null))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                returnValue = (T)serializer.ReadObject(jsonReader);

            }
        }
        return returnValue;
    }
    private void ClearControls()
    {
        ddlLang.SelectedIndex = 0;
        txtTitle.Text = string.Empty;
        txtDate.Text = string.Empty;
        //fUpload.Text = string.Empty;
        txtDescription.Text = string.Empty;
        imgAlt.ImageUrl = string.Empty;
    }
}