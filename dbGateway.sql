USE master
go

if exists(select * from sys.databases where name = 'dbGateway')
drop database dbGateway
go

create database dbGateway
go

USE [dbGateway]
GO
/****** Object:  Table [dbo].[tbSMS]    Script Date: 10.06.2021 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbSMS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Msg] [nvarchar](max) NOT NULL,
	[Phone] [int] NOT NULL,
	[Sent] [nvarchar](1) NOT NULL
	)

GO
/****** Object:  Table [dbo].[tbUser]    Script Date: 10.06.2021 20:48:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DeviceInfo] [nvarchar](max) NOT NULL
	)
go