-------------------------------------------------------------------------------
-- This will add a new payment.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Payment_Add]
(
    @Flags           BIGINT      ,
    @DateCreated     DATETIME    ,
    @DateModified    DATETIME    ,
    @PaymentMethodId INT         ,
    @OrderNumber     NVARCHAR(64),
    @PaymentStatus   INT         ,
    @CurrencyCode    NVARCHAR(3) ,
    @Amount          DECIMAL(18,2)
)
AS 
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    INSERT INTO [core].[Payments]
        (
            [IsActive]       ,
            [Flags]          ,
            [DateCreated]    ,
            [DateModified]   ,
            [PaymentMethodId],
            [OrderNumber]    ,
            [PaymentStatus]  ,
            [CurrencyCode]   ,
            [Amount]    
        )
    VALUES
        (
            1               ,
            @Flags          ,
            @DateCreated    ,
            @DateModified   ,
            @PaymentMethodId,
            @OrderNumber    ,
            @PaymentStatus  ,
            @CurrencyCode   ,
            @Amount
        ) 

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Payments] WHERE [RowId] = @@IDENTITY

END
