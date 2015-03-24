using System;
using System.Text;
using log4net;
using log4net.Config;

namespace ECLLogic
{
	/// <summary> This is the Pusher that formats its content in a nonstandard ;-dilimited format meant
	/// to be read by the custom "catcher" program running on the handheld. It is derived from the base clas Pusher
	/// </summary>
	internal class CustomAppPusher:Pusher
	{
		/// <summary>
		/// Define a static logger variable so that it references the
		/// Logger instance named "DataReader".
		/// </summary>
		private static readonly ILog log = LogManager.GetLogger("CustomAppPusher");
		/// <summary> Returns the port at which the device is listening
		/// </summary>
		override protected internal int DevicePort
		{
			// specified in Pusher
		
			get
			{
				return 911;
			}
		
		}

		/// <summary> Default constructor.  
		/// </summary>
		public CustomAppPusher()
		{
			_workingMessage = new System.Text.StringBuilder();
		}
	
		// specified in Pusher
		/// <summary> Adds the group name to the Pusher in a nonstandard ;-dilimited format.
		/// </summary>
		public override void  beginGroup(System.String groupName)
		{
			_workingMessage.Append("NEXT_GROUP;");
			_workingMessage.Append(groupName);
			_workingMessage.Append(";");
		}
	
		// specified in Pusher
		/// <summary> Adds the contact details to the Pusher in a nonstandard ;-dilimited format.
		/// </summary>
		public override void  addContact(System.String[] dataFields)
		{
			_workingMessage.Append("NEW_CONTACT;");
			for (int i = 0; i < dataFields.Length; i++)
			{
				if (dataFields[i] != null && !dataFields[i].Trim().Equals(""))
				{
					_workingMessage.Append(dataFields[i]);
					_workingMessage.Append(";");
				}
			}
		}
	
		// specified in Pusher
		/// <summary> Ends the page.
		/// </summary>
		public override void  finishedConstruction(String FileSave)
		{
			_finishedMessage = new ASCIIEncoding().GetBytes(_workingMessage.ToString());
			log.Info(_workingMessage.ToString());
			papmessage=_workingMessage.ToString();
			_workingMessage = null;
		}
		public override String PapContent()
		{
			return papmessage;
		}
	
		// specified in Pusher
		/// <summary> Writes the finished page to the output stream.
		/// </summary>
		public override void  writeTo(System.IO.Stream os)
		{
			os.Write(_finishedMessage, 0, _finishedMessage.Length);
		}
		
		// Called by reliable push
		public override void  writeTo(System.IO.Stream os, byte[] papContent)
		{
			if(papContent.Equals(null))		
				os.Write(_finishedMessage, 0, _finishedMessage.Length);
			else
				os.Write(papContent,0,papContent.Length);
		}
			
	
	
		// specified in Pusher
		/// <summary> Adds the headers during the push operation.
		/// </summary>
		protected internal override void  establishRequestHeaders(System.Net.HttpWebRequest HttpWReq,ECLConfiguration config)
		{
			HttpWReq.ContentType ="text/plain";
			HttpWReq.ContentLength = _finishedMessage.Length;
		}
	
		/// <summary> The stores the message while it is under construction.
		/// </summary>
		private System.Text.StringBuilder _workingMessage;
	
		/// <summary> The stores the message after it is complete.
		/// </summary>
		private byte[] _finishedMessage;
		
		// Holds the string representation of the content
		string papmessage;

	}
}
// class CustomAppPusher