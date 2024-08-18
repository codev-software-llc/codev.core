CREATE TABLE [core].[ServiceLinks]
(
    [RowId]        INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]   TIMESTAMP           NOT NULL,
    [IsActive]     BIT                 NOT NULL,
    [Flags]        BIGINT              NOT NULL,
    [DateCreated]  DATETIME            NOT NULL,
    [DateModified] DATETIME            NOT NULL,
    [IdentityId]   INT                 NOT NULL,
    [TinyUrl]      NVARCHAR(64)        NOT NULL,
    [DetailType]   NVARCHAR(64)        NOT NULL,
    [Detail]       NVARCHAR(2048)      NOT NULL
);
GO

ALTER TABLE [core].[ServiceLinks]
    ADD CONSTRAINT [PK_ServiceLinks] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[ServiceLinks]
    ADD CONSTRAINT [FK_ServiceLinks_Identities] FOREIGN KEY ([IdentityId]) REFERENCES [core].[Identities] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_ServiceLink_TinyUrl]
    ON [core].[ServiceLinks]([TinyUrl] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];
GO