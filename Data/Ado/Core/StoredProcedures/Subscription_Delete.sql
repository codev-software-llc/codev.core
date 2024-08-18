-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Subscription_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[Subscriptions] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
