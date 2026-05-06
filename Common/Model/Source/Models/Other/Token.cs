//-----------------------------------------------------------------------------
// <copyright file="Token.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines a token object to represent an entity from a supported
    /// provider.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Token
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token()
        {
            this.IsSuccess   = true;
            this.Id          = String.Empty;
            this.Type        = String.Empty;
            this.Title       = String.Empty;
            this.Description = String.Empty;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the token identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the token retrieval was a successful one.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsSuccess { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the token type (Plan, Customer, Subscription, Payment).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Type { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set a title that represents the token entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Title { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the description of the token (can be anything really).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Description { get; set; }
        #endregion
    }
}