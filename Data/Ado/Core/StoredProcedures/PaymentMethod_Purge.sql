-------------------------------------------------------------------------------
-- This will permanently remove a payment method.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentMethod_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[PaymentMethods] WHERE [RowId] = @RowId

END
