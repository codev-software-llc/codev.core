-------------------------------------------------------------------------------
-- This will update the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCard_Update]
(
    @RowId           INT          ,
    @RowVersion      TIMESTAMP    ,
    @Flags           BIGINT       ,
    @DateCreated     DATETIME     ,
    @DateModified    DATETIME     ,
    @Name            NVARCHAR(128),
    @Type            NVARCHAR(16) ,
    @Number          NVARCHAR(32) ,
    @Code            NVARCHAR(16) ,
    @ExpirationYear  INT          ,
    @ExpirationMonth INT          ,
    @CustomerId      INT
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[MerchantCards] SET
        [Flags]           = @Flags          ,
        [DateCreated]     = @DateCreated    ,
        [DateModified]    = @DateModified   ,
        [Name]            = @Name           ,
        [Type]            = @Type           ,
        [Number]          = @Number         ,
        [Code]            = @Code           ,
        [ExpirationYear]  = @ExpirationYear ,
        [ExpirationMonth] = @ExpirationMonth,
        [CustomerId]      = @CustomerId
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId], [RowVersion] FROM [core].[MerchantCards] WHERE [RowId] = @RowId

END
