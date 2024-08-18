-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[EnumType_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[EnumTypes] WHERE [RowId] = @RowId

END
