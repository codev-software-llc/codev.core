-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Communication_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[Communications] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
