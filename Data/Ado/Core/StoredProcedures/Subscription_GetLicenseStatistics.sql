-------------------------------------------------------------------------------
-- Return all entities.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Subscription_GetStatistics]
(
    @LicenseId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
       e.[LicenseId],
       COUNT(e.IdentityId)
     FROM [core].[Subscriptions] e
     WHERE (e.[LicenseId] = @LicenseId) AND (e.[IsActive] = 1)
     GROUP BY e.[LicenseId]

END
