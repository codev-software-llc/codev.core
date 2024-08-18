-------------------------------------------------------------------------------
-- This will return the entity by the tiny url.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ServiceLink_GetByTinyUrl]
(
    @TinyUrl NVARCHAR(64)
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
        e.[TinyUrl]      as 'TinyUrl'     ,
        e.[DetailType]   as 'DetailType'  ,
        e.[Detail]       as 'Detail'
     FROM [core].[ServiceLinks] e
     WHERE (e.[TinyUrl] = @TinyUrl) AND (e.[IsActive] = 1)

END
