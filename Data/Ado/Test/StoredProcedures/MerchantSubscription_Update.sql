-------------------------------------------------------------------------------
-- This will update the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantSubscription_Update]
(
    @RowId        INT      ,
    @RowVersion   TIMESTAMP,
    @Flags        BIGINT   ,
    @DateCreated  DATETIME ,
    @DateModified DATETIME ,
    @PlanId       INT      ,
    @CustomerId   INT
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[MerchantSubscriptions] SET
        [Flags]        = @Flags       ,
        [DateCreated]  = @DateCreated ,
        [DateModified] = @DateModified,
        [PlanId]       = @PlanId      ,
        [CustomerId]   = @CustomerId
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId], [RowVersion] FROM [core].[MerchantSubscriptions] WHERE [RowId] = @RowId

END
