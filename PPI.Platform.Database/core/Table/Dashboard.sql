CREATE TABLE [core].[Dashboard]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [RegisteredPluginsId] INT NOT NULL, 
    [PartialViewName] NVARCHAR(150) NOT NULL, 
    [Role] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Dashboard_RegisteredPlugins] FOREIGN KEY ([RegisteredPluginsId]) REFERENCES [core].[RegisteredPlugins]([Id]) On Delete cascade
)
