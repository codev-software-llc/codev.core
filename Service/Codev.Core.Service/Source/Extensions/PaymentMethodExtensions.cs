//-----------------------------------------------------------------------------
// <copyright file="PaymentMethodExtensions.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the PaymentMethod entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class PaymentMethodExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static PaymentMethod ToModel(
            this PaymentMethodEntity entity)
        {
            return new PaymentMethod()
                {
                    Id               = entity.Id,
                    IsPrimary        = (entity.Flags & PaymentMethodFlags.Primary) != 0,
                    Type             = entity.Token.Title,
                    OffuscatedNumber = entity.OffuscatedNumber,
                    Expiration       = entity.Expiration
                };
        }
        #endregion
    }
}
