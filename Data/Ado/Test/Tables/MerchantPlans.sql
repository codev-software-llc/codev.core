CREATE TABLE [core].[MerchantPlans] 
(
    [RowId]         INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]    TIMESTAMP           NOT NULL,
    [IsActive]      BIT                 NOT NULL,
    [Flags]         BIGINT              NOT NULL,
    [DateCreated]   DATETIME            NOT NULL,
    [DateModified]  DATETIME            NOT NULL,
    [Source]        NVARCHAR(128)       NOT NULL,
    [Name]          NVARCHAR(128)       NOT NULL,
    [Interval]      INT                 NOT NULL,
    [IntervalCount] INT                 NOT NULL,
    [CurrencyCode]  NVARCHAR(3)         NOT NULL,
    [Amount]        DECIMAL(18,2)       NOT NULL,
    [TrialDays]     INT                 NOT NULL
)
GO

ALTER TABLE [core].[MerchantPlans]
    ADD CONSTRAINT [PK_MerchantPlans] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO
