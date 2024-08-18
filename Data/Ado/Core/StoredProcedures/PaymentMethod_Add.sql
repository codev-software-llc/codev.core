-------------------------------------------------------------------------------
-- This will add a new payment method for an account.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentMethod_Add]
(
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

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    INSERT INTO [core].[PaymentMethods]
        (
            [IsActive]        ,
            [Flags]           ,
            [DateCreated]     ,
            [DateModified]    ,
            [Expiration]      ,
            [OffuscatedNumber],
            [IdentityId]      ,
            [SerializedData]
        )
    VALUES
        (
            1                ,
            @Flags           ,
            @DateCreated     ,
            @DateModified    ,
            @Expiration      ,
            @OffuscatedNumber,
            @IdentityId      ,
            @SerializedData
        ) 

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[PaymentMethods] WHERE [RowId] = @@IDENTITY

END
