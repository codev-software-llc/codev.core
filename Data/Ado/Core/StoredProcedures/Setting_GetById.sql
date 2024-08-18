-------------------------------------------------------------------------------
-- Return the entity using its unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Setting_GetById]
(
    @RowId INT
)
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
     WHERE (e.[RowId] = @RowId) AND (e.[IsActive] = 1)

END
