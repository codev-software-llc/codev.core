-------------------------------------------------------------------------------
-- Return the entity by its fully qualified name.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Blob_GetByName]
(
    @Name NVARCHAR(448)
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
        e.[Name]         as 'Name'        ,
        c.[MimeType]     as 'MimeType'    ,
        c.[Size]         as 'Size'
    FROM [core].[Blobs] e
        JOIN [core].[BlobContents] c on c.[BlobId] = e.[RowId]
    WHERE (e.[Name] = @Name) AND (e.[IsActive] = 1)
END