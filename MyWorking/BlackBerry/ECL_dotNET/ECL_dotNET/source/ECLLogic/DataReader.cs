using System;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Web;
using log4net;
using log4net.Config;


namespace ECLLogic 
{
	/// <summary> This class reads contact data from the data source.
	/// </summary>
	internal class DataReader
	{
        /// <summary>
        /// OleDb Connection to the data source
        /// </summary> 
		OleDbConnection objConn ;

		/// <summary>
		/// Define a static logger variable so that it references the
		/// Logger instance named "DataReader".
		/// </summary>
		private static readonly ILog log = LogManager.GetLogger("DataReader");		
		
		/// <summary>
		/// This method obtains the recipients for the BESMgmt override scenario.
		/// </summary>
		/// <param name="type">ECLConfiguration has all the configuration values needed to push the message</param>
		/// <returns>This method returns an ArrayList of Recipients </returns>
		public System.Collections.ArrayList getRecipients(ECLConfiguration config)
		{
			ArrayList SenderList = new ArrayList();
			try
			{
				OleDbCommand cmd = null;

				// BESMgmt override control
				if (config.BESMgmt && config.BESUsersConnType.Equals("MSSQL"))
				{
					StringBuilder cmdStr = new StringBuilder("SELECT TOP 100 PERCENT t1.Id, t1.MachineName ");
					cmdStr.Append("as BESName, t2.WebServerListenPort as Port, t3.MailboxSMTPAddr as Recipient ");
					cmdStr.Append("FROM ServerConfig t1 INNER JOIN MDSConfig t2 ON t1.Id = t2.ServerConfigId ");
					cmdStr.Append("INNER JOIN UserConfig t3 ON t1.Id = t3.ServerConfigId");  
					String str = cmdStr.ToString();
					log.Debug("BESMgmt Recipient Query: " + str);

					cmd = new OleDbCommand(str, objConn);

					OleDbDataReader reader = cmd.ExecuteReader();
					while (reader.Read()) 
					{
						SenderInfo Sender = new SenderInfo();
						Sender.BESAddress = reader.IsDBNull(1) ? "" : reader.GetString(1); 
						int port = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
						Sender.BESPort = Convert.ToString(port);
						Sender.Email      = reader.IsDBNull(3) ? "" : reader.GetString(3);
						SenderList.Add(Sender);									
					}
					reader.Close();	
				}
			}
			catch(Exception t)
			{
				log.Info("Exception: "+ t.Message,t);	
			}
			finally
			{
				;
			}
			return SenderList;
		}

		/// <summary>
		/// This method obtains the recipients for the BESMgmt override scenario.
		/// </summary>
		/// <param name="type">ECLConfiguration has all the configuration values needed to push the message</param>
		/// <returns>This method returns an ArrayList of ContactInfo </returns>
		public System.Collections.ArrayList getContactInfo(ECLConfiguration config)
		{
			ArrayList ContactList = new ArrayList();
			try
			{
				OleDbCommand cmd = null;

				// BESMgmt override control
				if (config.BESMgmt && config.BESContactConnType.Equals("MSSQL"))
				{
					StringBuilder cmdStr = new StringBuilder("SELECT TOP 100 PERCENT t3.DisplayName as Recipient, ");
					cmdStr.Append("t3.MailboxSMTPAddr as Email, t4.PhoneNumber as Phone, t3.Pin as Pin ");
					cmdStr.Append("FROM ServerConfig t1 ");
					cmdStr.Append("INNER JOIN MDSConfig t2 ON t1.Id = t2.ServerConfigId ");
					cmdStr.Append("INNER JOIN UserConfig t3 ON t1.Id = t3.ServerConfigId "); 
					cmdStr.Append("INNER JOIN SyncDeviceMgmtSummary t4 ON t3.Id = t4.UserConfigId ");
					cmdStr.Append("ORDER BY Recipient");
					String str = cmdStr.ToString();
					log.Debug("BESMgmt Contacts Query: " + str);

					cmd = new OleDbCommand(str, objConn);

					OleDbDataReader reader = cmd.ExecuteReader();
					int fieldCnt = Convert.ToInt32(ConfigurationSettings.AppSettings["BESColumnNum"]);
					while (reader.Read()) 
					{
						String[] Contact = new String[fieldCnt];
						for (int i = 0; i < fieldCnt; i++)
						{
							String dataStr = reader.IsDBNull(i) ? "" : reader.GetString(i); 
							String formatStr = config.ColumnFormat[i].ToString();
							Contact[i] = formatStr.Replace("%0", dataStr);
						}
						ContactList.Add(Contact);	
					}
					reader.Close();	
				}
			}
			catch(Exception t)
			{
				log.Info("Exception: "+ t.Message,t);	
			}
			finally
			{
				;
			}
			return ContactList;
		}
			
		/// <summary>This method extracts the group names from the data source.  Returns them
		/// in a vector of strings.
		/// </summary>
		/// <param name="type">This parameter is of type ECLConfiguration which has all the configuration values needed to push the message</param>
		/// <returns>This method returns an ArrayList of Group names </returns>
		public System.Collections.ArrayList GroupList(ECLConfiguration type)
		{
			
			System.Collections.ArrayList groupNames = new System.Collections.ArrayList();
			System.String grpName;
			DataTable dt;
			OleDbCommand objCmdSelect=null;
			
			// Contacts overridden from BESMgmt
			if (type.ContactType == ECLLogicEngine.CONTACT_BESMGMT_OVERRIDE)
			{
				if (type.BESContactConnType.Equals("Excel"))
				{
					// Create new OleDbCommand to return data from worksheet.
					objCmdSelect =new OleDbCommand("SELECT * FROM ["+type.Sheet_Table_Name.ToString() + "$]", objConn);
				}
				else if (type.BESContactConnType.Equals("Access") || type.BESContactConnType.Equals("MSSQL"))
				{
					// Create new OleDbCommand to return data from table.
					objCmdSelect =new OleDbCommand("SELECT * FROM ["+type.Sheet_Table_Name.ToString() + "]", objConn);
				}				
			}
			else
			{
				if (type.ConnectionType.Equals("Excel"))
				{
					// Create new OleDbCommand to return data from worksheet.
					objCmdSelect =new OleDbCommand("SELECT * FROM ["+type.Sheet_Table_Name.ToString() + "$]", objConn);
				}
				else if (type.ConnectionType.Equals("Access") || type.ConnectionType.Equals("MSSQL"))
				{
					// Create new OleDbCommand to return data from table.
					objCmdSelect =new OleDbCommand("SELECT * FROM ["+type.Sheet_Table_Name.ToString() + "]", objConn);
				}
			}

			// Create new OleDbDataAdapter that is used to build a DataSet
			// based on the preceding SQL SELECT statement.
			OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

			// Pass the Select command to the adapter.
			objAdapter1.SelectCommand = objCmdSelect;
				
			// Create new DataSet to hold information from the worksheet.
			DataSet resultSet = new DataSet();
				
			// Fill the DataSet with the information from the worksheet.
			objAdapter1.Fill(resultSet, type.Sheet_Table_Name.ToString());
			dt = resultSet.Tables[type.Sheet_Table_Name.ToString()];

			//write table data
			for (int irow=0; irow<dt.Rows.Count; irow++)
			{
				//Extracting a row
				DataRow dr = dt.Rows[irow];
				string str = string.Empty;
				//parsing all columns of that row
				for (int icol=0; icol<dt.Columns.Count; icol++)
				{
					//cheking if first column is not blank
					if(dr[icol].ToString()!="")
					{
						//getting value of first column
						grpName=dr[icol].ToString();
						if(icol<=5)
						{
							//checking if second column is blank
							if(dr[icol+1].ToString()=="")
							{
								//adding the value of column one to list of group names
								groupNames.Add(grpName);
								//setting icol to end of columns
								icol=dt.Columns.Count;
							}
							else
							{	//if second column is not blank then setting icol to end of columns
								icol=dt.Columns.Count;
							}								
						}
					}						
				}					
			}						
			groupNames.TrimToSize(); //clean up the vector
			return groupNames;		
		}
	
		/// <summary> Default constructor.  
		/// </summary>
		public DataReader()
		{			
		}
	
		/// <summary>
		/// Opens an ODBC connection
		/// </summary>
		/// <param name="option">The data option</param>
		/// <param name="type">ECLConfiguration has the configuration values needed to push the message</param>/// 
		public virtual void  openDB(int option, ECLConfiguration type)
		{			
			String sConnectionString="";

			log.Debug("openDB request: " + option);
			switch (option)
			{
                // Use the BES4.0 Database
				case ECLLogicEngine.USER_BESMGMT:
					log.Debug("BESMgmt USER_BESMGMT");
					sConnectionString = ConfigurationSettings.AppSettings["BESMgmtConn"];
					break;
				
				case ECLLogicEngine.CONTACT_BESMGMT:		
					log.Debug("BESMgmt CONTACT_BESMGMT");
					sConnectionString = ConfigurationSettings.AppSettings["BESMgmtConn"];
					break;

                // Use the Default (Non BESMgmt) DataSource
				case ECLLogicEngine.USER_DEFAULT:
					log.Debug("BESMgmt USER_DEFAULT - should not occur.");
					break;

				case ECLLogicEngine.CONTACT_DEFAULT:
					log.Debug("BESMgmt CONTACT_DEFAULT");
					// Build an ODBC connection for Access
					if (type.ConnectionType.Equals("Access"))
					{
						sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
							"Data Source=" + type.File_Server_Name+";";
					}
					// Build an ODBC connection for Excel
					else if (type.ConnectionType.Equals("Excel"))
					{
						sConnectionString="Provider=Microsoft.Jet.OLEDB.4.0;" +
							"Data Source=" + type.File_Server_Name+";" + 
							"Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'" ;
					}
					// Build an ODBC connection for MS SQL
					else if (type.ConnectionType.Equals("MSSQL"))
					{ 				
						//"User ID='dbo'; Password='';";
						sConnectionString=@"Integrated Security=SSPI;" +
							"Packet Size=4096;"+
							"Data Source="+type.File_Server_Name+";"+
							"Initial Catalog="+ type.Database+";"+
							"Tag with column collation when possible=False;"+
							"Provider=SQLOLEDB.1;uid=sa;password=sa;"+
							"Use Procedure for Prepare=1;"+
							"Auto Translate=True;"+
							"Persist Security Info=False;"+
							"Use Encryption for Data=False;";
					}
					break;

				case ECLLogicEngine.USER_BESMGMT_OVERRIDE:
					log.Debug("BESMgmt USER_BESMGMT_OVERRIDE - should not occur.");
					break;

				case ECLLogicEngine.CONTACT_BESMGMT_OVERRIDE:
					log.Debug("BESMgmt CONTACT_BESMGMT_OVERRIDE");		
					// Build an ODBC connection for Access
					if (type.BESContactConnType.Equals("Access"))
					{
						sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
							"Data Source=" + type.BESContactServer +";";
					}
						// Build an ODBC connection for Excel
					else if (type.BESContactConnType.Equals("Excel"))
					{
						sConnectionString="Provider=Microsoft.Jet.OLEDB.4.0;" +
							"Data Source=" + type.BESContactServer + ";" + 
							"Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'" ;
					}
						// Build an ODBC connection for MS SQL
					else if (type.BESContactConnType.Equals("MSSQL"))
					{ 				
						//"User ID='dbo'; Password='';";
						sConnectionString=@"Integrated Security=SSPI;" +
							"Packet Size=4096;"+
							"Data Source="+type.File_Server_Name+";"+
							"Initial Catalog="+ type.Database+";"+
							"Tag with column collation when possible=False;"+
							"Provider=SQLOLEDB.1;uid=sa;password=sa;"+
							"Use Procedure for Prepare=1;"+
							"Auto Translate=True;"+
							"Persist Security Info=False;"+
							"Use Encryption for Data=False;";
					}
					break;
			}
			
			// Create connection object by using the preceding connection string.
			objConn = new OleDbConnection(sConnectionString);

			// Open connection with the database.
			objConn.Open();
		}
	
		/// <summary> Close the ODBC connection 
		/// </summary>
		public virtual void  closeDB()
		{
			try
			{
				objConn.Close();
			}
			catch (System.Data.OleDb.OleDbException sqlex)
			{
				log.Error("Error closing ODBC connection to file.",sqlex);				
			}
		}	
	
		/// <summary> Extracts the names of all people in the indicated group.  Returns them
		/// as a vector of strings.
		/// </summary>
		/// <param name="groupName">The name of the group for which the method has to obtain contact list</param>
		/// <param name="type">This parameter is of type ECLConfiguration which has all the configuration values needed to push the message</param>
		/// <returns>An ArrayList of Contacts in a group</returns>
		public virtual System.Collections.ArrayList getContactList(System.String groupName,ECLConfiguration type)
		{
			System.Collections.ArrayList contactList = new System.Collections.ArrayList();
			
			string grpName;
			DataTable dt;
							
			OleDbCommand objCmdSelect=null;
			if(type.ConnectionType.Equals("Excel"))
			{
				// Create new OleDbCommand to return data from worksheet.
				objCmdSelect =new OleDbCommand("SELECT * FROM ["+type.Sheet_Table_Name.ToString() + "$]", objConn);
			}
			else if(type.ConnectionType.Equals("Access") || type.ConnectionType.Equals("MSSQL"))
			{
				// Create new OleDbCommand to return data from table.
				objCmdSelect =new OleDbCommand("SELECT * FROM ["+type.Sheet_Table_Name.ToString() + "]", objConn);
			}
			// Create new OleDbDataAdapter that is used to build a DataSet
			// based on the preceding SQL SELECT statement.
			OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

			// Pass the Select command to the adapter.
			objAdapter1.SelectCommand = objCmdSelect;

			// Create new DataSet to hold information from the worksheet.
			DataSet resultSet = new DataSet();

			// Fill the DataSet with the information from the worksheet.
			objAdapter1.Fill(resultSet, type.Sheet_Table_Name.ToString());
			dt = resultSet.Tables[type.Sheet_Table_Name.ToString()];
			//look for the group name and compare
			//bool moreContacts = true;
			bool groupFound = false;
			int tempRow=0;
			//write table data
			for (int irow=0; irow<dt.Rows.Count; irow++)
			{
				//Extracting a row
				DataRow dr = dt.Rows[irow];
				string str = string.Empty;
				//parsing through all columns of the row
				for (int icol=0; icol<dt.Columns.Count; icol++)
				{
					//checking if first column is not blank
					if(dr[icol].ToString()!="")
					{
						//storing value of first column
						grpName=dr[icol].ToString();
						if(icol<=5)
						{
							//checking if second column is blank
							if(dr[icol+1].ToString()=="")
							{	
								//checking if group name for which we need contacts is the same as this group
								if (groupName.Equals(grpName))
								{
									groupFound = true;
									//setting the current row
									tempRow=irow;
									//setting icol to end of columns
									icol=dt.Columns.Count;
									//setting irow to end of rows			
									irow=dt.Rows.Count;
								}						
							}
							else
							{	
								//if second column is not blank the setting icol to end of columns
								icol=dt.Columns.Count;
							}								
						}
					}						
				}					
			}
											
			//After finding the group we parse to get the contact info
			if(groupFound)
			{
				//Start parsing from row next to the one in which we found the group name
				for (int irow=tempRow+1; irow<dt.Rows.Count; irow++)
				{
					//extracting a row
					DataRow dr = dt.Rows[irow];
					string str = string.Empty;
					//parsing through all columns
					for (int icol=0; icol<dt.Columns.Count; icol++)
					{
						//checking if first clumn is not blank
						if(dr[icol].ToString()!="")
						{
							//storing value of first column
							grpName=dr[icol].ToString();
							//checking if second column is not blank
							if(dr[icol+1].ToString()!="")
							{	
								//storing the value of first column to the contact list
								contactList.Add(grpName);
								//setting icol to end of columns
								icol=dt.Columns.Count;
							}
							else
							{
								//if second column is blank setting icol and irow to end values
								icol=dt.Columns.Count;
								irow=dt.Rows.Count;
							}	
						}
					}
				}
			}		
			contactList.TrimToSize(); //clean up the vector		
			return contactList;
		}
	
		/// <summary> Extracts the contact details of the named person.  The resulting string
		/// array contains an entry for each non-empty cell in the person's row,
		/// formatted according to the rules from the properties file.
		/// </summary>
		/// <param name="name">Contact Name for which to extract the details</param>
		/// <param name="type">This parameter is of type ECLConfiguration which has all the configuration values needed to push the message</param>
		/// <returns>Array of contact data</returns>
		public virtual System.String[] getContactData(System.String name,ECLConfiguration type)
		{			
			System.String columnData;
		
			DataTable dt;
			OleDbCommand objCmdSelect=null;
			if(type.ConnectionType.Equals("Excel"))
			{
				// Create new OleDbCommand to return data from worksheet.
				objCmdSelect =new OleDbCommand("SELECT * FROM ["+type.Sheet_Table_Name.ToString() + "$]", objConn);
			}
			else if(type.ConnectionType.Equals("Access") || type.ConnectionType.Equals("MSSQL"))
			{
				// Create new OleDbCommand to return data from table.
				objCmdSelect =new OleDbCommand("SELECT * FROM ["+type.Sheet_Table_Name.ToString() + "]", objConn);
			}
			// Create new OleDbDataAdapter that is used to build a DataSet
			// based on the preceding SQL SELECT statement.
			OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

			// Pass the Select command to the adapter.
			objAdapter1.SelectCommand = objCmdSelect;

			// Create new DataSet to hold information from the worksheet.
			DataSet resultSet = new DataSet();

			// Fill the DataSet with the information from the worksheet.
			objAdapter1.Fill(resultSet, type.Sheet_Table_Name.ToString());
			dt = resultSet.Tables[type.Sheet_Table_Name.ToString()];
            System.String[] contactData = new System.String[dt.Columns.Count];
			//write table data
			int icol=0;
			bool isFound = false;
			for (int irow=0; irow<dt.Rows.Count; irow++)
			{
				if(isFound == false)
				{
					//extracting a row
					DataRow dr = dt.Rows[irow];
					string str = string.Empty;
					//checking if column 1 is not blank and is equal to Contact name
					if((dr[icol].ToString()!="") && dr[icol].Equals(name))
					{
						isFound = true;

						//Storing the row in contact data
						for (int i=0; i<dt.Columns.Count; i++)
						{						
							columnData = dr[i].ToString();;						
							if (!dr.Equals(null))
							{
								int length=type.ColumnFormat[i].Length;
								int pos;
								String formatStr=null;
								if(type.Source=="GUI")
								{
									pos=type.ColumnFormat[i].IndexOf("=",0,length);
									formatStr = type.ColumnFormat[i].Substring(pos+2,length-(pos+2));
								}
								else
								{
									formatStr=type.ColumnFormat[i].ToString();
								}
								contactData[i] = formatStr.Replace("%0", columnData);
							}
						}						
					}
				}				
			}		
			return contactData;
		}		
	}
}
//end of DataReader