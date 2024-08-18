CREATE TABLE [core].[MerchantCustomers] 
(
    [RowId]        INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]   TIMESTAMP           NOT NULL,
    [IsActive]     BIT                 NOT NULL,
    [Flags]        BIGINT              NOT NULL,
    [DateCreated]  DATETIME            NOT NULL,
    [DateModified] DATETIME            NOT NULL,
    [Name]         NVARCHAR(128)       NOT NULL,
    [EmailAddress] NVARCHAR(1024)      NOT NULL
)
GO

ALTER TABLE [core].[MerchantCustomers]
    ADD CONSTRAINT [PK_MerchantCustomers] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO
