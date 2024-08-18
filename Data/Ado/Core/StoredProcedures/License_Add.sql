-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[License_Add]
(
    @Flags          BIGINT        ,
    @DateCreated    DATETIME      ,
    @DateModified   DATETIME      ,
    @IdentityId     INT           ,
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

    INSERT INTO [core].[Licenses]
        (
            [IsActive]    ,
            [Flags]       ,
            [DateCreated] ,
            [DateModified],
            [IdentityId]  ,
            [Application] ,
            [Name]        ,
            [CurrencyCode],
            [Amount]      ,
            [Features]    ,
            [SerializedData]
        ) 
    VALUES
        (
            1            ,
            @Flags       ,
            @DateCreated ,
            @DateModified,
            @IdentityId  ,
            @Application ,
            @Name        ,
            @CurrencyCode,
            @Amount      ,
            @Features    ,
            @SerializedData
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Licenses] WHERE [RowId] = @@IDENTITY

END
