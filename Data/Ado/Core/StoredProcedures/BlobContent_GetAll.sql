-------------------------------------------------------------------------------
-- Return all entities.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[BlobContent_GetAll]
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
     FROM [core].[BlobContents]
     WHERE ([IsActive] = 1)

END
