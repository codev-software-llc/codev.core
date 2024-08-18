-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Setting_Add]
(
    @DateCreated  DATETIME    ,
    @DateModified DATETIME    ,
    @Flags        BIGINT      ,
    @Name         NVARCHAR(64),
    @Value        NVARCHAR(1024) = NULL
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[Settings]
        (
            [IsActive]    ,
            [DateCreated] ,
            [DateModified],
            [Flags]       ,
            [Name]        ,
            [Value]
        ) 
    VALUES
        (
            1            ,
            @DateCreated ,
            @DateModified,
            @Flags       ,
            @Name        ,
            @Value
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Settings] WHERE [RowId] = @@IDENTITY

END
