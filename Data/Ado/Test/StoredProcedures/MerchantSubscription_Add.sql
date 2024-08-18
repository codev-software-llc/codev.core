-------------------------------------------------------------------------------
-- This will add a new entity to the repository.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantSubscription_Add]
(
    @Flags        BIGINT  ,
    @DateCreated  DATETIME,
    @DateModified DATETIME,
    @PlanId       INT     ,
    @CustomerId   INT
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[MerchantSubscriptions]
        (           
            [IsActive]    ,
            [Flags]       ,
            [DateCreated] ,
            [DateModified],
            [PlanId]      ,
            [CustomerId]
        )
    VALUES
        (
            1            ,
            @Flags       ,
            @DateCreated ,
            @DateModified,
            @PlanId      ,
            @CustomerId
        )

    SELECT [RowId], [RowVersion] FROM [core].[MerchantSubscriptions] WHERE [RowId] = @@IDENTITY

END
