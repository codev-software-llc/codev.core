-------------------------------------------------------------------------------
-- Return the entity using its unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[License_GetAllByIdentityId]
(
    @IdentityId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]          as 'RowId'         ,
        e.[RowVersion]     as 'RowVersion'    ,
        e.[IsActive]       as 'IsActive'      ,
        e.[IdentityId]     as 'IdentityId'    ,
        e.[Flags]          as 'Flags'         ,
        e.[DateCreated]    as 'DateCreated'   ,
        e.[DateModified]   as 'DateModified'  ,
        e.[Application]    as 'Application'   ,
        e.[Name]           as 'Name'          ,
        e.[CurrencyCode]   as 'CurrencyCode'  ,
        e.[Amount]         as 'Amount'        ,
        e.[Features]       as 'Features'      ,
        e.[SerializedData] as 'SerializedData'
     FROM [core].[Licenses] e
     WHERE (e.[IdentityId] = @IdentityId) AND (e.[IsActive] = 1)

END
