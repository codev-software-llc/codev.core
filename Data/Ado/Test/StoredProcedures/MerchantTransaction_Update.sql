-------------------------------------------------------------------------------
-- This will update the transaction information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantTransaction_Update]
(
    @RowId            INT             ,
    @RowVersion       TIMESTAMP       ,
    @Flags            BIGINT          ,
    @DateCreated      DATETIME        ,
    @DateModified     DATETIME        ,
    @TransactionGroup UNIQUEIDENTIFIER,
    @TransactionType  INT             ,
    @CurrencyCode     NVARCHAR(3)     ,
    @Amount           DECIMAL(18,2)   ,
    @OrderNumber      NVARCHAR(64)    ,
    @CardId           INT
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[MerchantTransactions] SET
        [Flags]            = @Flags           ,
        [DateCreated]      = @DateCreated     ,
        [DateModified]     = @DateModified    ,
        [TransactionGroup] = @TransactionGroup,
        [TransactionType]  = @TransactionType ,
        [CurrencyCode]     = @CurrencyCode    ,
        [Amount]           = @Amount          ,
        [OrderNumber]      = @OrderNumber     ,
        [CardId]           = @CardId
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId], [RowVersion] FROM [core].[MerchantTransactions] WHERE [RowId] = @RowId

END
