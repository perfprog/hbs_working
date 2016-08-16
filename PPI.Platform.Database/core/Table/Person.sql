CREATE TABLE [core].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,     
    [Prefix] NVARCHAR(50) NULL, 
	[First Name] NVARCHAR(150) NOT NULL, 
    [Last Name] NVARCHAR(150) NOT NULL, 
    [Middle Name] NVARCHAR(150) NULL,
    [Suffix] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(250) NOT NULL, 
    [AspNetUsersId] NVARCHAR(50) NULL,      
    
)
