-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ServiceLink_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[ServiceLinks] WHERE [RowId] = @RowId

END
