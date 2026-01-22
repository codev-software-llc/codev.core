//-----------------------------------------------------------------------------
// <copyright file="Subscription.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the Subscription model that binds a License to an
    /// identity.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Subscription : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Subscription() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the license.
        /// </summary>
        ///--------------------------------------------------------------------
        public License License { get; set;}

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Identity Identity { get; set; }
        #endregion
    }
}
