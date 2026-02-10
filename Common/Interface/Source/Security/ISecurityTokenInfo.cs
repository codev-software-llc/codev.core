//-----------------------------------------------------------------------------
// <copyright file="ISecurityTokenInfo.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the information needed for JWT generation.
    /// component.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ISecurityTokenInfo
    {
        #region Properties
        /// -------------------------------------------------------------------
        /// <summary>
        /// Return the signing key.
        /// </summary>
        /// -------------------------------------------------------------------
        Guid SigningKey { get; }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Return the issuer value.
        /// </summary>
        /// -------------------------------------------------------------------
        String Issuer { get; }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Return the audience value.
        /// </summary>
        /// -------------------------------------------------------------------
        String Audience { get; }
        #endregion
    }
}
