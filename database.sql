USE [master]
GO
/****** Object:  Database [PMS]    Script Date: 11/08/2023 11:21:37 ******/
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
/****** Object:  Table [dbo].[Doctors]    Script Date: 11/08/2023 11:21:37 ******/
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
/****** Object:  Table [dbo].[LabTest]    Script Date: 11/08/2023 11:21:37 ******/
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
/****** Object:  Table [dbo].[LabTestResult]    Script Date: 11/08/2023 11:21:37 ******/
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
/****** Object:  Table [dbo].[MedicalAppointments]    Script Date: 11/08/2023 11:21:37 ******/
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
/****** Object:  Table [dbo].[Patients]    Script Date: 11/08/2023 11:21:37 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 11/08/2023 11:21:37 ******/
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
SET IDENTITY_INSERT [dbo].[Doctors] ON 

INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (1, N'Eduardo', N'martines', N'e@exa.com', N'000-000-0000', N'000-00000-0', N'test', CAST(N'2023-06-19T10:06:19.450' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (2, N'Enriquillo2', N'Perez2', N'e@exa.com2', N'18090000002', N'000-000000-2', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-07-18T10:39:18.100' AS DateTime))
INSERT [dbo].[Doctors] ([Id_Doctors], [Name_Doctor], [LastName_Doctor], [Email_Doctor], [Phone_Doctor], [Identity__Doctor], [Img_Doctor], [Date_Doctor]) VALUES (4, N'Albert', N'Perez', N'ep@exa.com', N'1809000000', N'100-0000000-1', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-07-19T10:35:41.363' AS DateTime))
SET IDENTITY_INSERT [dbo].[Doctors] OFF
GO
SET IDENTITY_INSERT [dbo].[LabTest] ON 

INSERT [dbo].[LabTest] ([Id_LabTest], [Name_LabTest], [Date_LabTest]) VALUES (1, N'diagnostico 2', CAST(N'2023-06-19T10:34:21.703' AS DateTime))
INSERT [dbo].[LabTest] ([Id_LabTest], [Name_LabTest], [Date_LabTest]) VALUES (2, N'Cirugía', CAST(N'2023-07-19T10:57:16.357' AS DateTime))
SET IDENTITY_INSERT [dbo].[LabTest] OFF
GO
SET IDENTITY_INSERT [dbo].[LabTestResult] ON 

INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (1, 1, 1, 1, 1, N'test', 1, CAST(N'2023-08-08T11:19:59.673' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (2, 2, 2, 1, 2, N'test', 0, CAST(N'2023-08-10T11:31:45.447' AS DateTime))
INSERT [dbo].[LabTestResult] ([Id_LabTestResult], [Id_Patient], [Id_MedicalAppointment], [Id_LabTest], [Id_Doctor], [Test_Result], [State_Result], [Date_TestResult]) VALUES (3, 1, 4, 2, 1, N'test', 1, CAST(N'2023-08-11T11:12:57.583' AS DateTime))
SET IDENTITY_INSERT [dbo].[LabTestResult] OFF
GO
SET IDENTITY_INSERT [dbo].[MedicalAppointments] ON 

INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (1, 1, 1, CAST(N'2023-06-20T10:31:27.930' AS DateTime), N'Revisar que tiene', 2)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (2, 2, 2, CAST(N'2023-08-07T10:20:00.000' AS DateTime), N'diagnostico', 1)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (3, 4, 4, CAST(N'2023-08-17T12:07:00.000' AS DateTime), N'Chequeo de ojos', 0)
INSERT [dbo].[MedicalAppointments] ([Id_MA], [Id_Patient], [Id_Doctors], [Date_MA], [Cause_MA], [State_MA]) VALUES (4, 1, 1, CAST(N'2023-08-12T11:12:00.000' AS DateTime), N'sangre', 2)
SET IDENTITY_INSERT [dbo].[MedicalAppointments] OFF
GO
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (1, N'Alejandro2', N'Magno2', N'809-000-00002', N'santo domingo 2', N'000-0000000-2', CAST(N'2023-07-19T00:00:00.000' AS DateTime), 0, N'arroz', N'test', CAST(N'2023-06-19T11:03:59.060' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (2, N'Albert', N'Perez', N'1809000000', N'santo domingo Norte', N'100-1000000-2', CAST(N'2023-01-12T00:00:00.000' AS DateTime), 1, N'tomate', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-07-24T10:23:18.827' AS DateTime))
INSERT [dbo].[Patients] ([Id_Patient], [Name_Patient], [LastName_Patient], [Phone_Patient], [Address_Patient], [Identity_Patient], [Birthdate_Patient], [Smoker_Patient], [Allergies_Patient], [Img_Patient], [Date_Patient]) VALUES (4, N'Leonidas', N'Trujillo', N'+18091111111', N'Seibo', N'000-00014222-3', CAST(N'2006-02-10T00:00:00.000' AS DateTime), 0, N'guayaba', N'https://assets.stickpng.com/images/5a4613ddd099a2ad03f9c994.png', CAST(N'2023-08-10T12:06:09.633' AS DateTime))
SET IDENTITY_INSERT [dbo].[Patients] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (1, N'eduardo1', N'martines1', N'e1@exa.com', N'eduardo12', N'80Yy4oy6wPCBZmlwSCGtaQ==', CAST(N'2023-06-16T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (2, N'Jose', N'Ortega', N'J@exa.com', N'joseOrt', N'jose123', CAST(N'2023-06-16T11:27:10.110' AS DateTime), 2)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (3, N'Albert', N'Perez', N'a@exa.com', N'admin', N'dYvU0nyvIOT/eMr6NIqAPQ==', CAST(N'2023-06-20T11:12:54.043' AS DateTime), 2)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (4, N'danilo', N'medina', N'd@exa.com', N'danilo', N'ewndXcvwPXJhGdLPrnaGGQ==', CAST(N'2023-06-20T11:39:57.570' AS DateTime), 1)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (5, N'Leonidas', N'Trujillo', N't@exa.com', N'trujillo', N'MI2I525KmOgkWGzRs+sDHh9hkC4gGr8gRwWIN8BV1qw=', CAST(N'2023-06-26T10:24:47.907' AS DateTime), 2)
INSERT [dbo].[Users] ([Id_User], [Name_User], [LastName_User], [Email_User], [UserName], [Password_User], [Date_User], [type_User]) VALUES (7, N'Maribel2', N'Reyes', N'm@exa.com', N'mariReyes', N'BboAZhFkGWQTmWBARecFPg==', CAST(N'2023-06-28T11:20:37.837' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
USE [master]
GO
ALTER DATABASE [PMS] SET  READ_WRITE 
GO
