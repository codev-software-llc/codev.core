-------------------------------------------------------------------------------
-- User table for managing user sessions.
-------------------------------------------------------------------------------
CREATE TABLE [core].[Sessions]
(
    [RowId]          INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]     TIMESTAMP           NOT NULL,
    [IsActive]       BIT                 NOT NULL,
    [Flags]          BIGINT              NOT NULL,
    [DateCreated]    DATETIME            NOT NULL,
    [DateModified]   DATETIME            NOT NULL,
    [IdentityId]     INT                 NOT NULL,
    [DateExpiration] DATETIME            NOT NULL,
    [SessionId]      UNIQUEIDENTIFIER    NOT NULL,
    [Secret]         NVARCHAR(64)        NOT NULL
)                                       
GO

ALTER TABLE [core].[Sessions]
    ADD CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[Sessions]
    ADD CONSTRAINT [FK_Sessions_Identities] FOREIGN KEY ([IdentityId]) REFERENCES [core].[Identities] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
