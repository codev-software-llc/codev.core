//-----------------------------------------------------------------------------
// <copyright file="CodevSecretProvider.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Authentication
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the secret provider for live.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class CodevSecretProvider : ISecretProvider
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Default confirmation secret length.
        /// </summary>
        ///-------------------------------------------------------------------
        private const Int32 ConfirmationSecretLength = 4; // of base-32
        #endregion

        #region Constructors
        /// -------------------------------------------------------------------
        /// <summary>
        /// Construct the provider.
        /// </summary>
        /// -------------------------------------------------------------------
        public CodevSecretProvider()
        {
        }
        #endregion

        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Generate a secret key.
        /// </summary>
        ///---------------------------------------------------------------
        public String GenerateSecret()
        {
            return SecretHelper.Generate(ConfirmationSecretLength);
        }
        #endregion
    }
}
