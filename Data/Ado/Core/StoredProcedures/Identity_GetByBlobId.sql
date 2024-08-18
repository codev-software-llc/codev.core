-------------------------------------------------------------------------------
-- Return the entity using the blob identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Identity_GetByBlobId]
(
    @BlobId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]                as 'RowId'               ,
        e.[RowVersion]           as 'RowVersion'          ,
        e.[IsActive]             as 'IsActive'            ,
        e.[Flags]                as 'Flags'               ,
        e.[DateCreated]          as 'DateCreated'         ,
        e.[DateModified]         as 'DateModified'        ,
        e.[ConfirmationAttempts] as 'ConfirmationAttempts',
        e.[DateTimeZoneId]       as 'DateTimeZoneId'      ,
        e.[SerializedData]       as 'SerializedData'
     FROM [core].[Identities] e
         JOIN [core].[Blobs] b on b.[IdentityId] = e.[RowId]
     WHERE (b.[RowId] = @BlobId) AND (e.[IsActive] = 1)

END
