--new generic types
SET IDENTITY_INSERT [core].[GenericType] ON
INSERT INTO [core].[GenericType] ([Id], [Name]) VALUES (18, N'NetworkRelationshipDemo_RegionType')
INSERT INTO [core].[GenericType] ([Id], [Name]) VALUES (19, N'ParticipantProfile_NetworkPercentType')
SET IDENTITY_INSERT [core].[GenericType] OFF
--

-- new resournces
SET IDENTITY_INSERT [core].[Resource] ON
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1217, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1218, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1219, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1220, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1221, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1222, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1223, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1224, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1225, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1226, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1227, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1228, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1229, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1230, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1231, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1232, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1233, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1234, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1235, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1236, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1237, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1238, N'GenericTypeValue')
SET IDENTITY_INSERT [core].[Resource] OFF
-- And values
SET IDENTITY_INSERT [core].[ResourceValue] ON
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1218, 1220, 1, N'Asia')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1219, 1221, 1, N'Africa')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1220, 1222, 1, N'Central America')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1221, 1223, 1, N'Europe')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1222, 1224, 1, N'Midlle East')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1223, 1225, 1, N'North America')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1224, 1226, 1, N'Oceania')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1225, 1227, 1, N'South America')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1226, 1228, 1, N'55 - 64')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1228, 1229, 1, N'65+')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1229, 1230, 1, N'0-10%')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1230, 1231, 1, N'11-20%')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1231, 1232, 1, N'21-30%')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1232, 1233, 1, N'31-40%')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1233, 1234, 1, N'41-50%')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1234, 1235, 1, N'51-60%')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1235, 1236, 1, N'61-70%')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1236, 1237, 1, N'701-80%')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1237, 1238, 1, N'81-90%')
SET IDENTITY_INSERT [core].[ResourceValue] OFF

-- New type settings and addittions
SET IDENTITY_INSERT [core].[GenericTypeValue] ON
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1217, 18, 1220)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1218, 18, 1221)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1219, 18, 1222)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1220, 18, 1223)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1221, 18, 1224)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1222, 18, 1225)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1223, 18, 1226)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1224, 18, 1227)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1225, 13, 1228)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1226, 13, 1229)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1227, 19, 1230)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1228, 19, 1231)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1229, 19, 1232)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1230, 19, 1233)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1231, 19, 1234)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1232, 19, 1235)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1233, 19, 1236)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1234, 19, 1237)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1235, 19, 1238)
SET IDENTITY_INSERT [core].[GenericTypeValue] OFF
