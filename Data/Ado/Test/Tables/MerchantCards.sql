CREATE TABLE [core].[MerchantCards] 
(
    [RowId]           INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]      TIMESTAMP           NOT NULL,
    [IsActive]        BIT                 NOT NULL,
    [Flags]           BIGINT              NOT NULL,
    [DateCreated]     DATETIME            NOT NULL,
    [DateModified]    DATETIME            NOT NULL,
    [Name]            NVARCHAR(128)       NOT NULL,
    [Type]            NVARCHAR(16)        NOT NULL,
    [Number]          NVARCHAR(32)        NOT NULL,
    [Code]            NVARCHAR(16)        NOT NULL,
    [ExpirationYear]  INT                 NOT NULL,
    [ExpirationMonth] INT                 NOT NULL,
    [CustomerId]      INT                 NOT NULL
)
GO

ALTER TABLE [core].[MerchantCards]
    ADD CONSTRAINT [PK_MerchantCards] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[MerchantCards]
    ADD CONSTRAINT [FK_MerchantCards_MerchantCustomers] FOREIGN KEY ([CustomerId]) REFERENCES [core].[MerchantCustomers] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO