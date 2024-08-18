-------------------------------------------------------------------------------
-- This will return the list of all entities.  The normal case is to return
-- those which are active.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ServiceLink_GetAll]
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]        as 'RowId'       ,
        e.[RowVersion]   as 'RowVersion'  ,
        e.[IsActive]     as 'IsActive'    ,
        e.[IdentityId]   as 'IdentityId'  ,
        e.[Flags]        as 'Flags'       ,
        e.[DateCreated]  as 'DateCreated' ,
        e.[DateModified] as 'DateModified',
        e.[TinyUrl]      as 'TinyUrl'     ,
        e.[DetailType]   as 'DetailType'  ,
        e.[Detail]       as 'Detail'
     FROM [core].[ServiceLinks] e
     WHERE (e.[IsActive] = 1)

END
