//-----------------------------------------------------------------------------
// <copyright file="BasePayment.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides the base class for a payment type.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BasePayment
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public BasePayment()
        {
            this.Name = String.Empty;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------  
        /// <summary>
        /// Get or set the name of the payment owner.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }
        #endregion
    }
}