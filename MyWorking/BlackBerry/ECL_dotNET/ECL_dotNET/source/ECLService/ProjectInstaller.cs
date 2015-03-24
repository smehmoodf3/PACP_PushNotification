using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace ECLService
{
	/// <summary>
	/// This class installs the service
	/// </summary>
	[RunInstaller(true)]
	public class ProjectInstaller : System.Configuration.Install.Installer
	{
		/// <summary>
		/// System variable required to perform installation of service
		/// </summary>
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
		/// <summary>
		/// System variable required to perform installation of service
		/// </summary>
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// This is the default class constructor
		/// </summary>
		public ProjectInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
			this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
			// 
			// serviceProcessInstaller1
			// 
			this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessInstaller1.Password = null;
			this.serviceProcessInstaller1.Username = null;
			// 
			// serviceInstaller1
			// 
			this.serviceInstaller1.ServiceName = "ECLService";
			this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
																					  this.serviceProcessInstaller1,
																					  this.serviceInstaller1});

		}
		#endregion

		
	}
}
