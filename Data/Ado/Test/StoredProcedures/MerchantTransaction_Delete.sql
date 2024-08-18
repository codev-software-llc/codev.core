-------------------------------------------------------------------------------
-- This will delete the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantTransaction_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[MerchantTransactions] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
