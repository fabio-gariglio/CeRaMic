CREATE TABLE [dbo].[Commits] (
    [BucketId]         VARCHAR (40)     NOT NULL,
    [StreamId]         CHAR (40)        NOT NULL,
    [StreamIdOriginal] NVARCHAR (1000)  NOT NULL,
    [StreamRevision]   INT              NOT NULL,
    [Items]            TINYINT          NOT NULL,
    [CommitId]         UNIQUEIDENTIFIER NOT NULL,
    [CommitSequence]   INT              NOT NULL,
    [CheckpointNumber] BIGINT           IDENTITY (1, 1) NOT NULL,
    [Dispatched]       BIT              DEFAULT ((0)) NOT NULL,
    [Headers]          VARBINARY (MAX)  NULL,
    [Payload]          VARBINARY (MAX)  NOT NULL,
    [CommitStamp]      DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Commits] PRIMARY KEY CLUSTERED ([CheckpointNumber] ASC),
    CHECK ([CommitId]<>0x00),
    CHECK ([CommitSequence]>(0)),
    CHECK ([Headers] IS NULL OR datalength([Headers])>(0)),
    CHECK ([Items]>(0)),
    CHECK ([StreamRevision]>(0)),
    CHECK (datalength([Payload])>(0))
);


GO
CREATE NONCLUSTERED INDEX [IX_Commits_Stamp]
    ON [dbo].[Commits]([CommitStamp] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Commits_Dispatched]
    ON [dbo].[Commits]([Dispatched] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Commits_Revisions]
    ON [dbo].[Commits]([BucketId] ASC, [StreamId] ASC, [StreamRevision] ASC, [Items] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Commits_CommitId]
    ON [dbo].[Commits]([BucketId] ASC, [StreamId] ASC, [CommitId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Commits_CommitSequence]
    ON [dbo].[Commits]([BucketId] ASC, [StreamId] ASC, [CommitSequence] ASC);

