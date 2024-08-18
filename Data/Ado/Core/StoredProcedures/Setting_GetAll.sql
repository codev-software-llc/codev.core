-------------------------------------------------------------------------------
-- Return all entities.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Setting_GetAll]
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    
    SET NOCOUNT ON;    

    SELECT
        e.[RowId]        as 'RowId'       ,
        e.[RowVersion]   as 'RowVersion'  ,
        e.[IsActive]     as 'IsActive'    ,
        e.[DateCreated]  as 'DateCreated' ,
        e.[DateModified] as 'DateModified',
        e.[Flags]        as 'Flags'       ,
        e.[Name]         as 'Name'        ,
        e.[Value]        as 'Value'
     FROM [core].[Settings] e
     WHERE (e.[IsActive] = 1)

END
