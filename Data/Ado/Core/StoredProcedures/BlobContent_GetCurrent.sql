-------------------------------------------------------------------------------
-- Return a list of all entities by a their blob identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[BlobContent_GetCurrent]
(
    @BlobId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        [RowId]        as 'RowId'       ,
        [RowVersion]   as 'RowVersion'  ,
        [IsActive]     as 'IsActive'    ,
        [BlobId]       as 'BlobId'      ,
        [Flags]        as 'Flags'       ,
        [DateCreated]  as 'DateCreated' ,
        [DateModified] as 'DateModified',
        [MimeType]     as 'MimeType'    ,
        [Size]         as 'Size'        ,
        [Content]      as 'Content'
    FROM [core].[BlobContents] e
    WHERE ([BlobId] = @BlobId) AND (([Flags] & 1) <> 0) AND ([IsActive] = 1)

END