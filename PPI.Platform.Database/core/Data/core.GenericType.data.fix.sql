
SET IDENTITY_INSERT [core].[GenericType] ON
INSERT INTO [core].[GenericType] ([Id], [Name]) VALUES (20, N'PersonGroupEmail_SendStatusType')
SET IDENTITY_INSERT [core].[GenericType] OFF

SET IDENTITY_INSERT [core].[Resource] ON
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1239, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1240, N'GenericTypeValue')
INSERT INTO [core].[Resource] ([Id], [TableName]) VALUES (1241, N'GenericTypeValue')
SET IDENTITY_INSERT [core].[Resource] OFF



SET IDENTITY_INSERT [core].[ResourceValue] ON
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1238, 1239, 1, N'Inprocess')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1239, 1240, 1, N'Complete')
INSERT INTO [core].[ResourceValue] ([Id], [ResourceId], [CultureId], [Value]) VALUES (1240, 1241, 1, N'Errors')
SET IDENTITY_INSERT [core].[ResourceValue] OFF


SET IDENTITY_INSERT [core].[GenericTypeValue] ON
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1236, 20, 1239)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1237, 20, 1240)
INSERT INTO [core].[GenericTypeValue] ([Id], [GenericTypeId], [ResourceId]) VALUES (1238, 20, 1241)
SET IDENTITY_INSERT [core].[GenericTypeValue] OFF
