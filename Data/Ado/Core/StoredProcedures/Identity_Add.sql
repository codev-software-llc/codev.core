-------------------------------------------------------------------------------
-- Add a new entity to the store.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Identity_Add]
(
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

    INSERT INTO [core].[Identities]
        (
            [IsActive]            ,
            [Flags]               ,
            [DateCreated]         ,
            [DateModified]        ,
            [ConfirmationAttempts],
            [DateTimeZoneId]      ,
            [SerializedData]
        ) 
    VALUES
        (
            1                    ,
            @Flags               ,
            @DateCreated         ,
            @DateModified        ,
            @ConfirmationAttempts,
            @DateTimeZoneId      ,
            @SerializedData
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[Identities] WHERE [RowId] = @@IDENTITY

END
