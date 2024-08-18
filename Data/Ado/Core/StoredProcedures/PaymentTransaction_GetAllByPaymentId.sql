-------------------------------------------------------------------------------
-- This will return the list of all payment transactions associated with a 
-- payment.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentTransaction_GetAllByPaymentId]
(
    @PaymentId INT
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
        e.[PaymentId]        as 'PaymentId'       ,
        e.[TransactionType]  as 'TransactionType' ,
        e.[IsSuccess]        as 'IsSuccess'       ,
        e.[SerializedData]   as 'SerializedData'
     FROM [core].[PaymentTransactions] e
     WHERE (e.[PaymentId] = @PaymentId) AND (e.[IsActive] = 1)

END
