-------------------------------------------------------------------------------
-- This will update the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCustomer_Update]
(
    @RowId        INT          ,
    @RowVersion   TIMESTAMP    ,
    @Flags        BIGINT       ,
    @DateCreated  DATETIME     ,
    @DateModified DATETIME     ,
    @Name         NVARCHAR(128),
    @EmailAddress NVARCHAR(1024)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[MerchantCustomers] SET
        [Flags]        = @Flags       ,
        [DateCreated]  = @DateCreated ,  
        [DateModified] = @DateModified,
        [Name]         = @Name        ,
        [EmailAddress] = @EmailAddress
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId], [RowVersion] FROM [core].[MerchantCustomers] WHERE [RowId] = @RowId

END
