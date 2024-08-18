-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Session_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[Sessions] WHERE [RowId] = @RowId

END
