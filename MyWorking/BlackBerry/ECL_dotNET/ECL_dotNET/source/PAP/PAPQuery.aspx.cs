using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;
using log4net;
using log4net.Config;

namespace PAP
{
	/// <summary>
	/// This is used for when a reliable push query is made. It finds data from the PAP table located in the ECL database.
	/// </summary>
	public class PAPQuery : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Div5;
		protected System.Web.UI.WebControls.RadioButtonList SearchType;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
		protected System.Web.UI.WebControls.DataGrid MyDataGrid;
		protected System.Web.UI.WebControls.TextBox PushID;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit2;
		NameValueCollection settings;
		protected System.Web.UI.HtmlControls.HtmlImage IMG1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Div2;
		protected System.Web.UI.HtmlControls.HtmlGenericControl DIV1;
		protected System.Web.UI.WebControls.TextBox InputDate;
		protected System.Web.UI.WebControls.Image Image1;
		private static readonly ILog log = LogManager.GetLogger("ECLQuery");
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(InputDate.Text.Equals(""))
				InputDate.Text="MM/DD/YYYY";
			if(!IsPostBack)
				Div5.Visible=false;
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
			this.SearchType.SelectedIndexChanged += new System.EventHandler(this.SearchType_SelectedIndexChanged);
			this.Submit1.ServerClick += new System.EventHandler(this.Submit1_ServerClick);
			this.Submit2.ServerClick += new System.EventHandler(this.Submit2_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		/// <summary>
		/// 4 options are avaliable; Search all pushes, successful pushes, unsuccessful pushes or a specific push.
		/// For a specific push, a push-ID is required
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void SearchType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//only for a specific push, show the push ID field, else show the Date field
			if(SearchType.SelectedIndex==3)
			{
				Div2.Visible=false;
				Div5.Visible=true;
			}
			else
			{
				Div5.Visible=false;
				Div2.Visible=true;
			}
		}
		/// <summary>
		/// Depending on what type of search criteria is selected, the correct values will be displayed in a data grid.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
	
		private void Submit1_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				//intialize the data grid
				MyDataGrid.DataSource=null;
				MyDataGrid.DataBind();
				OleDbConnection myConnection = new OleDbConnection(GetConnection());
				string command;
				string Date = InputDate.Text;
				
				switch(SearchType.SelectedIndex)
				{
				
					//if a date is not provided, search all records, else search all records under the specified date.
					case 0:
						
						if(Date.Equals("MM/DD/YYYY")|| Date.Equals(""))
						{
							command = "SELECT * FROM PAP";
						}
						else
						{
							command = "SELECT * FROM PAP WHERE Date BETWEEN '"+Date+"' AND '"+Date+" 23:59:59'";
						}
						break;
					//if date is not provided, search all successful records, else searh all successful records under the specified date.
					case 1:

						if(Date.Equals("MM/DD/YYYY")|| Date.Equals(""))
						{
							command = "SELECT * FROM PAP WHERE Delivered = 'delivered'";
						}
						else
						{
							command = "SELECT * FROM PAP WHERE Delivered = 'delivered' AND Date BETWEEN '"+Date+"' AND '"+Date+" 23:59:59'";	
						}
						break;
						//if date is not provided, search all unsuccessful records, else search all unusuccessful records under the specified date.
					case 2:
						if(Date.Equals("MM/DD/YYYY")|| Date.Equals(""))
						{
							command = "SELECT * FROM PAP WHERE Delivered != 'delivered'";
						}
						else
						{
							command = "SELECT * FROM PAP WHERE Delivered != 'delivered' AND Date BETWEEN '"+Date+"' AND '"+Date+" 23:59:59'";							
						}
						break;
						//if a push ID is not provided, go to default, else search the record under that specific ID.
					case 3:
						
						if(PushID.Text.Equals(""))
						{
							goto default;
						}
						else
						{
							command = "SELECT * FROM PAP Where PushID = '"+PushID.Text+"';";
						}
						break;
					//provides a list of all pushes.
					default :
						command = "SELECT * FROM PAP";
						break;

				}
				//open up the connection to the sql server, read the table and bind the result to the data grid
				OleDbCommand myCommand = new OleDbCommand(command, myConnection);
				myConnection.Open();
				OleDbDataReader dr = myCommand.ExecuteReader();
				MyDataGrid.DataSource = dr;
				MyDataGrid.DataBind();
				myConnection.Close();
			}
			catch(Exception ex)
			{
				log.Info(ex.Message);
			}



		}
		/// <summary>
		/// Gets the server and database address where the PAP table is stored
		/// </summary>
		/// <returns></returns>
		public string GetConnection()
		{
			settings = ConfigurationSettings.AppSettings;
			return settings["DataAddress"];
		}
		/// <summary>
		/// ECL.aspx is the home page for this app
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Submit2_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect("http://localhost/ECL",true);
		}
		

		
	}
}
