-------------------------------------------------------------------------------
-- This will delete the entity.  This is not removed from the database, but 
-- is tagged as Inactive.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ServiceLink_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[ServiceLinks] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
