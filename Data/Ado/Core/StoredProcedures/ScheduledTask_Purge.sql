-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ScheduledTask_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[ScheduledTasks] WHERE [RowId] = @RowId

END
