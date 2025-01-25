//-----------------------------------------------------------------------------
// <copyright file="AddressInfo.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines an address class.  This is used wherever we need a
    /// common means to represent a billing, shipping, home address.
    /// </summary>
    ///------------------------------------------------------------------------
    public class AddressInfo
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public AddressInfo()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Returns whether the address is fully qualified.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsValid
        {
            get
            {
                return !((String.IsNullOrEmpty(this.Street1) && String.IsNullOrEmpty(this.Street2) && String.IsNullOrEmpty(this.Street3)) ||
                        String.IsNullOrEmpty(this.City) ||
                        String.IsNullOrEmpty(this.State) ||
                        String.IsNullOrEmpty(this.PostalCode));
            }
        }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set address line 1.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Street1 { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set address line 2.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Street2 { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set address line 3.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Street3 { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the city.
        /// </summary>
        ///--------------------------------------------------------------------
        public String City { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the state provice.
        /// </summary>
        ///--------------------------------------------------------------------
        public String State { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the postal code.
        /// </summary>
        ///--------------------------------------------------------------------
        public String PostalCode { get; set; }

        ///-------------------------------------------------------------------- 
        /// <summary>
        /// Get or set the (3) character ISO country code.
        /// </summary>
        ///--------------------------------------------------------------------
        public String CountryCode { get; set; }
        #endregion
    }
}