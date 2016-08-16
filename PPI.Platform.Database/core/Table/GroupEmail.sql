CREATE TABLE [core].[GroupEmail]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [GroupId] INT NOT NULL, 
    [EmailId] INT NOT NULL, 
    CONSTRAINT [FK_GroupEmail_Group] FOREIGN KEY ([GroupId]) REFERENCES [core].[Group]([Id]) on delete cascade, 
    CONSTRAINT [FK_GroupEmail_Email] FOREIGN KEY ([EmailId]) REFERENCES [core].[Email]([Id]) on delete cascade
)
