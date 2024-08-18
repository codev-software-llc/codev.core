-------------------------------------------------------------------------------
-- Return all entities which represent the same Error
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ErrorLog_GetStatistics]
(
    @DateStart DATETIME,
    @DateEnd   DATETIME
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
     WHERE ((e.[DateCreated] >= @DateStart) AND (e.[DateCreated] <= @DateEnd)) AND (e.[IsActive] = 1)

END
