-------------------------------------------------------------------------------
-- This will permanently remove an entity from the repository.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCard_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[MerchantCards] WHERE [RowId] = @RowId
   
END
