-------------------------------------------------------------------------------
-- This table is used to map Enumerations across both the C# and Database 
-- layers.
-------------------------------------------------------------------------------
CREATE TABLE [core].[EnumTypes]
(
    [RowId]        INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]   TIMESTAMP           NOT NULL,
    [IsActive]     BIT                 NOT NULL,
    [Flags]        BIGINT              NOT NULL,
    [DateCreated]  DATETIME            NOT NULL,
    [DateModified] DATETIME            NOT NULL,
    [IsFlag]       BIT                 NOT NULL,
    [IsBig]        BIT                 NOT NULL,
    [Application]  NVARCHAR(128)       NOT NULL,
    [Name]         NVARCHAR(64)        NOT NULL,
    [EnumKey]      NVARCHAR(64)        NOT NULL,
    [Value]        BIGINT              NOT NULL,
    [Comment]      NVARCHAR(128)       NOT NULL
)
GO

ALTER TABLE [core].[EnumTypes]
    ADD CONSTRAINT [PK_EnumTypes] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_EnumTypes_ApplicationNameKey]
    ON [core].[EnumTypes]([Application], [Name], [EnumKey] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];
GO