USE [master]
GO

CREATE DATABASE [JMeDQgddW9]
GO

USE [JMeDQgddW9]
GO

CREATE TABLE [dbo].[Tokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TokenValue] [nvarchar](max) NOT NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
	CONSTRAINT [PK_Tokens] PRIMARY KEY([Id])
)
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Phone] [nvarchar](14) NULL,
	[Login] [nvarchar](15) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	 CONSTRAINT [PK_Users] PRIMARY KEY([Id])
) 
GO

ALTER TABLE [dbo].[Tokens] ADD CONSTRAINT [FK_Tokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

INSERT INTO [dbo].[Users]([Name], [Email], [Login], [Password])
	VALUES('JMeDQgddW9', 'jmedqgddw9@jmalucelli.com.br', 'JMeDQgddW9', 'Sk1hbHVjZWxsaQ==')
GO