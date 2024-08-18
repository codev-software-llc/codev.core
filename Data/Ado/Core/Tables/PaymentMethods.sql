-------------------------------------------------------------------------------
-- This table is used to manage payment methods for accounts.
-------------------------------------------------------------------------------
CREATE TABLE [core].[PaymentMethods]
(
    [RowId]            INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]       TIMESTAMP           NOT NULL,
    [IsActive]         BIT                 NOT NULL,
    [Flags]            BIGINT              NOT NULL,
    [DateCreated]      DATETIME            NOT NULL,
    [DateModified]     DATETIME            NOT NULL,
    [Expiration]       NVARCHAR(16)        NOT NULL,
    [OffuscatedNumber] NVARCHAR(32)        NOT NULL,
    [IdentityId]       INT                 NOT NULL,
    [SerializedData]   NVARCHAR(MAX)       NOT NULL
)
GO

ALTER TABLE [core].[PaymentMethods]
    ADD CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[PaymentMethods]
    ADD CONSTRAINT [FK_PaymentMethods_Identities] FOREIGN KEY ([IdentityId]) REFERENCES [core].[Identities] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO