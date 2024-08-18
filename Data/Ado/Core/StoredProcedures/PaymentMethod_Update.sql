-------------------------------------------------------------------------------
-- This will update a payment method.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentMethod_Update]
(
    @RowId            INT         ,
    @RowVersion       TIMESTAMP   ,
    @Flags            BIGINT      ,
    @DateCreated      DATETIME    ,
    @DateModified     DATETIME    ,
    @Expiration       NVARCHAR(16),
    @OffuscatedNumber NVARCHAR(32),
    @IdentityId       INT         ,
    @SerializedData   NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[PaymentMethods] SET
        [Flags]            = @Flags           ,
        [DateCreated]      = @DateCreated     ,
        [DateModified]     = @DateModified    ,
        [Expiration]       = @Expiration      ,
        [OffuscatedNumber] = @OffuscatedNumber,
        [IdentityId]       = @IdentityId      ,
        [SerializedData]   = @SerializedData
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[PaymentMethods] WHERE [RowId] = @RowId

END
