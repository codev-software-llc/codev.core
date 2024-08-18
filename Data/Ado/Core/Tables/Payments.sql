-------------------------------------------------------------------------------
-- This table is used to manage payments for timecog subscriptions.
-------------------------------------------------------------------------------
CREATE TABLE [core].[Payments]
(
    [RowId]           INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]      TIMESTAMP           NOT NULL,
    [IsActive]        BIT                 NOT NULL,
    [Flags]           BIGINT              NOT NULL,
    [DateCreated]     DATETIME            NOT NULL,
    [DateModified]    DATETIME            NOT NULL,
    [PaymentMethodId] INT                 NOT NULL,
    [OrderNumber]     NVARCHAR(64)        NOT NULL,
    [PaymentStatus]   INT                 NOT NULL,
    [CurrencyCode]    NVARCHAR(3)         NOT NULL,
    [Amount]          DECIMAL(18,2)       NOT NULL
)
GO

ALTER TABLE [core].[Payments]
    ADD CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[Payments]
    ADD CONSTRAINT [FK_Payments_PaymentMethods] FOREIGN KEY ([PaymentMethodId]) REFERENCES [core].[PaymentMethods] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO