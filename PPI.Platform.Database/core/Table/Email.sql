CREATE TABLE [core].[Email]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [EmailGenericTypeValueId] INT NOT NULL, 
    [DefaultFrom] NVARCHAR(250) NOT NULL, 
    [Subject] NVARCHAR(250) NOT NULL, 
    [Introduction] NVARCHAR(MAX) NOT NULL, 
    [Closing] NVARCHAR(MAX) NOT NULL, 
    [Template] BIT NOT NULL, 
    [TemplateName] NVARCHAR(150) NULL, 
    CONSTRAINT [FK_Email_EmailGenericTypeValue] FOREIGN KEY ([EmailGenericTypeValueId]) REFERENCES [core].[GenericTypeValue]([Id]) 
)
