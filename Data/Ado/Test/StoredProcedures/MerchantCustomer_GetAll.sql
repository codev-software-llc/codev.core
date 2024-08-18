-------------------------------------------------------------------------------
-- This will retriev all entities for this type.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCustomer_GetAll]
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
    WHERE (e.[IsActive] = 1)

END
