CREATE VIEW [survey].[NetworkContacts_V]
	AS
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+ContactName1 as Pkey, ID, ParticipantId, ContactName1 as ContactName from [survey].NetworkInfoContacts where ContactName1 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+ContactName2 as Pkey,ID, ParticipantId, ContactName2 as ContactName from [survey].NetworkInfoContacts where ContactName2 is not null
Union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+ContactName3 as Pkey,ID, ParticipantId, ContactName3 as ContactName from [survey].NetworkInfoContacts where ContactName3 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+ContactName4 as Pkey,ID, ParticipantId, ContactName4 as ContactName from [survey].NetworkInfoContacts where ContactName4 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+ContactName5 as Pkey,ID, ParticipantId, ContactName5 as ContactName from [survey].NetworkInfoContacts where ContactName5 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+ContactName6 as Pkey,ID, ParticipantId, ContactName6 as ContactName from [survey].NetworkInfoContacts where ContactName6 is not null
Union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+ContactName7 as Pkey,ID, ParticipantId, ContactName7 as ContactName from [survey].NetworkInfoContacts where ContactName7 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+ContactName8 as Pkey,ID, ParticipantId, ContactName8 as ContactName from [survey].NetworkInfoContacts where ContactName8 is not null
union 
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+JobContactName1 as Pkey,ID, ParticipantId, JobContactName1 as ContactName from [survey].NetworkInfoContacts where JobContactName1 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+JobContactName2 as Pkey,ID, ParticipantId, JobContactName2 as ContactName from [survey].NetworkInfoContacts where JobContactName2 is not null
Union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+JobContactName3 as Pkey,ID, ParticipantId, JobContactName3 as ContactName from [survey].NetworkInfoContacts where JobContactName3 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+JobContactName4 as Pkey,ID, ParticipantId, JobContactName4 as ContactName from [survey].NetworkInfoContacts where JobContactName4 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+JobContactName5 as Pkey,ID, ParticipantId, JobContactName5 as ContactName from [survey].NetworkInfoContacts where JobContactName5 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+JobContactName6 as Pkey,ID, ParticipantId, JobContactName6 as ContactName from [survey].NetworkInfoContacts where JobContactName6 is not null
Union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+JobContactName7 as Pkey,ID, ParticipantId, JobContactName7 as ContactName from [survey].NetworkInfoContacts where JobContactName7 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+JobContactName8 as Pkey,ID, ParticipantId, JobContactName8 as ContactName from [survey].NetworkInfoContacts where JobContactName8 is not null
Union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+CrrContactName1 as Pkey, ID, ParticipantId, CrrContactName1 as ContactName from [survey].NetworkInfoContacts where CrrContactName1 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+CrrContactName2 as Pkey, ID, ParticipantId, CrrContactName2 as ContactName from [survey].NetworkInfoContacts where CrrContactName2 is not null
Union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+CrrContactName3 as Pkey, ID, ParticipantId, CrrContactName3 as ContactName from [survey].NetworkInfoContacts where CrrContactName3 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+CrrContactName4 as Pkey, ID, ParticipantId, CrrContactName4 as ContactName from [survey].NetworkInfoContacts where CrrContactName4 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+CrrContactName5 as Pkey, ID, ParticipantId, CrrContactName5 as ContactName from [survey].NetworkInfoContacts where CrrContactName5 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+CrrContactName6 as Pkey, ID, ParticipantId, CrrContactName6 as ContactName from [survey].NetworkInfoContacts where CrrContactName6 is not null
Union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+CrrContactName7 as Pkey, ID, ParticipantId, CrrContactName7 as ContactName from [survey].NetworkInfoContacts where CrrContactName7 is not null
union
select cast(ID as Varchar)+cast(ParticipantId as Varchar)+CrrContactName8 as Pkey, ID, ParticipantId, CrrContactName8 as ContactName from [survey].NetworkInfoContacts where CrrContactName8 is not null