-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ErrorLog_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[ErrorLog] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
