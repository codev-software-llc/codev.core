-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[BlobContent_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[BlobContents] WHERE [RowId] = @RowId

END
