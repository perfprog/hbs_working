CREATE TABLE [core].[RegisteredPlugins]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Version] NVARCHAR(50) NOT NULL, 
    [Custom] BIT NOT NULL, 
    [Description] NVARCHAR(250) NULL,
	[InstallationDate] DATETIME2 NOT NULL, 
    [FullPath] NVARCHAR(250) NOT NULL
    
)
