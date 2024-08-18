-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Communication_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[Communications] WHERE [RowId] = @RowId

END
