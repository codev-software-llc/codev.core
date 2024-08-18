-------------------------------------------------------------------------------
-- This will delete the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentTransaction_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[PaymentTransactions] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
