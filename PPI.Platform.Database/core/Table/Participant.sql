CREATE TABLE [core].[Participant]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [PersonId] INT NOT NULL
	CONSTRAINT [FK_Participant_Person] FOREIGN KEY ([PersonId]) REFERENCES [core].[Person]([Id]),     
)
