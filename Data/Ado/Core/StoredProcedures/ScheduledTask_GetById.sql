-------------------------------------------------------------------------------
-- Return the entity using its unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ScheduledTask_GetById]
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
        e.[IdentityId]   as 'IdentityId'  ,
        e.[Flags]        as 'Flags'       ,
        e.[DateCreated]  as 'DateCreated' ,
        e.[DateModified] as 'DateModified',
        e.[AttentionAt]  as 'AttentionAt' ,
        e.[Priority]     as 'Priority'    ,
        e.[Category]     as 'Category'    ,
        e.[DetailType]   as 'DetailType'  ,
        e.[Detail]       as 'Detail'
    FROM [core].[ScheduledTasks] e
    WHERE (e.[RowId] = @RowId) AND (e.[IsActive] = 1)

END
