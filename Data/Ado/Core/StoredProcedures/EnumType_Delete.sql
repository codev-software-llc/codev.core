-------------------------------------------------------------------------------
-- Delete the entity from the repository as flagging the IsActive field.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[EnumType_Delete]
(
    @RowId INT
)
AS
BEGIN

    UPDATE [core].[EnumTypes] SET [IsActive] = 0 WHERE ([RowId] = @RowId)

END
