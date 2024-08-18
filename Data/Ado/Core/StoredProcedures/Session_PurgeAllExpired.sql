-------------------------------------------------------------------------------
-- Purge all sessions that have expired from the date.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Session_PurgeAllExpired]
(
    @DateExpired DATETIME
)
AS
BEGIN

    DELETE FROM [core].[Sessions] WHERE ([DateExpiration] < @DateExpired)

END
