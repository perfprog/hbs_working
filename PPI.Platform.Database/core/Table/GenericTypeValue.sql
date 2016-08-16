CREATE TABLE [core].[GenericTypeValue]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [GenericTypeId] INT NOT NULL, 
    [ResourceId] INT NOT NULL, 
    CONSTRAINT [FK_GenericTypeValue_GenericType] FOREIGN KEY ([GenericTypeId]) REFERENCES [core].[GenericType]([Id]) on delete cascade, 
    CONSTRAINT [FK_GenericTypeValue_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [core].[Resource]([Id]) on delete cascade
)
