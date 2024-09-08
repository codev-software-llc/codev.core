//-----------------------------------------------------------------------------
// <copyright file="PaymentMethodExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using Codev.Core.Base;

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
