-------------------------------------------------------------------------------
-- Return the entity using the content identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Blob_GetByBlobContentId]
(
    @BlobContentId INT
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
    WHERE (c.[RowId] = @BlobContentId) AND (e.[IsActive] = 1)

END
