CREATE FUNCTION [survey].[ContactNetwork]
(
	@ContactName nvarchar(150),
	@ParticipantId int
)
RETURNS nvarchar(20)
begin	
	Declare @Network nvarchar(60)
	Declare @SN nvarchar(20)
	Declare @ON nvarchar(20)
	Declare @DN nvarchar(20)
	set @SN = (Select top 1 'S' as [TYPE] from survey.NetworkInfoContacts where ParticipantId = @ParticipantId and (ContactName1 = @ContactName or ContactName2 = @ContactName or ContactName3 = @ContactName or ContactName4 = @ContactName or ContactName5 = @ContactName or ContactName6 = @ContactName or ContactName7 = @ContactName or ContactName8 = @ContactName))
	set @ON = (Select top 1 'O' as [TYPE] from survey.NetworkInfoContacts where ParticipantId = @ParticipantId and (JobContactName1 = @ContactName or JobContactName2 = @ContactName or JobContactName3 = @ContactName or JobContactName4 = @ContactName or JobContactName5 = @ContactName or JobContactName6 = @ContactName or JobContactName7 = @ContactName or JobContactName8 = @ContactName))
	set @DN = (Select top 1 'D' as [TYPE] from survey.NetworkInfoContacts where ParticipantId = @ParticipantId and (CrrContactName1 = @ContactName or CrrContactName2 = @ContactName or CrrContactName3 = @ContactName or CrrContactName4 = @ContactName or CrrContactName5 = @ContactName or CrrContactName6 = @ContactName or CrrContactName7 = @ContactName or CrrContactName8 = @ContactName))	
	set @Network = concat(@SN,@ON,@DN);
	
	return @Network;
end
