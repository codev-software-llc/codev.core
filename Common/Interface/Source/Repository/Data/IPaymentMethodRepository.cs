//-----------------------------------------------------------------------------
// <copyright file="IPaymentMethodRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the payment method repository access.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IPaymentMethodRepository : IRepository<PaymentMethodEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return all payment methods for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<PaymentMethodEntity> GetAllByIdentity(
            IdentityEntity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return payment method for the payment.
        /// </summary>
        ///--------------------------------------------------------------------
        PaymentMethodEntity GetByPayment(
            PaymentEntity payment);
        #endregion
    }
}
