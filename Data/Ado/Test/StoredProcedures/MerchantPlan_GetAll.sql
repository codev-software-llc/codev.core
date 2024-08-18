-------------------------------------------------------------------------------
-- This will retriev all entities for this type.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantPlan_GetAll]
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
    WHERE (e.[IsActive] = 1)

END
