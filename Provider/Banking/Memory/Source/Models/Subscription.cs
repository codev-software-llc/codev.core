//-----------------------------------------------------------------------------
// <copyright file="Subscription.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Memory
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the local object for a subscription.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class Subscription
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public Subscription()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier for the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public String PlanTokenId { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the customer identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public String CustomerTokenId { get; set; }
        #endregion
    }
}