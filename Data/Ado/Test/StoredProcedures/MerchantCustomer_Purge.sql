-------------------------------------------------------------------------------
-- This will permanently remove an entity from the repository.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCustomer_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[MerchantTransactions]
    FROM [core].[MerchantTransactions] t
        JOIN [core].[MerchantCards] c on c.[RowId] = t.[CardId]
    WHERE c.[CustomerId] = @RowId

    DELETE FROM [core].[MerchantSubscriptions] WHERE [CustomerId] = @RowId
    DELETE FROM [core].[MerchantCards]         WHERE [CustomerId] = @RowId
    DELETE FROM [core].[MerchantCustomers]     WHERE [RowId]      = @RowId

END
