-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Subscription_Add]
(
    @Flags          BIGINT  ,
    @DateCreated    DATETIME,
    @DateModified   DATETIME,
    @DateExpiration DATETIME,
    @LicenseId      INT     ,
    @IdentityId     INT     ,
    @SerializedData NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[Subscriptions]
        (
            [IsActive]      ,
            [Flags]         ,
            [DateCreated]   ,
            [DateModified]  ,
            [DateExpiration],
            [LicenseId]     ,
            [IdentityId]    ,
            [SerializedData]
        )
    VALUES
        (
            1              ,
            @Flags         ,
            @DateCreated   ,
            @DateModified  ,
            @DateExpiration,
            @LicenseId     ,
            @IdentityId    ,
            @SerializedData
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Subscriptions] WHERE [RowId] = @@IDENTITY  

END
