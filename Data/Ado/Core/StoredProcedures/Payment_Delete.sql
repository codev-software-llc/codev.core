--------------------------------------------------------------------------------
-- This will delete the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Payment_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[Payments] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END

