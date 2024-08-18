-------------------------------------------------------------------------------
-- This will delete the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantCard_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[MerchantCards] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
