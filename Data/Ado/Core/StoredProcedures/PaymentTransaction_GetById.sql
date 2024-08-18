-------------------------------------------------------------------------------
-- This will return the payment transaction for by its identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentTransaction_GetById]
(
    @RowId INT
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
     WHERE (e.[RowId] = @RowId) AND (e.[IsActive] = 1)

END
