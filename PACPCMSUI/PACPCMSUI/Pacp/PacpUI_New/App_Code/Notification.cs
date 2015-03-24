using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using MoonAPNS;
using PacpService;

/// <summary>
/// Summary description for Notification
/// </summary>
public class Notification
{
    PacpClient clntobj = new PacpClient();
    public string apns_Pass_Phrase = WebConfigurationManager.AppSettings["apns_Pass_Phrase"];//"pacp123"; 
    public string apns_certificate_path = WebConfigurationManager.AppSettings["apns_certificate_path"];//"C:/Users/ibaig/Downloads/PACP_PUSH_STUFF/PACP_PUSH_STUFF/for windows/openSSL/bin/pacp_prod.p12";
    public string gcm_applicationId = WebConfigurationManager.AppSettings["gcm_applicationId"];//"AIzaSyBfHCkNwarsjtIs9CsRN81UKzZpmVWoryQ";
    public string gcm_sender_Id = WebConfigurationManager.AppSettings["gcm_sender_Id"];//"active-district-761";
    public string message = WebConfigurationManager.AppSettings["message"];//"You have a new Notification ";
    //0:News, 1:Recall, 2:Warning, 3:Awareness, 4:Other Message
    public string[] messageTypes = {"News","Recall","Warning","Awareness","Other Message"};
	public Notification()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string sendAndroidNotifications(int notificationType, string alertTitle , string alertDescription,string language, int alertId = 0)
    {
        try
        {
          
            // your RegistrationID paste here which is received from GCM server.                                                               
            //string regId = "APA91bFh7gPaRQy2yJ5odP13HPDSinkziu4L6VjRrJ1SaKF9yI9AKQu4VY7u9uYw3Y7AxuHXWNbIZFXemQS59Qrh1HYcGLFs8HgOg32fxI87ranAkK2g5Fc4soowI6OfMX-UduibKYsmGj1nu_Tc1CpO8sJ23yk7GHPYYt80qKfqsf-W65T46GA";                                                                                              
            // applicationID means google Api key                                                                                                     
            //var applicationID = "AIzaSyBfHCkNwarsjtIs9CsRN81UKzZpmVWoryQ";                                                         
            // SENDER_ID is nothing but your ProjectID (from API Console- google code)//                                          
            //var SENDER_ID = "active-district-761"; 
            //var value = txtMessage.Text; //message text box 
            
            //[Sarim] : It is not used for now , the notification title is alert's title and notification message is alert's description as per requirement.
            //[Sarim] : To send notification message , replace alertDescription from postData
            var notificationMessage = message + messageTypes[notificationType];
            int deviceType = 1;
            List<Device> devices = getDevices(deviceType);
            if(devices.Count > 0)
            {
                foreach(var device in devices)
                {  
                    string regId = device.deviceRegistrationId;
                    WebRequest tRequest;
                    tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send"); 
                    tRequest.Method = "post"; 
                    tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", gcm_applicationId)); 
                    tRequest.Headers.Add(string.Format("Sender: id={0}", gcm_sender_Id));

                    //Sarim: Reducing the description to small length so that the GCM notification size rules are not violated.

                    if(alertDescription.Length > 100)
                        alertDescription = alertDescription.Substring(0, 100);

                    //[Sarim]: Encoded the title and description for arabic language. 
                    var encodedTitle = HttpUtility.UrlEncode(alertTitle);
                    var encodedDescription = HttpUtility.UrlEncode(alertDescription);
                    
                    string lang = (language == "Arabic") ? "ar" : "en";
                    //Data post to server   
                    //Sarim: Adding lang parameter to the GCM postData                                                                                           
                    string postData =
                      "collapse_key=score_update&time_to_live=1208&delay_while_idle=1&data.message="
                       + encodedDescription + "&data.messageType=" + notificationType + "&data.title=" + encodedTitle + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" +
                          regId + "&data.redirect=3" + "&data.alertid=" + alertId + "&data.lang=" + lang;

                    
                     //Console.WriteLine(postData);
                     Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                     tRequest.ContentLength = byteArray.Length; 
                     Stream dataStream = tRequest.GetRequestStream();
                     dataStream.Write(byteArray, 0, byteArray.Length);
                     dataStream.Close();
                     WebResponse tResponse = tRequest.GetResponse(); 
                     dataStream = tResponse.GetResponseStream();
                     StreamReader tReader = new StreamReader(dataStream);
                     String sResponseFromServer = tReader.ReadToEnd();   //Get response from GCM server.
                     //lblMessage.Text = sResponseFromServer;      //Assigning GCM response to Label text 
                     tReader.Close();
                     dataStream.Close();                                                                                                             
                     tResponse.Close(); 
                }
                string notification_Status = "Notification Send to Android devices";
                return notification_Status;
            }
            else
            {
                string state = "No registered device found";
                return  state;
            }

        }
        catch(Exception ex)
        {
            return ex.ToString();
        }
    }

    public string sendAppleNotifications(int notificationType)
    {
        try
        {
            //var deviceToken = "060c44f853310d169bb2b75f65d5c25c463fc279e82798fca666510e2c8891ee";
            //var msg = txtMessage.Text;
            var notificationMessage = message + messageTypes[notificationType];
            int deviceType = 2;
            List<Device> devices = getDevices(deviceType);
            if(devices.Count > 0)
            {
                foreach(var device in devices)
                {
                    string deviceToken = device.deviceRegistrationId;
                    var payload = new NotificationPayload(deviceToken, notificationMessage, 1); // 1 is the badge no
                    payload.AddCustom("messageType", notificationType);
                    var notificationList = new List<NotificationPayload>() { payload };
                    //var filepath="C:/Users/ibaig/Downloads/PACP_PUSH_STUFF/PACP_PUSH_STUFF/for windows/openSSL/bin/PushPACP.p12";
                    //string apns_Pass_Phrase = "pacp123";
                    var push = new PushNotification(true, apns_certificate_path, apns_Pass_Phrase);
                    var result = push.SendToApple(notificationList); // You are done!}
                }
                string notification_Status =  "Notification Send to Apple devices";
                return notification_Status;
            }
            else
            {
                string state = "No registered device found";
                return  state;
            }
            
        }
        catch(Exception ex)
        {
            return ex.ToString();
        }
    }

    public List<Device> getDevices(int deviceType)
    {  
        //DeviceRegistrationService.DeviceRegistrationService drs = new DeviceRegistrationService.DeviceRegistrationService();
        List<Device> devices = new List<Device>();
        //Device device = new Device();

        var devList = clntobj.GetDevicesByType(deviceType);//drs.GetDevicesByType(deviceType, true);
        foreach(var dev in devList)
        {
            //device.deviceType = device.
            devices.Add(dev);
        }
        return devices;
 
    }
}