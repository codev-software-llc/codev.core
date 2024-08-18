-------------------------------------------------------------------------------
-- Licensing subscription information.
-------------------------------------------------------------------------------
CREATE TABLE [core].[Subscriptions]
(
    [RowId]          INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]     TIMESTAMP           NOT NULL,
    [IsActive]       BIT                 NOT NULL,
    [Flags]          BIGINT              NOT NULL,
    [DateCreated]    DATETIME            NOT NULL,
    [DateModified]   DATETIME            NOT NULL,
    [DateExpiration] DATETIME            NOT NULL,
    [LicenseId]      INT                 NOT NULL,
    [IdentityId]     INT                 NOT NULL,
    [SerializedData] NVARCHAR(MAX)       NOT NULL
)                                       
GO

ALTER TABLE [core].[Subscriptions]
    ADD CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[Subscriptions]
    ADD CONSTRAINT [FK_Subscriptions_Identities] FOREIGN KEY ([IdentityId]) REFERENCES [core].[Identities] ([RowId]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

ALTER TABLE [core].[Subscriptions]
    ADD CONSTRAINT [FK_Subscriptions_Licenses] FOREIGN KEY ([LicenseId]) REFERENCES [core].[Licenses] ([RowId]) ON DELETE NO ACTION ON UPDATE NO ACTION;
GO