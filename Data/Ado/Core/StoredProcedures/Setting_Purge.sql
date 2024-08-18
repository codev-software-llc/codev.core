-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Setting_Purge]
(
    @RowId INT
)
AS
BEGIN
    
    DELETE FROM [core].[Settings] WHERE [RowId] = @RowId
    
END
