using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using log4net;
using log4net.Config;
using ECLLogic;
using System.Collections.Specialized;
using System.Configuration;
using System.Timers;
using System.Reflection;
namespace ECLService
{
	/// <summary>
	/// This Service is used to schedule a ECL push operation. The Service picks up its 
	/// configuration from Settings.config. If the Settings.config file is modified while 
	/// service is running, then the service has to be restarted for the changes to take place.
	/// </summary>
	public class ECLService : System.ServiceProcess.ServiceBase
	{
		/// <summary>
		/// Components used by the service
		/// </summary>
		private System.ComponentModel.IContainer components=null;
		/// <summary>
		/// The timer used to wake the service. The settings for timer interval can be made 
		/// in Settings.config
		/// </summary>
		private System.Timers.Timer timer=null;
		/// <summary>
		/// Define a static logger variable so that it references the
		/// Logger instance named "ECLService".
		/// </summary>
		private static readonly ILog log = LogManager.GetLogger("ECLService");
		/// <summary>
		/// The contructor for the service
		/// </summary>
		public ECLService()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();
			
			//getting timer interval from settings.config
			string servicepollinterval = System.Configuration.ConfigurationSettings.AppSettings["servicepollinterval"];

			double interval=10000;
			try 
			{
				interval = Convert.ToDouble(servicepollinterval);
			}
			catch(Exception) {}
	
			timer = new System.Timers.Timer(interval);
			timer.Elapsed += new ElapsedEventHandler( this.ServiceTimer_Tick );
		}

		
		/// <summary>
		/// The main entry point for the process
		/// </summary>
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new ECLService() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Service1
			// 
			this.ServiceName = "ECLService";

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so the service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			
				timer.AutoReset = true;
				timer.Enabled=true;
				timer.Start();
				log.Info("Service Started");
		}

		
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			timer.AutoReset = false;
			timer.Enabled = false;
		}
/// <summary>
/// Actions to perform when Service is paused
/// </summary>
		protected override void OnPause() 
		{
			this.timer.Stop();
			log.Info("Service Paused");
		}
/// <summary>
/// Actions to perform when Service is continued
/// </summary>
		protected override void OnContinue() 
		{
			this.timer.Start();
			log.Info("Service Continued");
		}
/// <summary>
/// This event is called when the timer interval is complete and the service is called to action
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
		private void ServiceTimer_Tick(object sender, System.Timers.ElapsedEventArgs e) 
		{
			this.timer.Stop();
			//IMPLEMENT TIMER TICK HERE
			
			ECLConfiguration config = new ECLConfiguration();
			ECLLogicEngine push = new ECLLogicEngine();

			string temp=null;
			int i=0;
			NameValueCollection settings;
			Assembly a = Assembly.GetExecutingAssembly();
			AssemblyName an = a.GetName();	
			
			log.Info("BlackBerry ECL Service v"+ an.Version.ToString());
			log.Info("The timer event is occuring....");
			try
			{
				//setting configuration values
				settings = ConfigurationSettings.AppSettings;
				// Push Method, added for Reliable Delivery
				config.PushMethod = settings["PushMethod"];
				config.PushTo = settings["PushTo"];//channel or catcher
				config.recipientFile = settings["RecipientEmails"]; //recipient list
				config.ConnectionType = settings["ConnectionType"]; //Source Type
				config.File_Server_Name = settings["FileServerName"];//File location/server
				config.Sheet_Table_Name = settings["Sheet_Table_Name"]; //worksheet/table name

				// BESMgmt Overrides
				if (settings["BESMgmt"].Equals("true"))
				{
					config.BESMgmt = true;
					config.File_Server_Name = settings["BESMgmtServer"];
					config.Sheet_Table_Name = settings["BESMgmtContact"];
					config.recipientFile = settings["BESMgmtUsers"];
				}
			
				config.Database=settings["Database"];
				config.WebRoot= settings["WebContextRoot"];
				config.fileStoreLoc =  settings["StoreLocation"];
				config.ChannelName= settings["ChannelName"];
				config.PushType= Convert.ToInt32(settings["PushType"]);
			
				config.ColumnFormat = new String[settings.Count];
				foreach(string keyname in settings.AllKeys)
				{
					temp="ColumnFormat"+i;
					if(temp.Equals(keyname))
					{
						config.ColumnFormat[i]= settings[keyname];
						i++;
					}
				}
				config.Source="Console";
				// PAP settings
				config.PushID = settings["PushID"];
				config.BeforeTimestamp = settings["BeforeTimestamp"];
				config.AfterTimestamp  = settings["AfterTimestamp"];
				config.Priority = settings["Priority"];
				config.DeliveryMethod = settings["Delivery"];

				if (config.PushType.Equals("") || config.ConnectionType.Equals("") || config.recipientFile.Equals("") || config.File_Server_Name.Equals("") ||  config.Sheet_Table_Name.Equals(""))
				{
					log.Error("Error:Fields Missing from Application.config");
					System.Environment.Exit(- 1);
				}
				if(config.ConnectionType=="MSSQL" && config.Database=="")
				{
					log.Error("Error:Fields Missing from Application.config");
					System.Environment.Exit(- 1);
				}
				config.Display();
				//sending push - calling the dll methos to process the ECL parameters and perform the push
				log.Info(push.processECL(config));
			}
			catch(Exception ex)
			{				
				log.Error("Message from CONSOLE:",ex);
			}
			finally
			{//cleanup
				settings = null;
			}	
			//Starting Timer
			this.timer.Start();
		}
	}
}
