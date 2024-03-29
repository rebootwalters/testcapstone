USE [master]
GO
/****** Object:  Database [testcapstone]    Script Date: 8/2/2019 12:52:51 PM ******/
CREATE DATABASE [testcapstone]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'testcapstone', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\testcapstone.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'testcapstone_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\testcapstone_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [testcapstone] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [testcapstone].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [testcapstone] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [testcapstone] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [testcapstone] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [testcapstone] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [testcapstone] SET ARITHABORT OFF 
GO
ALTER DATABASE [testcapstone] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [testcapstone] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [testcapstone] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [testcapstone] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [testcapstone] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [testcapstone] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [testcapstone] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [testcapstone] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [testcapstone] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [testcapstone] SET  DISABLE_BROKER 
GO
ALTER DATABASE [testcapstone] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [testcapstone] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [testcapstone] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [testcapstone] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [testcapstone] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [testcapstone] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [testcapstone] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [testcapstone] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [testcapstone] SET  MULTI_USER 
GO
ALTER DATABASE [testcapstone] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [testcapstone] SET DB_CHAINING OFF 
GO
ALTER DATABASE [testcapstone] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [testcapstone] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [testcapstone]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/2/2019 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](80) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/2/2019 12:52:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[EMail] [nvarchar](100) NULL,
	[Hash] [nvarchar](100) NULL,
	[Salt] [nvarchar](100) NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Administrator')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'PowerUser')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (3, N'NormalUser')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1001, N'NonNull')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1002, NULL)
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [EMail], [Hash], [Salt], [DateOfBirth], [RoleID]) VALUES (1, N'Alligator@Email.com', N'AAAAHASH', N'AAAASalt', CAST(N'2010-01-01T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Users] ([UserID], [EMail], [Hash], [Salt], [DateOfBirth], [RoleID]) VALUES (2, N'Bear@Email.com', N'BBBBHASH', N'BBBSalt', CAST(N'2011-01-01T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Users] ([UserID], [EMail], [Hash], [Salt], [DateOfBirth], [RoleID]) VALUES (3, N'Cat@Email.com', N'CCCCHASH', N'CCCCSalt', CAST(N'2012-01-01T00:00:00.0000000' AS DateTime2), 2)
INSERT [dbo].[Users] ([UserID], [EMail], [Hash], [Salt], [DateOfBirth], [RoleID]) VALUES (4, N'dog@Email.com', N'DDDDHASH', N'DDDDSalt', CAST(N'2013-01-01T00:00:00.0000000' AS DateTime2), 2)
INSERT [dbo].[Users] ([UserID], [EMail], [Hash], [Salt], [DateOfBirth], [RoleID]) VALUES (5, N'gecko@Email.com', N'DDDDHASH', N'DDDDSalt', CAST(N'2014-01-01T00:00:00.0000000' AS DateTime2), 3)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [AK_EMail]    Script Date: 8/2/2019 12:52:52 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [AK_EMail] ON [dbo].[Users]
(
	[EMail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
/****** Object:  StoredProcedure [dbo].[CreateRole]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[CreateRole]
   @RoleID int output,
   @RoleName nvarchar(80)
as
begin
 insert into roles (RoleName) values (@RoleName);
 select @RoleID = @@Identity
end
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[CreateUser]
  @UserID int output,
  @EMail nvarchar(100),
  @Hash nvarchar(100),
  @Salt nvarchar(100),
  @DateOfBirth datetime2(7),
  @RoleID int
as
begin
    insert into users (EMail, Hash, Salt, DateOfBirth, RoleID)
	  values (@Email, @Hash, @Salt, @DateOfBirth, @RoleID);
	  select @UserID = @@Identity;

end
GO
/****** Object:  StoredProcedure [dbo].[DeleteRole]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[DeleteRole]
      @RoleID int	 
as
begin
     delete from roles
	 where RoleID = @RoleID
end
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[DeleteUser]
  @UserID int
as
begin
    delete from users
	where UserID = @UserID;
	 

end
GO
/****** Object:  StoredProcedure [dbo].[FindRoleByRoleID]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[FindRoleByRoleID]
 @RoleID int
as
begin
select RoleID, RoleName from Roles
  where RoleID = @RoleID
end
GO
/****** Object:  StoredProcedure [dbo].[FindUserByEmail]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[FindUserByEmail]
  @EMail nVarchar(100)
as
Begin
  select UserID, Email, Hash, Salt, DateOfBirth, Users.RoleID, RoleName 
  from Users
  inner join Roles on Users.RoleID = Roles.RoleID
  where EMail = @EMail
end
GO
/****** Object:  StoredProcedure [dbo].[FindUserByUserID]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[FindUserByUserID]
  @UserID int
as
Begin
  select UserID, Email, Hash, Salt, DateOfBirth, Users.RoleID, RoleName 
  from Users
  inner join Roles on Users.RoleID = Roles.RoleID
  where UserID = @UserID
end
GO
/****** Object:  StoredProcedure [dbo].[GetRoles]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetRoles]
 @Skip int,
 @Take int
as
begin
select RoleID, RoleName from Roles 
order by RoleID 
Offset @Skip Rows
Fetch next @Take rows only

  
end
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetUsers]
  @skip int,
  @Take int
as
Begin
  select UserID, Email, Hash, Salt, DateOfBirth, Users.RoleID, RoleName 
  from Users
  inner join Roles on Users.RoleID = Roles.RoleID
  order by UserID
  offset @Skip rows
  fetch next @Take rows only
end
GO
/****** Object:  StoredProcedure [dbo].[GetUsersRelatedToRoleID]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetUsersRelatedToRoleID]
  @RoleID int,
  @skip int,
  @take int
as
Begin
  select UserID, Email, Hash, Salt, DateOfBirth, Users.RoleID, RoleName 
  from Users
  inner join Roles on Users.RoleID = Roles.RoleID
  where Users.RoleID = @RoleID
  order by UserID
  offset @Skip rows
  fetch next @Take rows only
end
GO
/****** Object:  StoredProcedure [dbo].[JustUpdateRole]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[JustUpdateRole]
      @RoleID int,
	  @RoleName nvarchar(80)
as
begin
      update Roles Set RoleName = @RoleName
	  where RoleID = @RoleID
end
GO
/****** Object:  StoredProcedure [dbo].[JustUpdateUser]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[JustUpdateUser]
  @UserID int,
  @EMail nvarchar(100),
  @Hash nvarchar(100),
  @Salt nvarchar(100),
  @DateOfBirth datetime2(7),
  @RoleID int
as
begin
    Update users set EMail = @Email, Hash = @Hash, Salt = @Salt, 
	DateOfBirth = @DateOfBirth, RoleID =@RoleID 
	where UserID = @UserID;
	 

end
GO
/****** Object:  StoredProcedure [dbo].[ObtainRoleCount]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ObtainRoleCount]
as
begin
select count(*) from Roles
end
GO
/****** Object:  StoredProcedure [dbo].[ObtainUserCount]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ObtainUserCount]
as
begin
select count(*) from Users
end
GO
/****** Object:  StoredProcedure [dbo].[reload]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[reload]
as
begin

declare @aid int
declare @pid int
declare @nid int

declare @id int

exec CreateRole @aid output,'Administrator'
exec CreateRole @pid output,'PowerUser'
exec CreateRole @nid output,'NormalUser'

exec CreateUser @id, 'Alligator@Email.com','AAAAHASH','AAAASalt','1/1/2010',@aid
exec CreateUser @id, 'Bear@Email.com','BBBBHASH','BBBSalt','1/1/2011',@aid
exec CreateUser @id, 'Cat@Email.com','CCCCHASH','CCCCSalt','1/1/2012',@pid
exec CreateUser @id, 'dog@Email.com','DDDDHASH','DDDDSalt','1/1/2013',@pid
exec CreateUser @id, 'gecko@Email.com','DDDDHASH','DDDDSalt','1/1/2014',@nid

end
GO
/****** Object:  StoredProcedure [dbo].[reset]    Script Date: 8/2/2019 12:52:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[reset]
as
Begin
   delete from users;
   delete from roles;

      dbcc checkident('users',reseed,0)
	  dbcc checkident('Roles',reseed,0)

End
GO
USE [master]
GO
ALTER DATABASE [testcapstone] SET  READ_WRITE 
GO
