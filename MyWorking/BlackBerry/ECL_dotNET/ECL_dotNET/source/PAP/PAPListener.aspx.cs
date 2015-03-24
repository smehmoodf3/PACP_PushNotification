using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using PAPLogic;
using System.Xml;
using log4net;
using log4net.Config;
namespace PAP

{
	/// <summary>
	/// This listener is only called by the MDS. It proccess the XML document that MDS forwards
	/// to this listener and responds back with a confirmation that it has received the result-notification XML.
	/// The response must also be sent in an XML format.
	/// For a full definition of the format of result notification and response message please refer to the ECL guide.
	/// </summary>
	public class PAPListener:System.Web.UI.Page,IHttpHandler
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
		StringReader data;
		XmlTextReader reader;
		string Code;
		string PushId;
		string MessageState;
		string DeviceAddress;
		private static readonly ILog log = LogManager.GetLogger("PAPListener");

		/// <summary>
		/// when MDS sends a result-notification message for a reliable push, this method parses it
		/// and collects the push-id, device address, the state of message if it has been delivered or not
		/// and the device code, which states if it was received okay or not.
		/// </summary>
		/// <param name="context"></param>

		public void ProcessRequest(HttpContext context) 
		{
			String logFile = ConfigurationSettings.AppSettings["LogPropertyFile"];
			FileInfo fi = new FileInfo(logFile); 

			// Activate this line to enable log4net internal debugging to console
			log4net.helpers.LogLog.InternalDebugging = true;
			
			// Define the config file to be monitored for changes
			log4net.Config.DOMConfigurator.ConfigureAndWatch(fi);
			try
			{
				HttpRequest request = context.Request;
				//open up a stream and read the message
				StreamReader PapStream = new StreamReader(request.InputStream);
				data = new StringReader(PapStream.ReadToEnd());
				PapStream.Close();
				//since it's a XML document, read it in the appropriate stream
				reader = new XmlTextReader(data);
				//move the document along to the next element
				reader.MoveToContent();
				//gather the values
				reader.Read();
				//device code, which states if the device received the content or not
				Code = reader.GetAttribute("code");
				PushId= reader.GetAttribute("push-id");
				//ususally delivered or not delivered
				MessageState = reader.GetAttribute("message-state");
				//move to the next element
				reader.Read();
				//the address of the device to which the push was sent
				DeviceAddress = reader.GetAttribute("address-value");
				reader.Close();
				int timer=0;
				//about 10 sec delay, to ensure the pap database does contain an initial record
				while(timer<10000)
				{
					timer++;
				}	
				//store the result in the sql database
				DataStore dataStore = new DataStore(PushId,DeviceAddress,Code,MessageState);
				MDSResponse(context,PushId,DeviceAddress);
				log.Info("MDS notification received properly, updating database");
							
			}
			catch(System.Exception e)
			{
				log.Info(e.Message);
				HttpResponse response = context.Response;
				response.StatusCode=(int)HttpStatusCode.ExpectationFailed;
			}
			
		}
		/// <summary>
		/// This function will send a response back to MDS stating that it received the result notificaiton properly
		/// The exception will be caught above.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="pushID"></param>
		/// <param name="DeviceAddress"></param>
		public void MDSResponse(HttpContext context,string pushID, string DeviceAddress)
		{
			HttpResponse response = context.Response;
			response.StatusCode=(int)HttpStatusCode.OK;
			//setting up the XML document to send
			string ResponseMessage ="<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>";
			ResponseMessage+="\r\n<!DOCTYPE pap PUBLIC \"-//WAPFORUM//DTD PAP 2.0//EN\" \"http://www.wapforum.org/DTD/pap_2.0.dtd\" [<?wap-pap-ver supported-versions=\"2.0\"?>]>";
			ResponseMessage+="\r\n<pap>";
			ResponseMessage+="\r\n<resultnotification-response push-id="+"\""+pushID+"\""+ " code=\"1000\" desc=\"OK\">";
			ResponseMessage+="\r\n<address address-value=\""+DeviceAddress+"\"/>";
			ResponseMessage+="\r\n</resultnotification-response></pap>";
			response.Write(ResponseMessage);
			log.Info("Notification-response message send to MDS properly");
		}
		public bool IsReusable 
		{
			get { return true; }  
		}
		}
}
