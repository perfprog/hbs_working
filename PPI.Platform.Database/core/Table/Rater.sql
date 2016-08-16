CREATE TABLE [core].[Rater]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [PersonId] INT NOT NULL, 
    CONSTRAINT [FK_Rater_Person] FOREIGN KEY ([PersonId]) REFERENCES [core].[Person]([Id])
)
