//-----------------------------------------------------------------------------
// <copyright file="Session.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the Session model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Session : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Session() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the session creation date.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateCreated { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date the session expires.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateExpiration { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the session secret.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Secret { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Identity Identity { get; set; }
        #endregion
    }
}
