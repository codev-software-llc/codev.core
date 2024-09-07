//-----------------------------------------------------------------------------
// <copyright file="PaymentService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Banking
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;
    using Codev.Core.Repository.Ado;

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
            Reference<Identity>      identityReference,
            Reference<PaymentMethod> paymentMethodReference,
            PaymentAmount            paymentAmount,
            String                   orderNumber)
        {
            try
            {
                Validation.ValidateParameter<Reference<Identity>>     ("identityReference"     , identityReference     );
                Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);
                Validation.ValidateParameter<PaymentAmount>           ("paymentAmount"         , paymentAmount         );

                Payment receipt = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                        if (paymentMethodEntity != null)
                        {
                            receipt = this.Charge(identityEntity, paymentMethodEntity, paymentAmount, orderNumber);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
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
            Reference<Identity>      identityReference,
            Reference<PaymentMethod> paymentMethodReference,
            Reference<Payment>       paymentReference,
            PaymentAmount            paymentAmount)
        {
            try
            {
                Validation.ValidateParameter<Reference<Identity>>     ("identityReference"     , identityReference     );
                Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);
                Validation.ValidateParameter<Reference<Payment>>      ("paymentReference"      , paymentReference      );
                Validation.ValidateParameter<PaymentAmount>           ("paymentAmount"         , paymentAmount         );

                Payment receipt = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        PaymentMethodEntity paymentMethodEntity = this.PaymentMethodRepository.GetById(paymentMethodReference.Id);

                        if (paymentMethodEntity != null)
                        {
                            receipt = this.Refund(identityEntity, paymentMethodEntity, null, paymentAmount);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.PaymentMethodDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
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
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
                Validation.ValidateParameter<PaymentCreditCard>  ("creditCard"       , creditCard       );

                PaymentMethod paymentMethod = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
            try
            {
                Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
            try
            {
                Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);

                PaymentMethod paymentMethod = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

                List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        paymentMethods = this.GetPaymentMethods(identityEntity);
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
            try
            {
                Validation.ValidateParameter<Reference<PaymentMethod>>("paymentMethodReference", paymentMethodReference);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
                Validation.ValidateParameter<PaymentCreditCard>  ("creditCard"       , creditCard       );

                Boolean isValid = false;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
