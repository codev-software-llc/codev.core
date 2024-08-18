-------------------------------------------------------------------------------
-- This will delete the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCustomer_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[MerchantCustomers]     SET [IsActive] = 0 WHERE ([RowId]      = @RowId)
	UPDATE [core].[MerchantCards]         SET [IsActive] = 0 WHERE ([CustomerId] = @RowId)
	UPDATE [core].[MerchantSubscriptions] SET [IsActive] = 0 WHERE ([CustomerId] = @RowId)

    UPDATE [core].[MerchantTransactions]
        SET [IsActive] = 0
    FROM [core].[MerchantTransactions] t
        JOIN [core].[MerchantCards] c on c.[RowId] = t.[CardId]
    WHERE (c.[CustomerId] = @RowId)

END
