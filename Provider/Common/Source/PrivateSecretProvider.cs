//-----------------------------------------------------------------------------
// <copyright file="PrivateSecretProvider.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Authentication
{
    using System;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the secret provider for cashen/williams usage.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class PrivateSecretProvider : ISecretProvider
    {
        #region Constructors
        /// -------------------------------------------------------------------
        /// <summary>
        /// Construct the provider.
        /// </summary>
        /// -------------------------------------------------------------------
        public PrivateSecretProvider()
        {
        }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a secret key.
        /// </summary>
        ///--------------------------------------------------------------------
        public String GenerateSecret()
        {
            return "sccw";
        }
        #endregion
    }
}
