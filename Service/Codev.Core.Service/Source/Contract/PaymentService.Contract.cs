//-----------------------------------------------------------------------------
// <copyright file="PaymentService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Banking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains the explicit interface hooks that we will use to perform
    /// call validation on the parameters.  Each of these methods will in-turn
    /// call the actual implementations.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class PaymentService : IPaymentService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Perform an authorization and capture at the same transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        Payment IPaymentService.Charge(
            Reference<PaymentMethod> paymentMethodReference,
            PaymentAmount            paymentAmount,
            String                   orderNumber)
        {
            Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);
            Validation.ValidateParameter<PaymentAmount>           ("paymentAmount"         , paymentAmount         );

            try
            {
                PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                if (paymentMethodEntity != null)
                {
                    return this.Charge(paymentMethodEntity, paymentAmount, orderNumber);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Refund a "Captured" payment.
        /// </summary>
        ///--------------------------------------------------------------------
        Payment IPaymentService.Refund(
            Reference<PaymentMethod> paymentMethodReference,
            Reference<Payment>       paymentReference,
            PaymentAmount            paymentAmount)
        {
            Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);
            Validation.ValidateParameter<Reference<Payment>>      ("paymentReference"      , paymentReference      );
            Validation.ValidateParameter<PaymentAmount>           ("paymentAmount"         , paymentAmount         );

            try
            {
                PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                if (paymentMethodEntity != null)
                {
                    return this.Refund(paymentMethodEntity, null, paymentAmount);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                };
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        PaymentMethod IPaymentService.Register(
            Reference<Identity> identityReference,
            PaymentCreditCard   creditCard,
            Boolean             isPrimary)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
            Validation.ValidateParameter<PaymentCreditCard>  ("creditCard"       , creditCard       );

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    return this.Register(identityEntity, creditCard, isPrimary);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the payment method.  If this payment method is utilized in
        /// any subscriptions, it to will be removed as well as the
        /// subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        void IPaymentService.Unregister(
            Reference<PaymentMethod> paymentMethodReference)
        {
            Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);

            try
            {
                PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                if (paymentMethodEntity != null)
                {
                    this.Unregister(paymentMethodEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        PaymentMethod IPaymentService.GetPaymentMethod(
            Reference<PaymentMethod> paymentMethodReference)
        {
            Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);

            try
            {
                PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                if (paymentMethodEntity != null)
                {
                    return this.GetPaymentMethod(paymentMethodEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the payment methods for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<PaymentMethod> IPaymentService.GetPaymentMethods(
            Reference<Identity> identityReference)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    EntityCollection<PaymentMethodEntity> entities = this.PaymentMethodRepository.GetAllByIdentity(identityEntity);

                    return entities.Select(x => x.ToModel()).ToList();
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the payment method as primary.
        /// </summary>
        ///--------------------------------------------------------------------
        void IPaymentService.SetPrimary(
            Reference<PaymentMethod> paymentMethodReference)
        {
            Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);

            try
            {
                PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                if (paymentMethodEntity != null)
                {
                    this.SetPrimary(paymentMethodEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Validate a card.  This does not result in any service related
        /// calls.
        /// </summary>
        ///--------------------------------------------------------------------
        Boolean IPaymentService.Validate(
            Reference<Identity> identityReference,
            PaymentCreditCard   creditCard)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
            Validation.ValidateParameter<PaymentCreditCard>  ("creditCard"       , creditCard       );

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    return this.Validate(identityEntity, creditCard);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion
    }
}
