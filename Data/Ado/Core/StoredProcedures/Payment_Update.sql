-------------------------------------------------------------------------------
-- This will update the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Payment_Update]
(  
    @RowId           INT          ,
    @RowVersion      TIMESTAMP    ,
    @Flags           BIGINT       ,
    @DateCreated     DATETIME     ,
    @DateModified    DATETIME     ,
    @PaymentMethodId INT          ,
    @OrderNumber     NVARCHAR(64) ,
    @PaymentStatus   INT          ,
    @CurrencyCode    NVARCHAR(3)  ,
    @Amount          DECIMAL(18,2)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[Payments] SET
        [Flags]           = @Flags          ,
        [DateCreated]     = @DateCreated    ,
        [DateModified]    = @DateModified   ,
        [PaymentMethodId] = @PaymentMethodId,
        [OrderNumber]     = @OrderNumber    ,
        [PaymentStatus]   = @PaymentStatus  ,
        [CurrencyCode]    = @CurrencyCode   ,
        [Amount]          = @Amount
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Payments] WHERE [RowId] = @RowId

END