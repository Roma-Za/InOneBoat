USE [master]
GO
/****** Object:  Database [ProjectsDB]    Script Date: 23.11.2014 18:00:40 ******/
CREATE DATABASE [ProjectsDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjectsDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ProjectsDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProjectsDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ProjectsDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ProjectsDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectsDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectsDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectsDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectsDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectsDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectsDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectsDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjectsDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectsDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectsDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectsDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectsDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectsDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectsDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectsDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectsDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectsDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjectsDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectsDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectsDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectsDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectsDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectsDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectsDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectsDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ProjectsDB] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectsDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectsDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectsDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectsDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [ProjectsDB]
GO
/****** Object:  Table [dbo].[attachments]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[attachments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[task_id] [int] NULL,
	[file_path] [nvarchar](max) NULL,
 CONSTRAINT [PK_attachments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[comments]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[comment] [nvarchar](max) NULL,
	[task_id] [int] NULL,
	[employee_id] [int] NULL,
 CONSTRAINT [PK_comments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[employees]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employees](
	[user_id] [int] NOT NULL,
	[people_id] [int] NULL,
	[role_id] [int] NULL,
	[settings_id] [int] NULL,
 CONSTRAINT [PK_employees_1] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[log_items]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[log_items](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[task_id] [int] NULL,
	[start_time] [datetime] NULL,
	[end_time] [datetime] NULL,
	[employee_id] [int] NULL,
 CONSTRAINT [PK_log_items] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[participants]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[participants](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[task_id] [int] NULL,
	[employee_id] [int] NULL,
 CONSTRAINT [PK_participants] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[people]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[people](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[surname] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[patronymic] [nvarchar](50) NOT NULL,
	[phone_number] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_people] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[priorities]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[priorities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_priorities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[projects]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[projects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_projects] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[projects_employees]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[projects_employees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[project_id] [int] NULL,
	[employee_id] [int] NULL,
 CONSTRAINT [PK_team_employees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[roles]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[settings]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[settings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[time_zone] [int] NOT NULL,
	[send_to_email] [bit] NOT NULL,
 CONSTRAINT [PK_settings] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[task_statuses]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[task_statuses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_task_statuses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tasks]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parent_task_id] [int] NULL,
	[description] [nvarchar](max) NULL,
	[estimate] [float] NULL,
	[status_id] [int] NULL,
	[project_id] [int] NULL,
	[priority_id] [int] NULL,
	[summary] [nvarchar](max) NULL,
 CONSTRAINT [PK_tasks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user_types]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_user_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[users]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[user_type_id] [int] NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[watchers]    Script Date: 23.11.2014 18:00:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[watchers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee_id] [int] NULL,
	[task_id] [int] NULL,
 CONSTRAINT [PK_watchers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[attachments]  WITH CHECK ADD  CONSTRAINT [FK_attachments_tasks] FOREIGN KEY([task_id])
REFERENCES [dbo].[tasks] ([id])
GO
ALTER TABLE [dbo].[attachments] CHECK CONSTRAINT [FK_attachments_tasks]
GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD  CONSTRAINT [FK_comments_employees] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employees] ([user_id])
GO
ALTER TABLE [dbo].[comments] CHECK CONSTRAINT [FK_comments_employees]
GO
ALTER TABLE [dbo].[comments]  WITH CHECK ADD  CONSTRAINT [FK_comments_tasks] FOREIGN KEY([task_id])
REFERENCES [dbo].[tasks] ([id])
GO
ALTER TABLE [dbo].[comments] CHECK CONSTRAINT [FK_comments_tasks]
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_people] FOREIGN KEY([people_id])
REFERENCES [dbo].[people] ([id])
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_people]
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_roles] FOREIGN KEY([role_id])
REFERENCES [dbo].[roles] ([id])
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_roles]
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_settings] FOREIGN KEY([settings_id])
REFERENCES [dbo].[settings] ([id])
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_settings]
GO
ALTER TABLE [dbo].[employees]  WITH CHECK ADD  CONSTRAINT [FK_employees_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[employees] CHECK CONSTRAINT [FK_employees_users]
GO
ALTER TABLE [dbo].[log_items]  WITH CHECK ADD  CONSTRAINT [FK_log_items_employees] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employees] ([user_id])
GO
ALTER TABLE [dbo].[log_items] CHECK CONSTRAINT [FK_log_items_employees]
GO
ALTER TABLE [dbo].[log_items]  WITH CHECK ADD  CONSTRAINT [FK_log_items_tasks] FOREIGN KEY([task_id])
REFERENCES [dbo].[tasks] ([id])
GO
ALTER TABLE [dbo].[log_items] CHECK CONSTRAINT [FK_log_items_tasks]
GO
ALTER TABLE [dbo].[participants]  WITH CHECK ADD  CONSTRAINT [FK_participants_employees] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employees] ([user_id])
GO
ALTER TABLE [dbo].[participants] CHECK CONSTRAINT [FK_participants_employees]
GO
ALTER TABLE [dbo].[participants]  WITH CHECK ADD  CONSTRAINT [FK_participants_tasks] FOREIGN KEY([task_id])
REFERENCES [dbo].[tasks] ([id])
GO
ALTER TABLE [dbo].[participants] CHECK CONSTRAINT [FK_participants_tasks]
GO
ALTER TABLE [dbo].[projects]  WITH CHECK ADD  CONSTRAINT [FK_projects_users] FOREIGN KEY([customer_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[projects] CHECK CONSTRAINT [FK_projects_users]
GO
ALTER TABLE [dbo].[projects_employees]  WITH CHECK ADD  CONSTRAINT [FK_projects_employees_projects] FOREIGN KEY([project_id])
REFERENCES [dbo].[projects] ([id])
GO
ALTER TABLE [dbo].[projects_employees] CHECK CONSTRAINT [FK_projects_employees_projects]
GO
ALTER TABLE [dbo].[projects_employees]  WITH CHECK ADD  CONSTRAINT [FK_team_employees_employees] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employees] ([user_id])
GO
ALTER TABLE [dbo].[projects_employees] CHECK CONSTRAINT [FK_team_employees_employees]
GO
ALTER TABLE [dbo].[tasks]  WITH CHECK ADD  CONSTRAINT [FK_tasks_priorities] FOREIGN KEY([priority_id])
REFERENCES [dbo].[priorities] ([id])
GO
ALTER TABLE [dbo].[tasks] CHECK CONSTRAINT [FK_tasks_priorities]
GO
ALTER TABLE [dbo].[tasks]  WITH CHECK ADD  CONSTRAINT [FK_tasks_task_statuses] FOREIGN KEY([status_id])
REFERENCES [dbo].[task_statuses] ([id])
GO
ALTER TABLE [dbo].[tasks] CHECK CONSTRAINT [FK_tasks_task_statuses]
GO
ALTER TABLE [dbo].[tasks]  WITH CHECK ADD  CONSTRAINT [FK_tasks_tasks] FOREIGN KEY([parent_task_id])
REFERENCES [dbo].[tasks] ([id])
GO
ALTER TABLE [dbo].[tasks] CHECK CONSTRAINT [FK_tasks_tasks]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_user_type] FOREIGN KEY([user_type_id])
REFERENCES [dbo].[user_types] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_user_type]
GO
ALTER TABLE [dbo].[watchers]  WITH CHECK ADD  CONSTRAINT [FK_watchers_employees] FOREIGN KEY([employee_id])
REFERENCES [dbo].[employees] ([user_id])
GO
ALTER TABLE [dbo].[watchers] CHECK CONSTRAINT [FK_watchers_employees]
GO
ALTER TABLE [dbo].[watchers]  WITH CHECK ADD  CONSTRAINT [FK_watchers_tasks] FOREIGN KEY([task_id])
REFERENCES [dbo].[tasks] ([id])
GO
ALTER TABLE [dbo].[watchers] CHECK CONSTRAINT [FK_watchers_tasks]
GO
INSERT INTO [dbo].[roles]
           ([name])
     VALUES
           (N'PM');
GO
INSERT INTO [dbo].[roles]
           ([name])
     VALUES
           (N'QA');
GO
INSERT INTO [dbo].[roles]
           ([name])
     VALUES
           (N'Dev');
GO

INSERT INTO [dbo].[task_statuses]
           ([name])
     VALUES
           (N'not started');
GO
INSERT INTO [dbo].[task_statuses]
           ([name])
     VALUES
           (N'in progress');
GO
INSERT INTO [dbo].[task_statuses]
           ([name])
     VALUES
           (N'completed');
GO
INSERT INTO [dbo].[task_statuses]
           ([name])
     VALUES
           (N'reopened');
GO
INSERT INTO [dbo].[task_statuses]
           ([name])
     VALUES
           (N'closed');
GO

INSERT INTO [dbo].[priorities]
           ([name])
     VALUES
           (N'high');
GO
INSERT INTO [dbo].[priorities]
           ([name])
     VALUES
           (N'normal');
GO
INSERT INTO [dbo].[priorities]
           ([name])
     VALUES
           (N'low');
INSERT INTO [dbo].[user_types]
           ([name])
     VALUES
           (N'admin');
GO
INSERT INTO [dbo].[user_types]
           ([name])
     VALUES
           (N'customer');
GO
INSERT INTO [dbo].[user_types]
           ([name])
     VALUES
           (N'employee');
GO
INSERT INTO [dbo].[users]
          
     VALUES
           (N'admin',
		   N'admin',
		   (SELECT TOP 1 [id] 
		   FROM [dbo].[user_types] 
		   WHERE [name]='admin'));
GO
USE [master]
GO
ALTER DATABASE [ProjectsDB] SET  READ_WRITE 
GO
