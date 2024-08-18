-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Communication_Add]
(
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

    INSERT INTO [core].[Communications]
        (
            [IsActive]    ,
            [Flags]       ,
            [DateCreated] ,
            [DateModified],
            [Application] ,
            [Category]    ,
            [Subcategory] ,
            [Name]        ,
            [EmailAddress],
            [Comments]
        ) 
    VALUES
        (
            1            ,
            @Flags       ,
            @DateCreated ,
            @DateModified,
            @Application ,
            @Category    ,
            @Subcategory ,
            @Name        ,
            @EmailAddress,
            @Comments
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Communications] WHERE [RowId] = @@IDENTITY

END
