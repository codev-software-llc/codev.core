-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Subscription_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[Subscriptions] WHERE [RowId] = @RowId

END
