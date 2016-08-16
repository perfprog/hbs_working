CREATE TABLE [core].[SurveyGroupParticipant]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [SurveyId] INT NOT NULL,
	[GroupParticipantId] INT NOT NULL, 
    [StatusGenericTypeValueId] INT NULL, 
    CONSTRAINT [FK_SurveyGroupParticipant_Survey] FOREIGN KEY ([SurveyId]) REFERENCES [core].[Survey]([Id]) on delete cascade, 
    CONSTRAINT [FK_SurveyGroupParticipant_GroupParticipant] FOREIGN KEY ([GroupParticipantId]) REFERENCES [core].[GroupParticipant]([Id]) on delete cascade,
	CONSTRAINT [FK_SurveyGroupParticipant_GenericTypeValue] FOREIGN KEY ([StatusGenericTypeValueId]) REFERENCES [core].[GenericTypeValue]([Id]) on delete cascade,
    
)
