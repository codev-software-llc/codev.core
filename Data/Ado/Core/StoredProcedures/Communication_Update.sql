-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Communication_Update]
(
    @RowId        INT          ,
    @RowVersion   TIMESTAMP    ,
    @Flags        BIGINT       ,
    @DateCreated  DATETIME     ,
    @DateModified DATETIME     ,
    @Application  NVARCHAR(128),
    @Category     NVARCHAR(128),
    @Subcategory  NVARCHAR(128),
    @Name         NVARCHAR(128),
    @EmailAddress NVARCHAR(512),
    @Comments     NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[Communications] SET
        [Flags]        = @Flags       ,
        [DateCreated]  = @DateCreated ,
        [DateModified] = @DateModified,
        [Application]  = @Application ,
        [Category]     = @Category    ,
        [Subcategory]  = @Subcategory ,
        [Name]         = @Name        ,
        [EmailAddress] = @EmailAddress,
        [Comments]     = @Comments
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Communications] WHERE [RowId] = @RowId

END
