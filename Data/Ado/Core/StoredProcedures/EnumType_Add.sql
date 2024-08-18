-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[EnumType_Add]
(
    @Flags        INT             ,
    @DateCreated  DATETIME        ,
    @DateModified DATETIME        ,
    @IsFlag       BIT             ,
    @IsBig        BIT             ,
    @Application  NVARCHAR(128)   ,
    @Name         NVARCHAR(64)    ,
    @EnumKey      NVARCHAR(64)    ,
    @Value        BIGINT          ,
    @Comment      NVARCHAR(128) = ''
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[EnumTypes]
        (
            [IsActive]    ,
            [Flags]       ,
            [DateCreated] ,
            [DateModified],
            [IsFlag]      ,
            [IsBig]       ,
            [Application] ,
            [Name]        ,
            [EnumKey]     ,
            [Value]       ,
            [Comment]
        )
    VALUES
        (
            1            ,
            @Flags       ,
            @DateCreated ,
            @DateModified,
            @IsFlag      ,
            @IsBig       ,
            @Application ,
            @Name        ,
            @EnumKey     ,
            @Value       , 
            @Comment
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[EnumTypes] WHERE [RowId] = @@IDENTITY

END
