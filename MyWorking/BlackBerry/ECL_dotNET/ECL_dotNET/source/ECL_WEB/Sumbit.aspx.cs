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

namespace ECL
{
	/// <summary>
	/// Summary description for Sumbit.
	/// </summary>
	public class Sumbit : System.Web.UI.Page
	{
		public ECL sourcepage;
		protected System.Web.UI.WebControls.Button Back;
		protected System.Web.UI.HtmlControls.HtmlImage IMG1;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit2;
		protected System.Web.UI.WebControls.TextBox TextBox1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				ECL sourcepage = (ECL) Context.Handler;
				TextBox1.Text = sourcepage.WebLog;
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
			this.Back.Click += new System.EventHandler(this.Back_Click);
			this.Submit2.ServerClick += new System.EventHandler(this.Submit2_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Back_Click(object sender, System.EventArgs e)
		{
			Server.Transfer("ECL.aspx");
		}

		private void Submit2_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect("http://localhost/PAP",true);
		}
	}
}
