CREATE TABLE [core].[ResourceValue]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [ResourceId] INT NOT NULL, 
    [CultureId] INT NOT NULL, 
    [Value] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_ResourceValue_Culture] FOREIGN KEY ([CultureId]) REFERENCES [core].[Culture]([Id]) on delete cascade,
    CONSTRAINT [FK_ResourceValue_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [core].[Resource]([Id]) on delete cascade
)
