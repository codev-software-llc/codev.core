-------------------------------------------------------------------------------
-- Return all entities.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Session_GetAll]
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
     WHERE (e.[IsActive] = 1)

END
