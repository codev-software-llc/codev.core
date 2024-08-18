-------------------------------------------------------------------------------
-- This is the table of communications for contact and support.
-------------------------------------------------------------------------------
CREATE TABLE [core].[Communications]
(
    [RowId]        INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]   TIMESTAMP           NOT NULL,
    [IsActive]     BIT                 NOT NULL,
    [Flags]        BIGINT              NOT NULL,
    [DateCreated]  DATETIME            NOT NULL,
    [DateModified] DATETIME            NOT NULL,
    [Application]  NVARCHAR(128)       NOT NULL,
    [Category]     NVARCHAR(128)       NOT NULL,
    [Subcategory]  NVARCHAR(128)       NOT NULL,
    [Name]         NVARCHAR(128)       NOT NULL,
    [EmailAddress] NVARCHAR(512)       NOT NULL,
    [Comments]     NVARCHAR(MAX)       NOT NULL
)                                       
GO

ALTER TABLE [core].[Communications]
    ADD CONSTRAINT [PK_Communications] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO