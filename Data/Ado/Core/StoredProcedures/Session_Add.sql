-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Session_Add]
(
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

    INSERT INTO [core].[Sessions]
        (
            [IsActive]      ,
            [IdentityId]    ,
            [Flags]         ,
            [DateCreated]   ,
            [DateModified]  ,
            [DateExpiration],
            [SessionId]     ,
            [Secret]
        ) 
    VALUES
        (
            1              ,
            @IdentityId    ,
            @Flags         ,
            @DateCreated   ,
            @DateModified  ,
            @DateExpiration,
            @SessionId     ,
            @Secret
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Sessions] WHERE [RowId] = @@IDENTITY

END
