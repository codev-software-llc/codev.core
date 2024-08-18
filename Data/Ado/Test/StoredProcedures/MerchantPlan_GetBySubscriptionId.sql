-------------------------------------------------------------------------------
-- This will look for an entity with the unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantPlan_GetBySubscriptionId]
(
    @SubscriptionId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]         as 'RowId'        ,
        e.[RowVersion]    as 'RowVersion'   ,
        e.[IsActive]      as 'IsActive'     ,
        e.[Flags]         as 'Flags'        ,
        e.[DateCreated]   as 'DateCreated'  ,
        e.[DateModified]  as 'DateModified' ,
        e.[Source]        as 'Source'       ,
        e.[Name]          as 'Name'         ,
        e.[Interval]      as 'Interval'     ,
        e.[IntervalCount] as 'IntervalCount',
        e.[CurrencyCode]  as 'CurrencyCode' ,
        e.[Amount]        as 'Amount'       ,
        e.[TrialDays]     as 'TrialDays'
    FROM [core].[MerchantPlans] e
        JOIN [core].[MerchantSubscriptions] s on s.[PlanId] = e.[RowId]
    WHERE (s.[RowId] = @SubscriptionId) AND (e.[IsActive] = 1)

END
