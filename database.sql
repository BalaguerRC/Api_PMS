USE [master]
GO
/****** Object:  Database [PMS]    Script Date: 25/09/2023 11:06:02 ******/
CREATE DATABASE [PMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PMS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PMS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [PMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PMS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PMS] SET RECOVERY FULL 
GO
ALTER DATABASE [PMS] SET  MULTI_USER 
GO
ALTER DATABASE [PMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PMS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PMS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PMS', N'ON'
GO
ALTER DATABASE [PMS] SET QUERY_STORE = ON
GO
ALTER DATABASE [PMS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PMS]
GO
/****** Object:  Table [dbo].[MedicalAppointments]    Script Date: 25/09/2023 11:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalAppointments](
	[Id_MA] [int] IDENTITY(1,1) NOT NULL,
	[Id_Patient] [int] NULL,
	[Id_Doctors] [int] NULL,
	[Date_MA] [datetime] NULL,
	[Cause_MA] [text] NULL,
	[State_MA] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_MA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwDashboard]    Script Date: 25/09/2023 11:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create view [dbo].[vwDashboard] AS
  SELECT
	(select COUNT(State_MA) from dbo.[MedicalAppointments] where State_MA=0) Pending_Consultation,
	(select COUNT(State_MA) from dbo.[MedicalAppointments] where State_MA=1) Pending_Results,
	(select COUNT(State_MA) from dbo.[MedicalAppointments] where State_MA=2) Results;
GO
/****** Object:  Table [dbo].[Users]    Script Date: 25/09/2023 11:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id_User] [int] IDENTITY(1,1) NOT NULL,
	[Name_User] [nvarchar](50) NULL,
	[LastName_User] [nvarchar](100) NULL,
	[Email_User] [nvarchar](100) NULL,
	[UserName] [nvarchar](100) NULL,
	[Password_User] [nvarchar](max) NULL,
	[Date_User] [datetime] NULL,
	[type_User] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 25/09/2023 11:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id_Doctors] [int] IDENTITY(1,1) NOT NULL,
	[Name_Doctor] [nvarchar](50) NULL,
	[LastName_Doctor] [nvarchar](100) NULL,
	[Email_Doctor] [nvarchar](100) NULL,
	[Phone_Doctor] [nvarchar](100) NULL,
	[Identity__Doctor] [nvarchar](100) NULL,
	[Img_Doctor] [text] NULL,
	[Date_Doctor] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Doctors] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LabTest]    Script Date: 25/09/2023 11:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LabTest](
	[Id_LabTest] [int] IDENTITY(1,1) NOT NULL,
	[Name_LabTest] [nvarchar](100) NULL,
	[Date_LabTest] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_LabTest] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwDashboardAdmin]    Script Date: 25/09/2023 11:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create view [dbo].[vwDashboardAdmin] AS
  SELECT
    (select count(*) from Users) Users,
    (select count(*) from Doctors) Doctors,
    (select count(*) from LabTest) LabTest;
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 25/09/2023 11:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id_Patient] [int] IDENTITY(1,1) NOT NULL,
	[Name_Patient] [nvarchar](50) NULL,
	[LastName_Patient] [nvarchar](100) NULL,
	[Phone_Patient] [nvarchar](100) NULL,
	[Address_Patient] [nvarchar](100) NULL,
	[Identity_Patient] [nvarchar](100) NULL,
	[Birthdate_Patient] [datetime] NULL,
	[Smoker_Patient] [int] NULL,
	[Allergies_Patient] [text] NULL,
	[Img_Patient] [text] NULL,
	[Date_Patient] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Patient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LabTestResult]    Script Date: 25/09/2023 11:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LabTestResult](
	[Id_LabTestResult] [int] IDENTITY(1,1) NOT NULL,
	[Id_Patient] [int] NULL,
	[Id_MedicalAppointment] [int] NULL,
	[Id_LabTest] [int] NULL,
	[Id_Doctor] [int] NULL,
	[Test_Result] [nvarchar](max) NULL,
	[State_Result] [int] NULL,
	[Date_TestResult] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_LabTestResult] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwDashboardDoctor]    Script Date: 25/09/2023 11:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create view [dbo].[vwDashboardDoctor] AS
  SELECT
    (select count(*) from Patients) Patients,
    (select count(*) from MedicalAppointments) MedicalAppointments,
    (select count(*) from LabTestResult) LabTestResult;
GO
SET IDENTITY_INSERT [dbo].[Doctors] ON 

INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (1, N'Eduardo', N'Martines', N'e@exa.com', N'000-000-0000', N'000-00000-0', N'test', CAST(N'2023-06-19T10:06:19.450' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (2, N'Enriquillo', N'Perez', N'er@exa.com', N'1809-000-0002', N'000-000000-2', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-07-18T10:39:18.100' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (4, N'Albert', N'Perez', N'ep@exa.com', N'1809000000', N'100-0000000-1', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-07-19T10:35:41.363' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (5, N'Josefina', N'Perez de la Cruz', N'josefina@exa.com', N'+18091111111', N'000-000010-0', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-08-21T12:26:31.637' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (7, N'Mark', N'Zuckerberg', N'm@exa.com', N'111-111-4444', N'd-000000-00', N'https://i.imgur.com/P7C19kA.jpg', CAST(N'2023-09-11T09:55:33.047' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (9, N'Larry', N'Castro', N'l@exa.comn', N'1809-000-1235', N'123-000000-1', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-09-14T10:23:23.220' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (10, N'Adolf', N'Hittler', N'ah@exa.com', N'1809000000', N'111-000001-1', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-09-15T10:48:14.777' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (1010, N'Alex', N'Monrou', N'a@exa.com', N'1809000000', N'0-0000000-0', N'https://i.imgur.com/IirJ2VN.jpg', CAST(N'2023-09-21T10:03:55.243' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (1011, N'William Bradley.', N'Pitt.', N'wp@exa.com', N'809-000-0001', N'000-000000-1', N'https://i.imgur.com/sUtkoMk.jpg', CAST(N'2023-09-25T10:51:28.247' AS DateTime))
SET IDENTITY_INSERT [dbo].[Doctors] OFF
GO
SET IDENTITY_INSERT [dbo].[LabTest] ON 

INSERT [dbo].[LabTest] ([Id_LabTest], [Name_LabTest], [Date_LabTest]) VALUES (1, N'diagnostico 2', CAST(N'2023-06-19T10:34:21.703' AS DateTime))
INSERT [dbo].[LabTest] ([Id_LabTest], [Name_LabTest], [Date_LabTest]) VALUES (2, N'Cirugía', CAST(N'2023-07-19T10:57:16.357' AS DateTime))
INSERT [dbo].[LabTest] ([Id_LabTest], [Name_LabTest], [Date_LabTest]) VALUES (5, N'Hemograma Completo', CAST(N'2023-08-22T11:12:31.950' AS DateTime))
INSERT [dbo].[LabTest] ([Id_LabTest], [Name_LabTest], [Date_LabTest]) VALUES (6, N'Creatinina', CAST(N'2023-08-22T11:17:46.810' AS DateTime))
INSERT [dbo].[LabTest] ([Id_LabTest], [Name_LabTest], [Date_LabTest]) VALUES (8, N'Diagnóstica Ocular', CAST(N'2023-08-29T11:24:52.770' AS DateTime))
INSERT [dbo].[LabTest] ([Id_LabTest], [Name_LabTest], [Date_LabTest]) VALUES (9, N'Test 1', CAST(N'2023-08-31T09:33:42.013' AS DateTime))
INSERT [dbo].[LabTest] ([Id_LabTest], [Name_LabTest], [Date_LabTest]) VALUES (10, N'Urianálisis completo', CAST(N'2023-09-11T09:59:19.260' AS DateTime))
SET IDENTITY_INSERT [dbo].[LabTest] OFF
GO
SET IDENTITY_INSERT [dbo].[LabTestResult] ON 

INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (1, 1, 1, 1, 1, N'test', 1, CAST(N'2023-08-08T11:19:59.673' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (2, 2, 2, 1, 2, N'test', 1, CAST(N'2023-08-10T11:31:45.447' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (3, 1, 4, 2, 1, N'test', 1, CAST(N'2023-08-11T11:12:57.583' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (4, 5, 5, 5, 5, N'test', 1, CAST(N'2023-08-28T10:54:22.080' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (5, 2, 2, 6, 2, N'test', 1, CAST(N'2023-08-29T11:10:38.230' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (9, 4, 3, 8, 4, N'test', 1, CAST(N'2023-08-31T07:46:18.940' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (10, 2, 10, 9, 4, N'test', 1, CAST(N'2023-09-08T10:17:31.223' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (11, 2, 2, 9, 2, N'test', 1, CAST(N'2023-09-08T10:20:42.590' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (12, 9, 11, 10, 7, N'test', 1, CAST(N'2023-09-11T10:11:18.690' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (13, 9, 12, 5, 7, N'test', 1, CAST(N'2023-09-13T17:10:49.740' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (14, 12, 13, 9, 10, N'test', 1, CAST(N'2023-09-18T10:37:02.143' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (15, 13, 14, 9, 1010, N'test', 1, CAST(N'2023-09-21T10:18:28.773' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (16, 13, 15, 8, 7, N'test', 0, CAST(N'2023-09-21T10:20:36.777' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (17, 14, 16, 9, 5, N'test', 1, CAST(N'2023-09-25T11:01:32.783' AS DateTime))
SET IDENTITY_INSERT [dbo].[LabTestResult] OFF
GO
SET IDENTITY_INSERT [dbo].[MedicalAppointments] ON 

INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (1, 1, 1, CAST(N'2023-06-20T10:31:27.930' AS DateTime), N'Revisar que tiene', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (2, 2, 2, CAST(N'2023-08-07T10:20:00.000' AS DateTime), N'diagnostico', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (3, 4, 4, CAST(N'2023-08-17T12:07:00.000' AS DateTime), N'Chequeo de ojos', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (4, 1, 1, CAST(N'2023-08-12T11:12:00.000' AS DateTime), N'sangre', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (5, 5, 5, CAST(N'2023-08-30T10:38:00.000' AS DateTime), N'Chequeo de sangre', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (6, 2, 2, CAST(N'2023-08-31T11:09:00.000' AS DateTime), N'prueba medica', 1)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (9, 7, 5, CAST(N'2023-09-04T13:06:00.000' AS DateTime), N'Chequeo Urgente', 0)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (10, 2, 4, CAST(N'2023-09-08T10:16:00.000' AS DateTime), N'pending results', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (11, 9, 7, CAST(N'2023-09-11T13:13:00.000' AS DateTime), N'Revisar si tiene diabetes', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (12, 9, 7, CAST(N'2023-09-13T17:09:00.000' AS DateTime), N'test', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (13, 12, 10, CAST(N'2023-09-22T10:35:00.000' AS DateTime), N'Prueba Medica', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (14, 13, 1010, CAST(N'2023-09-21T10:16:00.000' AS DateTime), N'Revisar que tiene', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (15, 13, 7, CAST(N'2023-09-23T10:17:00.000' AS DateTime), N'Revisar ojos', 1)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (16, 14, 5, CAST(N'2023-09-26T11:01:00.000' AS DateTime), N'Test', 2)
SET IDENTITY_INSERT [dbo].[MedicalAppointments] OFF
GO
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (1, N'Alejandro2', N'Magno2', N'809-000-00002', N'santo domingo 2', N'000-0000000-2', CAST(N'2023-07-19T00:00:00.000' AS DateTime), 0, N'arroz', N'test', CAST(N'2023-06-19T11:03:59.060' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (2, N'Albert', N'Perez', N'1809000000', N'santo domingo Norte', N'100-1000000-2', CAST(N'2023-01-12T00:00:00.000' AS DateTime), 1, N'tomate', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-07-24T10:23:18.827' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (4, N'Rafael', N'Leonidas Trujillo', N'1-809-000-0000', N'Seibo 2', N'000-0000000-1', CAST(N'2006-02-17T00:00:00.000' AS DateTime), 1, N'Frito', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-08-10T12:06:09.633' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (5, N'Gonzalo', N'Castillo', N'+18090000002', N'ninguna', N'1000-000', CAST(N'2000-02-24T00:00:00.000' AS DateTime), 0, N'ninguna', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-08-24T12:12:40.107' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (7, N'Bryan', N'Lopez', N'809-123-4567', N'Distrito Nacional', N'123-000000-5', CAST(N'1999-05-28T00:00:00.000' AS DateTime), 0, N'Ninguno', N'https://cdn.discordapp.com/attachments/649710964220100638/1144307499051270255/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-09-04T12:54:56.940' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (8, N'Vin', N'Diesel', N'1-500-000-0000', N'Condado de Alameda, California', N'000-000000-1', CAST(N'1967-07-18T00:00:00.000' AS DateTime), 0, N'Ninguna', N'https://cdn.discordapp.com/attachments/649710964220100638/1144307499051270255/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-09-07T11:06:16.173' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (9, N'pamela', N'castillo', N'809-000-1234', N'ninguna', N'000-00000-0', CAST(N'1991-06-22T00:00:00.000' AS DateTime), 1, N'Manzanas', N'https://cdn.discordapp.com/attachments/649710964220100638/1144307499051270255/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-09-11T10:07:41.637' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (12, N'Josep', N'Kurry', N'1809000000', N'Ninguna', N'100-00000-01', CAST(N'1995-09-13T00:00:00.000' AS DateTime), 0, N'Fresas y Limones', N'https://i.imgur.com/AhgNPsg.png', CAST(N'2023-09-18T10:13:43.890' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (13, N'Francisco', N'Ramirez', N'1-809-111-2222', N'Ninguna', N'1230-00000', CAST(N'2000-08-16T00:00:00.000' AS DateTime), 1, N'Papel', N'https://i.imgur.com/WMxhUDf.jpg', CAST(N'2023-09-21T09:53:02.753' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (14, N'William Bradley', N'Pitt', N'809-000-0000', N'Ninguna', N'000-000000-0', CAST(N'1963-12-18T00:00:00.000' AS DateTime), 0, N'Ninguna', N'https://i.imgur.com/mIIjUTS.jpg', CAST(N'2023-09-25T10:57:55.913' AS DateTime))
SET IDENTITY_INSERT [dbo].[Patients] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (1, N'Eduard3', N'Martínez3', N'e3@exa.com', N'eduardo123', N'yu+qB8RLA0/qv0pdEPhfdS2zgDJt5aL8k3Kyr7fJVik=', CAST(N'2023-06-16T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (3, N'Albert', N'Perez', N'a@exa.com', N'admin', N'NgKlzXP4zQ+eLvybJ8aq7g==', CAST(N'2023-06-20T11:12:54.043' AS DateTime), 1)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (4, N'danilo', N'medina', N'd@exa.com', N'danilo', N'ewndXcvwPXJhGdLPrnaGGQ==', CAST(N'2023-06-20T11:39:57.570' AS DateTime), 1)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (5, N'Leonidas', N'Trujillo', N't@exa.com', N'trujillo', N'MI2I525KmOgkWGzRs+sDHh9hkC4gGr8gRwWIN8BV1qw=', CAST(N'2023-06-26T10:24:47.907' AS DateTime), 2)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (7, N'Maribel2', N'Reyes', N'm@exa.com', N'mariReyes', N'BboAZhFkGWQTmWBARecFPg==', CAST(N'2023-06-28T11:20:37.837' AS DateTime), 2)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (9, N'Joaquin', N'Balaguer', N'j@exa.com', N'bala', N'jbc3p/YxrqdQTkx5yT/AGg==', CAST(N'2023-08-18T12:15:29.453' AS DateTime), 1)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (10, N'Marcela', N'Posada', N'm@exa.com', N'marcelaP', N'iYkbfwoAg4sA1E3dznuoBzEieEHi/MDMhqMGlkgA5To=', CAST(N'2023-08-21T11:15:07.533' AS DateTime), 2)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (13, N'Administrador', N'General', N'a@exa.com', N'admin123', N'UWTLNwJTEyCn/NpLodVijl8E6XO0cM9scxQF5StKrFI=', CAST(N'2023-09-11T09:51:59.047' AS DateTime), 1)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (15, N'Albert', N'Perez', N'ap@exa.com', N'test1', N'lNEMMjiRJuWUqyz7vesJeg==', CAST(N'2023-09-13T17:04:33.733' AS DateTime), 2)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (16, N'Adolf', N'Hittler', N'a@exa.com', N'bigotemisterioso', N'bddEKCidC8O208sQc2CVsCAmn5lL0lS57vZNS2q8w9mM/ErejG0EbAyNHm8Jvwpx', CAST(N'2023-09-15T10:25:55.690' AS DateTime), 1)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (1016, N'William Bradley', N'Pitt', N'wp@exa.com', N'BradPitt', N'jbc3p/YxrqdQTkx5yT/AGg==', CAST(N'2023-09-25T10:30:03.637' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
USE [master]
GO
ALTER DATABASE [PMS] SET  READ_WRITE 
GO
