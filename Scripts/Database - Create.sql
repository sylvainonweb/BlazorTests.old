USE [WebTestsDatabase]
GO
/****** Object:  User [WebTestsDatabaseUser]    Script Date: 05/07/2018 16:54:06 ******/
CREATE USER [WebTestsDatabaseUser] FOR LOGIN [WebTestsDatabaseUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [WebTestsDatabaseUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [WebTestsDatabaseUser]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 05/07/2018 16:54:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[CustomerTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerType]    Script Date: 05/07/2018 16:54:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_CustomerType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [Name], [FirstName], [CustomerTypeId]) VALUES (1, N'BLANCHARD', N'Sylvain', 1)
INSERT [dbo].[Customer] ([Id], [Name], [FirstName], [CustomerTypeId]) VALUES (2, N'RAFFIN', N'Nicolas', 2)
INSERT [dbo].[Customer] ([Id], [Name], [FirstName], [CustomerTypeId]) VALUES (3, N'GHERIB', N'Hanen', 1)
INSERT [dbo].[Customer] ([Id], [Name], [FirstName], [CustomerTypeId]) VALUES (4, N'PROST', N'Alex', 1)
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[CustomerType] ON 

INSERT [dbo].[CustomerType] ([Id], [Text]) VALUES (1, N'Particulier')
INSERT [dbo].[CustomerType] ([Id], [Text]) VALUES (2, N'Professionnel')
SET IDENTITY_INSERT [dbo].[CustomerType] OFF
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_CustomerType] FOREIGN KEY([CustomerTypeId])
REFERENCES [dbo].[CustomerType] ([Id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_CustomerType]
GO
USE [master]
GO
ALTER DATABASE [WebTestsDatabase] SET  READ_WRITE 
GO
