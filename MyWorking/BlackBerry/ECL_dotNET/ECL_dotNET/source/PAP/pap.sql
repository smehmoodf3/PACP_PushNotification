/****** Object:  Table [dbo].[PAP]    Script Date: 11/21/2005 10:05:18 AM ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PAP]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[PAP]
GO

/****** Object:  Table [dbo].[PAP]    Script Date: 11/21/2005 10:05:20 AM ******/
CREATE TABLE [dbo].[PAP] (
	[PushID] [numeric](18, 0) NOT NULL ,
	[MDSState] [char] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DeviceAddress] [nchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DeviceState] [numeric](18, 0) NULL ,
	[Delivered] [char] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

