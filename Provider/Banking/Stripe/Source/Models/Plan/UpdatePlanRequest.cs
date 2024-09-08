//-----------------------------------------------------------------------------
// <copyright file="UpdatePlanRequest.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to update the details of a plan.
    /// </summary>
    ///------------------------------------------------------------------------
    public class UpdatePlanRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the source.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Source { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the cost of the plan (0 is a free plan).
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentAmount Cost { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set payment interval (Day, Week, Month or Year).
        /// </summary>
        ///--------------------------------------------------------------------
        public SubscriptionInterval Interval { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set interval count frequency.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 IntervalCount { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set trial days.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 TrialDays { get; set; }
        #endregion
    }
}