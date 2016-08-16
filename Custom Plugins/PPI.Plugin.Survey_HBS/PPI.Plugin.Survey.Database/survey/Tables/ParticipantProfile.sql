CREATE TABLE [survey].[ParticipantProfile]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[NetworkContactID] INT NOT Null,
    [ParticipantID] INT NOT NULL, 
    [GenderId] INT NULL, 
    [AgeGroupId] INT NULL, 
    [RegionId] INT NULL, 
	[WorkRegionId] INT NULL, 
    [NativeLang] INT NULL,
	[NetworkPercentId] INT NULL, 
	[CreaterBy] INT NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL, 
    [ModifiedBy] INT NULL, 
    [ModifiedOn] DATETIME2 NULL,     
    CONSTRAINT [FK_ParticipantProfile_NetworkInfoContacts] FOREIGN KEY ([NetworkContactID]) REFERENCES [survey].[NetworkInfoContacts]([Id]) on delete cascade 
)
