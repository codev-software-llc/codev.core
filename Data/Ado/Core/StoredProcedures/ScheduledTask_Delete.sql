-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ScheduledTask_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[ScheduledTasks] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
