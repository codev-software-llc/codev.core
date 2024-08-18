-------------------------------------------------------------------------------
-- This will update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ServiceLink_Update]
(
    @RowId        INT             ,
    @RowVersion   TIMESTAMP       ,
    @IdentityId   INT             ,
    @Key          UNIQUEIDENTIFIER,
    @Flags        BIGINT          ,
    @DateCreated  DATETIME        ,
    @DateModified DATETIME        ,
    @TinyUrl      NVARCHAR(64)    ,
    @DetailType   NVARCHAR(64)    ,
    @Detail       NVARCHAR(2048)
)  
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[ServiceLinks] SET
        [IdentityId]   = @IdentityId  ,
        [Flags]        = @Flags       ,
        [DateCreated]  = @DateCreated ,
        [DateModified] = @DateModified,
        [TinyUrl]      = @TinyUrl     ,
        [DetailType]   = @DetailType  ,
        [Detail]       = @Detail
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[ServiceLinks] WHERE [RowId] = @RowId

END
