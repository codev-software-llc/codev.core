-------------------------------------------------------------------------------
-- Return the entity using its unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[EnumType_GetById]
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
        e.[Flags]        as 'Flags'       ,
        e.[DateCreated]  as 'DateCreated' ,
        e.[DateModified] as 'DateModified',
        e.[IsFlag]       as 'IsFlag'      ,
        e.[IsBig]        as 'IsBig'       ,
        e.[Application]  as 'Application' ,
        e.[Name]         as 'Name'        ,
        e.[EnumKey]      as 'EnumKey'     ,
        e.[Value]        as 'Value'       ,
        e.[Comment]      as 'Comment'
     FROM [core].[EnumTypes] e
     WHERE (e.[RowId] = @RowId) AND (e.[IsActive] = 1)

END
