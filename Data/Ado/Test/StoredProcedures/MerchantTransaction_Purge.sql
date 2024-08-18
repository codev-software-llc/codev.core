-------------------------------------------------------------------------------
-- This will remove the transaction.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantTransaction_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[MerchantTransactions] WHERE [RowId] = @RowId

END
