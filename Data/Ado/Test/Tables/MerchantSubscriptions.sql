CREATE TABLE [core].[MerchantSubscriptions] 
(
    [RowId]        INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]   TIMESTAMP           NOT NULL,
    [IsActive]     BIT                 NOT NULL,
    [Flags]        BIGINT              NOT NULL,
    [DateCreated]  DATETIME            NOT NULL,
    [DateModified] DATETIME            NOT NULL,
    [PlanId]       INT                 NOT NULL,
    [CustomerId]   INT                 NOT NULL
)
GO

ALTER TABLE [core].[MerchantSubscriptions]
    ADD CONSTRAINT [PK_MerchantSubscriptions] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[MerchantSubscriptions]
    ADD CONSTRAINT [FK_MerchantSubscriptions_MerchantPlans] FOREIGN KEY ([PlanId]) REFERENCES [core].[MerchantPlans] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO

ALTER TABLE [core].[MerchantSubscriptions]
    ADD CONSTRAINT [FK_MerchantSubscriptions_MerchantCustomers] FOREIGN KEY ([CustomerId]) REFERENCES [core].[MerchantCustomers] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO