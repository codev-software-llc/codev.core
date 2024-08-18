-------------------------------------------------------------------------------
-- User table for managing users.
-------------------------------------------------------------------------------
CREATE TABLE [core].[Identities]
(
    [RowId]                INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]           TIMESTAMP           NOT NULL,
    [IsActive]             BIT                 NOT NULL,
    [Flags]                BIGINT              NOT NULL,
    [DateCreated]          DATETIME            NOT NULL,
    [DateModified]         DATETIME            NOT NULL,
    [ConfirmationAttempts] INT                 NOT NULL,
    [DateTimeZoneId]       NVARCHAR(128)       NOT NULL,
    [SerializedData]       NVARCHAR(MAX)       NOT NULL
)
GO

ALTER TABLE [core].[Identities]
    ADD CONSTRAINT [PK_Identities] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO