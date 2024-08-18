-------------------------------------------------------------------------------
-- This will look for an entity with the unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCustomer_GetBySubscriptionId]
(
    @SubscriptionId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]        as 'RowId'       ,
        e.[RowVersion]   as 'RowVersion'  ,
        e.[IsActive]     as 'IsActive'    ,
        e.[Flags]        as 'Flags'       ,
        e.[DateCreated]  as 'DateCreated' ,
        e.[DateModified] as 'DateModified',
        e.[Name]         as 'Name'        ,
        e.[EmailAddress] as 'EmailAddress'
    FROM [core].[MerchantCustomers] e
        JOIN [core].[MerchantSubscriptions] s on s.[CustomerId] = e.[RowId]
    WHERE (s.[RowId] = @SubscriptionId) AND (e.[IsActive] = 1)

END
