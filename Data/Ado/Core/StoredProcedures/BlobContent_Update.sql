-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[BlobContent_Update]
(
    @RowId        INT             ,
    @RowVersion   TIMESTAMP       ,
    @BlobId       INT             ,
    @Flags        BIGINT          ,
    @DateCreated  DATETIME        ,
    @DateModified DATETIME        ,
    @MimeType     NVARCHAR(64)    ,
    @Size         BIGINT          ,
    @Content      IMAGE = NULL
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[BlobContents] SET
        [BlobId]       = @BlobId      ,
        [Flags]        = @Flags       ,
        [DateCreated]  = @DateCreated ,
        [DateModified] = @DateModified,
        [MimeType]     = @MimeType    ,
        [Size]         = @Size        ,
        [Content]      = @Content
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[BlobContents] WHERE [RowId] = @RowId

END
