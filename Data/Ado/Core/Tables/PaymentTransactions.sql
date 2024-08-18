-------------------------------------------------------------------------------
-- This table is used for tracking payment transactions for a payment.
-------------------------------------------------------------------------------
CREATE TABLE [core].[PaymentTransactions]
(
    [RowId]           INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]      TIMESTAMP           NOT NULL,
    [IsActive]        BIT                 NOT NULL,
    [Flags]           BIGINT              NOT NULL,
    [DateCreated]     DATETIME            NOT NULL,
    [DateModified]    DATETIME            NOT NULL,
    [PaymentId]       INT                 NOT NULL,
    [TransactionType] INT                 NOT NULL,
    [IsSuccess]       BIT                 NOT NULL,
    [SerializedData]  NVARCHAR(MAX)       NOT NULL
)
GO

ALTER TABLE [core].[PaymentTransactions]
    ADD CONSTRAINT [PK_PaymentTransactions] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[PaymentTransactions]
    ADD CONSTRAINT [FK_PaymentTransactions_Payments] FOREIGN KEY ([PaymentId]) REFERENCES [core].[Payments] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO