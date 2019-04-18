CREATE LOGIN WebTestsDatabaseUser WITH PASSWORD = 'WebTestsDatabaseUser'
GO  

USE [WebTestsDatabase]
GO
CREATE USER [WebTestsDatabaseUser] FOR LOGIN [WebTestsDatabaseUser]
GO

EXEC sp_addrolemember N'db_datareader', N'WebTestsDatabaseUser'
EXEC sp_addrolemember N'db_datawriter', N'WebTestsDatabaseUser'
GO