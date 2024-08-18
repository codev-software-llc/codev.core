-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ScheduledTask_Update]
(
    @RowId        INT             ,
    @RowVersion   TIMESTAMP       ,
    @IdentityId   INT             ,
    @Flags        BIGINT          ,
    @DateCreated  DATETIME        ,
    @DateModified DATETIME        ,
    @AttentionAt  DATETIME        ,
    @Priority     INT             ,
    @Category     NVARCHAR(128)   ,
    @DetailType   NVARCHAR(64)    ,
    @Detail       NVARCHAR(2048)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[ScheduledTasks] SET
        [IdentityId]   = @IdentityId  ,
        [Flags]        = @Flags       ,
        [DateCreated]  = @DateCreated ,
        [DateModified] = @DateModified,
        [AttentionAt]  = @AttentionAt ,
        [Priority]     = @Priority    ,
        [Category]     = @Category    ,
        [DetailType]   = @DetailType  ,
        [Detail]       = @Detail
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[ScheduledTasks] WHERE [RowId] = @RowId

END
