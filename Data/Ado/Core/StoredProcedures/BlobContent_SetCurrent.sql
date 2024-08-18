-------------------------------------------------------------------------------
-- Set the current blob content.  This will remove all others that are not.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[BlobContent_SetCurrent]
(
    @RowId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    DECLARE @BlobId INT = (SELECT [BlobId] FROM [core].[BlobContents] WHERE [RowId] = @RowId);

    UPDATE [core].[BlobContents] SET
        [Flags] = 0
    WHERE [BlobId] = @BlobId

    UPDATE [core].[BlobContents] SET
        [Flags] = 1
    WHERE [RowId] = @RowId

    DELETE FROM [core].[BlobContents]
    WHERE ([BlobId] = @BlobId) AND (([Flags] & 1) = 0)

END