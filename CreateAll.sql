USE [Master]

IF EXISTS (SELECT * FROM sysdatabases WHERE name='HumanResources') 
BEGIN 
	DROP DATABASE [HumanResources] 
END
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE name='Sales') 
BEGIN 
	DROP DATABASE [Sales] 
END
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE name='Calendar') 
BEGIN 
	DROP DATABASE [Calendar] 
END
GO

CREATE DATABASE [HumanResources] 
GO

CREATE DATABASE [Sales] 
GO

CREATE DATABASE [Calendar] 
GO

CREATE DATABASE [MasterData] 
GO

USE [HumanResources]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Department')
BEGIN
	CREATE TABLE [dbo].[Department](
		[Id] uniqueidentifier NOT NULL,
		[Name] nvarchar(100) NULL
		CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Department] ([Id], [Name]) VALUES ('30efbfda-f3b5-42fb-906e-098fb32be79d', 'Sales')
	INSERT INTO [Department] ([Id], [Name]) VALUES ('f7367187-406d-47db-931e-b9e4fa8a4774', 'Human Resources')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Employee')
BEGIN
	CREATE TABLE [dbo].[Employee](
		[Id] uniqueidentifier NOT NULL,
		[Forename] nvarchar(100) NULL,
		[Surname] nvarchar(100) NULL,
		[Joined] datetime NULL,
		[Left] datetime NULL,
		[HolidayEntitlement] int NULL,
		[DepartmentId] uniqueidentifier NULL
		CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Employee] ([Id], [Forename], [Surname], [Joined], [Left], [HolidayEntitlement], [DepartmentId]) VALUES ('54b26de9-2dae-4168-a66c-281b6f03f1b5', 'Barry', 'Blue', '2012-01-01 00:00:00', NULL, 21, '30efbfda-f3b5-42fb-906e-098fb32be79d')
	INSERT INTO [Employee] ([Id], [Forename], [Surname], [Joined], [Left], [HolidayEntitlement], [DepartmentId]) VALUES ('4f738440-258f-4539-8ac0-387836815361', 'Rachel', 'Red', '2012-03-01 00:00:00', NULL, 23, 'f7367187-406d-47db-931e-b9e4fa8a4774')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Holiday')
BEGIN
	CREATE TABLE [dbo].[Holiday](
		[Id] uniqueidentifier NOT NULL,
		[EmployeeId] uniqueidentifier NULL,
		[Approved] bit NOT NULL,
		[Start] datetime  NOT NULL,
		[End] datetime NOT NULL,
		[Invalidated] bit NOT NULL,
		[InvalidatedMessage] nvarchar(500) NULL
		CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Holiday] ([Id], [EmployeeId], [Approved], [Start], [End], [Invalidated], [InvalidatedMessage]) VALUES ('086838fc-76c0-4bf7-afd7-9b0d53372d7b', '54b26de9-2dae-4168-a66c-281b6f03f1b5', 1, '2012-08-13 09:00:00', '2012-08-17 17:00:00', 0, NULL)
END
GO

USE [Sales]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Appointment')
BEGIN
	CREATE TABLE [dbo].[Appointment](
		[Id] uniqueidentifier NOT NULL,
		[ConsultantId] uniqueidentifier NULL,
		[Start] datetime  NOT NULL,
		[End] datetime NOT NULL,
		[LeadName] nvarchar(100) NULL,
		[Address] nvarchar(100) NULL,
		[Invalidated] bit NOT NULL,
		[InvalidatedMessage] nvarchar(500) NULL
		CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Appointment] ([Id], [ConsultantId], [Start], [End], [LeadName], [Address], [Invalidated], [InvalidatedMessage]) VALUES ('c81a69b9-40be-4553-abbf-e334b64e5f8a', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-07 10:00:00', '2012-08-07 11:00:00', 'The Orange Company', '6 Orange Road, Orangeborough', 0, NULL)
	INSERT INTO [Appointment] ([Id], [ConsultantId], [Start], [End], [LeadName], [Address], [Invalidated], [InvalidatedMessage]) VALUES ('f346bcc5-b2d1-4b4e-9359-f810d1880fcb', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-07 10:00:00', '2012-08-07 11:00:00', 'Purple Inc.', '1 Purple Street, Purpleton', 0, NULL)
END
GO

USE [Calendar]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'BookingType')
BEGIN
	CREATE TABLE [dbo].[BookingType](
		[Id] uniqueidentifier NOT NULL,
		[Description] nvarchar(100) NOT NULL
		CONSTRAINT [PK_BookingType] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [BookingType] ([Id], [Description]) VALUES ('851ec921-fe34-47f5-8060-2a592b35266d', 'Holiday')
	INSERT INTO [BookingType] ([Id], [Description]) VALUES ('31b6a4a7-839b-4a29-b762-b671be05ffbd', 'SalesAppointment')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Booking')
BEGIN
	CREATE TABLE [dbo].[Booking](
		[Id] uniqueidentifier NOT NULL,
		[EmployeeId] uniqueidentifier NOT NULL,
		[Start] datetime  NOT NULL,
		[End] datetime NOT NULL,
		[BookingTypeId] uniqueidentifier NOT NULL
		CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Booking] ([Id], [EmployeeId], [Start], [End], [BookingTypeId]) VALUES ('086838fc-76c0-4bf7-afd7-9b0d53372d7b', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-13 09:00:00', '2012-08-17 17:00:00', '851ec921-fe34-47f5-8060-2a592b35266d')
	INSERT INTO [Booking] ([Id], [EmployeeId], [Start], [End], [BookingTypeId]) VALUES ('c81a69b9-40be-4553-abbf-e334b64e5f8a', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-07 10:00:00', '2012-08-07 11:00:00', '31b6a4a7-839b-4a29-b762-b671be05ffbd')
	INSERT INTO [Booking] ([Id], [EmployeeId], [Start], [End], [BookingTypeId]) VALUES ('f346bcc5-b2d1-4b4e-9359-f810d1880fcb', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-23 14:00:00', '2012-08-23 17:00:00', '31b6a4a7-839b-4a29-b762-b671be05ffbd')
END
GO
