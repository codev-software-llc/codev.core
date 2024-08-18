-------------------------------------------------------------------------------
-- Return the entity by the confirmation secret.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Destination_GetByAddress]
(
    @Address NVARCHAR(512),
    @Type    INT = NULL
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
     WHERE (e.[Address] = @Address) AND ((@Type IS NULL) OR (e.[Type] = @Type)) AND (e.[IsActive] = 1)

END
