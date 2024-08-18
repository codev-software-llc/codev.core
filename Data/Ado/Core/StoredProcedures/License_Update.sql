-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[License_Update]
(
    @RowId          INT           ,
    @RowVersion     TIMESTAMP     ,
    @IdentityId     INT           ,
    @Flags          BIGINT        ,
    @DateCreated    DATETIME      ,
    @DateModified   DATETIME      ,
    @Application    NVARCHAR(128) ,
    @Name           NVARCHAR(128) ,
    @CurrencyCode   NVARCHAR(3)   ,
    @Amount         DECIMAL(18,2) ,
    @Features       NVARCHAR(2048),
    @SerializedData NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[Licenses] SET
        [IdentityId]     = @IdentityId    ,
        [Flags]          = @Flags         ,
        [DateCreated]    = @DateCreated   ,
        [DateModified]   = @DateModified  ,
        [Application]    = @Application   ,
        [Name]           = @Name          ,
        [CurrencyCode]   = @CurrencyCode  ,
        [Amount]         = @Amount        ,
        [Features]       = @Features      ,
        [SerializedData] = @SerializedData
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Licenses] WHERE [RowId] = @RowId

END
