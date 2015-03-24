using System;
using System.Net;
using log4net;
using log4net.Config;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using PAPLogic;


namespace ECLLogic
{
	/// <summary> Abstract definition of a component that pushes content to a BlackBerry
	/// via MDS.  The Pusher class manages the HTTP connection, while the
	/// implementing subclass provides the content to push, names the port on
	/// the device to push to and sets the push headers.
	/// 
	/// This pusher begins in a "construction" state where the beginGroup and
	/// addContact methods are used to build the message.  Followin a call to
	/// finishedConstruction, this pusher transitions to a "sending" state where
	/// repeated calls to sendToHandheld push the content out to devices.
	/// </summary>
	/// 
	internal abstract class Pusher
	{
		string requestTemplate;
		NameValueCollection settings;
		
		/// <summary>
		/// Array which holds the push type values Browser-Channel and Browser-Channel-Delete
		/// </summary>
		public static string[] PUSH_TYPES = new string[]{"Browser-Channel","Browser-Channel-Delete","Browser-Message"};
				
		/// <summary>
		/// Define a static logger variable so that it references the
		/// Logger instance named "Pusher".
		/// </summary>
		private static readonly ILog log = LogManager.GetLogger("Pusher");
		
		/// <summary> Provides the port on the handheld device that should receive pushes.
		/// Gets called once for each invocation of sendToHandheld, while in the
		/// "sending" state.
		/// </summary>
		protected internal abstract int DevicePort{get;}
	
		/// <summary> Default constructor.  
		/// </summary>
		protected internal Pusher()
		{
		}
	
		// NOTE: the next three methods are used by the PushDialog::main
		// to build the message that gets pushed.  
	
		/// <summary> Starts a new group with the given name.  Contacts added between the
		/// current and following invocations belong to that group.  Can only be
		/// called while in the "construction" state.
		/// </summary>
		public abstract void  beginGroup(System.String groupName);
	
		/// <summary> Creates a new contact with the given data to the current group.  Can
		/// only be called while in the "construction" state.
		/// </summary>
		public abstract void  addContact(System.String[] dataFields);
		/// <summary>PAP push requires the content to be part of the XML document.
		/// Can only be called while in the "construction" state.
		/// </summary>
		public abstract String PapContent();
	
		/// <summary> Transitions this pusher into its "sending" state.  Can only be
		/// called while in the "construction" state.
		/// </summary>
		public abstract void  finishedConstruction(String WebRoot);
	
		// NOTE: the next three methods are called from sendToHandheld.
	
		/// <summary> Writes the previously-constructed message to the given byte stream.
		/// Gets called once for each invocation of sendToHandheld.  Can also be
		/// called from outside while in the "sending" state.
		/// </summary>
		public abstract void  writeTo(System.IO.Stream os);
	
		/// <summary>
		/// Same function as above, but for reliable push as it writes the new constructed message to the given byte stream. 
		/// </summary>
		/// <param name="os"></param>
		/// <param name="bytes"></param>
		public abstract void  writeTo(System.IO.Stream os, byte[] bytes);		
		
		/// <summary> Adds any necessary headers to the provided http connection.  That
		/// connection will subsequently be used for the push.  Gets called once
		/// for each invocation of sendToHandheld, while in the "sending" state.
		/// </summary>
		protected internal abstract void  establishRequestHeaders(System.Net.HttpWebRequest con,ECLConfiguration config);
	
		/// <summary>
		///Reads a PAP XML document and outputs its string equivalent.
		///Gets called only if the push is reliable since reliable push requires data to be sent in XML format and not in bytes.
		/// </summary>
		public void readPapTemplete(string filetoread)
		{
			try 
			{
				string papFilename =  filetoread;
				StreamReader reader = new StreamReader(filetoread);
				this.requestTemplate=reader.ReadToEnd();//reads the file including all return and end line variables

				
			} 
			catch (Exception ex) //in case file could not open or could not be read
			{
				Console.Out.WriteLine(ex.Message);
			}
		}
	
		/// <summary> Pushes the already-constructed message to the indicated recipient.  May
		/// be used many times, but only in the "sending" state.
		/// </summary>
		public virtual bool  sendToHandheld(SenderInfo user, ECLConfiguration config )
		{
            bool success=true;
			string pushPin = user.Email;
			string BESAddress=user.BESAddress;
			string BESWebserverListenPort=user.BESPort;
			string pushPort = Convert.ToString(this.DevicePort);
				
			Stream requestStream=null;
			HttpWebResponse HttpWRes=null;
			HttpWebRequest HttpWReq=null;
			DataStore dataStore;

			try
			{
				if (config.PushMethod.Equals("NONPAP"))
				{
					//for HTTP push		
				
					//Create the push URL for MDS this is of the format
					//http://<BESName>:<BESPort>/push?DESTINATTION=<PIN/EMAIL>&PORT=<PushPort>&REQUESTURI=/
				
					// Build the URL to define our connection to the BES.
					string httpURL = "http://"+BESAddress+":"+BESWebserverListenPort
						+"/push?DESTINATION="+pushPin+"&PORT="+pushPort
						+"&REQUESTURI=/";
						
					//make the connection
					//we'll put this whole thing in a try/catch block for some basic
					//error handling.
					HttpWReq = (HttpWebRequest)WebRequest.Create(httpURL);				
					HttpWReq.Method = ("POST");
					//add the headers nessecary for the push
					establishRequestHeaders(HttpWReq,config);
					requestStream = HttpWReq.GetRequestStream();
					//Write the data from the source
					writeTo(requestStream);
				}
				else if (config.PushMethod.Equals("PAP"))
				{
					settings = ConfigurationSettings.AppSettings;
					//FOR PAP Push
					//only bes address and listen port is specificed in the uri
					string httpURL = "http://"+BESAddress+":"+BESWebserverListenPort+"/pap";
					//generate an unique ID for PAP push
					Random random= new Random();
					string pushID = random.Next(10000).ToString();
					//make the connection
					//we'll put this whole thing in a try/catch block for some basic
					//error handling.
					string filetoread = settings["PAPAddress"];
					readPapTemplete(filetoread);//read the pap xml file
					HttpWReq = (HttpWebRequest)WebRequest.Create(httpURL);
					string boundary="asdlfkjiurwghasf";
					HttpWReq.Method="POST";
					
					//adding the appropriate headers for PAP

					HttpWReq.ContentType="multipart/related; type=\"application/xml\"; boundary=" + boundary;
					HttpWReq.Headers.Add("X-Wap-Application-Id", "/");
					HttpWReq.Headers.Add("X-Rim-Push-Dest-Port",pushPort);
					
					string output = this.requestTemplate;

					//update the PAP XML file with current content and variables
					if(config.PushID.Equals(""))//if a pushID is not entered, use a random one
						config.PushID=pushID;

					output = output.Replace("$(pushid)", config.PushID);
					output = output.Replace("$(boundary)", boundary);
					output = output.Replace("$(notifyURL)", "" + settings["NotifyURL"]);
					output = output.Replace("$(pin)", pushPin);

					//the two statements check if there is a before or after timestamp specified
					if (!config.BeforeTimestamp.Equals("YYYY-MM-DD:hh-mm-ss"))
					{
						config.BeforeTimestamp.Replace(":","T");
						output = output.Replace("$(Btimestamp)","deliver-before-timestamp=\"%"+config.BeforeTimestamp+"Z\"");
					}
					else
					{
						int index = output.IndexOf("$(Btimestamp)");
						output = output.Remove(index,13);
					}
					if (!config.AfterTimestamp.Equals("YYYY-MM-DD:hh-mm-ss"))
					{
						config.AfterTimestamp.Replace(":","T");
						output = output.Replace("$(Atimestamp)","deliver-after-timestamp=\"%"+config.AfterTimestamp+"Z\"");
					}
					else
					{
						int index = output.IndexOf("$(Atimestamp)");
						output = output.Remove(index,13);
					}
						
					//sets the priority and devliveryMethod for PAP
					output = output.Replace("$(priority)",config.Priority);
					output = output.Replace("$(deliverymethod)",config.DeliveryMethod);

					string data = PapContent();//get the data in string format to add to the file
					data.TrimStart(null);
					//determing what type of push, 1 for catcher, 0 for browser
					if (config.PushTo.Equals("Catcher"))
						output = output.Replace("$(headers)", "Content-Type: text/plain");
						//if browser push add the headers in the XML, for PAP headers are added in the XML
						//not set as HTTP headers
					else
					{
						string url = config.WebRoot;
						string pushTitle=config.ChannelName;
						string pushType=PUSH_TYPES[config.PushType];//channel, channel-delete or message
						string channelImage=null;
						string headers =  "Content-Type: text/html"+"\n"+
							"Content-Location:"+url+"/ecl.html"+"\n"+
							"X-RIM-Push-Title:"+pushTitle+"\n"+
							"X-RIM-Push-Type:"+pushType+"\n"+
							"X-RIM-Push-Channel-ID:"+pushTitle+"\n";
				
						if (pushType.Equals("Browser-Channel"))
						{
                         
							channelImage="X-RIM-Push-Unread-Icon-URL:"+url + "/ecl_unread_icon.gif"+"\n"+
								"X-RIM-Push-Read-Icon-URL:"+ url + "/ecl_read_icon.gif";
						}
						//adding the header to the string PAP XML
						output = output.Replace("$(headers)", headers+channelImage);
					
					}
					//get the content and replace all end line and return with return end line 
					output = output.Replace("$(content)", data);
					output = output.Replace("\r\n", "EOL");
					output = output.Replace("\n", "EOL");
					//output = output.Replace("EOLEOL", "\r\n");
					output=output.Replace("EOL","\r\n");
				
					
					//convert string in to bytes to push through MDS
					byte[] bytes=Encoding.ASCII.GetBytes(output);
					HttpWReq.ContentLength=bytes.Length;
					
					//push data to device
					Console.WriteLine(output);
					requestStream  = HttpWReq.GetRequestStream();
					writeTo(requestStream,bytes);					
				}				
				
				//get the response
				HttpWRes = (HttpWebResponse)HttpWReq.GetResponse();
				
				//if it is PAP then update the database wiht PUSH ID, time of push and MDS status code
				if (config.PushMethod.Equals("PAP"))
				{	
					dataStore = new DataStore(config.PushID,HttpWRes.StatusCode.ToString());
				}
				//if the MDS received the push parameters correctly it will either respond with okay or accepted
				if (HttpWRes.StatusCode == HttpStatusCode.OK || HttpWRes.StatusCode == HttpStatusCode.Accepted)
				{
					success=true;
				} 
				else
				{
					success=false;
					log.Info("Failed to Push");						
				}
				//Close the streams
			
				HttpWRes.Close();
				requestStream.Close();									
			}
			catch(System.Exception e)
			{
				log.Error("Push Failed "+ e.Message,e);
				success=false;				
			}			
			return success;
		}
	}
}
// class Pusher