-------------------------------------------------------------------------------
-- This will look for merchant transactions by a grouping identifier.

-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantTransaction_GetAllByGroupId]
(
    @TransactionGroup UNIQUEIDENTIFIER
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]            as 'RowId'           ,
        e.[RowVersion]       as 'RowVersion'      ,
        e.[IsActive]         as 'IsActive'        ,
        e.[Flags]            as 'Flags'           ,
        e.[DateCreated]      as 'DateCreated'     ,
        e.[DateModified]     as 'DateModified'    ,
        e.[TransactionGroup] as 'TransactionGroup',
        e.[TransactionType]  as 'TransactionType' ,
        e.[CurrencyCode]     as 'CurrencyCode'    ,
        e.[Amount]           as 'Amount'          ,
        e.[OrderNumber]      as 'OrderNumber'     ,
        e.[CardId]           as 'CardId'
    FROM [core].[MerchantTransactions] e
    WHERE (e.[TransactionGroup] = @TransactionGroup) AND (e.[IsActive] = 1)

END
