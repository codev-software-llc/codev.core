-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[EnumType_Update]
(
    @RowId        INT          ,
    @RowVersion   TIMESTAMP    ,
    @Flags        BIGINT       ,
    @DateCreated  DATETIME     ,
    @DateModified DATETIME     ,
    @IsFlag       BIT          ,
    @IsBig        BIT          ,
    @Application  NVARCHAR(128),
    @Name         NVARCHAR(64) ,
    @EnumKey      NVARCHAR(64) ,
    @Value        BIGINT       ,
    @Comment      NVARCHAR(128) = ''
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[EnumTypes] SET
        [Flags]        = @Flags       ,
        [DateCreated]  = @DateCreated ,
        [DateModified] = @DateModified,
        [IsFlag]       = @IsFlag      ,
        [IsBig]        = @IsBig       ,
        [Application]  = @Application ,
        [Name]         = @Name        ,
        [EnumKey]      = @EnumKey     ,
        [Value]        = @Value       ,
        [Comment]      = @Comment
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[EnumTypes] WHERE [RowId] = @RowId

END
