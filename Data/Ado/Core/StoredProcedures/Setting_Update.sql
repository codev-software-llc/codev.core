-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Setting_Update]
(
    @RowId        INT         ,
    @RowVersion   TIMESTAMP   ,
    @DateCreated  DATETIME    ,
	@DateModified DATETIME    ,
    @Flags        BIGINT      ,
    @Name         NVARCHAR(64),
    @Value        NVARCHAR(1024) = NULL
)
AS
BEGIN

    SET NOCOUNT ON;      

    UPDATE [core].[Settings] SET
        [DateCreated]  = @DateCreated ,
		[DateModified] = @DateModified,
        [Flags]        = @Flags       ,
        [Name]         = @Name        ,
        [Value]        = @Value
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Settings] WHERE [RowId] = @RowId

END
