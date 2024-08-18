-------------------------------------------------------------------------------
-- This will permanently remove an entity from the repository.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantPlan_Purge]
(
    @RowId INT
)
AS
BEGIN
    
    DELETE FROM [core].[MerchantPlans] WHERE [RowId] = @RowId
    
END
