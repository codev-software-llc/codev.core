//-----------------------------------------------------------------------------
// <copyright file="CreatePlanRequest.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the means to create a new plan.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CreatePlanRequest
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the source of the plan (ApplicationName).
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
        /// Get or set interval count.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 IntervalCount { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the number of days for a trial period.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 TrialDays { get; set; }
        #endregion
    }
}