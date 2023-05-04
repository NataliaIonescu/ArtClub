SET IDENTITY_INSERT [dbo].[Resources] ON
INSERT INTO [dbo].[Resources] ([Id], [Name], [Description], [Price]) VALUES (1, N'Exhibition hall', N'Exhibition hall', CAST(250.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[Resources] ([Id], [Name], [Description], [Price]) VALUES (2, N'Conference room', N'Conference room', CAST(200.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Resources] OFF
