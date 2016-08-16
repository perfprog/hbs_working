CREATE PROCEDURE [core].[CreateGenericType]
	@SourceTable Nvarchar(150),
	@Field Nvarchar(150),
	@NumberOfValues int,
	@CultureId int
AS
	Declare @NewGenericTypeId int
	declare @i int 
	declare @NewResourceID int
	declare @Value nvarchar(max)
	Begin
		insert into [core].[GenericType] values (@SourceTable + '_' + @Field + 'Type')
		set @NewGenericTypeId = @@IDENTITY
		set @i = 1
		while @i <= @NumberOfValues
		Begin						
			
			insert into [core].[Resource] values('GenericTypeValue')
			set @NewResourceID = @@IDENTITY
			set @Value = @SourceTable + '_' + @Field + '_Value' + cast(@i as nvarchar(5))
			insert into [core].[ResourceValue] values (@NewResourceID,@CultureId,@Value)
			insert into [core].[GenericTypeValue] values (@NewGenericTypeId,@NewResourceID)
		set @i = @i + 1
		End;
		
	End
RETURN 0
