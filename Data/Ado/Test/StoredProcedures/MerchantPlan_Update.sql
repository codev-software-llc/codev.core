-------------------------------------------------------------------------------
-- This will update the entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[MerchantPlan_Update]
(
    @RowId         INT          ,
    @RowVersion    TIMESTAMP    ,
    @Flags         BIGINT       ,
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

    UPDATE [core].[MerchantPlans] SET
        [Flags]         = @Flags        ,
        [DateCreated]   = @DateCreated  ,
        [DateModified]  = @DateModified ,
        [Source]        = @Source       ,
        [Name]          = @Name         ,
        [Interval]      = @Interval     ,
        [IntervalCount] = @IntervalCount,
        [CurrencyCode]  = @CurrencyCode ,
        [Amount]        = @Amount       ,
        [TrialDays]     = @TrialDays
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

    IF (@@ROWCOUNT = 0)
    BEGIN
        RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
    END

    SELECT [RowId], [RowVersion] FROM [core].[MerchantPlans] WHERE [RowId] = @RowId

END
