-------------------------------------------------------------------------------
-- This will delete the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantSubscription_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[MerchantSubscriptions] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
