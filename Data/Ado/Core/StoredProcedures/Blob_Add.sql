-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Blob_Add]
(
    @IdentityId   INT     ,
    @Flags        BIGINT  ,
    @DateCreated  DATETIME,
    @DateModified DATETIME,
    @Name         NVARCHAR(448)
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[Blobs]
        (
            [IsActive]    ,
            [IdentityId]  ,
            [Flags]       ,
            [DateCreated] ,
            [DateModified],
            [Name]
        ) 
    VALUES
        (
            1            ,
            @IdentityId  ,
            @Flags       ,
            @DateCreated ,
            @DateModified,
            @Name
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Blobs] WHERE [RowId] = @@IDENTITY

END
