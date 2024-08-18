-------------------------------------------------------------------------------
-- Return the entity using its unique identifier.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[Communication_GetById]
(
    @RowId INT
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    SELECT
        e.[RowId]        as 'RowId'       ,
        e.[RowVersion]   as 'RowVersion'  ,
        e.[IsActive]     as 'IsActive'    ,
        e.[Flags]        as 'Flags'       ,
        e.[DateCreated]  as 'DateCreated' ,
        e.[DateModified] as 'DateModified',
        e.[Application]  as 'Application' ,
        e.[Category]     as 'Category'    ,
        e.[Subcategory]  as 'Subcategory' ,
        e.[Name]         as 'Name'        ,
        e.[EmailAddress] as 'EmailAddress',
        e.[Comments]     as 'Comments'
     FROM [core].[Communications] e
     WHERE (e.[RowId] = @RowId) AND (e.[IsActive] = 1)

END
