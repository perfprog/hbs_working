CREATE TABLE [core].[Program]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [SiteId] INT NOT NULL, 
    [Name] NVARCHAR(150) NOT NULL, 
    CONSTRAINT [FK_Program_Site] FOREIGN KEY ([SiteId]) REFERENCES [core].[Site]([Id]) On delete cascade,     
)
