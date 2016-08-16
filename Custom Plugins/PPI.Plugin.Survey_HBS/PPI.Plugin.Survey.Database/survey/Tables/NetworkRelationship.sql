CREATE TABLE [survey].[NetworkRelationship]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ParticipantId] INT NOT NULL, 
    [NetworkContactID] INT NOT NULL,
	[ContactName] NVarchar(150) NOT NULL, 
    [VeryClose] BIT NULL, 
    [Close] BIT NULL, 
    [NotVeryClose] BIT NULL, 
    [Distant] BIT NULL,
	[CreaterBy] INT NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedOn] DATETIME2 NULL, 
    CONSTRAINT [FK_NetworkRelationship_NetworkInfoContacts] FOREIGN KEY ([NetworkContactID]) REFERENCES [survey].[NetworkInfoContacts]([Id]) on delete cascade
)
