-------------------------------------------------------------------------------
-- Return all entities using the user identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Destination_GetAllByIdentityId]
(
    @IdentityId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]                      as 'RowId'             ,
        e.[RowVersion]                 as 'RowVersion'        ,
        e.[IsActive]                   as 'IsActive'          ,
        e.[IdentityId]                 as 'IdentityId'        ,
        e.[Flags]                      as 'Flags'             ,
        e.[DateCreated]                as 'DateCreated'       ,
        e.[DateModified]               as 'DateModified'      ,
        e.[Address]                    as 'Address'           ,
        e.[Type]                       as 'Type'              ,
        e.[ConfirmationSecret]         as 'ConfirmationSecret',
        e.[ConfirmationExpirationDate] as 'ConfirmationExpirationDate'
     FROM [core].[Destinations] e
     WHERE (e.[IdentityId] = @IdentityId) AND (e.[IsActive] = 1)

END
