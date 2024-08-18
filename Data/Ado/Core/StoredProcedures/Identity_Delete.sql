-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Identity_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[Identities] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
