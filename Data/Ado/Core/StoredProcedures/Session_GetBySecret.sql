-------------------------------------------------------------------------------
-- Return the the entity by the secret.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Session_GetBySecret]
(
    @Secret NVARCHAR(64)
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
        e.[DateExpiration] as 'DateExpiration',
        e.[Secret]         as 'Secret'
     FROM [core].[Sessions] e
     WHERE (e.[Secret] = @Secret) AND (e.[IsActive] = 1)

END
