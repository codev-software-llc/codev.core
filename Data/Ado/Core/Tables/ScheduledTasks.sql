-------------------------------------------------------------------------------
--This table supports the Scheduled task scheduling.
-------------------------------------------------------------------------------
CREATE TABLE [core].[ScheduledTasks]
(
    [RowId]        INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]   TIMESTAMP           NOT NULL,
    [IsActive]     BIT                 NOT NULL,
    [Flags]        BIGINT              NOT NULL,
    [DateCreated]  DATETIME            NOT NULL,
    [DateModified] DATETIME            NOT NULL,
    [IdentityId]   INT                 NOT NULL,
    [AttentionAt]  DATETIME            NOT NULL,
    [Priority]     INT                 NOT NULL,
    [Category]     NVARCHAR(128)       NOT NULL,
    [DetailType]   NVARCHAR(64)        NOT NULL,
    [Detail]       NVARCHAR(2048)      NOT NULL
)                                       
GO

ALTER TABLE [core].[ScheduledTasks]
    ADD CONSTRAINT [PK_ScheduledTasks] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[ScheduledTasks]
    ADD CONSTRAINT [FK_ScheduledTasks_Identities] FOREIGN KEY ([IdentityId]) REFERENCES [core].[Identities] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO

