-------------------------------------------------------------------------------
-- This will add a new entity to the repository.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCustomer_Add]
(
    @Flags        BIGINT       ,
    @DateCreated  DATETIME     ,
    @DateModified DATETIME     ,
    @Name         NVARCHAR(128),
    @EmailAddress NVARCHAR(1024)
)
AS
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[MerchantCustomers]
        (           
            [IsActive]    ,
            [Flags]       ,
            [DateCreated] ,
            [DateModified],
            [Name]        ,
            [EmailAddress]
        )
    VALUES
        (
            1            ,
            @Flags       ,
            @DateCreated ,
            @DateModified,
            @Name        ,
            @EmailAddress
        )

    SELECT [RowId], [RowVersion] FROM [core].[MerchantCustomers] WHERE [RowId] = @@IDENTITY

END
