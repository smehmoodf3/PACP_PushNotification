using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using ECLLogic;
using System.Configuration;
using System.Collections.Specialized;
using log4net;
using log4net.Config;
namespace ECL
{
	/// <summary>
	/// This is the main page for ECL. All push requirements are inputted here.
	/// Web.config does contain certain app settings, which usually remain constant or static.
	/// </summary>
	public class ECL : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label DeviceInfo;
		protected System.Web.UI.WebControls.RadioButtonList Options;
		protected System.Web.UI.WebControls.RadioButtonList PushType;
		protected System.Web.UI.WebControls.TextBox ChannelName;
		protected System.Web.UI.WebControls.Label Browser_Push;
		protected System.Web.UI.WebControls.DropDownList Browser_List;
		protected System.Web.UI.WebControls.Label ChName;
		protected System.Web.UI.WebControls.Label RootAddress;
		protected System.Web.UI.WebControls.TextBox ContextAddress;
		protected System.Web.UI.WebControls.TextBox PushID;
		protected System.Web.UI.WebControls.TextBox DelBTimestamp;
		protected System.Web.UI.WebControls.DropDownList Priority;
		protected System.Web.UI.WebControls.DropDownList DelMethod;
		protected System.Web.UI.WebControls.TextBox DelATimestamp;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Div5;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Div3;
		protected System.Web.UI.WebControls.Panel Panel1;
		protected System.Web.UI.WebControls.Panel Panel2;
		protected System.Web.UI.WebControls.CheckBox ReliableParameters;
		protected System.Web.UI.HtmlControls.HtmlImage IMG1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		string weblog;
		NameValueCollection settings;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.Button PushResult;
		private static readonly ILog log = LogManager.GetLogger("ECL");

		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				Div5.Visible=false;
				String logFile = ConfigurationSettings.AppSettings["LogPropertyFile"];
				FileInfo fi = new FileInfo(logFile); 

				// Activate this line to enable log4net internal debugging to console
				log4net.helpers.LogLog.InternalDebugging = true;
			
				// Define the config file to be monitored for changes
				log4net.Config.DOMConfigurator.ConfigureAndWatch(fi);
			}
		
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
			this.Options.SelectedIndexChanged += new System.EventHandler(this.Options_SelectedIndexChanged);
			this.PushType.SelectedIndexChanged += new System.EventHandler(this.PushType_SelectedIndexChanged);
			this.ReliableParameters.CheckedChanged += new System.EventHandler(this.ReliableParameters_CheckedChanged);
			this.Submit1.ServerClick += new System.EventHandler(this.Submit1_ServerClick);
			this.PushResult.Click += new System.EventHandler(this.PushResult_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
		
		private void Submit1_ServerClick(object sender, System.EventArgs e)
		{
			log.Info("Starting ECL Web");
			settings = ConfigurationSettings.AppSettings;
			//initiate variable
			String[] column=new String[8];
			ECLConfiguration config= new ECLConfiguration();
			ECLLogicEngine push=new ECLLogicEngine();
			//defines the push method either Http or PAP
			config.PushMethod=Options.SelectedValue;
			//connection type, for this only MSSQL is the option
			config.ConnectionType="MSSQL";
			//get the database info
			config.File_Server_Name=settings["FileServerName"];
			config.Database=settings["Database"]; 
			config.Sheet_Table_Name=settings["Sheet_Table_Name"];
			//gets either browser or catcher for push to
			config.PushTo=PushType.SelectedValue;
			//other optional info for browser push
			config.PushType=Browser_List.SelectedIndex;
			config.ChannelName=ChannelName.Text;
			config.WebRoot= "http://"+ContextAddress.Text;
			config.fileStoreLoc="c:\\inetpub\\wwwroot\\ecl\\ecl.html";
			//optional parameters for reliable push
			config.PushID=PushID.Text;
			config.BeforeTimestamp=DelBTimestamp.Text;
			config.AfterTimestamp=DelATimestamp.Text;
			config.Priority=Priority.SelectedValue;
			config.DeliveryMethod=DelMethod.SelectedValue;
		
			//hardcoding the coulmn format of the data to be parsed
			column[0]="%0";
			column[1]="%0";
			column[2]="Office: %0";
			column[3]="Cell: %0";
			column[4]="Email: %0";
			column[5]="Pin: %0";
			column[6]="Back Up: %0";
			config.ColumnFormat=column;

			try
			{
				File1.PostedFile.SaveAs("c:\\temp\\user.txt");
				config.recipientFile="c:\\temp\\user.txt";
				config.Display();
				//calls the ecl logic for push to occur
				weblog = push.processECL(config);
				//display the result in the submit page
				

			}
			catch(System.Exception ex)
			{
				log.Info(ex.Message);
				weblog = "Push Failed, Please check logs";
			}
			Server.Transfer("Sumbit.aspx");
		}
		/// <summary>
		/// Disables the optional parameters if it's not checked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ReliableParameters_CheckedChanged(object sender, System.EventArgs e)
		{
			if(ReliableParameters.Checked==true)
				Div5.Visible=true;
			else
				Div5.Visible=false;

			

		}
		/// <summary>
		/// Disables the check mark for optional parameters if reliable push is not selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Options_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(Options.SelectedValue.Equals("PAP"))
				ReliableParameters.Visible=true;

			else
			{
				ReliableParameters.Visible=false;
				ReliableParameters.Checked=false;
			}
			
		}
		/// <summary>
		/// Disables browser push options, if catcher selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PushType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(PushType.SelectedValue.Equals("Catcher"))
				Div3.Visible=false;
			else
				Div3.Visible=true;

			
		}

		private void PushResult_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("http://localhost/PAP",true);
		}
		/// <summary>
		/// This function gets called in the submit page.
		/// It returns the comments from ECL logic
		/// </summary>
		public string WebLog
		{
			get
			{
				return weblog;
			}
		}



	}	
}
