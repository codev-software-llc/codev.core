-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ScheduledTask_Add]
(
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

    INSERT INTO [core].[ScheduledTasks]
        (
            [IsActive]    ,
            [IdentityId]  ,
            [Flags]       ,
            [DateCreated] ,
            [DateModified],
            [AttentionAt] ,
            [Priority]    ,
            [Category]    ,
            [DetailType]  ,
            [Detail]
        ) 
    VALUES
        (
            1            ,
            @IdentityId  ,
            @Flags       ,
            @DateCreated ,
            @DateModified,
            @AttentionAt ,
            @Priority    ,
            @Category    ,
            @DetailType  ,
            @Detail
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[ScheduledTasks] WHERE [RowId] = @@IDENTITY

END
