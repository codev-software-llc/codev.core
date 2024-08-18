-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ErrorLog_Update]
(
    @RowId         INT             ,
    @RowVersion    TIMESTAMP       ,
    @IdentityId    INT             ,
    @Flags         BIGINT          ,
    @DateCreated   DATETIME        ,
    @DateModified  DATETIME        ,
    @ComponentType INT             ,
    @SeverityType  INT             ,
    @Message       NVARCHAR(2048)  ,
    @ServerName    NVARCHAR(128)   ,
    @TrackingTag   NVARCHAR(256)   ,
    @StackTrace    NVARCHAR(2048) = ''
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[ErrorLog] SET
        [IdentityId]    = @IdentityId   ,
        [Flags]         = @Flags        ,
        [DateCreated]   = @DateCreated  ,
        [DateModified]  = @DateModified ,
        [ComponentType] = @ComponentType,
        [SeverityType]  = @SeverityType ,
        [Message]       = @Message      ,
        [ServerName]    = @ServerName   ,
        [TrackingTag]   = @TrackingTag  ,
        [StackTrace]    = @StackTrace
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[ErrorLog] WHERE [RowId] = @RowId

END
