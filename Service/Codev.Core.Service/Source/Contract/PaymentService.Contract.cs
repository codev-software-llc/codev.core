//-----------------------------------------------------------------------------
// <copyright file="PaymentService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Banking
{
    using System;
    using System.Collections.Generic;
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
                Payment receipt = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                    if (paymentMethodEntity != null)
                    {
                        receipt = this.Charge(paymentMethodEntity, paymentAmount, orderNumber);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return receipt;
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
                Payment receipt = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                    if (paymentMethodEntity != null)
                    {
                        receipt = this.Refund(paymentMethodEntity, null, paymentAmount);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return receipt;
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
                PaymentMethod paymentMethod = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        paymentMethod = this.Register(identityEntity, creditCard, isPrimary);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return paymentMethod;
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
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

                    work.Commit();
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
                PaymentMethod paymentMethod = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                    if (paymentMethodEntity != null)
                    {
                        paymentMethod = this.GetPaymentMethod(paymentMethodEntity);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return paymentMethod;
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
                List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        EntityCollection<PaymentMethodEntity> entities = this.PaymentMethodRepository.GetAllByIdentity(identityEntity);

                        foreach (PaymentMethodEntity entity in entities)
                        {
                            paymentMethods.Add(entity.ToModel());
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return paymentMethods;
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
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

                    work.Commit();
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
                Boolean isValid = false;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        isValid = this.Validate(identityEntity, creditCard);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return isValid;
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
