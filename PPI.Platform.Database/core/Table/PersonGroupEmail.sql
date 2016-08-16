CREATE TABLE [core].[PersonGroupEmail]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [PersonId] INT NOT NULL,
	[GroupEmailId] INT NOT NULL, 
    [SendDate] DATETIME2 NOT NULL, 
    [SendStatusGenericTypeValueId] INT NULL, 
	[OrginalToAddress] NVARCHAR(250) NULL, 
    [Body] NVARCHAR(MAX) NULL, 
    [RetryCount] INT NULL, 
    [ErrorMessage] NVARCHAR(500) NULL, 
    CONSTRAINT [FK_PersonGroupEmail_Person] FOREIGN KEY ([PersonId]) REFERENCES [core].[Person]([Id]) on delete cascade,
    CONSTRAINT [FK_PersonGroupEmail_GroupEmail] FOREIGN KEY ([GroupEmailId]) REFERENCES [core].[GroupEmail]([Id]) on delete cascade,
	CONSTRAINT [FK_Email_SendStatusGenericTypeValue] FOREIGN KEY ([SendStatusGenericTypeValueId]) REFERENCES [core].[GenericTypeValue]([Id]) 

)
