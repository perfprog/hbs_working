CREATE FUNCTION [core].[GetGenericType]
(	
	@Value int,
	@Culture int

)
RETURNS nvarchar(max)
Begin
	Declare @Return nvarchar(max)
	Select @Return = Value from core.GenericType GT
	join core.GenericTypeValue GTV on GTV.GenericTypeId = GT.Id
	join core.Resource RS on GTV.ResourceId = RS.Id
	join core.ResourceValue RSV on RSV.ResourceId = RS.Id
	where rsv.CultureId = @Culture and GTV.Id = @Value
	return @Return;
End
