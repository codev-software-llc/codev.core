-------------------------------------------------------------------------------
-- This will delete the entity permantently.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentTransaction_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[PaymentTransactions] WHERE [RowId] = @RowId

END
