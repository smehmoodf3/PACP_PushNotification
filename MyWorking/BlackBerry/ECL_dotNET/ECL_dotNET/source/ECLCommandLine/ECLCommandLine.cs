using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using ECLLogic;

namespace ECLCommandLine
{
	/// <summary>
	/// This class gets the parameters from the Command Line and Settings.config and 
	/// sends it to the ECLLogic class library 
	/// </summary>
	class ECLCommandLine
	{
		/// <summary>
		/// Define a static logger variable so that it references the
		/// Logger instance named "ECLCommandLine".
		/// </summary>
		private static readonly ILog log = LogManager.GetLogger("ECLCommandLine");
		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// <param name="args">The arguements from the console screen(channel or catcher)</param>
		[STAThread]
		static void Main(string[] args)
		{
			// Log4net - logging file location
			String logFile = ConfigurationSettings.AppSettings["LogPropertyFile"];
			FileInfo fi = new FileInfo(logFile); 

			// Activate this line to enable log4net internal debugging to console
			log4net.helpers.LogLog.InternalDebugging = true;
			
			// Define the config file to be monitored for changes
			log4net.Config.DOMConfigurator.ConfigureAndWatch(fi);
			
			int i=0;
			if (log.IsInfoEnabled)
			{
				// Startup and configuration values
				log.Info("ECLCommandLine Started.");
				log.Info("ECLCommandLine Logging Control File: " + logFile);

				// Configuration AppSetting values
				int j = ConfigurationSettings.AppSettings.Count;
				log.Info("ECLCommandLine Application Settings contain " + j + " entries.");
				String[] keys = new String[j];
				keys = ConfigurationSettings.AppSettings.AllKeys;
				for (i=0; i<j;i++)
				{
					String keyVal = ConfigurationSettings.AppSettings[i];
					log.Info("Setting Key " + (i+1) + " of " + j + " key:" + keys[i] + " value: " + keyVal);
				}
			}
			//declaring a variable of type ECLConfiguration(DLL)
			ECLConfiguration config = new ECLConfiguration();

			//declaring a variable of type ECL Logic Engine(DLL)
			ECLLogicEngine push = new ECLLogicEngine();

			string temp=null;

			NameValueCollection settings;
			Assembly a = Assembly.GetExecutingAssembly();
			AssemblyName an = a.GetName();
						
			log.Info("BlackBerry ECL Console v"+ an.Version.ToString());
			
			//configuring settings
			if (args.Length > 0)
			{
				if (args[0].Equals("channel"))
				{
					config.PushTo="Channel";
				}
				else if (args[0].Equals("catcher"))
				{
					config.PushTo="Catcher";
				}
			}
			else
			{
				config.PushTo="Channel";
			}

			try
			{
				// Application Configuration Settings
				settings = ConfigurationSettings.AppSettings;

				// Push Method, added for Reliable Delivery
				config.PushMethod = settings["PushMethod"];

				config.recipientFile = settings["RecipientEmails"];     //recipient list
				config.ConnectionType = settings["ConnectionType"];     //Source Type
				config.File_Server_Name = settings["FileServerName"];   //File location/server
				config.Sheet_Table_Name = settings["Sheet_Table_Name"]; //worksheet/table name

				// BESMgmt Overrides
				if (settings["BESMgmt"].Equals("true"))
				{
					config.BESMgmt = true;
					// Recipients Overrides
					config.BESMgmtUsers = settings["BESMgmtUsers"];
					config.BESUsersConnType = settings["BESUsersConnType"];
					config.recipientFile = settings["BESMgmtUsers"];
					// Contacts Overrides
					config.BESMgmtContact = settings["BESMgmtContact"];
					config.File_Server_Name = settings["BESMgmtServer"];
					config.Sheet_Table_Name = settings["BESMgmtContact"];
					config.BESContactConnType = settings["BESContactsConnType"];
					config.BESContactServer = settings["BESContactsServer"];

					// Set the BESMgmtUserType/BESMgmtContactType
					if (config.BESMgmtUsers.Equals("BESMgmt"))
					{
						config.UserType = ECLLogicEngine.USER_BESMGMT;
					}
					else
					{
						config.UserType = ECLLogicEngine.USER_BESMGMT_OVERRIDE;
					}
					// Set the BESMgmtUserType/BESMgmtContactType
					if (config.BESMgmtContact.Equals("BESMgmt"))
					{
						config.ContactType = ECLLogicEngine.CONTACT_BESMGMT;
					}
					else
					{
						config.ContactType = ECLLogicEngine.CONTACT_BESMGMT_OVERRIDE;
					}
				}
				else
				{
					config.UserType = ECLLogicEngine.USER_DEFAULT;
					config.ContactType = ECLLogicEngine.CONTACT_DEFAULT;
				}

				config.Database=settings["Database"];
				config.WebRoot= settings["WebContextRoot"];
				config.fileStoreLoc =  settings["StoreLocation"];
				config.ChannelName= settings["ChannelName"];
				config.PushType= Convert.ToInt32(settings["PushType"]);
			
				config.ColumnFormat = new String[settings.Count];
				//setting column format
				i = 0;
				foreach (string keyname in settings.AllKeys)
				{
					temp="ColumnFormat"+i;
					if(temp.Equals(keyname))
					{
						config.ColumnFormat[i]= settings[keyname];
						i++;
					}
				}
				config.Source="CommandLine";

				// PAP settings
				config.PushID = settings["PushID"];
				config.BeforeTimestamp = settings["BeforeTimestamp"];
				config.AfterTimestamp  = settings["AfterTimestamp"];
				config.Priority = settings["Priority"];
				config.DeliveryMethod = settings["Delivery"];

				//validating inputs
				if (config.PushType.Equals("") || config.ConnectionType.Equals("") || config.recipientFile.Equals("") || config.File_Server_Name.Equals("") ||  config.Sheet_Table_Name.Equals(""))
				{
					log.Error("Error:Fields Missing from Application.config");
					System.Environment.Exit(- 1);
				}
				if (config.ConnectionType=="MSSQL" && config.Database=="")
				{
					log.Error("Error:Fields Missing from Application.config");
					System.Environment.Exit(- 1);
				}
				log.Info("Starting...");
				
				//sending push - calling the dll method to process the ECL parameters and perform the push
				config.Display();
				String result = push.processECL(config);	
				System.Console.WriteLine(result);
				System.Console.WriteLine("Press any key to exit.");
				System.Console.ReadLine();
			}
			catch(Exception e)
			{				
				log.Error("Message from CONSOLE:",e);
			}
			finally
			{//cleanup
				settings = null;
			}	
			
		}
	}
}
