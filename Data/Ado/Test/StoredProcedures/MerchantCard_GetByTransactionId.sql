-------------------------------------------------------------------------------
-- This will look for an entity with the unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCard_GetByTransactionId]
(
    @TransactionId INT
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
        e.[Name]            as 'Name'           ,
        e.[Type]            as 'Type'           ,
        e.[Number]          as 'Number'         ,
        e.[Code]            as 'Code'           ,
        e.[ExpirationYear]  as 'ExpirationYear' ,
        e.[ExpirationMonth] as 'ExpirationMonth',
        e.[CustomerId]      as 'CustomerId'
    FROM [core].[MerchantCards] e
        JOIN [core].[MerchantTransactions] t on t.[CardId] = e.[RowId]
    WHERE (t.[RowId] = @TransactionId) AND (e.[IsActive] = 1)

END
