-------------------------------------------------------------------------------
-- This will add a new entity record.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[ServiceLink_Add]
(
    @IdentityId   INT         ,
    @Flags        BIGINT      ,
    @DateCreated  DATETIME    ,
    @DateModified DATETIME    ,
    @TinyUrl      NVARCHAR(64),
    @DetailType   NVARCHAR(64),
    @Detail       NVARCHAR(2048)
)
AS 
BEGIN

    SET NOCOUNT ON;

    INSERT INTO [core].[ServiceLinks]
        (           
            [IsActive]    ,
            [IdentityId]  ,
            [Flags]       ,
            [DateCreated] ,
            [DateModified],
            [TinyUrl]     ,
            [DetailType]  ,
            [Detail]
        )
    VALUES
        (
            1            ,
            @IdentityId  ,
            @Flags       ,
            @DateCreated ,
            @DateModified,
            @TinyUrl     ,
            @DetailType  ,
            @Detail
        ) 
    
    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[ServiceLinks] WHERE [RowId] = @@IDENTITY

END
