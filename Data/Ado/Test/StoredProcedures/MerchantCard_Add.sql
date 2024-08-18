-------------------------------------------------------------------------------
-- This will add a new entity to the repository.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCard_Add]
(
    @Flags           BIGINT       ,
    @DateCreated     DATETIME     ,
    @DateModified    DATETIME     ,
    @Name            NVARCHAR(128),
    @Type            NVARCHAR(16) ,
    @Number          NVARCHAR(32) ,
    @Code            NVARCHAR(16) ,
    @ExpirationYear  INT          ,
    @ExpirationMonth INT          ,
    @CustomerId      BIGINT
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[MerchantCards]
        (           
            [IsActive]       ,
            [Flags]          ,
            [DateCreated]    ,
            [DateModified]   ,
            [Name]           ,
            [Type]           ,
            [Number]         ,
            [Code]           ,
            [ExpirationYear] ,
            [ExpirationMonth],
            [CustomerId]
        )
    VALUES
        (
            1               ,
            @Flags          ,
            @DateCreated    ,
            @DateModified   ,
            @Name           ,
            @Type           ,
            @Number         ,
            @Code           ,
            @ExpirationYear ,
            @ExpirationMonth,
            @CustomerId
        )
    
    SELECT [RowId], [RowVersion] FROM [core].[MerchantCards] WHERE [RowId] = @@IDENTITY

END
