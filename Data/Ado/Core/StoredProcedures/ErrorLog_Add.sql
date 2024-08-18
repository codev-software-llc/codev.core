-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ErrorLog_Add]
(
    @IdentityId    INT           ,
    @Flags         BIGINT        ,
    @DateCreated   DATETIME      ,
    @DateModified  DATETIME      ,
    @ComponentType INT           ,
    @SeverityType  INT           ,
    @Message       NVARCHAR(2048),
    @ServerName    NVARCHAR(128) ,
    @TrackingTag   NVARCHAR(256) ,
    @StackTrace    NVARCHAR(2048)
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[ErrorLog]
        (
            [IsActive]     ,
            [IdentityId]   ,
            [Flags]        ,
            [DateCreated]  ,
            [DateModified] ,
            [ComponentType],
            [SeverityType] ,
            [Message]      ,
            [ServerName]   ,
            [TrackingTag]  ,
            [StackTrace]
        )
    VALUES
        (
            1             ,
            @IdentityId   ,
            @Flags        ,
            @DateCreated  ,
            @DateModified ,
            @ComponentType,
            @SeverityType ,
            @Message      ,
            @ServerName   ,
            @TrackingTag  ,
            @StackTrace
        ) 

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[ErrorLog] WHERE [RowId] = @@IDENTITY

END