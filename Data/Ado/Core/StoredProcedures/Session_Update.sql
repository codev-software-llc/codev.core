-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Session_Update]
(
    @RowId          INT             ,
    @RowVersion     TIMESTAMP       ,
    @IdentityId     INT             ,
    @Flags          BIGINT          ,
    @DateCreated    DATETIME        ,
    @DateModified   DATETIME        ,
    @DateExpiration DATETIME        ,
    @SessionId      UNIQUEIDENTIFIER,
    @Secret         NVARCHAR(64)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[Sessions] SET
        [IdentityId]     = @IdentityId    ,
        [Flags]          = @Flags         ,
        [DateCreated]    = @DateCreated   ,
        [DateModified]   = @DateModified  ,
        [DateExpiration] = @DateExpiration,
        [SessionId]      = @SessionId     ,
        [Secret]         = @Secret
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Sessions] WHERE [RowId] = @RowId

END
