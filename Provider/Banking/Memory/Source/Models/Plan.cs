//-----------------------------------------------------------------------------
// <copyright file="Plan.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Memory
{
    using System;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the local object for a subscription plan.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class Plan
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public Plan()
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
        /// Get or set the name of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment amount of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentAmount Cost { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment interval.
        /// </summary>
        ///--------------------------------------------------------------------
        public SubscriptionInterval Interval { get; set; }
        #endregion
    }
}