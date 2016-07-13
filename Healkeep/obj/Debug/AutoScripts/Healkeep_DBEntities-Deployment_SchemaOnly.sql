SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimType] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserComments](
	[AspNetUserCommentID] [bigint] IDENTITY(1,1) NOT NULL,
	[CheckedUp] [bit] NULL CONSTRAINT [DF_AspNetUserComments_Checked]  DEFAULT ((0)),
	[AspNetUserID] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CommentID] [bigint] NULL,
	[NaturalTreatmentID] [bigint] NULL,
	[CheckedDown] [bit] NULL,
 CONSTRAINT [PK_AspNetUserComments] PRIMARY KEY CLUSTERED 
(
	[AspNetUserCommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProviderKey] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[RoleId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentID] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedTime] [datetime] NOT NULL CONSTRAINT [DF_Comments]  DEFAULT (getutcdate()),
	[AspNetUserID] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NaturalTreatmentID] [bigint] NOT NULL,
	[CommentCommentID] [bigint] NULL,
	[VoteUp] [int] NOT NULL DEFAULT ((0)),
	[VoteDown] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Diseases](
	[DiseaseID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[URLwiki] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Disease] PRIMARY KEY CLUSTERED 
(
	[DiseaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NaturalTreatments](
	[NaturalTreatmentID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ScoreUsers] [decimal](4, 2) NULL,
	[ScoreEntities] [decimal](4, 2) NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiseaseID] [bigint] NOT NULL,
	[Treatment] [bit] NOT NULL,
	[Prevention] [bit] NOT NULL,
	[Approved] [bit] NOT NULL,
	[AspNetUserID] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_NaturalTreatment] PRIMARY KEY CLUSTERED 
(
	[NaturalTreatmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[test] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)

GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_FK_AspNetUserRoles_AspNetUser] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_FK_AspNetUserComment] ON [dbo].[Comments]
(
	[AspNetUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_CommentComment] ON [dbo].[Comments]
(
	[CommentCommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_NaturalTreatmentComment] ON [dbo].[Comments]
(
	[NaturalTreatmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_FK_NaturalTreatment_AspNetUsers] ON [dbo].[NaturalTreatments]
(
	[AspNetUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
CREATE NONCLUSTERED INDEX [IX_FK_NaturalTreatment_Disease] ON [dbo].[NaturalTreatments]
(
	[DiseaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserComments]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserComments_AspNetUsers] FOREIGN KEY([AspNetUserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserComments] CHECK CONSTRAINT [FK_AspNetUserComments_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserComments]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserComments_Comments] FOREIGN KEY([CommentID])
REFERENCES [dbo].[Comments] ([CommentID])
GO
ALTER TABLE [dbo].[AspNetUserComments] CHECK CONSTRAINT [FK_AspNetUserComments_Comments]
GO
ALTER TABLE [dbo].[AspNetUserComments]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserComments_NaturalTreatment] FOREIGN KEY([NaturalTreatmentID])
REFERENCES [dbo].[NaturalTreatments] ([NaturalTreatmentID])
GO
ALTER TABLE [dbo].[AspNetUserComments] CHECK CONSTRAINT [FK_AspNetUserComments_NaturalTreatment]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserComment] FOREIGN KEY([AspNetUserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_AspNetUserComment]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_CommentComment] FOREIGN KEY([CommentCommentID])
REFERENCES [dbo].[Comments] ([CommentID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_CommentComment]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_NaturalTreatmentComment] FOREIGN KEY([NaturalTreatmentID])
REFERENCES [dbo].[NaturalTreatments] ([NaturalTreatmentID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_NaturalTreatmentComment]
GO
ALTER TABLE [dbo].[NaturalTreatments]  WITH CHECK ADD  CONSTRAINT [FK_NaturalTreatment_AspNetUsers] FOREIGN KEY([AspNetUserID])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NaturalTreatments] CHECK CONSTRAINT [FK_NaturalTreatment_AspNetUsers]
GO
ALTER TABLE [dbo].[NaturalTreatments]  WITH CHECK ADD  CONSTRAINT [FK_NaturalTreatment_Disease] FOREIGN KEY([DiseaseID])
REFERENCES [dbo].[Diseases] ([DiseaseID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NaturalTreatments] CHECK CONSTRAINT [FK_NaturalTreatment_Disease]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spAddRole]
  --@Id NVARCHAR(128),
  @Name nvarchar(256)
AS
BEGIN
	INSERT INTO [AspNetRoles] (Id,Name)
	VALUES (
			CASE (select max(cast(Id as bigint)) from [dbo].[AspNetRoles])
				WHEN NULL THEN '1'
				ELSE (select cast(max(cast(Id as bigint)+1) as nvarchar) from [dbo].[AspNetRoles])
			END
			--@Id
			,@Name)
	
END



GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllRoles]
AS
BEGIN
	SELECT Id,Name
	FROM [AspNetRoles]
END


GO
