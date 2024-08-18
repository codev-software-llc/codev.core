-------------------------------------------------------------------------------
-- Return all entities.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Subscription_GetAll]
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]          as 'RowId'         ,
        e.[RowVersion]     as 'RowVersion'    ,
        e.[IsActive]       as 'IsActive'      ,
        e.[Flags]          as 'Flags'         ,
        e.[DateCreated]    as 'DateCreated'   ,
        e.[DateModified]   as 'DateModified'  ,
        e.[DateExpiration] as 'DateExpiration',
        e.[LicenseId]      as 'LicenseId'     ,
        e.[IdentityId]     as 'IdentityId'    ,
        e.[SerializedData] as 'SerializedData'
     FROM [core].[Subscriptions] e
     WHERE (e.[IsActive] = 1)

END
