-------------------------------------------------------------------------------
-- This will delete the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentMethod_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[PaymentMethods] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END

