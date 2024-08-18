-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Blob_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[Blobs] WHERE [RowId] = @RowId

END
