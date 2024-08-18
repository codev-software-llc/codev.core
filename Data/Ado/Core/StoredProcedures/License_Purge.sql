-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[License_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[Licenses] WHERE [RowId] = @RowId

END
