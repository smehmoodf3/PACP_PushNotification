
SELECT TOP 100 PERCENT t1.Id, t1.MachineName as BESName, t2.WebServerListenPort as Port, t3.MailboxSMTPAddr as Recipient
FROM ServerConfig t1 
INNER JOIN MDSConfig t2 ON t1.Id = t2.ServerConfigId 
INNER JOIN UserConfig t3 ON t1.Id = t3.ServerConfigId 


SELECT TOP 100 PERCENT t3.DisplayName as Recipient, t3.MailboxSMTPAddr as Email, t4.PhoneNumber as Phone, t3.Pin as Pin
FROM ServerConfig t1 
INNER JOIN MDSConfig t2 ON t1.Id = t2.ServerConfigId 
INNER JOIN UserConfig t3 ON t1.Id = t3.ServerConfigId 
INNER JOIN SyncDeviceMgmtSummary t4 ON t3.Id = t4.UserConfigId
