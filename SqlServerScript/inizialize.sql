USE [master]
GO
/****** Object:  Database [AG_Crm]    Script Date: 13/05/2021 11:11:28 ******/
CREATE DATABASE [AG_Crm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AG_Crm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\AG_Crm.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AG_Crm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\AG_Crm_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [AG_Crm] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AG_Crm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AG_Crm] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [AG_Crm] SET ANSI_NULLS ON 
GO
ALTER DATABASE [AG_Crm] SET ANSI_PADDING ON 
GO
ALTER DATABASE [AG_Crm] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [AG_Crm] SET ARITHABORT ON 
GO
ALTER DATABASE [AG_Crm] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AG_Crm] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AG_Crm] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AG_Crm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AG_Crm] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [AG_Crm] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [AG_Crm] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AG_Crm] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [AG_Crm] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AG_Crm] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AG_Crm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AG_Crm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AG_Crm] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AG_Crm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AG_Crm] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AG_Crm] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AG_Crm] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AG_Crm] SET RECOVERY FULL 
GO
ALTER DATABASE [AG_Crm] SET  MULTI_USER 
GO
ALTER DATABASE [AG_Crm] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [AG_Crm] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AG_Crm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AG_Crm] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AG_Crm] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AG_Crm', N'ON'
GO
ALTER DATABASE [AG_Crm] SET QUERY_STORE = OFF
GO
USE [AG_Crm]
GO
/****** Object:  Schema [Reference]    Script Date: 13/05/2021 11:11:29 ******/
CREATE SCHEMA [Reference]
GO
/****** Object:  Schema [Web]    Script Date: 13/05/2021 11:11:29 ******/
CREATE SCHEMA [Web]
GO
/****** Object:  Table [Web].[Contacts]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[Contacts](
	[Id] [uniqueidentifier] NOT NULL,
	[EmployeeId] [uniqueidentifier] NOT NULL,
	[AddressType] [varchar](50) NOT NULL,
	[Value] [varchar](50) NOT NULL,
	[IsRemoved] [bit] NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Web_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Web].[Employees]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[Employees](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[Surname] [varchar](80) NOT NULL,
	[ContactType] [varchar](50) NULL,
	[IsRemoved] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [Web].[GetCompanyContacts]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [Web].[GetCompanyContacts]
(	
	-- Add the parameters for the function here
	@CompanyId uniqueidentifier
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT [Contacts].[Id]
	  ,[Contacts].[EmployeeId]
      ,[Contacts].[AddressType]
      ,[Contacts].[Value]
      ,[Contacts].[IsRemoved]
      ,[Contacts].[CreationDate]
  FROM [AG_Crm].[Web].[Contacts]  
  INNER JOIN [AG_Crm].[Web].[Employees] ON [Web].[Contacts].[EmployeeId] = [Web].[Employees].[Id]

 
  WHERE [AG_Crm].[Web].[Employees].[CompanyId] = @CompanyId
)
GO
/****** Object:  Table [Web].[Addresses]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[Addresses](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[AddressType] [varchar](50) NOT NULL,
	[Value] [varchar](50) NOT NULL,
	[IsRemoved] [bit] NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Web_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [Web].[GetCompanyAddresses]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [Web].[GetCompanyAddresses]
(	
	-- Add the parameters for the function here
	@CompanyId uniqueidentifier
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	SELECT [Id]
      ,[AddressType]
      ,[Value]
      ,[IsRemoved]
      ,[CreationDate]
  FROM [AG_Crm].[Web].[Addresses] WHERE [AG_Crm].[Web].[Addresses].CompanyId = @CompanyId
)
GO
/****** Object:  Table [dbo].[InternalCommands]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternalCommands](
	[Id] [uniqueidentifier] NOT NULL,
	[EnqueueDate] [datetime2](7) NOT NULL,
	[Type] [varchar](255) NOT NULL,
	[Data] [varchar](max) NOT NULL,
	[ProcessedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_app_InternalCommands_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Reference].[ActivityType]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reference].[ActivityType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ActivityType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Activity_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Reference].[AddressType]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reference].[AddressType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CompanyAddressType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_CompanyAddressType_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Reference].[ContactAddressType]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reference].[ContactAddressType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ContactAddressType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_ContactAddressType_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Reference].[ContactType]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reference].[ContactType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ContactType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_ContactType_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Reference].[DimensionType]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reference].[DimensionType](
	[Id] [uniqueidentifier] NOT NULL,
	[ContractType] [varchar](80) NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[Fee] [money] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [Reference].[SectorType]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reference].[SectorType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SectorType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Sector_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Reference].[StateType]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reference].[StateType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_StateTypee_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_StateType_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Reference].[TeamLevelType]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reference].[TeamLevelType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TeamLevel_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_TeamLevel_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Reference].[TicketType]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Reference].[TicketType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TicketType_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Ticket_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Web].[Activities]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[Activities](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[SectorType] [varchar](50) NOT NULL,
	[ActivityType] [varchar](50) NOT NULL,
	[Value] [bit] NOT NULL,
	[IsRemoved] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Web].[Companies]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[Companies](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[Address] [varchar](80) NOT NULL,
	[Cap] [varchar](5) NULL,
	[City] [varchar](50) NULL,
	[Province] [varchar](2) NULL,
	[FiscalCode] [varchar](20) NULL,
	[Piva] [varchar](20) NULL,
	[ContractType] [varchar](20) NULL,
	[SubScriptionType] [varchar](20) NULL,
	[SubScriptionDate] [date] NULL,
	[isRemoved] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Web_Companies_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Web].[Dimensions]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[Dimensions](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[DimensionType] [varchar](80) NOT NULL,
	[Fee] [money] NOT NULL,
	[ExpireDate] [datetime] NULL,
	[IsRemoved] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Dimension] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Web].[EmployeesOverView]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[EmployeesOverView](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[ContractLevelType] [varchar](80) NOT NULL,
	[Employees] [smallint] NOT NULL,
	[IsRemoved] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Web_EmployeesOverView] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Web].[TeamLevel]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[TeamLevel](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[TeamLevelType] [varchar](50) NOT NULL,
	[Total] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TeamLevel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Web].[Ticket]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[Ticket](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[TiketType] [varchar](50) NOT NULL,
	[OpenDate] [datetime] NOT NULL,
	[CloseDate] [datetime] NULL,
	[StateType] [varchar](50) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Web].[TicketMessage]    Script Date: 13/05/2021 11:11:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Web].[TicketMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[TicketId] [uniqueidentifier] NOT NULL,
	[Text] [varchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_TicketMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [Web].[Activities] ADD  CONSTRAINT [DF__tmp_ms_xx__Creat__37703C52]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[Addresses] ADD  CONSTRAINT [DF__Address__Creatio__160F4887]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[Companies] ADD  CONSTRAINT [DF_Companies_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[Contacts] ADD  CONSTRAINT [DF_Contacts_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[Dimensions] ADD  CONSTRAINT [DF_Dimensions_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[Employees] ADD  CONSTRAINT [DF_Employees_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[EmployeesOverView] ADD  CONSTRAINT [DF_EmployeesOverView_CreationDate]  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[TeamLevel] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[Ticket] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[TicketMessage] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [Web].[Activities]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Company] FOREIGN KEY([CompanyId])
REFERENCES [Web].[Companies] ([Id])
GO
ALTER TABLE [Web].[Activities] CHECK CONSTRAINT [FK_Activity_Company]
GO
ALTER TABLE [Web].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Web_Addresses_Companies] FOREIGN KEY([CompanyId])
REFERENCES [Web].[Companies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Web].[Addresses] CHECK CONSTRAINT [FK_Web_Addresses_Companies]
GO
ALTER TABLE [Web].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [Web].[Employees] ([Id])
GO
ALTER TABLE [Web].[Contacts] CHECK CONSTRAINT [FK_Contacts_Employees]
GO
ALTER TABLE [Web].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Companies] FOREIGN KEY([CompanyId])
REFERENCES [Web].[Companies] ([Id])
GO
ALTER TABLE [Web].[Employees] CHECK CONSTRAINT [FK_Employees_Companies]
GO
ALTER TABLE [Web].[EmployeesOverView]  WITH CHECK ADD  CONSTRAINT [FK_EmployeesOverView_Companies] FOREIGN KEY([CompanyId])
REFERENCES [Web].[Companies] ([Id])
GO
ALTER TABLE [Web].[EmployeesOverView] CHECK CONSTRAINT [FK_EmployeesOverView_Companies]
GO
ALTER TABLE [Web].[TeamLevel]  WITH CHECK ADD  CONSTRAINT [FK_TeamLevel_Company] FOREIGN KEY([CompanyId])
REFERENCES [Web].[Companies] ([Id])
GO
ALTER TABLE [Web].[TeamLevel] CHECK CONSTRAINT [FK_TeamLevel_Company]
GO
ALTER TABLE [Web].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Company] FOREIGN KEY([CompanyId])
REFERENCES [Web].[Companies] ([Id])
GO
ALTER TABLE [Web].[Ticket] CHECK CONSTRAINT [FK_Ticket_Company]
GO
ALTER TABLE [Web].[TicketMessage]  WITH CHECK ADD  CONSTRAINT [FK_TicketMessage_Ticket] FOREIGN KEY([TicketId])
REFERENCES [Web].[Ticket] ([Id])
GO
ALTER TABLE [Web].[TicketMessage] CHECK CONSTRAINT [FK_TicketMessage_Ticket]
GO
USE [master]
GO
ALTER DATABASE [AG_Crm] SET  READ_WRITE 
GO
