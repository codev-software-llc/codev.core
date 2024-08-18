-------------------------------------------------------------------------------
-- This will add a new entity.
-------------------------------------------------------------------------------
CREATE PROCEDURE [core].[PaymentTransaction_Add]
(
    @Flags           BIGINT       ,
    @DateCreated     DATETIME     ,
    @DateModified    DATETIME     ,
    @PaymentId       INT          ,
    @TransactionType INT          ,
    @IsSuccess       BIT          ,
    @SerializedData  NVARCHAR(MAX)
)
AS
BEGIN

    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

    SET NOCOUNT ON;

    INSERT INTO [core].[PaymentTransactions]
        (
            [IsActive]       ,
            [Flags]          ,
            [DateCreated]    ,
            [DateModified]   ,
            [PaymentId]      ,
            [TransactionType],
            [IsSuccess]      ,
            [SerializedData]
        )
    VALUES
        (
            1               ,
            @Flags          ,
            @DateCreated    ,
            @DateModified   ,
            @PaymentId      ,
            @TransactionType,
            @IsSuccess      ,
            @SerializedData
        )

    SELECT [RowId] as 'RowId', [RowVersion] as 'RowVersion' FROM [core].[PaymentTransactions] WHERE [RowId] = @@IDENTITY

END