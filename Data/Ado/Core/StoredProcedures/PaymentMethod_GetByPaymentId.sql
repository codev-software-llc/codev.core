-------------------------------------------------------------------------------
-- This will return the payment method by a payment ID.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentMethod_GetByPaymentId]
(
    @PaymentId INT
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
         JOIN [core].[Payments] p on p.[PaymentMethodId] = e.[RowId]
     WHERE (p.[RowId] = @PaymentId) AND (e.[IsActive] = 1)

END

