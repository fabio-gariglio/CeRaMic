CREATE TABLE [dbo].[Snapshots] (
    [BucketId]       VARCHAR (40)    NOT NULL,
    [StreamId]       CHAR (40)       NOT NULL,
    [StreamRevision] INT             NOT NULL,
    [Payload]        VARBINARY (MAX) NOT NULL,
    CONSTRAINT [PK_Snapshots] PRIMARY KEY CLUSTERED ([BucketId] ASC, [StreamId] ASC, [StreamRevision] ASC),
    CHECK ([StreamRevision]>(0)),
    CHECK (datalength([Payload])>(0))
);

