-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Destination_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[Destinations] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
