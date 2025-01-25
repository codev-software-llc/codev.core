//-----------------------------------------------------------------------------
// <copyright file="DeleteCardRequest.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to delete a card.
    /// </summary>
    ///------------------------------------------------------------------------
    public class DeleteCardRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the card to remove.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }
        #endregion
    }
}