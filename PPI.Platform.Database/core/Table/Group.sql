CREATE TABLE [core].[Group]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [ProgramId] INT NULL, 
    [Name] NVARCHAR(150) NOT NULL, 
    [Administrator] NVARCHAR(250) NOT NULL, 
    [EmailAddress] NVARCHAR(250) NOT NULL, 
    [StartDate] DATETIME2 NOT NULL, 
    [EndDate] DATETIME2 NOT NULL, 
    [CreateDate] DATETIME2 NULL, 
    [StatusGenericTypeValueId] INT NULL, 
    [TypeGenericTypeValueId] INT NULL,     
    [CreatedBy] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Group_Program] FOREIGN KEY ([ProgramId]) REFERENCES [core].[Program]([Id]) on delete cascade, 
    CONSTRAINT [FK_Group_Status] FOREIGN KEY (StatusGenericTypeValueId) REFERENCES [core].[GenericTypeValue]([Id]),
	CONSTRAINT [FK_Group_Type] FOREIGN KEY (TypeGenericTypeValueId) REFERENCES [core].[GenericTypeValue]([Id])
)
