-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ErrorLog_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[ErrorLog] WHERE [RowId] = @RowId

END
