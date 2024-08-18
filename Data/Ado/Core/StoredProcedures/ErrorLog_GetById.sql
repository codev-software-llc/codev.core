-------------------------------------------------------------------------------
-- Return the entity using its unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ErrorLog_GetById]
(
    @RowId INT
)
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
     WHERE (e.[RowId] = @RowId) AND (e.[IsActive] = 1)

END
