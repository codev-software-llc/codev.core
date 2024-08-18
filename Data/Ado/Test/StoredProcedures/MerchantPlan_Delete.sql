-------------------------------------------------------------------------------
-- This will delete the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantPlan_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[MerchantPlans] SET [IsActive] = 0 WHERE ([RowId] = @RowId)
    
END
