-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[License_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[Licenses] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
