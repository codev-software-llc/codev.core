-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Identity_Update]
(
    @RowId                INT          ,
    @RowVersion           TIMESTAMP    ,
    @Flags                BIGINT       ,
    @DateCreated          DATETIME     ,
    @DateModified         DATETIME     ,
    @ConfirmationAttempts INT          ,
    @DateTimeZoneId       NVARCHAR(128),
    @SerializedData       NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[Identities] SET
        [Flags]                = @Flags               ,
        [DateCreated]          = @DateCreated         ,
        [DateModified]         = @DateModified        ,
        [ConfirmationAttempts] = @ConfirmationAttempts,
        [DateTimeZoneId]       = @DateTimeZoneId      ,
        [SerializedData]       = @SerializedData
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Identities] WHERE [RowId] = @RowId

END
