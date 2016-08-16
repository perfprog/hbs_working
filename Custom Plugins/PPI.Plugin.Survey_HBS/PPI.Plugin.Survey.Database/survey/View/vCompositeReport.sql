CREATE VIEW [survey].[vCompositeReport]
	AS 
	Select
 G.Name,
 G.StartDate,
 PC.Id, PR.[First Name],
 PR.[Last Name],
 (select (count(*) * (count(*) - 1) / 2) from survey.vWhoKnowWho NCV where NCV.ParticipantId = PC.Id and [Order] = -1) as [MaxDensity],
 (select count(*) from survey.vWhoKnowWho NCV where NCV.ParticipantId = PC.Id and [Order] = 1) as [CurrentDensity],
 (select count(GP.ParticipantId) from core.GroupParticipant GP where GP.GroupId = GP1.GroupId) as [TotalParticipants],
 (select count(GP.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
  ) as [TotalSurveysCompleted],
 (
    (select count(GP.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009) * 1.0 --completed	
	/
	(select count(GP.ParticipantId) from core.GroupParticipant GP where GP.GroupId = GP1.GroupId)
 )as [Yield],
 core.GetGenericType(GenderId,1) as [Gender],
 core.GetGenericType(AgeGroupId,1) as [AgeGroup],
 core.GetGenericType(RegionId,1) as [Region],
 core.GetGenericType(WorkRegionId,1) as [WorkRegion],
 core.GetGenericType(NativeLang,1) as [NativeLanguage],
 core.GetGenericType(NetworkPercentId,1) as [NetworkPercentage],
 (select count(*) from survey.vWhoKnowWho NCV where NCV.ParticipantId = PC.Id and [Order] = -1) as Contacts,
 (select count(*) from survey.vWhoKnowWho NCV where NCV.ParticipantId = PC.Id and [Order] = -1 and Networks like'%S%') as StategicContacts,
 (select count(*) from survey.vWhoKnowWho NCV where NCV.ParticipantId = PC.Id and [Order] = -1 and Networks like'%O%') as OperationalContacts,
 (select count(*) from survey.vWhoKnowWho NCV where NCV.ParticipantId = PC.Id and [Order] = -1 and Networks like'%D%') as DevelopmentalContacts,
(select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Gender = 1214 -- FEMALE
	and GP.ParticipantId = PC.Id
  ) as [FEMALE],
 (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Gender = 1213 -- MALE
	and GP.ParticipantId = PC.Id
  ) as [MALE],
 (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Region = 1217 -- ASIA
	and GP.ParticipantId = PC.Id
  ) as [RFAsia],
  (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Region = 1218 -- AFrica
	and GP.ParticipantId = PC.Id
  ) as [RFAfrica],
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Region = 1219 -- Central
	and GP.ParticipantId = PC.Id
  ) as [RFCentral],
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Region = 1220 -- Europe
	and GP.ParticipantId = PC.Id
  ) as [RFEurope],
   (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Region = 1221 -- Europe
	and GP.ParticipantId = PC.Id
  ) as [RFMiddleEast],
   (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Region = 1222 -- Europe
	and GP.ParticipantId = PC.Id
  ) as [RFNorthAmerica],
  (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Region = 1223 -- Europe
	and GP.ParticipantId = PC.Id
  ) as [RFOceania],
   (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.Region = 1224 -- Europe
	and GP.ParticipantId = PC.Id
  ) as [RFSouthAmerica],
  (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[DiffFirm],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.DiffFirm = 0 -- false
	and GP.ParticipantId = PC.Id
  ) as [SameCompany],
  (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[DiffFuncArea],[DiffFirm],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.DiffFuncArea = 0 -- false
	and GP.ParticipantId = PC.Id
  ) as [SameFunction],
   (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[DiffFuncArea],[DiffBsnUnit],[DiffFirm],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.DiffBsnUnit = 0 -- false
	and GP.ParticipantId = PC.Id
  ) as [SameBusinessUnit],
  (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[SameNativeLang],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[SameNativeLang] = 1 -- false
	and GP.ParticipantId = PC.Id
  ) as [SameLang],
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[RelationshipID] = 1209 -- Senior
	and GP.ParticipantId = PC.Id
  ) as [Senior]
  ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[RelationshipID] = 1210 -- Peer
	and GP.ParticipantId = PC.Id
  ) as [Peer]
  ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[RelationshipID] = 1211 -- Junior
	and GP.ParticipantId = PC.Id
  ) as [Junior]
  ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[ContactName],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[RelationshipID] = 1212 -- Other
	and GP.ParticipantId = PC.Id
  ) as [Other]
  ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[AgeGroup],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[AgeGroup] = 1013 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [18_24]
   ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[AgeGroup],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[AgeGroup] = 1014 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [25_34]
  ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[AgeGroup],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[AgeGroup] = 1015 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [35_44]
  ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[AgeGroup],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[AgeGroup] = 1016 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [45_54]
  ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[AgeGroup],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[AgeGroup] = 1225 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [55_64]
   ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join (SELECT DISTINCT [ParticipantId],[RelationshipID],[AgeGroup],[Region],[Gender] FROM [survey].[NetworkRelationshipDemo]) NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[AgeGroup] = 1226 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [65+]
  ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join survey.NetworkRelationship NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.VeryClose = 1 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [VeryClose]
   ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join survey.NetworkRelationship NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[Close] = 1 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [Close]
   ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join survey.NetworkRelationship NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[Distant] = 1 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [Distant]
   ,
    (select count(NRD.ParticipantId) 
	from core.GroupParticipant GP 
	Join core.SurveyGroupParticipant SGP on SGP.GroupParticipantId = GP.Id 	
	join survey.NetworkRelationship NRD on GP.ParticipantId = NRD.ParticipantID
	where GP.GroupId = GP1.GroupId 
	and SGP.StatusGenericTypeValueId = 1009 --completed	
	and NRD.[NotVeryClose] = 1 -- 18-24
	and GP.ParticipantId = PC.Id
  ) as [NotVeryClose]
from survey.ParticipantProfile PP
join core.Participant PC on PP.ParticipantID = PC.Id
join core.GroupParticipant GP1 on PC.id = GP1.ParticipantId
join core.[Group] G on GP1.GroupId = G.Id
join core.Person PR on PC.PersonId = PR.Id
