CREATE TABLE [core].[PluginAction]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [RegisteredPluginsId] INT NOT NULL, 
    [ForView] NVARCHAR(150) NOT NULL, 
    [ActionGenericTypeIValueId] INT NOT NULL, 
    [Action] NVARCHAR(50) NOT NULL, 
    [Controller] NVARCHAR(50) NOT NULL, 
    [RoutingData] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_PluginAction_RegisteredPlugins] FOREIGN KEY ([RegisteredPluginsId]) REFERENCES [core].[RegisteredPlugins]([Id]),
	CONSTRAINT [FK_Group_Action] FOREIGN KEY (ActionGenericTypeIValueId) REFERENCES [core].[GenericTypeValue]([Id])
)
