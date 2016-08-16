CREATE VIEW [core].[vGenericTypes]
	AS 
	SELECT Top 100 PERCENT GT.Name,RV.Value as EnglishValue,GTV.Id as GenericTypeValueId
	FROM [core].[GenericType] GT
	join [core].[GenericTypeValue] GTV on GTV.GenericTypeId = GT.Id
	join [core].[ResourceValue] RV on GTV.ResourceId = RV.ResourceId AND RV.CultureId = 1 --English
	Order By GT.Id asc
