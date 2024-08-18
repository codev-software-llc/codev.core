-------------------------------------------------------------------------------
-- Return all entities.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ErrorLog_GetAll]
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]         as 'RowId'        ,
        e.[RowVersion]    as 'RowVersion'   ,
        e.[IsActive]      as 'IsActive'     ,
        e.[IdentityId]    as 'IdentityId'   ,
        e.[Flags]         as 'Flags'        ,
        e.[DateCreated]   as 'DateCreated'  ,
        e.[DateModified]  as 'DateModified' ,
        e.[ComponentType] as 'ComponentType',
        e.[SeverityType]  as 'SeverityType' ,
        e.[Message]       as 'Message'      ,
        e.[ServerName]    as 'ServerName'   ,
        e.[TrackingTag]   as 'TrackingTag'  ,
        e.[StackTrace]    as 'StackTrace'
     FROM [core].[ErrorLog] e
     WHERE (e.[IsActive] = 1)

END
