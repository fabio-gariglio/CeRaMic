﻿CREATE TABLE [dbo].[Relations]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[ReferentId] UNIQUEIDENTIFIER NOT NULL,
	[ClientId] UNIQUEIDENTIFIER NOT NULL,
	[OwnerId] UNIQUEIDENTIFIER NOT NULL,
	[PartnerId] UNIQUEIDENTIFIER NULL,
	[ReferentName] VARCHAR(MAX) NULL,
	[ClientName] VARCHAR(MAX) NULL,
	[OwnerName] VARCHAR(MAX) NULL,
	[PartnerName] VARCHAR(MAX) NULL,
	[NotesJson] VARCHAR(MAX) NULL,
	[EmailAddress] NVARCHAR(255) NULL,
	[LandlineNumber] VARCHAR(50) NULL,
	[MobilePhone] VARCHAR(50) NULL,
	[Priority] TINYINT NOT NULL DEFAULT 0
)
