-------------------------------------------------------------------------------
-- This will update a transaction.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentTransaction_Update]
(
    @RowId           INT      ,
    @RowVersion      TIMESTAMP,
    @Flags           BIGINT   ,
    @DateCreated     DATETIME ,
    @DateModified    DATETIME ,
    @PaymentId       INT      ,
    @TransactionType INT      ,
    @IsSuccess       BIT      ,
    @SerializedData  NVARCHAR(MAX)
)
AS
BEGIN

    SET NOCOUNT ON;

    UPDATE [core].[PaymentTransactions] SET
        [Flags]           = @Flags          ,
        [DateCreated]     = @DateCreated    ,
        [DateModified]    = @DateModified   ,
        [PaymentId]       = @PaymentId      ,
        [TransactionType] = @TransactionType,
        [IsSuccess]       = @IsSuccess      ,
        [SerializedData]  = @SerializedData
    WHERE ([RowId] = @RowId) AND ([RowVersion] = @RowVersion)

   IF (@@ROWCOUNT = 0)
   BEGIN
       RAISERROR('Entity not found or Version is out of date - %d', 16, 1, 1)
   END

   SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[PaymentTransactions] WHERE [RowId] = @RowId

END
