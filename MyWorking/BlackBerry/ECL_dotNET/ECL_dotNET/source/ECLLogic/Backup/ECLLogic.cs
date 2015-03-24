using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.IO;
using log4net;
using log4net.Config;
using System.Reflection;
using System.Diagnostics;

namespace ECLLogic
{
	/// <summary>
	/// This structure is used to store recipient informaton.
	/// </summary>
	internal struct SenderInfo
	{
		/// <summary>
		/// Store User's BES Address
		/// </summary>
		public string BESAddress;
		/// <summary>
		/// Store User's BES Port
		/// </summary>
		public string BESPort;
		/// <summary>
		/// Store User's Email/PIN 
		/// </summary>
		public string Email;
	}

	/// <summary>
	/// This class is used to send Connection informaton.
	///</summary>
	public class ECLConfiguration
	{
		
		private static readonly ILog log = LogManager.GetLogger("ECLConfiguration");
		/// <summary>
		/// BESMgmt override.
		/// </summary>
		public bool BESMgmt;
		/// <summary>
		/// BESMgmtContact.
		/// </summary>
		public String BESMgmtContact;
		/// <summary>
		/// BESContactConnType.
		/// </summary>
		public String BESContactConnType;
		/// <summary>
		/// BESContactServer.
		/// </summary>
		public String BESContactServer;
		/// <summary>
		/// BESMgmtUsers.
		/// </summary>
		public String BESMgmtUsers;
		/// <summary>
		/// BESUsersConnType.
		/// </summary>
		public String BESUsersConnType;
		///<summary>
		/// ContactType
		///</summary>
		public int ContactType;
		///<summary>
		/// UserType
		///</summary>
		public int UserType;
		/// <summary>
		/// File containing user information(Server Port Email/Pin)
		/// </summary>
		public String recipientFile;
		/// <summary>
		/// Push Destination(Channel/Catcher)
		/// </summary>
		public String PushTo;
		/// <summary>
		/// has a value 0(Browser-Channel) or 1(Browser-Channel-Delete) or 2(Message)
		/// </summary>
		public int PushType;
		/// <summary>
		/// This can be Access,Excel,MSSQL
		/// </summary>
		public String ConnectionType;
		/// <summary>
		/// Database name,used in case of MS SQL
		/// </summary>
		public String Database;
		/// <summary>
		/// File name(.xls or .mdb) or Server Name(used in MSSQL)
		/// </summary>
		public String File_Server_Name;
		/// <summary>
		/// Worksheet Name(Used in Excel) or Table name(used in Access or MSSQL)
		/// </summary>
		public String Sheet_Table_Name;
		/// <summary>
		///  The format in which data is displayed on the handheld
		/// </summary>
		public String[] ColumnFormat;
		/// <summary>
		/// The location where the html file should stored after it has been formed
		/// </summary>
		public String fileStoreLoc;
		/// <summary>
		/// The http:\\ URL where the read and unread icon files are
		/// </summary>
		public String WebRoot;
		/// <summary>
		/// The name to be displayed as Channel Title on the handheld
		/// </summary>
		public String ChannelName;
		/// <summary>
		/// The Source of the input(GUI/Console/Service)
		/// </summary>
		public String Source;
		/// <summary>
		/// PushID for PAP push
		/// </summary>
		public string PushID;
		/// <summary>
		/// Setting a timestamp for PAP push, so it can be delivered prior to the specified time or it will not
		/// be pushed.
		/// </summary>
		public string BeforeTimestamp;
		/// <summary>
		/// Setting a timestamp for PAP push, so it can be delivered after the specified time or it will not
		/// be pushed.
		/// </summary>
		public string AfterTimestamp;
		/// <summary>
		/// can be either High, Medium or Low for PAP push
		/// </summary>
		public string Priority;
		/// <summary>
		/// Can be confirmed or unconfirmed
		/// </summary>
		public string DeliveryMethod;
		/// <summary>
		/// Either NONPAP or PAP
		/// </summary>
		public string PushMethod;
		/// <summary>
		/// Use the BES database as content to push out 
		/// </summary>
		///
		public bool BESData;

		public void Display()
		{
			if(log.IsInfoEnabled)
			{
				log.Info("********Start of ECLConfiguration Parameters********");
				log.Info("AfterTimestamp= "+this.AfterTimestamp);
				log.Info("BeforeTimestamp= "+this.BeforeTimestamp);
				log.Info("BESContactConnType= "+this.BESContactConnType);
				log.Info("BESContactServer= "+ this.BESContactServer);
				log.Info("BESData= "+this.BESData);
				log.Info("BESMgmt= "+this.BESMgmt);
				log.Info("BESMgmtContact= "+this.BESMgmtContact);
				log.Info("BESMgmtUsers= "+this.BESMgmtUsers);
				log.Info("BESUsersConnType= "+this.BESUsersConnType);
				log.Info("ChannelName= "+this.ChannelName);
				log.Info("ColumnFormat= "+this.ColumnFormat);
				log.Info("ContactType= "+this.ContactType);
				log.Info("Database= "+this.Database);
				log.Info("DeliveryMethod= "+this.DeliveryMethod);
				log.Info("File_Server_Name= "+this.File_Server_Name);
				log.Info("fileStoreLoc= "+this.fileStoreLoc);
				log.Info("Priority= "+this.Priority);
				log.Info("PushID= "+this.PushID);
				log.Info("PushMethod= "+this.PushMethod);
				log.Info("PushTo= "+this.PushTo);
				log.Info("PushType= "+this.PushType);
				log.Info("recipientFile= "+this.recipientFile);
				log.Info("Sheet_Table_Name= "+this.Sheet_Table_Name);
				log.Info("Source= "+this.Source);
				log.Info("UserType= "+this.UserType);
				log.Info("WebRoot= "+this.WebRoot);
				log.Info("********End of ECLConfiguration Parameters********");


			}
		}
	}

	/// <summary>
	/// This class contains all the logic for the ECL application. It takes the
	/// data, processes it and then send it to the handheld.
	/// </summary>
	public class ECLLogicEngine
	{
		public static int BROWSER_CHANNEL = 0;
		public static int BROWSER_CHANNEL_DELETE = 1;

		public const int USER_DEFAULT = 0;
		public const int USER_BESMGMT = 1;
		public const int USER_BESMGMT_OVERRIDE = 2;
		public const int CONTACT_DEFAULT = 3;
		public const int CONTACT_BESMGMT = 4;
		public const int CONTACT_BESMGMT_OVERRIDE = 5;

		/// <summary>
		/// Define a static logger variable so that it references the
		/// Logger instance named "PushDialog".
		/// </summary>		
		private static readonly ILog log = LogManager.GetLogger("ECLLogic");
		
		/// <summary>
		/// This is the constructor
		/// </summary>
		public ECLLogicEngine()
		{
		}

		/// <summary>
		/// This method processes ECL parameters. It is the entry point into this dll class library
		/// </summary>
		/// <param name="config">This parameter is of type ECLConfiguration which has all the configuration values needed to push the message</param>
		/// <returns>It returns a string to the calling program indicating the status of the push</returns>
		public string processECL(ECLConfiguration config)
		{		
			Pusher pusher = null;
			DataReader dataReader = null;

			//boolean tells the status of push
			bool success=true;

			//Number of times the push failed
			int timesFailed=0;

			String strMsg;
			Assembly a = Assembly.GetExecutingAssembly();
			AssemblyName an = a.GetName();
			log.Info("Starting ECL Logic Engine v" + an.Version.ToString());
			
			// Push type have different formats for the content
			if (config.PushTo == "Channel")
			{
				log.Info("Push To: Channel");
				// Construct a pusher that sends to a browser channel.
				pusher = new BrowserChannelPusher();
			}
			else if (config.PushTo == "Catcher")
			{
				log.Info("Push To: Custom Catcher");
				// Construct a pusher that sends to a custom catcher.
				pusher = new CustomAppPusher();
			}

			// Push Action starts here..
			try
			{
				// Get the list of emails to push to.
				log.Info("Parsing User Information File");
				ArrayList recipientEmails = null;
				ArrayList contactInfo = null;
				
				// BESMgmt override control
				if (config.UserType == ECLLogicEngine.USER_BESMGMT)
				{
					dataReader = new DataReader();
					log.Info("Opening BESMgmt Data Source for retrieving data");
					dataReader.openDB(config.UserType, config);
					recipientEmails = dataReader.getRecipients(config);
				}				
				else // User Information File
				{
					recipientEmails = getRecipients(config.recipientFile);
				}

				// Contact Info
				// Catcher or Browser-Channel, but not Browser-Channel-Delete
				if (config.PushType != BROWSER_CHANNEL_DELETE)
				{
					// BESMgmt override control
					if (config.ContactType == ECLLogicEngine.CONTACT_BESMGMT)
					{
						dataReader = new DataReader();
						log.Info("Opening BESMgmt Data Source for retrieving data");
						dataReader.openDB(config.ContactType, config);
						contactInfo = dataReader.getContactInfo(config);

						// Define a GroupName
						String groupName = ConfigurationSettings.AppSettings["BESGroupName"];
						pusher.beginGroup(groupName);

						IEnumerator e = contactInfo.GetEnumerator();
						while (e.MoveNext())
						{
							String[] contact = (String[]) e.Current;
							pusher.addContact(contact);
						}
					}
					else
					{
						// Open the data source from which we will read in contact data.
						config.ContactType=CONTACT_DEFAULT;
						dataReader = new DataReader();
						log.Info("Opening Data Source for retrieving data");
						dataReader.openDB(config.ContactType, config);
				
						//Fetch the group names 
						log.Info("Getting list of group names");
						System.Collections.ArrayList groupDescription = dataReader.GroupList(config);
				
						// Assemble the data of all contacts (from all groups) that we
						// will push to handhelds.
						log.Info("Getting user contact information");
						for (int i = 0; i < groupDescription.Count; i++)
						{					
							// Define the current group.
							pusher.beginGroup((System.String) groupDescription[i]);
					
							// Add all its members.
							System.Collections.ArrayList groupContactList = dataReader.getContactList((System.String) groupDescription[i],config);
							for (int j = 0; j < groupContactList.Count; j++)
							{
								System.String[] dataFields = dataReader.getContactData((System.String) groupContactList[j],config);
								pusher.addContact(dataFields);
							}
						}
						// Log the message we just built
						log.Info("Finished building the push message with " + "contacts in " + groupDescription.Count + " groups.  " );						
					}
				}

				// Indicate that we're done building the contacts list.  After,
				// the pusher is ready to send the message multiple times.
				log.Info("The data that follows will be pushed:");
				pusher.finishedConstruction(config.fileStoreLoc);

				if (dataReader!=null)
					dataReader.closeDB();

				// Push the message we just built to all recipients.
				for (int i = 0; i < recipientEmails.Count; i++)
				{
					SenderInfo curRecipient = (SenderInfo) recipientEmails[i];
					try
					{
						log.Info("Contacting BES for " + curRecipient.BESAddress + " " + curRecipient.BESPort + " " + curRecipient.Email);
						//Sends the message
						success=pusher.sendToHandheld(curRecipient,config);
						
						if(success==false)
						{
							log.Info(" - unsuccessful push.");
							++timesFailed;
						}
						else
						{
							log.Info(" - successful push.");
						}
					}
					catch (System.Exception ex)
					{
						// Unexpected error. Log and move to next email
						log.Error(" Exception : "+ex.Message,ex);
						++timesFailed;
					}
				}
				if(timesFailed==0)
				{
					strMsg= "Push was successfully sent to all "+ recipientEmails.Count +" recipients.";
				}
				else
				{
					strMsg= timesFailed + " of "+ recipientEmails.Count + " push failed. Please check logs.";
				}
			}
			catch (System.Exception ex)
			{			
				log.Info("Exception: "+ ex.Message,ex);
				return "Push Failed. Please check logs.";
			}
			finally
			{			
				if (dataReader != null)
				{
					try
					{
						dataReader.closeDB();
					}
					catch (System.Exception ex)
					{
						log.Info("Exception: ",ex);						
					}
				}				
			}
			return strMsg;
		}

		/// <summary>
		/// This method parses the User Information file and stores the user information 
		/// in an ArrayList of struct SenderInfo
		/// </summary>
		/// <param name="FileName">User Information file in a string format</param>
		/// <returns>It returns an Arraylist of all user's in the file. </returns>
		private ArrayList getRecipients(String FileName)
		{
			int pos,start;		
			//string BESAddress,BESPort,Email;
			string fDelimiter= " ";
			SenderInfo Sender = new SenderInfo();
			ArrayList SenderList=new ArrayList();
			// create reader & open file
			TextReader sr = new StreamReader(FileName);
			//reading contents into string variable
			try
			{
				string fContent = sr.ReadLine();

				/*looping thrugh the reader and parsing the
				BES address,port and email from the file*/
				while (true)
				{       
					if (fContent==null || fContent=="")
					{
						break;
					} 
					pos=0;
					start=0;
					
					//looking for " "
					pos=fContent.IndexOf(fDelimiter,start);
					//using pos and start variable to parse Address
					Sender.BESAddress=fContent.Substring(start,pos);
					//setting start position for port
					start=pos+1;
					//looking for " "
					pos=fContent.IndexOf(fDelimiter,start);
					//using pos and start variable to parse Port
					Sender.BESPort=fContent.Substring(start,pos-start);
					//setting start
					start=pos+1;
					//using length and start to parse email
					Sender.Email=fContent.Substring(start,fContent.Length-start);
						
					//logging the parsed infomation  
					log.Info("Sending: " + Sender.BESAddress + " " + Sender.BESPort + " " + Sender.Email);
						
					//Verifying the parsed information
					if (verifyFile(Sender))
					{
						SenderList.Add(Sender);
					}
					else 
					{
						//MessageBox.Show("Wrong Formatting: " + Sender.BESAddress + " " + Sender.BESPort + " " + Sender.Email+". Please check User Information File");
						log.Error("Wrong Formatting: " + Sender.BESAddress + " " + Sender.BESPort + " " + Sender.Email);
						System.Environment.Exit(0);
					}
					
					//reading line
					fContent=  sr.ReadLine();					
				}
			}
			catch(Exception t)
			{
				log.Info("Exception: "+ t.Message,t);	
				t.Equals(null);
			}
			finally
			{
				//closing stream reader
				sr.Close();				
			}
			return SenderList;
		}

		/// <summary>
		/// This method verifies if the parsed information is correct
		/// </summary>
		/// <param name="Sender">It takes in user information and verifies if it is not null</param>
		/// <returns>It returns a true if value is not null otherwise returns a false</returns>
		
		private bool verifyFile(SenderInfo Sender)
		{
			bool result = true;
			if (Sender.BESAddress.Equals(""))
			{
				result = false;
			} 			
			if (Sender.BESPort.Equals(""))
			{
				result = false;
			} 			
			if (Sender.Email.Equals(""))
			{
				result = false;
			}
			return result;
		}
	}
}
