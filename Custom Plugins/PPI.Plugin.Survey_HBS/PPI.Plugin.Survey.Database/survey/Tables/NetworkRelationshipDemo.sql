CREATE TABLE [survey].[NetworkRelationshipDemo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ParticipantId] INT NOT NULL, 
    [NetworkContactID] INT NOT NULL, 
	[ContactName] NVarchar(150) NOT NULL, 
    [RelationshipID] int NOT NULL, 
    [DiffFuncArea] BIT NULL, 
    [DiffBsnUnit] BIT NULL, 
    [DiffFirm] BIT NULL, 
    [Gender] INT NULL, 
    [Region] INT NULL, 
    [SameNativeLang] BIT NULL, 
    [AgeGroup] INT NULL,
	[CreaterBy] INT NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedOn] DATETIME2 NULL
	,
	CONSTRAINT [FK_NetworkRelationshipDemo__NetworkInfoContacts] FOREIGN KEY ([NetworkContactID]) REFERENCES [survey].[NetworkInfoContacts]([Id]) on delete cascade
    
)
