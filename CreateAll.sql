IF NOT EXISTS (SELECT * FROM sysdatabases WHERE name='HumanResources') 
BEGIN 
	CREATE DATABASE [HumanResources] 
END
GO

IF NOT EXISTS (SELECT * FROM sysdatabases WHERE name='Sales') 
BEGIN 
	CREATE DATABASE [Sales] 
END
GO

USE [HumanResources]
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
		[HolidayEntitlement] int NULL
		CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Employee] ([Id], [Forename], [Surname], [Joined], [Left], [HolidayEntitlement]) VALUES ('54b26de9-2dae-4168-a66c-281b6f03f1b5', 'Barry', 'Blue', '2012-01-01 00:00:00', NULL, 21)
	INSERT INTO [Employee] ([Id], [Forename], [Surname], [Joined], [Left], [HolidayEntitlement]) VALUES ('4f738440-258f-4539-8ac0-387836815361', 'Rachel', 'Red', '2012-03-01 00:00:00', NULL, 23)
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'TimeAllocation')
BEGIN
	CREATE TABLE [dbo].[TimeAllocation](
		[Id] uniqueidentifier NOT NULL,
		[EmployeeId] uniqueidentifier NOT NULL,
		[Start] datetime  NOT NULL,
		[End] datetime NOT NULL
		CONSTRAINT [PK_TimeAllocation] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [TimeAllocation] ([Id], [EmployeeId], [Start], [End]) VALUES ('086838fc-76c0-4bf7-afd7-9b0d53372d7b', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-13 09:00:00', '2012-08-17 09:00:00')
	INSERT INTO [TimeAllocation] ([Id], [EmployeeId], [Start], [End]) VALUES ('c81a69b9-40be-4553-abbf-e334b64e5f8a', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-07 10:00:00', '2012-08-07 11:00:00')
	INSERT INTO [TimeAllocation] ([Id], [EmployeeId], [Start], [End]) VALUES ('f346bcc5-b2d1-4b4e-9359-f810d1880fcb', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-23 14:00:00', '2012-08-23 17:00:00')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Holiday')
BEGIN
	CREATE TABLE [dbo].[Holiday](
		[Id] uniqueidentifier NOT NULL,
		[Approved] bit NOT NULL
		CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Holiday] ([Id], [Approved]) VALUES ('086838fc-76c0-4bf7-afd7-9b0d53372d7b', 1)
END
GO

USE [Sales]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Consultant')
BEGIN
	CREATE TABLE [dbo].[Consultant](
		[Id] uniqueidentifier NOT NULL,
		[Forename] nvarchar(100) NULL,
		[Surname] nvarchar(100) NULL
		CONSTRAINT [PK_Consultant] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Consultant] ([Id], [Forename], [Surname]) VALUES ('54b26de9-2dae-4168-a66c-281b6f03f1b5', 'Barry', 'Blue')
	INSERT INTO [Consultant] ([Id], [Forename], [Surname]) VALUES ('4f738440-258f-4539-8ac0-387836815361', 'Rachel', 'Red')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'TimeAllocation')
BEGIN
	CREATE TABLE [dbo].[TimeAllocation](
		[Id] uniqueidentifier NOT NULL,
		[ConsultantId] uniqueidentifier NOT NULL,
		[Start] datetime  NOT NULL,
		[End] datetime NOT NULL
		CONSTRAINT [PK_TimeAllocation] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [TimeAllocation] ([Id], [ConsultantId], [Start], [End]) VALUES ('086838fc-76c0-4bf7-afd7-9b0d53372d7b', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-13 09:00:00', '2012-08-17 09:00:00')
	INSERT INTO [TimeAllocation] ([Id], [ConsultantId], [Start], [End]) VALUES ('c81a69b9-40be-4553-abbf-e334b64e5f8a', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-07 10:00:00', '2012-08-07 11:00:00')
	INSERT INTO [TimeAllocation] ([Id], [ConsultantId], [Start], [End]) VALUES ('f346bcc5-b2d1-4b4e-9359-f810d1880fcb', '54b26de9-2dae-4168-a66c-281b6f03f1b5', '2012-08-23 14:00:00', '2012-08-23 17:00:00')
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'Appointment')
BEGIN
	CREATE TABLE [dbo].[Appointment](
		[Id] uniqueidentifier NOT NULL,
		[LeadName] nvarchar(100) NULL,
		[Address] nvarchar(100) NULL
		CONSTRAINT [PK_Holiday] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	INSERT INTO [Appointment] ([Id], [LeadName], [Address]) VALUES ('c81a69b9-40be-4553-abbf-e334b64e5f8a', 'The Orange Company', '6 Orange Road, Orangeborough')
	INSERT INTO [Appointment] ([Id], [LeadName], [Address]) VALUES ('f346bcc5-b2d1-4b4e-9359-f810d1880fcb', 'Purple Inc.', '1 Purple Street, Purpleton')
END
GO