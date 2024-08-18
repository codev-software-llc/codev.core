-------------------------------------------------------------------------------
-- Blob storage for large non-structured data.
-------------------------------------------------------------------------------
CREATE TABLE [core].[BlobContents]
(
    [RowId]        INT IDENTITY (1, 1) NOT NULL,
    [RowVersion]   TIMESTAMP           NOT NULL,
    [IsActive]     BIT                 NOT NULL,
    [Flags]        BIGINT              NOT NULL,
    [DateCreated]  DATETIME            NOT NULL,
    [DateModified] DATETIME            NOT NULL,
    [BlobId]       INT                 NOT NULL,
    [MimeType]     NVARCHAR(64)        NOT NULL,
    [Size]         BIGINT              NOT NULL,
    [Content]      IMAGE                   NULL
)                                       
GO

ALTER TABLE [core].[BlobContents]
    ADD CONSTRAINT [PK_BlobContents] PRIMARY KEY CLUSTERED ([RowId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO

ALTER TABLE [core].[BlobContents]
    ADD CONSTRAINT [FK_BlobContents_Blobs] FOREIGN KEY ([BlobId]) REFERENCES [core].[Blobs] ([RowId]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
