-------------------------------------------------------------------------------
-- Update the entity information.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Destination_Update]
(
    @RowId                      INT          ,
    @RowVersion                 TIMESTAMP    ,
    @IdentityId                 INT          ,
    @Flags                      BIGINT       ,
    @DateCreated                DATETIME     ,
    @DateModified               DATETIME     ,
    @Address                    NVARCHAR(512),
    @Type                       NVARCHAR(64) ,
    @ConfirmationSecret         NVARCHAR(64) ,
    @ConfirmationExpirationDate DATETIME
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[Destinations] SET
        [IdentityId]                 = @IdentityId        ,
        [Flags]                      = @Flags             ,
        [DateCreated]                = @DateCreated       ,
        [DateModified]               = @DateModified      ,
        [Address]                    = @Address           ,
        [Type]                       = @Type              ,
        [ConfirmationSecret]         = @ConfirmationSecret,
        [ConfirmationExpirationDate] = @ConfirmationExpirationDate
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Destinations] WHERE [RowId] = @RowId

END
