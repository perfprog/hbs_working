CREATE VIEW [survey].[vWhoKnowWho]
	AS 
Select cast(PT.Id as Varchar)+'You'+RD.ContactName as Pkey, 'You' as [ContactA], RD.ContactName as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(RD.ContactName,PT.Id) as [Networks], NRD.DiffFirm as SameOrganization, NRD.RelationshipID, -1 as [Order]
from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
join survey.NetworkRelationshipDemo NRD on PT.Id = NRD.ParticipantId and NIC.Id = NRD.NetworkContactID and NRD.ContactName = RD.ContactName
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName2 as Pkey, RD.ContactName, MapContactName2 as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName2,PT.Id) as [Networks], null as SameOrganization, null, 1 as [Order]   from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected2 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName3 as Pkey, RD.ContactName, MapContactName3 as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName3,PT.Id) as [Networks], null as SameOrganization, null, 1 as [Order]   from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected3 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName4 as Pkey,RD.ContactName, MapContactName4  as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName4,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected4 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName5 as Pkey,RD.ContactName, MapContactName5  as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName5,PT.Id) as [Networks] , null as SameOrganization, null  , 1 as [Order] from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected5 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName6 as Pkey,RD.ContactName, MapContactName6   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName6,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected6 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName7 as Pkey,RD.ContactName, MapContactName7   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName7,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected7 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName8 as Pkey,RD.ContactName, MapContactName8   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName8,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected8 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName9 as Pkey,RD.ContactName, MapContactName9   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName9,PT.Id) as [Networks], null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected9 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName10 as Pkey,RD.ContactName, MapContactName10   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName10,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected10 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName11 as Pkey,RD.ContactName, MapContactName11   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName11,PT.Id) as [Networks] , null as SameOrganization, null, 1 as [Order]   from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected11 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName12 as Pkey,RD.ContactName, MapContactName12   as [ContactB], PT.Id as [ParticipantId] ,survey.ContactNetwork(MapContactName12,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected12 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName13 as Pkey,RD.ContactName, MapContactName13   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName13,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected13 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName14 as Pkey,RD.ContactName, MapContactName14   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName14,PT.Id) as [Networks], null as SameOrganization, null, 1 as [Order]   from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected14 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName15 as Pkey,RD.ContactName, MapContactName15   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName15,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected15 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName16 as Pkey,RD.ContactName, MapContactName16   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName16,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected16 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName17 as Pkey,RD.ContactName, MapContactName17   as [ContactB], PT.Id as [ParticipantId] ,survey.ContactNetwork(MapContactName17,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected17 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName18 as Pkey,RD.ContactName, MapContactName18   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName18,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected18 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName19 as Pkey,RD.ContactName, MapContactName19   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName19,PT.Id) as [Networks]  , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected19 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName20 as Pkey,RD.ContactName, MapContactName20   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName20,PT.Id) as [Networks] , null as SameOrganization, null, 1 as [Order]   from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected20 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName21 as Pkey,RD.ContactName, MapContactName21   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName21,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected21 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName22 as Pkey,RD.ContactName, MapContactName22   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName22,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected22 = 1
Union
Select cast(PT.Id as Varchar)+RD.ContactName+MapContactName23 as Pkey,RD.ContactName, MapContactName23   as [ContactB], PT.Id as [ParticipantId],survey.ContactNetwork(MapContactName23,PT.Id) as [Networks] , null as SameOrganization, null , 1 as [Order]  from core.Participant PT
join survey.NetworkInfoContacts NIC on NIC.ParticipantId = PT.Id
join survey.RelationshipDensity RD on PT.Id = RD.ParticipantId
where RD.DensitySelected23 = 1

