using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using ECLLogic;
using log4net;
using log4net.Config;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;

namespace ECL
{
	/// <summary> Main class for the server part of the ECL application.  The main method
	/// runs for both the Browser Channel and Customer Catcher versions -- note
	/// that either a BrowserChannelPusher or a CustomAppPusher is used depending
	/// on the radio button selected.  
	/// </summary>
	public class PushDialog : System.Windows.Forms.Form
	{
		// Define a static logger variable so that it references the
		// Logger instance named "PushDialog".
		private static readonly ILog log = LogManager.GetLogger("PushDialog");
		//declaring a variable of type ECLConfiguration(DLL)
		ECLConfiguration config = new ECLConfiguration();
		//declaring a variable of type ECL Logic Engine(DLL)
		ECLLogicEngine push=new ECLLogicEngine();
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox PushTypeBox;
		private System.Windows.Forms.StatusBar PushStatusBar;
		private System.Windows.Forms.Label PushTypeLabel;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.GroupBox grpUserInfo;
		private System.Windows.Forms.Button btnOpenECL;
		private System.Windows.Forms.OpenFileDialog openFileDialog2;
		private System.Windows.Forms.TextBox txtECLFileName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblWorkSheet;
		private System.Windows.Forms.TextBox txtWorkSheet;
		private System.Windows.Forms.TextBox txtWebRoot;
		private System.Windows.Forms.TextBox txtChannelName;
		private System.Windows.Forms.Label lblWebRoot;
		private System.Windows.Forms.Label lblECL;
		private System.Windows.Forms.Label lblChannelName;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.RadioButton rdBtnCatcher;
		private System.Windows.Forms.RadioButton rdBtnBrowser;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFileStore;
		private System.Windows.Forms.RichTextBox ColumnFormat;
		private System.Windows.Forms.Button btnSettings;
		private System.Windows.Forms.Label lblFileLocation;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.GroupBox grpSource;
		private System.Windows.Forms.RadioButton rdBtnExcel;
		private System.Windows.Forms.RadioButton rdBtnAccess;
		private System.Windows.Forms.RadioButton rdBtnSQL;
		private System.Windows.Forms.TextBox txtDBName;
		private System.Windows.Forms.Label lblDBName;
		private System.ComponentModel.Container components = null;
		NameValueCollection settings;
		/// <summary>
		/// Default Constructor
		/// </summary>
		public PushDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
		
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSend = new System.Windows.Forms.Button();
			this.PushStatusBar = new System.Windows.Forms.StatusBar();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtDBName = new System.Windows.Forms.TextBox();
			this.lblDBName = new System.Windows.Forms.Label();
			this.txtChannelName = new System.Windows.Forms.TextBox();
			this.lblChannelName = new System.Windows.Forms.Label();
			this.txtWebRoot = new System.Windows.Forms.TextBox();
			this.lblWebRoot = new System.Windows.Forms.Label();
			this.lblWorkSheet = new System.Windows.Forms.Label();
			this.txtWorkSheet = new System.Windows.Forms.TextBox();
			this.btnOpenECL = new System.Windows.Forms.Button();
			this.txtECLFileName = new System.Windows.Forms.TextBox();
			this.lblECL = new System.Windows.Forms.Label();
			this.PushTypeLabel = new System.Windows.Forms.Label();
			this.PushTypeBox = new System.Windows.Forms.ComboBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.grpUserInfo = new System.Windows.Forms.GroupBox();
			this.btnOpen = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdBtnCatcher = new System.Windows.Forms.RadioButton();
			this.rdBtnBrowser = new System.Windows.Forms.RadioButton();
			this.btnSettings = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtFileStore = new System.Windows.Forms.TextBox();
			this.lblFileLocation = new System.Windows.Forms.Label();
			this.ColumnFormat = new System.Windows.Forms.RichTextBox();
			this.grpSource = new System.Windows.Forms.GroupBox();
			this.rdBtnSQL = new System.Windows.Forms.RadioButton();
			this.rdBtnAccess = new System.Windows.Forms.RadioButton();
			this.rdBtnExcel = new System.Windows.Forms.RadioButton();
			this.groupBox2.SuspendLayout();
			this.grpUserInfo.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.grpSource.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(88, 224);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(80, 24);
			this.btnSend.TabIndex = 7;
			this.btnSend.Text = "Send Push";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// PushStatusBar
			// 
			this.PushStatusBar.Location = new System.Drawing.Point(0, 247);
			this.PushStatusBar.Name = "PushStatusBar";
			this.PushStatusBar.Size = new System.Drawing.Size(346, 16);
			this.PushStatusBar.TabIndex = 4;
			this.PushStatusBar.Text = "READY";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtDBName);
			this.groupBox2.Controls.Add(this.lblDBName);
			this.groupBox2.Controls.Add(this.txtChannelName);
			this.groupBox2.Controls.Add(this.lblChannelName);
			this.groupBox2.Controls.Add(this.txtWebRoot);
			this.groupBox2.Controls.Add(this.lblWebRoot);
			this.groupBox2.Controls.Add(this.lblWorkSheet);
			this.groupBox2.Controls.Add(this.txtWorkSheet);
			this.groupBox2.Controls.Add(this.btnOpenECL);
			this.groupBox2.Controls.Add(this.txtECLFileName);
			this.groupBox2.Controls.Add(this.lblECL);
			this.groupBox2.Controls.Add(this.PushTypeLabel);
			this.groupBox2.Controls.Add(this.PushTypeBox);
			this.groupBox2.Location = new System.Drawing.Point(16, 88);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(328, 130);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Push Page Parameters";
			// 
			// txtDBName
			// 
			this.txtDBName.Location = new System.Drawing.Point(232, 40);
			this.txtDBName.Name = "txtDBName";
			this.txtDBName.Size = new System.Drawing.Size(80, 20);
			this.txtDBName.TabIndex = 21;
			this.txtDBName.Text = "";
			this.txtDBName.Visible = false;
			// 
			// lblDBName
			// 
			this.lblDBName.BackColor = System.Drawing.Color.Transparent;
			this.lblDBName.Location = new System.Drawing.Point(176, 44);
			this.lblDBName.Name = "lblDBName";
			this.lblDBName.Size = new System.Drawing.Size(64, 16);
			this.lblDBName.TabIndex = 20;
			this.lblDBName.Text = "Database:";
			this.lblDBName.Visible = false;
			// 
			// txtChannelName
			// 
			this.txtChannelName.Location = new System.Drawing.Point(120, 105);
			this.txtChannelName.Name = "txtChannelName";
			this.txtChannelName.Size = new System.Drawing.Size(192, 20);
			this.txtChannelName.TabIndex = 19;
			this.txtChannelName.Text = "Emergency Contact List";
			// 
			// lblChannelName
			// 
			this.lblChannelName.Location = new System.Drawing.Point(8, 107);
			this.lblChannelName.Name = "lblChannelName";
			this.lblChannelName.Size = new System.Drawing.Size(104, 16);
			this.lblChannelName.TabIndex = 18;
			this.lblChannelName.Text = "Channel Name:";
			// 
			// txtWebRoot
			// 
			this.txtWebRoot.Location = new System.Drawing.Point(120, 85);
			this.txtWebRoot.Name = "txtWebRoot";
			this.txtWebRoot.Size = new System.Drawing.Size(192, 20);
			this.txtWebRoot.TabIndex = 17;
			this.txtWebRoot.Text = "http://";
			// 
			// lblWebRoot
			// 
			this.lblWebRoot.Location = new System.Drawing.Point(8, 88);
			this.lblWebRoot.Name = "lblWebRoot";
			this.lblWebRoot.Size = new System.Drawing.Size(104, 23);
			this.lblWebRoot.TabIndex = 16;
			this.lblWebRoot.Text = "Web Context Root:";
			// 
			// lblWorkSheet
			// 
			this.lblWorkSheet.Location = new System.Drawing.Point(8, 64);
			this.lblWorkSheet.Name = "lblWorkSheet";
			this.lblWorkSheet.Size = new System.Drawing.Size(112, 23);
			this.lblWorkSheet.TabIndex = 15;
			this.lblWorkSheet.Text = "WorkSheet Name:";
			// 
			// txtWorkSheet
			// 
			this.txtWorkSheet.Location = new System.Drawing.Point(120, 64);
			this.txtWorkSheet.Name = "txtWorkSheet";
			this.txtWorkSheet.Size = new System.Drawing.Size(192, 20);
			this.txtWorkSheet.TabIndex = 14;
			this.txtWorkSheet.Text = "Contacts";
			// 
			// btnOpenECL
			// 
			this.btnOpenECL.Location = new System.Drawing.Point(240, 39);
			this.btnOpenECL.Name = "btnOpenECL";
			this.btnOpenECL.Size = new System.Drawing.Size(70, 23);
			this.btnOpenECL.TabIndex = 9;
			this.btnOpenECL.Text = "Open";
			this.btnOpenECL.Click += new System.EventHandler(this.btnOpenECL_Click);
			// 
			// txtECLFileName
			// 
			this.txtECLFileName.Location = new System.Drawing.Point(96, 40);
			this.txtECLFileName.Name = "txtECLFileName";
			this.txtECLFileName.Size = new System.Drawing.Size(144, 20);
			this.txtECLFileName.TabIndex = 3;
			this.txtECLFileName.Text = "";
			// 
			// lblECL
			// 
			this.lblECL.BackColor = System.Drawing.Color.Transparent;
			this.lblECL.Location = new System.Drawing.Point(8, 40);
			this.lblECL.Name = "lblECL";
			this.lblECL.Size = new System.Drawing.Size(96, 20);
			this.lblECL.TabIndex = 2;
			this.lblECL.Text = "ECL Excel File:";
			this.lblECL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PushTypeLabel
			// 
			this.PushTypeLabel.Location = new System.Drawing.Point(8, 16);
			this.PushTypeLabel.Name = "PushTypeLabel";
			this.PushTypeLabel.Size = new System.Drawing.Size(64, 20);
			this.PushTypeLabel.TabIndex = 1;
			this.PushTypeLabel.Text = "Push Type:";
			this.PushTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PushTypeBox
			// 
			this.PushTypeBox.Items.AddRange(new object[] {
															 "Browser-Channel",
															 "Browser-Channel-Delete"});
			this.PushTypeBox.Location = new System.Drawing.Point(96, 16);
			this.PushTypeBox.Name = "PushTypeBox";
			this.PushTypeBox.Size = new System.Drawing.Size(216, 21);
			this.PushTypeBox.TabIndex = 2;
			this.PushTypeBox.Text = "Push Type";
			this.PushTypeBox.SelectedIndexChanged += new System.EventHandler(this.PushTypeBox_SelectedIndexChanged);
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(176, 224);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(80, 24);
			this.btnReset.TabIndex = 8;
			this.btnReset.Text = "Reset Fields";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click_1);
			// 
			// txtFileName
			// 
			this.txtFileName.Location = new System.Drawing.Point(11, 15);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(192, 20);
			this.txtFileName.TabIndex = 0;
			this.txtFileName.Text = "";
			// 
			// grpUserInfo
			// 
			this.grpUserInfo.Controls.Add(this.btnOpen);
			this.grpUserInfo.Controls.Add(this.txtFileName);
			this.grpUserInfo.Location = new System.Drawing.Point(16, 8);
			this.grpUserInfo.Name = "grpUserInfo";
			this.grpUserInfo.Size = new System.Drawing.Size(328, 42);
			this.grpUserInfo.TabIndex = 9;
			this.grpUserInfo.TabStop = false;
			this.grpUserInfo.Text = "User Information File";
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(214, 12);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(88, 24);
			this.btnOpen.TabIndex = 1;
			this.btnOpen.Text = "Open";
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// openFileDialog2
			// 
			this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdBtnCatcher);
			this.groupBox1.Controls.Add(this.rdBtnBrowser);
			this.groupBox1.Location = new System.Drawing.Point(16, 52);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(168, 31);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Push To";
			// 
			// rdBtnCatcher
			// 
			this.rdBtnCatcher.BackColor = System.Drawing.Color.Transparent;
			this.rdBtnCatcher.Location = new System.Drawing.Point(68, 14);
			this.rdBtnCatcher.Name = "rdBtnCatcher";
			this.rdBtnCatcher.Size = new System.Drawing.Size(104, 16);
			this.rdBtnCatcher.TabIndex = 1;
			this.rdBtnCatcher.Text = "Custom Catcher";
			this.rdBtnCatcher.CheckedChanged += new System.EventHandler(this.rdBtnCatcher_CheckedChanged);
			// 
			// rdBtnBrowser
			// 
			this.rdBtnBrowser.BackColor = System.Drawing.Color.Transparent;
			this.rdBtnBrowser.Checked = true;
			this.rdBtnBrowser.Location = new System.Drawing.Point(8, 14);
			this.rdBtnBrowser.Name = "rdBtnBrowser";
			this.rdBtnBrowser.Size = new System.Drawing.Size(64, 16);
			this.rdBtnBrowser.TabIndex = 0;
			this.rdBtnBrowser.TabStop = true;
			this.rdBtnBrowser.Text = "Browser";
			// 
			// btnSettings
			// 
			this.btnSettings.Location = new System.Drawing.Point(264, 224);
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(72, 24);
			this.btnSettings.TabIndex = 12;
			this.btnSettings.Text = "Settings >>";
			this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.txtFileStore);
			this.groupBox3.Controls.Add(this.lblFileLocation);
			this.groupBox3.Controls.Add(this.ColumnFormat);
			this.groupBox3.Location = new System.Drawing.Point(8, 272);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(320, 155);
			this.groupBox3.TabIndex = 13;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "SpreadSheet Settings";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 16);
			this.label1.TabIndex = 18;
			this.label1.Text = "Column Format:";
			// 
			// txtFileStore
			// 
			this.txtFileStore.Location = new System.Drawing.Point(112, 130);
			this.txtFileStore.Name = "txtFileStore";
			this.txtFileStore.Size = new System.Drawing.Size(200, 20);
			this.txtFileStore.TabIndex = 17;
			this.txtFileStore.Text = "C:\\inetpub\\ecl\\ecl.html";
			// 
			// lblFileLocation
			// 
			this.lblFileLocation.Location = new System.Drawing.Point(8, 133);
			this.lblFileLocation.Name = "lblFileLocation";
			this.lblFileLocation.Size = new System.Drawing.Size(104, 16);
			this.lblFileLocation.TabIndex = 16;
			this.lblFileLocation.Text = "File Store Location:";
			// 
			// ColumnFormat
			// 
			this.ColumnFormat.Location = new System.Drawing.Point(8, 32);
			this.ColumnFormat.Name = "ColumnFormat";
			this.ColumnFormat.Size = new System.Drawing.Size(304, 96);
			this.ColumnFormat.TabIndex = 15;
			this.ColumnFormat.Text = "ColumnFormat0= %0\nColumnFormat1 = %0\nColumnFormat2 = Office: %0\nColumnFormat3 = C" +
				"ell: %0\nColumnFormat4 = Email: %0\nColumnFormat5 = Pin: %0\nColumnFormat6 = Prime " +
				"Backup: %0";
			this.ColumnFormat.TextChanged += new System.EventHandler(this.ColumnFormat_TextChanged);
			// 
			// grpSource
			// 
			this.grpSource.Controls.Add(this.rdBtnSQL);
			this.grpSource.Controls.Add(this.rdBtnAccess);
			this.grpSource.Controls.Add(this.rdBtnExcel);
			this.grpSource.Location = new System.Drawing.Point(192, 52);
			this.grpSource.Name = "grpSource";
			this.grpSource.Size = new System.Drawing.Size(152, 31);
			this.grpSource.TabIndex = 14;
			this.grpSource.TabStop = false;
			this.grpSource.Text = "Source";
			// 
			// rdBtnSQL
			// 
			this.rdBtnSQL.BackColor = System.Drawing.Color.Transparent;
			this.rdBtnSQL.Location = new System.Drawing.Point(110, 14);
			this.rdBtnSQL.Name = "rdBtnSQL";
			this.rdBtnSQL.Size = new System.Drawing.Size(72, 16);
			this.rdBtnSQL.TabIndex = 2;
			this.rdBtnSQL.Text = "SQL";
			this.rdBtnSQL.CheckedChanged += new System.EventHandler(this.rdBtnSQL_CheckedChanged);
			// 
			// rdBtnAccess
			// 
			this.rdBtnAccess.BackColor = System.Drawing.Color.Transparent;
			this.rdBtnAccess.Location = new System.Drawing.Point(52, 14);
			this.rdBtnAccess.Name = "rdBtnAccess";
			this.rdBtnAccess.Size = new System.Drawing.Size(59, 16);
			this.rdBtnAccess.TabIndex = 1;
			this.rdBtnAccess.Text = "Access";
			this.rdBtnAccess.CheckedChanged += new System.EventHandler(this.rdBtnAccess_CheckedChanged);
			// 
			// rdBtnExcel
			// 
			this.rdBtnExcel.BackColor = System.Drawing.Color.Transparent;
			this.rdBtnExcel.Checked = true;
			this.rdBtnExcel.Location = new System.Drawing.Point(3, 14);
			this.rdBtnExcel.Name = "rdBtnExcel";
			this.rdBtnExcel.Size = new System.Drawing.Size(56, 16);
			this.rdBtnExcel.TabIndex = 0;
			this.rdBtnExcel.TabStop = true;
			this.rdBtnExcel.Text = "Excel";
			this.rdBtnExcel.CheckedChanged += new System.EventHandler(this.rdBtnExcel_CheckedChanged);
			// 
			// PushDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(346, 263);
			this.Controls.Add(this.grpSource);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btnSettings);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpUserInfo);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.PushStatusBar);
			this.Controls.Add(this.btnSend);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "PushDialog";
			this.Text = "BlackBerry ECL";
			this.Load += new System.EventHandler(this.PushDialog_Load);
			this.groupBox2.ResumeLayout(false);
			this.grpUserInfo.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.grpSource.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{			
				Application.Run(new PushDialog());
		}
		
		/// <summary>
		/// This method is triggered when the "Send Push" Button is pressed.
		/// This method calls the ECLLogic Dll for processing the information
		/// </summary>
		private void btnSend_Click(object sender, System.EventArgs e)
		{		
			//verifying information is filled correctly 
			if(!verifyFields())
			{
				
				return;
			}
			try
			{
				//get the static settings from app.config
				settings = ConfigurationSettings.AppSettings;
				config.PushMethod = settings["PushMethod"];
				// PAP settings
				config.PushID = settings["PushID"];
				config.BeforeTimestamp = settings["BeforeTimestamp"];
				config.AfterTimestamp  = settings["AfterTimestamp"];
				config.Priority = settings["Priority"];
				config.DeliveryMethod = settings["Delivery"];


				this.PushStatusBar.Text ="";
				//invoking Pusher instance based on Push type selected
				if (rdBtnBrowser.Checked == true)
				{
					// Construct a pusher that sends to a browser channel.
					config.PushTo="Channel";

				}
				else if (rdBtnCatcher.Checked == true)
				{
					// Construct a pusher that sends to a custom catcher.
					config.PushTo="Catcher";
				}
			
				//setting configuration
				config.recipientFile = txtFileName.Text;
				config.File_Server_Name = txtECLFileName.Text;
				config.Sheet_Table_Name = txtWorkSheet.Text;
				if(rdBtnSQL.Checked==true)
				{
					config.Database=txtDBName.Text;
				}
				config.ChannelName = txtChannelName.Text;
				config.ColumnFormat = ColumnFormat.Lines;
				config.fileStoreLoc = txtFileStore.Text;
				config.WebRoot = txtWebRoot.Text;
				config.PushType = PushTypeBox.SelectedIndex;
				config.Source="GUI";
				log.Info("Starting Push");
				//sending push
				//calling the dll method to process the ECL parameters and perform the push
				config.Display();
				this.PushStatusBar.Text = push.processECL(config);
				
			}
			catch (System.Exception ex)
			{
			
				log.Info("Exception: "+ ex.Message,ex);
				this.PushStatusBar.Text ="Push Failed. Please check logs";
			}
			
		}

		/// <summary>
		/// Verifying the fields entered are correct
		/// </summary>
		/// <returns></returns>
		private bool verifyFields()
		{
			bool result = true;
			string temp;
			//verifies the user information file
			if (txtFileName.Text.Equals(""))
			{
				this.grpUserInfo.ForeColor = Color.Red;
				this.PushStatusBar.Text = "Missing User Information File Field";
				result = false;
				return result;
			} 
			else
			{   temp=(txtFileName.Text).Trim();
				//checking for correct file (.txt extension)
				if(temp.Substring(temp.Length-4,4) != ".txt")
				{
					this.PushStatusBar.Text = "Please enter a file with extension .txt";
					result = false;
					return result;
				}
				this.grpUserInfo.ForeColor = Color.Black;
			}
			//checking if file exists
			if (!File.Exists(txtFileName.Text))
			{ 
				this.PushStatusBar.Text ="User Information File does not exist"; 
				result = false ; 
				return result;
			} 
			//Verifies if Browser Push is selected then Push type must be selected also
			if (rdBtnCatcher.Checked==true||rdBtnBrowser.Checked == true)
			{
				this.PushTypeLabel.ForeColor = Color.Black;
				if(PushTypeBox.SelectedIndex==0||rdBtnCatcher.Checked==true)
				{
					if(txtECLFileName.Enabled==true)
					{
						//Verifies the ECL Excel File
						if (txtECLFileName.Text.Equals(""))
						{
							this.lblECL.ForeColor = Color.Red;
							this.PushStatusBar.Text = "Missing "+lblECL.Text;
							result = false;
							return result;
						} 
						else
						{
							//checking for correct file (.txt extension)
							if(rdBtnExcel.Checked==true)
							{
								config.ConnectionType="Excel";
								if((txtECLFileName.Text).Substring((txtECLFileName.Text).IndexOf(".",0)+1,3) != "xls")
								{
									this.PushStatusBar.Text = "Please enter a .xls file for ECL";
									result = false;
									return result;
								}
							}
							else if(rdBtnAccess.Checked==true)
							{
								config.ConnectionType="Access";
								if((txtECLFileName.Text).Substring((txtECLFileName.Text).IndexOf(".",0)+1,3) != "mdb")
								{
									this.PushStatusBar.Text = "Please enter a .mdb file for ECL";
									result=false;
									return result;
								}
							}
							this.lblECL.ForeColor = Color.Black;
						}
					}

					if(txtWorkSheet.Enabled==true)
					{
						//Verifies Worksheetname
						if (txtWorkSheet.Text.Equals(""))
						{
							this.lblWorkSheet.ForeColor = Color.Red;
							this.PushStatusBar.Text = "Missing "+lblWorkSheet.Text;
					
							result = false;
							return result;
						} 
						else this.lblWorkSheet.ForeColor = Color.Black;
					}
			
					//Ensuring Channel Name is selected
					if (txtChannelName.Text.Equals(""))
					{
						this.lblChannelName.ForeColor = Color.Red;
						this.PushStatusBar.Text = "Missing Browser Channel Name";
				
						result = false;
						return result;
					} 
					else this.lblChannelName.ForeColor = Color.Black;

				}
				//ensuring Push Type is selected			
				else if (PushTypeBox.SelectedIndex == -1&&rdBtnCatcher.Checked==false)
				{
					this.PushTypeLabel.ForeColor = Color.Red;
					this.PushStatusBar.Text = "Missing Browser Push Type";
				
					result = false;
					return result;
				} 

				if(txtWebRoot.Text.ToString().Substring(txtWebRoot.TextLength-1,1)=="/")
				{
					if(txtWebRoot.Text.ToString().Substring(txtWebRoot.TextLength-2,1)!="/")
					{
						txtWebRoot.Text= txtWebRoot.Text.ToString().Substring(0,txtWebRoot.TextLength -1);
					}
				}
				else if(txtWebRoot.Text.ToString().Substring(txtWebRoot.TextLength-1,1)=="\\")
				{
					this.lblWebRoot.ForeColor= Color.Red;
					this.PushStatusBar.Text = "Please replace \\ with / in Web Context Root";
					result=false;
					return result;

				}
			}
						
				if(rdBtnSQL.Checked==true)
				{
					config.ConnectionType="MSSQL";
					if(txtDBName.Enabled==true)
					{
						if(txtDBName.Text=="")
						{
							this.PushStatusBar.Text = "Missing Database Name";
							this.lblDBName.ForeColor = Color.Red;
					
							result = false;
							return result;
						}
						else 
						{
							this.lblDBName.ForeColor= Color.Black;	
						}
					}
				}
				else
				{
					//checking if file exists
					if(txtECLFileName.Enabled==true)
					{
						if (!File.Exists(txtECLFileName.Text))
						{ 
							this.PushStatusBar.Text =  lblECL.Text + " does not exist"; 
							result = false ; 
							return result;
						}  
					}
				}
			
			
			return result;
		}
		
			
		
		/// <summary>
		/// Opening the file Dialog for User information File
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		
		private void btnOpen_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.ShowDialog();

		}

		/// <summary>
		/// Selecting filename from the file dialog
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		
		private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			txtFileName.Text= openFileDialog1.FileName;
		}
        
		/// <summary>
		/// Opening the file Dialog for Excel Sheet
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOpenECL_Click(object sender, System.EventArgs e)
		{
		     openFileDialog2.ShowDialog();
		}

		/// <summary>
		/// Selecting the file and displaying in text box
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void openFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
		     txtECLFileName.Text= openFileDialog2.FileName;
		}

		/// <summary>
		/// Enabling/Disabling Fields based on the Browser Channel Push/Catcher Push
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rdBtnCatcher_CheckedChanged(object sender, System.EventArgs e)
		{
			
			PushTypeBox.Text="Push Type";
			if (rdBtnCatcher.Checked==true)
			{
				PushTypeLabel.Enabled=false;
				PushTypeBox.Enabled=false;
				lblWebRoot.Enabled=false;
				txtWebRoot.Enabled=false;
				lblChannelName.Enabled=false;
				txtChannelName.Enabled=false;
				this.txtFileStore.Enabled=false;
				lblFileLocation.Enabled=false;
				lblECL.Enabled=true;
				txtECLFileName.Enabled=true;
				btnOpenECL.Enabled=true;
					lblWorkSheet.Enabled=true;
				txtWorkSheet.Enabled=true;
				lblDBName.Enabled=true;
				txtDBName.Enabled=true;

			}
			else
			{
				PushTypeLabel.Enabled=true;
				PushTypeBox.Enabled=true;
				lblWebRoot.Enabled=true;
				txtWebRoot.Enabled=true;
				lblChannelName.Enabled=true;
				txtChannelName.Enabled=true;
				this.txtFileName.Enabled=true;
				this.txtFileStore.Enabled=true;
				lblFileLocation.Enabled=true;
				
				
				
			}
		
		}

		/// <summary>
		/// Showing and hiding the settings section to the user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSettings_Click(object sender, System.EventArgs e)
		{
			if(this.btnSettings.Text=="Settings >>")
			{
				this.Height=480;
				this.btnSettings.Text="Settings <<";
			}
			else if(this.btnSettings.Text=="Settings <<")
			{
				this.Height=304;
				this.btnSettings.Text="Settings >>";
			}
		}

		/// <summary>
		/// Enabling/Disabling Fields when radio button when Excel option is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rdBtnExcel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(rdBtnExcel.Checked==true)
			{
				lblECL.Text="ECL Excel File:";
				txtECLFileName.Text="";
				lblWorkSheet.Text="WorkSheet Name:";
				txtECLFileName.Width=144;
				txtDBName.Visible=false;
				lblDBName.Visible=false;
				btnOpenECL.Enabled=true;
				btnOpenECL.Visible=true;
			}
			
		}

		/// <summary>
		/// Enabling/Disabling Fields when radio button when Access option is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void rdBtnAccess_CheckedChanged(object sender, System.EventArgs e)
		{
			if(rdBtnAccess.Checked==true)
			{
				lblECL.Text="ECL Access File:";
				txtECLFileName.Text="";
				lblWorkSheet.Text="Table Name:";
				txtECLFileName.Width=144;
				txtDBName.Visible=false;
				lblDBName.Visible=false;
				//btnOpenECL.Enabled=true;
				btnOpenECL.Visible=true;
			}
		}

		/// <summary>
		/// Enabling/Disabling Fields when radio button when MSSQL option is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rdBtnSQL_CheckedChanged(object sender, System.EventArgs e)
		{
			if(rdBtnSQL.Checked==true)
			{
				lblECL.Text="Server Name:";
				txtECLFileName.Text="";
				txtECLFileName.Width=80;
				txtDBName.Visible=true;
				lblDBName.Visible=true;
				lblWorkSheet.Text="Table Name:";
				//btnOpenECL.Enabled=false;
				btnOpenECL.Visible=false;
			}
		}

		private void PushTypeBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(PushTypeBox.SelectedItem.Equals("Browser-Channel-Delete"))
			{
					grpSource.Enabled=false;
				groupBox1.Enabled=false;
				txtECLFileName.Enabled=false;
				txtDBName.Enabled=false;
				btnOpenECL.Enabled=false;
				lblECL.Enabled=false;
				lblWorkSheet.Enabled=false;
				txtWorkSheet.Enabled=false;
				lblChannelName.Enabled=false;
				txtChannelName.Enabled=false;
				ColumnFormat.Enabled=false;
				txtFileStore.Enabled=false;
			}
			else
			{
				grpSource.Enabled=true;
				groupBox1.Enabled=true;
				txtECLFileName.Enabled=true;
				txtDBName.Enabled=true;
				btnOpenECL.Enabled=true;
				lblECL.Enabled=true;
				lblWorkSheet.Enabled=true;
				txtWorkSheet.Enabled=true;
				lblChannelName.Enabled=true;
				txtChannelName.Enabled=true;
				ColumnFormat.Enabled=true;
				txtFileStore.Enabled=true;

			}
		}

		/// <summary>
		/// Reseting all fields
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		
		private void btnReset_Click_1(object sender, System.EventArgs e)
		{
			
			
			//this.PushTypeBox.Text = "Push Type";
			this.PushTypeBox.Text = "Push Type";
			txtFileName.Text="";
			this.txtECLFileName.Text="";
			this.txtWebRoot.Text="http://";
			this.txtChannelName.Text="Emergency Contact List";
			this.txtWorkSheet.Text="Contacts";
			this.txtFileStore.Text="C:\\ecl.html";
			this.PushTypeLabel.ForeColor = Color.Black;
			this.lblECL.ForeColor = Color.Black;
			this.lblWorkSheet.ForeColor = Color.Black;
			this.lblWebRoot.ForeColor = Color.Black;
			this.lblChannelName.ForeColor = Color.Black;
			
		}

		private void PushDialog_Load(object sender, System.EventArgs e)
		{
			Assembly a = Assembly.GetExecutingAssembly();
			AssemblyName an = a.GetName();
			

			this.FindForm().Text = "BlackBerry ECLGUI v"+ an.Version.ToString();
			log.Info("BlackBerry ECLGUI v"+ an.Version.ToString());

		}

		private void ColumnFormat_TextChanged(object sender, System.EventArgs e)
		{
		
		}

			
	}
}
