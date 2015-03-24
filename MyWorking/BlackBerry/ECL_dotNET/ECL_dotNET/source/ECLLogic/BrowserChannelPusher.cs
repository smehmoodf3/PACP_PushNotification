using System;
using System.Text;
using System.Net;
using System.IO;
using log4net;
using log4net.Config;


namespace ECLLogic
{
	/// <summary> A pusher that formats its push message as an HTML page and selects a device
	/// port and http headers for a channel push of that page. It is derived from the base class Pusher
	/// </summary>
	/// 
	internal class BrowserChannelPusher:Pusher
	{
		/// <summary>
		/// Define a static logger variable so that it references the
		/// Logger instance named "BrowserChannelPusher".
		/// </summary>
		private static readonly ILog log = LogManager.GetLogger("BrowserChannelPusher");
		/// <summary>
		/// Array which holds the push type values Browser-Channel and Browser-Channel-Delete
		/// </summary>

		private static string[] PushTypes = new string[]{"Browser-Channel","Browser-Channel-Delete","Browser-Message"};
				
		/// <summary> Returns the port at which the device is listening
		/// </summary>
		override protected internal int DevicePort
		{
			// specified in Pusher
			get
			{
				// The browser listens on 7874.
				return 7874;
			}
		
		}
	
		/// <summary> Default constructor.  Begins the HTML page.
		/// </summary>
		public BrowserChannelPusher()
		{
			_workingMessage = new System.Text.StringBuilder();
			_workingMessage.Append("<html><head><title>EmergencyContactList</title></head><body bgcolor=\"white\">");
			
		}
	
		// specified in Pusher
		/// <summary> Adds the group name to the HTML page.
		/// </summary>
		public override void  beginGroup(System.String groupName)
		{
			_workingMessage.Append("<h2>");
			_workingMessage.Append(groupName);
			_workingMessage.Append("</h2><br>");
		}
	
		// specified in Pusher
		/// <summary> Adds the contact details to the HTML page.
		/// </summary>
		public override void  addContact(System.String[] dataFields)
		{
			_workingMessage.Append("<b>");
			_workingMessage.Append(dataFields[0]);
			_workingMessage.Append("</b><br>");
			for (int i = 1; i < dataFields.Length; i++)
			{
				System.String field = dataFields[i];
				field.Trim();
				if (!field.EndsWith(" "))
				{
					_workingMessage.Append(dataFields[i]);
					_workingMessage.Append("<br>");
				}
			}
			_workingMessage.Append("<br>");
		}
		
		public override String PapContent()
		{
			return papmessage;
		}
		
		// specified in Pusher
		/// <summary> Ends the HTML page and stores the file if any location is specified.
		/// </summary>
		public override void  finishedConstruction(String FileLocation)
		{
			_workingMessage.Append("</body></html>");
			//_finishedMessage = new ASCIIEncoding().GetBytes(_workingMessage.ToString());
			//Use ISO-8591-1 Encoding instead of ASCII to support accented characters.
			_finishedMessage = Encoding.GetEncoding(28591).GetBytes(_workingMessage.ToString()); 


			log.Info(_workingMessage.ToString());
			
			//for reliable push data needs to be in string format, since it needs to be sent as XML format 
			papmessage=_workingMessage.ToString();
			
			_workingMessage = null;			
			storeFinishedMessage(FileLocation);
		}
	
		// specified in Pusher
		/// <summary> Writes the finished HTML page to the output stream.
		/// </summary>
		public override void  writeTo( System.IO.Stream os)
		{
						
			os.Write(_finishedMessage, 0, _finishedMessage.Length);
			
			
		}
		
		// Called if the push is reliable
		public override void  writeTo( System.IO.Stream os, byte[] papContent)
		{
			if(papContent==null)		
			os.Write(_finishedMessage, 0, _finishedMessage.Length);
			else
				os.Write(papContent,0,papContent.Length);

			Console.WriteLine(os.ToString());
			
			
		}		
	    
		
		// specified in Pusher
		/// <summary> Adds the headers during the push operation.
		/// </summary>
		protected internal override void  establishRequestHeaders(System.Net.HttpWebRequest HttpWReq,ECLConfiguration config)
		{
			
			//Settings
			System.String url = config.WebRoot;
			System.String pushTitle=config.ChannelName;
			System.String pushType=PushTypes[config.PushType];
			//Adding Headers
			
			//Expire in 1 year
			DateTime now = DateTime.Now;
			now = now.AddYears(1);
			String dateStr = now.ToString("r");
			HttpWReq.Headers.Add("Expires", now.ToString("r"));

			HttpWReq.Headers.Add("Content-Location",url+ "/ecl.html");
			HttpWReq.Headers.Add("X-RIM-Push-Title", pushTitle);
			HttpWReq.Headers.Add("X-RIM-Push-Type",pushType);
			HttpWReq.Headers.Add("X-RIM-Push-Channel-ID", url+ "/ecl.html");
			if (pushType.Equals("Browser-Channel"))
			{
				HttpWReq.Headers.Add("X-RIM-Push-UnRead-Icon-URL",url + "/ecl_unread_icon.gif");							
				HttpWReq.Headers.Add("X-RIM-Push-Read-Icon-URL", url + "/ecl_read_icon.gif");
			}
			HttpWReq.ContentType ="text/html";
			HttpWReq.ContentLength = _finishedMessage.Length;
			
		
			
			}
	
		/// <summary> Saves the constructed webpage content to a file on the local machine.
		/// The file name is retrieved from the channel property StoreLocation.
		/// The storeFinishedMessage operation is safe in that it logs any raised
		/// exceptions but does not rethrow them.  So if an invalid filename is
		/// specified, it won't kill the whole push operation.
		/// </summary>
		private void  storeFinishedMessage(String FileLocation)
		{
			System.IO.Stream out_Renamed = null;
			System.String filename = null;
			try
			{
				if (!FileLocation.Equals(null))
				{
					filename = FileLocation;
					out_Renamed = new System.IO.FileStream(filename, System.IO.FileMode.Create);
					writeTo(out_Renamed);
				}
				else
				{
					log.Error("Note: The generated page is not being " + "saved locally; set the channel property StoreLocation " + "to save it.");
				}
			}
			catch (System.Exception ex)
			{
				if (filename == null)
				{
					log.Error("Warning: exception occurred determining " + "location to store the generated page.",ex);
				}
				else
				{
					log.Error("Warning: unable to write the generated " + "page to " + filename,ex);
				}
				
			}
			finally
			{
				if (out_Renamed != null)
				{
					try
					{
						out_Renamed.Close();
					}
					catch (System.Exception ex)
					{
						log.Error("Ignorring exception raised while " + "closing file " + filename,ex);
						
					}
				}
			}
		}
	
		/// <summary> This stores message while it is under construction.
		/// </summary>
		private System.Text.StringBuilder _workingMessage;
	
		/// <summary> The stores the message after it is complete.
		/// </summary>
		private byte[] _finishedMessage;

		// Holds the string value of the content
		string papmessage;	
	}
}
// class BrowserChannelPusher