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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

      
            public void pushToWidget(string pushedMessage)
        {
           // String BESAddress = "https://pushapi.eval.blackberry.com/mss/PD_pushRequest";
            //String BESWebserverListenPort = "pushPort";
            String widgetNotificationUrl = "https://pushapi.eval.blackberry.com/mss/PD_pushRequest";
            String pushUserName = "5251-B58607eer2R262278ra95298cc22559k5s6";
            String pushPassword = "LD84yZ4T";


          //  String pushPort = "33215";
            string Boundary = "Boundary ";
            string DeliverBefore = DateTime.UtcNow.AddMinutes(5).ToString("s", System.Globalization.CultureInfo.InvariantCulture) + "Z";
            
            bool success = true;
            StringBuilder Data = new StringBuilder();
            Data.AppendLine("--" + Boundary);
            Data.AppendLine("Content-Type: application/xml; charset=utf-8");
            Data.AppendLine("");
            Data.AppendLine("<?xml version=\"1.0\"?>");
            Data.AppendLine("<!DOCTYPE pap PUBLIC \"-//WAPFORUM//DTD PAP 2.1//EN\">");
            Data.AppendLine("<pap>");
            Data.AppendLine("<push-message push-id=" + (char)34 + pushUserName + (char)34 + " deliver-before-timestamp=" +

(char)34 + DeliverBefore + (char)34 + " source-reference=" + (char)34 + pushUserName + (char)34 + ">");
            Data.AppendLine("<address address-value=\"" + "push_all" + "\"/>");
            Data.AppendLine("<quality-of-service delivery-method=\"unconfirmed\"/>");
            Data.AppendLine("</push-message>");
            Data.AppendLine("</pap>");
            Data.AppendLine("--" + Boundary);
            Data.AppendLine("Content-Type: text/plain");
            Data.AppendLine("Push-Message-ID: " + pushUserName);
            Data.AppendLine("");
            Data.AppendLine(pushedMessage);
            Data.AppendLine("--" + Boundary + "--");
            Data.AppendLine("");
            byte[] bytes = Encoding.ASCII.GetBytes(Data.ToString());

            Stream requestStream = null;
            HttpWebResponse HttpWRes = null;
            HttpWebRequest HttpWReq = null;

            try
            {
                //http://<BESName>:<BESPort>/push?DESTINATTION=<PIN/EMAIL>&PORT=<PushPort>&REQUESTURI=/
               // Build the URL to define our connection to the BES.

                String BESName = "cp5251.pushapi.eval.blackberry.com/mss/PD_pushRequest";

                string httpURL = "https://" + BESName + "/push?DESTINATION=2B838E45&PORT=33215&REQUESTURI=/";

             //   string httpURL = BESAddress + ":" + BESWebserverListenPort+ "/push?DESTINATION=" + pushPin + "&PORT=" + pushPort+ "&REQUESTURI=/";

                //make the connection
                HttpWReq = (HttpWebRequest)WebRequest.Create(httpURL);
                HttpWReq.Method = ("POST");
                //add the headers nessecary for the push
                HttpWReq.ContentType = "text/plain";
                HttpWReq.ContentLength = bytes.Length;
                // ******* Test this *******
                HttpWReq.Headers.Add("X-Rim-Push-Id", "push_all" + "~" + DateTime.Now); //"~" +pushedMessage +
                HttpWReq.Headers.Add("X-Rim-Push-Reliability", "application-preferred");
                HttpWReq.Headers.Add("X-Rim-Push-NotifyURL", (widgetNotificationUrl + "push_all" + "~" + pushedMessage + "~" + DateTime.Now).Replace(" ", ""));

                // *************************

               // HttpWReq.Credentials = new MDSCredentials(pushUserName, pushPassword);
                HttpWReq.Credentials = new NetworkCredential(pushUserName, pushPassword);

                Console.WriteLine(pushedMessage);
                requestStream = HttpWReq.GetRequestStream();
                //Write the data from the source
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                //get the response
                HttpWRes = (HttpWebResponse)HttpWReq.GetResponse();

                //if the MDS received the push parameters correctly it will either respond with okay or accepted
                if (HttpWRes.StatusCode == HttpStatusCode.OK || HttpWRes.StatusCode == HttpStatusCode.Accepted)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
                //Close the streams

                HttpWRes.Close();
                requestStream.Close();
            }
            catch (Exception e) { }
           
        }

            private void button1_Click(object sender, EventArgs e)
            {

            } 


    }
  
}

