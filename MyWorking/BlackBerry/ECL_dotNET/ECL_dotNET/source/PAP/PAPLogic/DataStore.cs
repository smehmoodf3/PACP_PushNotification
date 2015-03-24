using System;
using System.Data.OleDb;
using System.Data;
using log4net;
using System.Configuration;
using log4net.Config;
using System.Collections.Specialized;

namespace PAPLogic
{
	/// <summary>
	/// used for PAP push and stores data regarding to Push ID. Is called from both Pusher.cs and PAPListener.aspx
	/// to update the database.
	/// </summary>
	public class DataStore
	{
		NameValueCollection settings;
		
		private static readonly ILog log = LogManager.GetLogger("DataStore");

		/// <summary>
		/// only called by Pusher.cs as it provides the Push Id with the status of the contact between MDS 
		/// and APP server.
		/// The try and catch of Pusher catches any exception thrown by this.
		/// </summary>
		/// <param name="PushID"></param>
		/// <param name="MDSStatus"></param>
		public DataStore(string PushID, string MDSStatus)
		{
			OleDbConnection myConnection = new OleDbConnection(GetConnection());
			try
			{
				myConnection.Open();
				string InsertCommand = "INSERT INTO PAP(PushID, MDSState, Delivered) VALUES('"+PushID+"','"+MDSStatus+"','Not Delivered');";
				OleDbCommand myCommand = new OleDbCommand(InsertCommand, myConnection);
				myCommand.ExecuteNonQuery();
				myConnection.Close();
				log.Info("Created record, waiting for device confirmation");
			}
			catch(Exception ex)
			{
				log.Info("Error in creating record from MDS:" +ex.Message);
				myConnection.Close();
			}

		}
		/// <summary>
		/// called by PAPListener.aspx to update if the device received the push and the device's address
		/// </summary>
		/// <param name="PushID"></param>
		/// <param name="DeviceAddress"></param>
		/// <param name="DeviceStatus"></param>
		/// <param name="DeliveryStatus"></param>
		public DataStore(string PushID, string DeviceAddress, string DeviceStatus, string DeliveryStatus)
		{
			
			OleDbConnection myConnection;
			myConnection = new OleDbConnection(GetConnection());
			//we only want to store the address, so remove the padding around it
			DeviceAddress=DeviceAddress.Remove(0,8);
			int i=DeviceAddress.LastIndexOf("/TYPE=USER@rim.net");
			DeviceAddress=DeviceAddress.Remove(i,18);
			try
			{				
				myConnection.Open();
				//since the record already exists, we only need to update it.
				string UpdateCommand ="UPDATE PAP SET DeviceAddress='"+DeviceAddress+"', DeviceState='"+DeviceStatus+
					"', Delivered='"+DeliveryStatus+"' WHERE PushID='"+PushID+"';";
				OleDbCommand myCommand = new OleDbCommand(UpdateCommand, myConnection);
				myCommand.ExecuteNonQuery();
				myConnection.Close();
				log.Info("Updated record with delivery result");
			}	
			catch(System.Exception e)
			{
				log.Info("Error in updating record from device notification:" +e.Message);
				myConnection.Close();
			}
		}
		/// <summary>
		/// Returns the value for DataAddress set in web.config, under appSettings.
		/// </summary>
		/// <returns>Database Address</returns>
		public string GetConnection()
		{
			settings = ConfigurationSettings.AppSettings;
			return settings["DataAddress"];
		}
	}
}
