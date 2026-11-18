//-----------------------------------------------------------------------------
// <copyright file="IPaymentService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Banking
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the payment service funtionality.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IPaymentService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Perform an authorization and capture at the same transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        Payment Charge(
            Reference<PaymentMethod> paymentMethodReference,
            PaymentAmount            paymentAmount,
            String                   orderNumber);

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Refund a "Captured" payment.
        /// </summary>
        ///--------------------------------------------------------------------
        Payment Refund(
            Reference<PaymentMethod> paymentMethodReference,
            Reference<Payment>       paymentReference,
            PaymentAmount            paymentAmount);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        PaymentMethod Register(
            Reference<Identity> identityReference,
            PaymentCreditCard   creditCard,
            Boolean             isPrimary);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the payment method as primary.
        /// </summary>
        ///--------------------------------------------------------------------
        void SetPrimary(
            Reference<PaymentMethod> paymentMethodReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the payment method.  If this payment method is utilized in
        /// any subscriptions, it to will be removed as well as the
        /// subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        void Unregister(
            Reference<PaymentMethod> paymentMethodReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        PaymentMethod GetPaymentMethod(
            Reference<PaymentMethod> paymentMethodReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the payment methods for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<PaymentMethod> GetPaymentMethods(
            Reference<Identity> identityReference);

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Validate a card.  This does not result in any service related
        /// calls.
        /// </summary>
        ///--------------------------------------------------------------------
        Boolean Validate(
            Reference<Identity> identityReference,
            PaymentCreditCard   paymentCard);
        #endregion
    }
}
