-------------------------------------------------------------------------------
-- This will permanently remove all entities from the repository.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ErrorLog_PurgeAll]
AS
BEGIN

    DELETE FROM [core].[ErrorLog]

END
