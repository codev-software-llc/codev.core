//-----------------------------------------------------------------------------
// <copyright file="MerchantPlanEntity.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This represents an entity with tokenized information.
    /// </summary>
    ///------------------------------------------------------------------------
    public class MerchantPlanEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct our entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantPlanEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
        }
        #endregion

        #region Properties (Base)
        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the entity flags.
        /// </summary>   
        ///--------------------------------------------------------------------        
        public MerchantPlanFlags Flags { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the source of the plan (application name?).
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
        /// Get or set the plan interval.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public SubscriptionInterval Interval { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the plan interval count.  For instance, if a Month
        /// interval, a (3) would be every three monthds.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public Int32 IntervalCount { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the cost of the plan.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public PaymentAmount Cost { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the number of days in a trial before payment begins.
        /// </summary>   
        ///--------------------------------------------------------------------  
        public Int32 TrialDays { get; set; }
        #endregion
    }
}
