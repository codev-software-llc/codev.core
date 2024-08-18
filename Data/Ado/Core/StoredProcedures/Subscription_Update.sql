-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Subscription_Update]
(
    @RowId          INT      ,
    @RowVersion     TIMESTAMP,
    @Flags          BIGINT   ,
    @DateCreated    DATETIME ,
    @DateModified   DATETIME ,
    @DateExpiration DATETIME ,
    @LicenseId      INT      ,
    @IdentityId     INT      ,
    @SerializedData NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[Subscriptions] SET
        [Flags]          = @Flags         ,
        [DateCreated]    = @DateCreated   ,
        [DateModified]   = @DateModified  ,
        [DateExpiration] = @DateExpiration,
        [LicenseId]      = @LicenseId     ,
        [IdentityId]     = @IdentityId    ,
        [SerializedData] = @SerializedData
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Subscriptions] WHERE [RowId] = @RowId

END
