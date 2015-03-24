using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace TestBBNotification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            

            //pushMessageStatus();

            //subscriptionQuery();

            //String dt = "2015-03-18T00:00:00Z";

            /*
            pushMessageSample("Test msg from shoaib 1",dt);
            dt = "2015-03-18T01:00:00Z";
            pushMessageSample("Test msg from shoaib 2", dt);
            dt = "2015-03-18T02:00:00Z";
            pushMessageSample("Test msg from shoaib 3", dt);
            dt = "2015-03-18T03:00:00Z";
            pushMessageSample("Test msg from shoaib 4", dt);
            dt = "2015-03-18T04:00:00Z";
            pushMessageSample("Test msg from shoaib 5", dt);
            dt = "2015-03-18T05:00:00Z";
            pushMessageSample("Test msg from shoaib 6", dt);
            dt = "2015-03-18T06:00:00Z";
            pushMessageSample("Test msg from shoaib 7", dt);
            dt = "2015-03-18T07:00:00Z";
            pushMessageSample("Test msg from shoaib 8", dt);
            dt = "2015-03-18T08:00:00Z";
            pushMessageSample("Test msg from shoaib 9", dt);
            dt = "2015-03-18T09:00:00Z";
            pushMessageSample("Test msg from shoaib 10", dt);
            dt = "2015-03-18T10:00:00Z";
            
            
 

            pushMessageSample("Test msg from shoaib 11", dt);
            dt = "2015-03-18T11:00:00Z";
            pushMessageSample("Test msg from shoaib 12", dt);
            dt = "2015-03-18T12:00:00Z";
            pushMessageSample("Test msg from shoaib 13", dt);
            dt = "2015-03-18T13:00:00Z";
            pushMessageSample("Test msg from shoaib 14", dt);
         
             
            dt = "2015-03-18T13:15:00Z";
            pushMessageSample("Test msg from shoaib 15", dt);
            dt = "2015-03-18T15:00:00Z";
            pushMessageSample("Test msg from shoaib 16", dt);
            dt = "2015-03-18T16:00:00Z";
            pushMessageSample("Test msg from shoaib 17", dt);
            dt = "2015-03-18T17:00:00Z";
            pushMessageSample("Test msg from shoaib 18", dt);
            dt = "2015-03-18T18:00:00Z";
            pushMessageSample("Test msg from shoaib 19", dt);
            dt = "2015-03-18T19:00:00Z";
            pushMessageSample("Test msg from shoaib 20", dt);
            dt = "2015-03-18T20:00:00Z";
            pushMessageSample("Test msg from shoaib 21", dt);
            dt = "2015-03-18T21:00:00Z";
            pushMessageSample("Test msg from shoaib 22", dt);
            dt = "2015-03-18T22:00:00Z";
            pushMessageSample("Test msg from shoaib 23", dt);
            dt = "2015-03-18T23:00:00Z";
            
             * 
                */
            pushMessageSample("Test msg from shoaib 24", "");

            pushMessageStatus();
            
            


        }


        private void pushMessageSample(string pushedMessage,String dt)
        {
            String appid = "5250-89121B5r92i5M2n69040m1Mi4ha7229k965";
            String password = "V3287bmd";
            String deliverbefore = DateTime.UtcNow.AddMinutes(5).ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
            //String deliverbefore = dt;
            String pushPin = "2AB7AC7E";
            String Boundary = "mPsbVQo0a68eIL3OAxnm";
            //String Boundary="ASDFaslkdfjasfaSfdasfhpoiurwqrwm";

            //String Boundary = "ASDFaslkdfjasfaSfdasfhpoiurwqrwm";

            

            StringBuilder dataToSend = new StringBuilder();

            dataToSend.AppendLine("--" + Boundary);
            dataToSend.AppendLine("Content-Type: application/xml; charset=UTF-8");

            dataToSend.AppendLine("");
            dataToSend.AppendLine("<?xml version=\"1.0\"?>");
            dataToSend.AppendLine("<!DOCTYPE pap PUBLIC \"-//WAPFORUM//DTD PAP 2.1//EN\" \"http://www.openmobilealliance.org/tech/DTD/pap_2.1.dtd\">");
            dataToSend.AppendLine("<pap>");
            string myPushId = DateTime.Now.ToFileTime().ToString();
            dataToSend.AppendLine("<push-message push-id=" + (char)34 + myPushId + (char)34 + " deliver-before-timestamp=" +
           (char)34 + deliverbefore + (char)34 + " source-reference=" + (char)34 + appid + (char)34 + ">");
            //dataToSend.AppendLine("<push-message push-id=\"" + myPushId + "\" source-reference=\"" + appid + "\">");
            dataToSend.AppendLine("<address address-value=\"" + pushPin + "\"/>");
            //dataToSend.AppendLine("<address address-value=\"" + "push_all" + "\"/>");
            dataToSend.AppendLine("<quality-of-service delivery-method=\"unconfirmed\"/>");
            dataToSend.AppendLine("</push-message>");
            dataToSend.AppendLine("</pap>");
            dataToSend.AppendLine("--" + Boundary);

            //dataToSend.AppendLine("Content-Type: text/plain");
            dataToSend.AppendLine("Push-Message-ID: " + myPushId);
            dataToSend.AppendLine("");

            dataToSend.AppendLine(pushedMessage);

            dataToSend.AppendLine("--" + Boundary + "--");
            dataToSend.AppendLine("");

            byte[] bytes = Encoding.ASCII.GetBytes(dataToSend.ToString());
            String httpURL = "https://cp5250.pushapi.eval.blackberry.com/mss/PD_pushRequest";
            //String httpURL = "https://pushapi.eval.blackberry.com/mss/PD_pushRequest";

            WebRequest tRequest;
            tRequest = WebRequest.Create(httpURL);
            //SetProxy(tRequest);
            tRequest.Method = "POST";
            tRequest.ContentType = "text/plain";

            //tRequest.ContentLength = bytes.Length;
            tRequest.Credentials = new NetworkCredential(appid, password);

            tRequest.PreAuthenticate = true;
            tRequest.ContentType = "multipart/related; boundary=" + Boundary + "; type=application/xml";
            tRequest.ContentLength = bytes.Length;
            string rawCredentials = string.Format("{0}:{1}", appid, password);
            tRequest.Headers.Add("Authorization",
                string.Format(
                    "Basic {0}",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(rawCredentials))));

            SetBasicAuthHeader(tRequest, appid, password);

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }

        public static void SetBasicAuthHeader(WebRequest req, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            req.Headers["Authorization"] = "Basic " + authInfo;
        }

        private void pushMessageStatus()
        {
            String appid = "5250-89121B5r92i5M2n69040m1Mi4ha7229k965";
            String password = "V3287bmd";
            String deliverbefore = DateTime.UtcNow.AddMinutes(5).ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
            String pushPin = "2AB7AC7E";
            String Boundary = "mPsbVQo0a68eIL3OAxnm";
            //String Boundary="ASDFaslkdfjasfaSfdasfhpoiurwqrwm";

            StringBuilder dataToSend = new StringBuilder();

            dataToSend.AppendLine("<?xml version=\"1.0\"?>");
            dataToSend.AppendLine("<!DOCTYPE pap PUBLIC \"-//WAPFORUM//DTD PAP 2.1//EN\" \"http://www.openmobilealliance.org/tech/DTD/pap_2.1.dtd\">");
            dataToSend.AppendLine("<pap>");

            dataToSend.AppendLine("<statusquery-message push-id=\"130711613946998028\">");
            dataToSend.AppendLine("</statusquery-message>");
            dataToSend.AppendLine("</pap>");
           

           

            byte[] bytes = Encoding.ASCII.GetBytes(dataToSend.ToString());
            String httpURL = "https://cp5250.pushapi.eval.blackberry.com/mss/PD_pushRequest";

            WebRequest tRequest;
            tRequest = WebRequest.Create(httpURL);
            //SetProxy(tRequest);
            tRequest.Method = "POST";
            //tRequest.ContentType = "text/plain";

            //tRequest.ContentLength = bytes.Length;
            tRequest.Credentials = new NetworkCredential(appid, password);

            tRequest.PreAuthenticate = true;
            tRequest.ContentType = "multipart/related; boundary=" + Boundary + "; type=application/xml";
            tRequest.ContentLength = bytes.Length;
            string rawCredentials = string.Format("{0}:{1}", appid, password);
            tRequest.Headers.Add("Authorization",
                string.Format(
                    "Basic {0}",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(rawCredentials))));

            //SetBasicAuthHeader(tRequest, appid, password);

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }

        private void subscriptionQuery()
        {

            String appid = "5251-B58607eer2R262278ra95298cc22559k5s6"; //Qaisar
            //String appid = "5250-89121B5r92i5M2n69040m1Mi4ha7229k965";
            String password = "LD84yZ4T";
            //String password = "V3287bmd";
            String deliverbefore = DateTime.UtcNow.AddMinutes(5).ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
            String pushPin = "2AB7AC7E";
            String Boundary = "mPsbVQo0a68eIL3OAxnm";

            StringBuilder dataToSend = new StringBuilder();

            dataToSend.AppendLine("<?xml version=\"1.0\"?>");
            dataToSend.AppendLine("<!DOCTYPE pap PUBLIC \"-//WAPFORUM//DTD PAP 2.1//EN\" \"http://www.openmobilealliance.org/tech/DTD/pap_2.1.dtd\">");
            dataToSend.AppendLine("Content-Type: text/plain");
            dataToSend.AppendLine("<pap>");
          dataToSend.AppendLine("<bpds>");
	     // dataToSend.AppendLine("<subscriptionquery-message pushservice-id=\"5251-89121B5r92i5M2n69040m1Mi4ha722\">");
		//dataToSend.AppendLine("<status status-value=\"active\"/>");
	//dataToSend.AppendLine("</subscriptionquery-message>");
    dataToSend.AppendLine("</bpds>");
            dataToSend.AppendLine("</pap>");





            byte[] bytes = Encoding.ASCII.GetBytes(dataToSend.ToString());
            String httpURL = "https://cp5251.pushapi.eval.blackberry.com/mss/PD_pushRequest";

            WebRequest tRequest;
            tRequest = WebRequest.Create(httpURL);
            //SetProxy(tRequest);
            tRequest.Method = "POST";
            //tRequest.ContentType = "text/plain";

            //tRequest.ContentLength = bytes.Length;
            tRequest.Credentials = new NetworkCredential(appid, password);

            tRequest.PreAuthenticate = true;
            tRequest.ContentType = "multipart/related; boundary=" + Boundary + "; type=application/xml";
            tRequest.ContentLength = bytes.Length;
            string rawCredentials = string.Format("{0}:{1}", appid, password);
            tRequest.Headers.Add("Authorization",
                string.Format(
                    "Basic {0}",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(rawCredentials))));

            //SetBasicAuthHeader(tRequest, appid, password);

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        
        
        
        
        
        
        
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        // Code from code project
 
        /*
        
        private void pushMessageSample(string pushedMessage)
        {
            String appid = "5251-B58607eer2R262278ra95298cc22559k5s6";// Qaisar bhai
            //String appid = "5250-89121B5r92i5M2n69040m1Mi4ha7229k965"; //shoaib
            

            String password = "LD84yZ4T";  // Qaisar
           //String password = "V3287bmd";
            //String deliverbefore = DateTime.UtcNow.AddMinutes(5).ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
            //String deliverbefore = DateTime.UtcNow.AddMinutes(5).ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
            string deliverbefore = "2015-03-17T22:00:00Z";
            String pushPin = "2AB7AC7E";
            String Boundary = "mPsbVQo0a68eIL3OAxnm";

            StringBuilder dataToSend = new StringBuilder();

            dataToSend.AppendLine("--" + Boundary);
            dataToSend.AppendLine("Content-Type: application/xml; charset=UTF-8");
            dataToSend.AppendLine("");
            dataToSend.AppendLine("<?xml version=\"1.0\"?>");
            dataToSend.AppendLine("<!DOCTYPE pap PUBLIC \"-//WAPFORUM//DTD PAP 2.1//EN\" \"http://www.openmobilealliance.org/tech/DTD/pap_2.1.dtd\">");
            dataToSend.AppendLine("<pap>");
            string myPushId = DateTime.Now.ToFileTime().ToString();
            dataToSend.AppendLine("<push-message push-id=" + (char)34 + myPushId + (char)34 + " deliver-before-timestamp=" +
            (char)34 + deliverbefore + (char)34 + " source-reference=" + (char)34 + appid + (char)34 + ">");
            //dataToSend.AppendLine("<push-message push-id=\"" + myPushId + "\" source-reference=\"" + appid + "\">");
            dataToSend.AppendLine("<address address-value=\"" + pushPin + "\"/>");
            dataToSend.AppendLine("<quality-of-service delivery-method=\"unconfirmed\"/>");
            dataToSend.AppendLine("</push-message>");
            dataToSend.AppendLine("</pap>");
            dataToSend.AppendLine("--" + Boundary);
            dataToSend.AppendLine("Content-Type: text/plain");
            dataToSend.AppendLine("Push-Message-ID: " + myPushId);
            dataToSend.AppendLine("");
            dataToSend.AppendLine(pushedMessage);
            dataToSend.AppendLine("--" + Boundary + "--");
            dataToSend.AppendLine("");

            byte[] bytes = Encoding.ASCII.GetBytes(dataToSend.ToString());
            String httpURL = "https://cp5251.pushapi.eval.blackberry.com/mss/PD_pushRequest";

            WebRequest tRequest;
            tRequest = WebRequest.Create(httpURL);
            tRequest.Method = "POST";
            tRequest.Credentials = new NetworkCredential(appid, password);
            tRequest.PreAuthenticate = true;
            tRequest.ContentType = "multipart/related; boundary=" + Boundary + "; type=application/xml";
            tRequest.ContentLength = bytes.Length;
            string rawCredentials = string.Format("{0}:{1}", appid, password);
            tRequest.Headers.Add("Authorization", string.Format("Basic {0}",
            Convert.ToBase64String(Encoding.UTF8.GetBytes(rawCredentials))));
            SetBasicAuthHeader(tRequest, appid, password);

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }


        private void statusQueryMsg()
        {
            String appid = "5251-B58607eer2R262278ra95298cc22559k5s6";// Qaisar bhai
            //String appid = "5250-89121B5r92i5M2n69040m1Mi4ha7229k965"; //shoaib


            String password = "LD84yZ4T";  // Qaisar
            //String password = "V3287bmd";
            //String deliverbefore = DateTime.UtcNow.AddMinutes(5).ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
            //String deliverbefore = DateTime.UtcNow.AddMinutes(5).ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
            string deliverbefore = "2015-03-17T22:00:00Z";
            String pushPin = "2AB7AC7E";
            String Boundary = "mPsbVQo0a68eIL3OAxnm";

            StringBuilder dataToSend = new StringBuilder();

            dataToSend.AppendLine("--" + Boundary);
            dataToSend.AppendLine("Content-Type: application/xml; charset=UTF-8");
            dataToSend.AppendLine("");
            dataToSend.AppendLine("<?xml version=\"1.0\"?>");
            dataToSend.AppendLine("<!DOCTYPE pap PUBLIC \"-//WAPFORUM//DTD PAP 2.1//EN\" \"http://www.openmobilealliance.org/tech/DTD/pap_2.1.dtd\">");
            dataToSend.AppendLine("<pap>");
            dataToSend.AppendLine("<statusquery-message>");
            dataToSend.AppendLine("</pap>");
            

            byte[] bytes = Encoding.ASCII.GetBytes(dataToSend.ToString());
            String httpURL = "https://cp5251.pushapi.eval.blackberry.com/mss/PD_pushRequest";

            WebRequest tRequest;
            tRequest = WebRequest.Create(httpURL);
            tRequest.Method = "POST";
            tRequest.Credentials = new NetworkCredential(appid, password);
            tRequest.PreAuthenticate = true;
            tRequest.ContentType = "multipart/related; boundary=" + Boundary + "; type=application/xml";
            tRequest.ContentLength = bytes.Length;
            string rawCredentials = string.Format("{0}:{1}", appid, password);
            tRequest.Headers.Add("Authorization", string.Format("Basic {0}",
            Convert.ToBase64String(Encoding.UTF8.GetBytes(rawCredentials))));
            SetBasicAuthHeader(tRequest, appid, password);

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }


        public static void SetBasicAuthHeader(WebRequest req, String appID, String userPassword)
        {
            string authInfo = appID + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            req.Headers["Authorization"] = "Basic " + authInfo;
        }

        */



    }
}
