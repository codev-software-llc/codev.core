-------------------------------------------------------------------------------
-- This will add a new entity to the repository.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantPlan_Add]
(
    @Flags         INT          ,
    @DateCreated   DATETIME     ,
    @DateModified  DATETIME     ,
    @Source        NVARCHAR(128),
    @Name          NVARCHAR(128),
    @Interval      INT          ,
    @IntervalCount INT          ,
    @CurrencyCode  NVARCHAR(3)  ,
    @Amount        DECIMAL(18,2),
    @TrialDays     INT
)
AS
BEGIN
 
    SET NOCOUNT ON;

    INSERT INTO [core].[MerchantPlans]
        (           
            [IsActive]     ,
            [Flags]        ,
            [DateCreated]  ,
            [DateModified] ,
            [Source]       ,
            [Name]         ,
            [Interval]     ,
            [IntervalCount],
            [CurrencyCode] ,
            [Amount]       ,
            [TrialDays]
        )
    VALUES
        (
            1             ,
            @Flags        ,
            @DateCreated  ,
            @DateModified ,
            @Source       ,
            @Name         ,
            @Interval     ,
            @IntervalCount,
            @CurrencyCode ,
            @Amount       ,
            @TrialDays
        ) 

    SELECT [RowId], [RowVersion] FROM [core].[MerchantPlans] WHERE [RowId] = @@IDENTITY

END
