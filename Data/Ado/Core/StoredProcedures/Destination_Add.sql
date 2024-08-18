-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Destination_Add]
(
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

    INSERT INTO [core].[Destinations]
        (
            [IsActive]          ,
            [IdentityId]        ,
            [Flags]             ,
            [DateCreated]       ,
            [DateModified]      ,
            [Address]           ,
            [Type]              ,
            [ConfirmationSecret],
            [ConfirmationExpirationDate]
        ) 
    VALUES
        (
            1                 ,
            @IdentityId       ,
            @Flags            ,
            @DateCreated      ,
            @DateModified     ,
            @Address          ,
            @Type             ,
            @ConfirmationSecret,
            @ConfirmationExpirationDate
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Destinations] WHERE [RowId] = @@IDENTITY

END
