-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Destination_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[Destinations] WHERE [RowId] = @RowId

END
