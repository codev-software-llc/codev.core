-------------------------------------------------------------------------------
-- This will add a new merchant transaction.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantTransaction_Add]
(
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

    INSERT INTO [core].[MerchantTransactions]
        (
            [IsActive]        ,
            [Flags]           ,
            [DateCreated]     ,
            [DateModified]    ,
            [TransactionGroup],
            [TransactionType] ,
            [CurrencyCode]    ,
            [Amount]          ,
            [OrderNumber]     ,
            [CardId]
        )
    VALUES
        (
            1                ,
            @Flags           ,
            @DateCreated     ,
            @DateModified    ,
            @TransactionGroup,
            @TransactionType ,
            @CurrencyCode    ,
            @Amount          ,
            @OrderNumber     ,
            @CardId
        )
    
    SELECT [RowId], [RowVersion] FROM [core].[MerchantTransactions] WHERE [RowId] = @@IDENTITY  

END
