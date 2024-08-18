-------------------------------------------------------------------------------
-- This will permanently remove the entity from the database.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Identity_Purge]
(
    @RowId INT
)
AS
BEGIN

    DELETE FROM [core].[PaymentTransactions]
    FROM [core].[PaymentTransactions] t
        JOIN [core].[Payments]       p on p.[RowId] = t.[PaymentId]
        JOIN [core].[PaymentMethods] m on m.[RowId] = p.[PaymentMethodId]
    WHERE m.[IdentityId] = @RowId

    DELETE FROM [core].[Payments]
    FROM [core].[Payments] p
        JOIN [core].[PaymentMethods] m on m.[RowId] = p.[PaymentMethodId]
    WHERE m.[IdentityId] = @RowId

    DELETE FROM [core].[PaymentMethods] WHERE [IdentityId] = @RowId
    DELETE FROM [core].[ScheduledTasks] WHERE [IdentityId] = @RowId
    DELETE FROM [core].[ServiceLinks]   WHERE [IdentityId] = @RowId
    DELETE FROM [core].[Sessions]       WHERE [IdentityId] = @RowId
    DELETE FROM [core].[Subscriptions]  WHERE [IdentityId] = @RowId
    DELETE FROM [core].[Destinations]   WHERE [IdentityId] = @RowId
    DELETE FROM [core].[Licenses]       WHERE [IdentityId] = @RowId
    DELETE FROM [core].[Identities]     WHERE [RowId]      = @RowId

    DELETE FROM [core].[Identities] WHERE [RowId] = @RowId

END
