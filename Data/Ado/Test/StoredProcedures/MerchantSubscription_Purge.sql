-------------------------------------------------------------------------------
-- This will permanently remove an entity from the repository.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantSubscription_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[MerchantSubscriptions] WHERE [RowId] = @RowId

END
