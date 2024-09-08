//-----------------------------------------------------------------------------
// <copyright file="FakePaymentProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Fake
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the payment provider for direct charges and refunds.
    /// </summary>
    ///------------------------------------------------------------------------
    public class FakePaymentProvider : IPaymentProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public FakePaymentProvider(
            IMerchantCardRepository        cardRepository,
            IMerchantTransactionRepository transactionRepository,
            ISubscriptionCustomerProvider  customerProvider)
        {
            this.CardRepository        = cardRepository;
            this.TransactionRepository = transactionRepository;
            this.CustomerProvider      = customerProvider;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Token repository for holding customer carsd.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantCardRepository CardRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Transaction repository for holding transactions.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantTransactionRepository TransactionRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Tet or set the provider for customers.
        /// </summary>
        ///--------------------------------------------------------------------
        private ISubscriptionCustomerProvider CustomerProvider { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Perform a full charge on the card.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Charge(
            Instant       instantNow,
            LocalDate     currentDate,
            Token         cardToken,
            PaymentAmount paymentAmount,
            String        orderNumber)
        {
            MerchantCardEntity cardEntity = this.CardRepository.GetById(cardToken.ToId());

            if (cardEntity != null)
            {
                MerchantTransactionEntity chargeEntity;

                // Convert the card so that we can validate.
                //
                PaymentCreditCard paymentCard = cardEntity.ToCreditCard();

                CoreErrorCode errorCode;

                if (FakeLogic.Validate(currentDate, paymentCard, paymentAmount, out errorCode))
                {
                    chargeEntity = this.RecordTransaction(instantNow, cardEntity, MerchantTransactionFlags.Success, TransactionType.Charge, Guid.NewGuid(), paymentAmount, orderNumber);
                }
                else
                {
                    chargeEntity = this.RecordTransaction(instantNow, cardEntity, MerchantTransactionFlags.Failure, TransactionType.Charge, Guid.NewGuid(), paymentAmount, orderNumber);
                }

                return chargeEntity.ToToken();
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Card does not exist");
            }
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Refund a "Captured" payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Refund(
            Instant        instantNow,
            LocalDate      currentDate,
            Token          transactionToken,
            PaymentAmount  paymentAmount)
        {
            MerchantTransactionEntity transactionEntity = this.TransactionRepository.GetById(transactionToken.ToId());

            if (transactionEntity != null)
            {
                MerchantCardEntity cardEntity = this.CardRepository.GetById(transactionEntity.Card.Id);

                if (cardEntity != null)
                {
                    MerchantTransactionEntity refundEntity;

                    // Convert the card so that we can validate.
                    //
                    PaymentCreditCard paymentCard = cardEntity.ToCreditCard();

                    CoreErrorCode errorCode;

                    if (FakeLogic.Validate(currentDate, paymentCard, paymentAmount, out errorCode))
                    {
                        refundEntity = this.RecordTransaction(instantNow, cardEntity, MerchantTransactionFlags.Success, TransactionType.Refund, transactionEntity.TransactionGroup, paymentAmount, transactionEntity.OrderNumber);
                    }
                    else
                    {
                        refundEntity = this.RecordTransaction(instantNow, cardEntity, MerchantTransactionFlags.Failure, TransactionType.Refund, transactionEntity.TransactionGroup, paymentAmount, transactionEntity.OrderNumber);
                    }

                    return refundEntity.ToToken();
                }
                else
                {
                    throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Card does not exist");
                }
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Transaction does not exist");
            }
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------     
        /// <summary>
        /// Record a transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        private MerchantTransactionEntity RecordTransaction(
            Instant                  instantNow,
            MerchantCardEntity       cardEntity,
            MerchantTransactionFlags flags,
            TransactionType          type,
            Guid                     group,
            PaymentAmount            paymentAmount,
            String                   orderNumber)
        {
            MerchantTransactionEntity entity = new MerchantTransactionEntity(
                new BaseEntity(0, null, true, instantNow, instantNow))
                {
                    Flags            = flags,
                    TransactionType  = type,
                    TransactionGroup = group,
                    Payment          = paymentAmount,
                    OrderNumber      = orderNumber,
                    Card             = cardEntity,
                };

            this.TransactionRepository.Add(entity);

            return entity;
        }
        #endregion
    }
}