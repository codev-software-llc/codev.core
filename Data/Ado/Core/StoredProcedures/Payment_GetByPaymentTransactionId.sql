-------------------------------------------------------------------------------
-- This will return the payment from the transaction entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Payment_GetByPaymentTransactionId]
(
    @PaymentTransactionId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]           as 'RowId'          ,
        e.[RowVersion]      as 'RowVersion'     ,
        e.[IsActive]        as 'IsActive'       ,
        e.[Flags]           as 'Flags'          ,
        e.[DateCreated]     as 'DateCreated'    ,
        e.[DateModified]    as 'DateModified'   ,
        e.[PaymentMethodId] as 'PaymentMethodId',
        e.[OrderNumber]     as 'OrderNumber'    ,
        e.[PaymentStatus]   as 'PaymentStatus'  ,
        e.[CurrencyCode]    as 'CurrencyCode'   ,
        e.[Amount]          as 'Amount'
     FROM [core].[Payments] e
         JOIN [core].[PaymentTransactions] p on p.[PaymentId] = e.[RowId]
     WHERE (p.[RowId] = @PaymentTransactionId) AND (e.[IsActive] = 1)

END
