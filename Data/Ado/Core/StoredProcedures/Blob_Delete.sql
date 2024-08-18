-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Blob_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[Blobs] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
