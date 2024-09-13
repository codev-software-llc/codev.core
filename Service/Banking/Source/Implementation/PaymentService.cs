//-----------------------------------------------------------------------------
// <copyright file="PaymentService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Banking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using Codev.Core.Service.Clock;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the payment service for transaction processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class PaymentService : BaseService, IPaymentService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the service.
        /// </summary>
        ///---------------------------------------------------------------
        public PaymentService(
            ICoreUnitOfWork               unitOfWork,
            IIdentityRepository           identityRepository,
            IPaymentMethodRepository      paymentMethodRepository,
            IPaymentProvider              paymentProvider,
            ISubscriptionCustomerProvider customerProvider,
            IClockService                 clockService) : base(unitOfWork)
        {
            Validation.ValidateParameter<IIdentityRepository>          ("identityRepository"     , identityRepository     );
            Validation.ValidateParameter<IPaymentMethodRepository>     ("paymentMethodRepository", paymentMethodRepository);
            Validation.ValidateParameter<IPaymentProvider>             ("paymentProvider"        , paymentProvider        );
            Validation.ValidateParameter<ISubscriptionCustomerProvider>("customerProvider"       , customerProvider       );
            Validation.ValidateParameter<IClockService>                ("clockService"           , clockService           );

            this.IdentityRepository      = identityRepository;
            this.PaymentMethodRepository = paymentMethodRepository;
            this.PaymentProvider         = paymentProvider;
            this.CustomerProvider        = customerProvider;
            this.ClockService            = clockService;

            // Initialize our supported cards from a business standpoint.
            //
            this.InitializeSupportedCards();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the identity repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment method repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IPaymentMethodRepository PaymentMethodRepository { get; set; }


        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private IPaymentProvider PaymentProvider { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the customer provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private ISubscriptionCustomerProvider CustomerProvider { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Maintains the collection of supported credit cards that our
        /// system supports.
        /// </summary>
        ///--------------------------------------------------------------------
        private HashSet<CreditCardType> SupportedCreditCards { get; set; }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the clock service.
        /// </summary>
        ///--------------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Perform a one time charge.  This is essentially a Authorize/Capture
        /// provided by the merchant gateway.
        /// </summary>
        ///--------------------------------------------------------------------
        public Payment Charge(
            IdentityEntity      identityEntity,
            PaymentMethodEntity paymentMethodEntity,
            PaymentAmount       paymentAmount,
            String              orderNumber)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            LocalDateTime localDateTime = instantNow.ToLocalDateTime(identityEntity.TimeZone);

            // Invoke the payment provider with the receipt.
            //
            Token token = this.PaymentProvider.Charge(instantNow, localDateTime.Date, paymentMethodEntity.Token, paymentAmount, orderNumber);

            // Persist the information about our payment.
            //
            PaymentEntity payment = new PaymentEntity(instantNow)
                {
                     Flags         = PaymentFlags.None,
                     PaymentStatus = token.IsSuccess ? PaymentStatus.Success : PaymentStatus.Failure,
                     PaymentMethod = paymentMethodEntity,
                     PaymentAmount = paymentAmount,
                     OrderNumber   = orderNumber,
                     Token         = token
                };

            //
            // WORK: Need to add payment repository.
            //
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Refund a "Captured" payment.  The payment amount MUST match the
        /// payment receipt amount in the case where a Capture is still 
        /// pending settlement.  If in the future we can move the authorization
        /// information into this cal, we can void both the Authorize and 
        /// Capture in the case capture is unsettled.
        /// </summary>
        ///--------------------------------------------------------------------
        public Payment Refund(
            IdentityEntity      identityEntity,
            PaymentMethodEntity paymentMethodEntity,
            PaymentEntity       paymentEntity,
            PaymentAmount       paymentAmount)
        {
            // PaymentEntity paymentEntity = this.PaymentRepository.
            Instant instantNow = this.ClockService.GetCurrentInstant();
            
            LocalDateTime dateTime = instantNow.ToLocalDateTime(identityEntity.TimeZone);
            
            
             //this.PaymentProvider.Refund(instantNow, dateTime.Date, pay
            
             throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethod Register(
            IdentityEntity    identityEntity,
            PaymentCreditCard creditCard,
            Boolean           isPrimary)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            creditCard.Type = this.ValidateCard(identityEntity, creditCard).ToString();

            // Register the card with the payment provider.
            //
            //Token token = this.CustomerProvider.AddCard(identityEntity.Token, creditCard, instantNow);

            Token token = new Token()
                {
                    Id          = String.Empty,
                    Description = "Token",
                    IsSuccess   = true,
                    Title       = "Token",
                    Type        = "Stub"
                };

            // Clear out the primary destinations if this is the new
            // one.
            //
            if (isPrimary)
            {
                this.ClearPrimaryPaymentMethods(identityEntity);
            }

            PaymentMethodEntity entity = new PaymentMethodEntity(instantNow)
                {
                    Flags            = isPrimary ? PaymentMethodFlags.Primary : PaymentMethodFlags.None,
                    Identity         = identityEntity,
                    OffuscatedNumber = creditCard.Number.OffuscatedCardNumber(),
                    Expiration       = String.Format("{0:00}/{1:0000}", creditCard.DateExpriation.Month, creditCard.DateExpriation.Year),
                    Token            = token
                };

            this.PaymentMethodRepository.Add(entity);

            return entity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the payment method.  If this payment method is utilized in
        /// any subscriptions, it to will be removed as well as the
        /// subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Unregister(
            PaymentMethodEntity paymentMethodEntity)
        {
            // Invoke the provider to remove the card.
            //
            //this.CustomerProvider.RemoveCard(entity.Token);

            // Now remove it from our repository (soft delete).
            //
            this.PaymentMethodRepository.Delete(paymentMethodEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethod GetPaymentMethod(
            PaymentMethodEntity paymentMethodEntity)
        {
            return paymentMethodEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the payment methods for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<PaymentMethod> GetPaymentMethods(
            IdentityEntity identityEntity)
        {
            EntityCollection<PaymentMethodEntity> entities = this.PaymentMethodRepository.GetAllByIdentity(identityEntity);

            return entities.Select(x => x.ToModel()).ToList();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the payment method as primary.
        /// </summary>
        ///--------------------------------------------------------------------
        public void SetPrimary(
            PaymentMethodEntity paymentMethodEntity)
        {
            // Clear out the primary on all payment methods.
            //
            this.ClearPrimaryPaymentMethods(paymentMethodEntity.Identity);

            // Refetch our entity as it has now been versioned from 
            // the above call.
            //
            paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodEntity.Id);

            paymentMethodEntity.DateModified = this.ClockService.GetCurrentInstant();

            paymentMethodEntity.Flags |= PaymentMethodFlags.Primary;

            this.PaymentMethodRepository.Update(paymentMethodEntity);
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Validate a card.  This does not result in any service related
        /// calls.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean Validate(
            IdentityEntity    identityEntity,
            PaymentCreditCard paymentCard)
        {
            this.ValidateCard(identityEntity, paymentCard);
            
            return true;
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------       
        /// <summary>
        /// Initialize the known cards we support.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeSupportedCards()
        {
            this.SupportedCreditCards = new HashSet<CreditCardType>();

            this.SupportedCreditCards.Add(CreditCardType.Visa);
            this.SupportedCreditCards.Add(CreditCardType.MasterCard);
            this.SupportedCreditCards.Add(CreditCardType.Amex);
            this.SupportedCreditCards.Add(CreditCardType.JCB);
            this.SupportedCreditCards.Add(CreditCardType.Discover);
            this.SupportedCreditCards.Add(CreditCardType.Diners);
        }

        ///--------------------------------------------------------------------       
        /// <summary>
        /// Validate the credit card.
        /// </summary>
        ///--------------------------------------------------------------------
        private CreditCardType ValidateCard(
            IdentityEntity    identityEntity,
            PaymentCreditCard paymentCard)
        {
            CreditCardType cardType = CreditCardType.Unknown;

            CoreErrorCode errorCode;

            // Validate the card.
            //
            if (paymentCard != null)
            {
                LocalDateTime dateTime = this.ClockService.GetLocalDateTime(identityEntity.TimeZone);
            
                cardType = ValidateCreditCard.GetCardType(dateTime.Date, paymentCard.Number, paymentCard.DateExpriation, out errorCode);
            
                if (cardType == CreditCardType.Unknown)
                {
                    throw new CoreLogicException(errorCode, String.Format("Failed card validation - {0}", errorCode.ToString()));
                }
                else
                {
                    if (this.SupportedCreditCards.Contains(cardType) == false)
                    {
                        throw new CoreLogicException(CoreErrorCode.DeclinedCardType, String.Format("Failed card validation - {0}", errorCode.ToString()));
                    }
                }
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.InvalidParameter, "No payment information was provided.");
            }
            
            // Validate the CVC code.
            //
            if (String.IsNullOrWhiteSpace(paymentCard.Code))
            {
                throw new CoreLogicException(CoreErrorCode.DeclinedCvv, "Invalid CVC code.");
            }

            return cardType;
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Clear out the primary payment methods.
        /// </summary>
        ///---------------------------------------------------------------
        private void ClearPrimaryPaymentMethods(
            IdentityEntity identityEntity)
        {
            // Remove primary on all other payment methods.
            //
            EntityCollection<PaymentMethodEntity> paymentMethods = this.PaymentMethodRepository.GetAllByIdentity(identityEntity);

            foreach (PaymentMethodEntity item in paymentMethods)
            {
                item.Flags &= ~PaymentMethodFlags.Primary;

                this.PaymentMethodRepository.Update(item);
            }
        }
        #endregion
    }
}
