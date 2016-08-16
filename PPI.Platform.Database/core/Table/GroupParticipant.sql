CREATE TABLE [core].[GroupParticipant]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [GroupId] INT NOT NULL, 
    [ParticipantId] INT NOT NULL, 
    [AddedDate] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_GroupParticipant_Group] FOREIGN KEY ([GroupId]) REFERENCES [core].[Group]([Id]) on delete cascade, 
    CONSTRAINT [FK_GroupParticipant_Participant] FOREIGN KEY ([ParticipantId]) REFERENCES [core].[Participant]([Id]) on delete cascade

)
