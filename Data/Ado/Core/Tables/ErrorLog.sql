-------------------------------------------------------------------------------
-- Diagnostics table for tracking errors and such.
-------------------------------------------------------------------------------
CREATE TABLE [core].[ErrorLog]
(
    [RowId]         INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]    TIMESTAMP           NOT NULL,
    [IsActive]      BIT                 NOT NULL,
    [Flags]         BIGINT              NOT NULL,
    [DateCreated]   DATETIME            NOT NULL,
    [DateModified]  DATETIME            NOT NULL,
    [IdentityId]    INT                 NOT NULL,
    [ComponentType] INT                 NOT NULL,
    [SeverityType]  INT                 NOT NULL,
    [Message]       NVARCHAR(2048)      NOT NULL,
    [ServerName]    NVARCHAR(128)       NOT NULL,
    [TrackingTag]   NVARCHAR(256)       NOT NULL,
    [StackTrace]    NVARCHAR(2048)          NULL
)                                       
GO

ALTER TABLE [core].[ErrorLog]
    ADD CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[ErrorLog]
    ADD CONSTRAINT [FK_ErrorLog_Identities] FOREIGN KEY ([IdentityId]) REFERENCES [core].[Identities] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO