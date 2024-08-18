-------------------------------------------------------------------------------
-- This will look for an entity with the unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantSubscription_GetAllByCustomerId]
(
    @CustomerId INT
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
        e.[PlanId]       as 'PlanId'      ,
        e.[CustomerId]   as 'CustomerId'
    FROM [core].[MerchantSubscriptions] e
    WHERE (e.[CustomerId] = @CustomerId) AND (e.[IsActive] = 1)

END
