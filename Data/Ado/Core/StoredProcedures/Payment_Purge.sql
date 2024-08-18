-------------------------------------------------------------------------------
-- This will permanently remove the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Payment_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[Payments] WHERE [RowId] = @RowId

END
