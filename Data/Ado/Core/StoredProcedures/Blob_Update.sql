-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Blob_Update]
(
    @RowId        INT      ,
    @RowVersion   TIMESTAMP,
    @IdentityId   INT      ,
    @Flags        BIGINT   ,
    @DateCreated  DATETIME ,
    @DateModified DATETIME ,
    @Name         NVARCHAR(448)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[Blobs] SET
        [IdentityId]   = @IdentityId  ,
        [Flags]        = @Flags       ,
        [DateCreated]  = @DateCreated ,
        [DateModified] = @DateModified,
        [Name]         = @Name
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Blobs] WHERE [RowId] = @RowId

END
