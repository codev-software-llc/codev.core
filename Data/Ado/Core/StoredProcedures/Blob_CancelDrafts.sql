-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Blob_CancelDrafts]
(
    @BlobId INT
)
AS
BEGIN

    DELETE FROM [core].[BlobContents] WHERE ([BlobId] = @BlobId) AND (([Flags] & 1) = 0)

END
