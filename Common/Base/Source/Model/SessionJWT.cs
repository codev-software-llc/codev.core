//-----------------------------------------------------------------------------
// <copyright file="SessionJWT.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the Session model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SessionJWT
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public SessionJWT() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the session token.
        /// </summary>
        ///--------------------------------------------------------------------
        public String SessionToken { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the session refresh token.
        /// </summary>
        ///--------------------------------------------------------------------
        public String RefreshToken { get; set; }
        #endregion
    }
}
