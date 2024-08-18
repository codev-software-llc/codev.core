CREATE TABLE [core].[MerchantTransactions]
(
    [RowId]            INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]       TIMESTAMP           NOT NULL,
    [IsActive]         BIT                 NOT NULL,
    [Flags]            BIGINT              NOT NULL,
    [DateCreated]      DATETIME            NOT NULL,
    [DateModified]     DATETIME            NOT NULL,
    [TransactionGroup] UNIQUEIDENTIFIER    NOT NULL,
    [TransactionType]  INT                 NOT NULL,
    [CurrencyCode]     NVARCHAR(3)         NOT NULL,
    [Amount]           DECIMAL(18,2)       NOT NULL,
    [OrderNumber]      NVARCHAR(64)        NOT NULL,
    [CardId]           INT                 NOT NULL
)
GO

ALTER TABLE [core].[MerchantTransactions]
    ADD CONSTRAINT [PK_MerchantTransactions] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[MerchantTransactions]
    ADD CONSTRAINT [FK_MerchantTransactions_MerchantCards] FOREIGN KEY ([CardId]) REFERENCES [core].[MerchantCards] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO