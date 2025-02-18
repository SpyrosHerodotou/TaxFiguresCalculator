USE [master]
GO
/****** Object:  Database [Tax_Figures_Calculator]    Script Date: 8/24/2018 6:27:08 AM ******/
CREATE DATABASE [Tax_Figures_Calculator]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'test', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\test.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'test_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\test_log.ldf' , SIZE = 6912KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Tax_Figures_Calculator] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tax_Figures_Calculator].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tax_Figures_Calculator] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET RECOVERY FULL 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET  MULTI_USER 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tax_Figures_Calculator] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Tax_Figures_Calculator] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Tax_Figures_Calculator', N'ON'
GO
USE [Tax_Figures_Calculator]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 8/24/2018 6:27:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [varchar](900) NOT NULL,
	[customer_id] [bigint] NOT NULL,
	[account_type] [nvarchar](100) NOT NULL,
	[account_description] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/24/2018 6:27:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](80) NOT NULL,
	[surname] [nvarchar](80) NOT NULL,
	[telephone] [varchar](50) NOT NULL,
	[email_address] [nvarchar](320) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 8/24/2018 6:27:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[account_id] [varchar](900) NOT NULL,
	[amount] [decimal](19, 4) NOT NULL,
	[description] [nvarchar](1000) NOT NULL,
	[currency_code] [nvarchar](6) NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A102230', 3, N'CarPurchaes', N'Account responsible for car loans')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A102312', 3, N'OfficePurchases', N'Account reponsible for office payments')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A132313', 3, N'Utilities', N'Account reponsible for utility purchases')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A143233', 3, N'Entertaiment', N'Account reponsible for entertainment')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A1a313132', 3, N'FoodPurchases', N'Account reponsible for food purchases')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A321e321', 4, N'CarPurchaes', N'Account responsible for car loans')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A321e32r', 5, N'CarPurchaes', N'Account responsible for car loans')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A3321d32', 4, N'FoodPurchases', N'Account reponsible for food purchases')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A3321d3g', 5, N'FoodPurchases', N'Account reponsible for food purchases')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A4324e23', 4, N'Utilities', N'Account reponsible for utility purchases')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A4324e2y', 5, N'Utilities', N'Account reponsible for utility purchases')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A432t334', 4, N'OfficePurchases', N'Account reponsible for office payments')
INSERT [dbo].[Account] ([id], [customer_id], [account_type], [account_description]) VALUES (N'A432t33t', 5, N'OfficePurchases', N'Account reponsible for office payments')
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([id], [name], [surname], [telephone], [email_address]) VALUES (3, N'Spyros', N'Herodotou', N'0035899381', N'spyrosherodotou@gmail.com')
INSERT [dbo].[Customer] ([id], [name], [surname], [telephone], [email_address]) VALUES (4, N'Solon', N'Solomou', N'0037848399', N'solonsolomou@hotmail.com')
INSERT [dbo].[Customer] ([id], [name], [surname], [telephone], [email_address]) VALUES (5, N'Micheal', N'James', N'0034781931', N'michealjames@gmail.com')
INSERT [dbo].[Customer] ([id], [name], [surname], [telephone], [email_address]) VALUES (6, N'Clarice', N'Sheridan', N'0034899309', N'claricecsheridan@gmail.com')
INSERT [dbo].[Customer] ([id], [name], [surname], [telephone], [email_address]) VALUES (7, N'Mary', N'Poskowitz', N'0003200938', N'maryposkowitz@gmail.com')
INSERT [dbo].[Customer] ([id], [name], [surname], [telephone], [email_address]) VALUES (8, N'Antoine', N'Griez', N'0032131003', N'antoinegriez@gmail.com')
SET IDENTITY_INSERT [dbo].[Customer] OFF
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Customers] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Accounts_Customers]
GO
ALTER TABLE [dbo].[Transaction]  WITH NOCHECK ADD  CONSTRAINT [FK_Transaction_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account]
GO
USE [master]
GO
ALTER DATABASE [Tax_Figures_Calculator] SET  READ_WRITE 
GO
