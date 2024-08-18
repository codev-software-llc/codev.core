-------------------------------------------------------------------------------
-- This is the table of identity destinations.
-------------------------------------------------------------------------------
CREATE TABLE [core].[Destinations]
(
    [RowId]                      INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]                 TIMESTAMP           NOT NULL,
    [IsActive]                   BIT                 NOT NULL,
    [Flags]                      BIGINT              NOT NULL,
    [DateCreated]                DATETIME            NOT NULL,
    [DateModified]               DATETIME            NOT NULL,
    [IdentityId]                 INT                 NOT NULL,
    [Address]                    NVARCHAR(512)       NOT NULL,
    [Type]                       INT                 NOT NULL,
    [ConfirmationSecret]         NVARCHAR(16)        NOT NULL,
    [ConfirmationExpirationDate] DATETIME            NOT NULL
)                                       
GO

ALTER TABLE [core].[Destinations]
    ADD CONSTRAINT [PK_Destinations] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[Destinations]
    ADD CONSTRAINT [FK_Destinations_Identities] FOREIGN KEY ([IdentityId]) REFERENCES [core].[Identities] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
