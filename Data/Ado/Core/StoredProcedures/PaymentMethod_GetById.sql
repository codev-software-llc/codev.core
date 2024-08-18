-------------------------------------------------------------------------------
-- This will return the payment method by its identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentMethod_GetById]
(
    @RowId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;
  
    SELECT
        e.[RowId]            as 'RowId'           ,
        e.[RowVersion]       as 'RowVersion'      ,
        e.[IsActive]         as 'IsActive'        ,
        e.[Flags]            as 'Flags'           ,
        e.[DateCreated]      as 'DateCreated'     ,
        e.[DateModified]     as 'DateModified'    ,
        e.[Expiration]       as 'Expiration'      ,
        e.[OffuscatedNumber] as 'OffuscatedNumber',
        e.[IdentityId]       as 'IdentityId'      ,
        e.[SerializedData]   as 'SerializedData'
     FROM [core].[PaymentMethods] e
     WHERE (e.[RowId] = @RowId) AND (e.[IsActive] = 1)

END