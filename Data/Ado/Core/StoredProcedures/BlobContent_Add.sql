-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[BlobContent_Add]
(
    @BlobId       INT         ,
    @Flags        BIGINT      ,
    @DateCreated  DATETIME    ,
    @DateModified DATETIME    ,
    @MimeType     NVARCHAR(64),
    @Size         BIGINT      ,
    @Content      IMAGE = NULL
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[BlobContents]
        (
            [IsActive]    ,
            [BlobId]      ,
            [Flags]       ,
            [DateCreated] ,
            [DateModified],
            [MimeType]    ,
            [Size]        ,
            [Content]
        ) 
    VALUES
        (
            1            ,
            @BlobId      ,
            @Flags       ,
            @DateCreated ,
            @DateModified,
            @MimeType    ,
            @Size        ,
            @Content
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[BlobContents] WHERE [RowId] = @@IDENTITY

END
