-------------------------------------------------------------------------------
-- This will permanently remove all blobs associated to the identity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Blob_PurgeAllByIdentityId]
(
    @IdentityId INT
)
AS
BEGIN

    DELETE [core].[BlobContents]
    FROM [core].[BlobContents] c
        JOIN [core].[Blobs] b on b.[RowId] = c.[BlobId]
    WHERE b.[IdentityId] = @IdentityId

    DELETE FROM [core].[Blobs] WHERE [IdentityId] = @IdentityId

END
