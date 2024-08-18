-------------------------------------------------------------------------------
-- Licensing information for applications.
-------------------------------------------------------------------------------
CREATE TABLE [core].[Licenses]
(
    [RowId]          INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]     TIMESTAMP           NOT NULL,
    [IsActive]       BIT                 NOT NULL,
    [Flags]          BIGINT              NOT NULL,
    [DateCreated]    DATETIME            NOT NULL,
    [DateModified]   DATETIME            NOT NULL,
    [IdentityId]     INT                 NOT NULL,
    [Application]    NVARCHAR(128)       NOT NULL,
    [Name]           NVARCHAR(128)       NOT NULL,
    [CurrencyCode]   NVARCHAR(3)         NOT NULL,
    [Amount]         DECIMAL(18,2)       NOT NULL,
    [Features]       NVARCHAR(2048)      NOT NULL,
    [SerializedData] NVARCHAR(MAX)       NOT NULL
)                                       
GO

ALTER TABLE [core].[Licenses]
    ADD CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[Licenses]
    ADD CONSTRAINT [FK_Licenses_Identities] FOREIGN KEY ([IdentityId]) REFERENCES [core].[Identities] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO