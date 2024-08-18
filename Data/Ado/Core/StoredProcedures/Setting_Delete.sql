-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Setting_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[Settings] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
