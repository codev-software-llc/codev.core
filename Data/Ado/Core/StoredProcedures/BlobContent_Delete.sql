-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[BlobContent_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[BlobContents] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
