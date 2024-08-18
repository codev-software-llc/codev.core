-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Session_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[Sessions] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
