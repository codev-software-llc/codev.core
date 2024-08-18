-------------------------------------------------------------------------------
-- This will retrieve all payments.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Payment_GetAll]
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
     WHERE (e.[IsActive] = 1)
          
END
