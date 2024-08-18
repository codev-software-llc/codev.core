-------------------------------------------------------------------------------
-- This will permanently remove all entities that share the same key.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ServiceLink_PurgeAllByIdentityId]
(
    @IdentityId INT
)
AS
BEGIN

    DELETE FROM [core].[ServiceLinks] WHERE [IdentityId] = @IdentityId

END
