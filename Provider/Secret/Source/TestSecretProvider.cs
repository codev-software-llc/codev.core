//-----------------------------------------------------------------------------
// <copyright file="TestSecretProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider
{
    using System;
    using Codev.Core.Common.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the secret provider for testing.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class TestSecretProvider : ISecretProvider
    {
        #region Constructors
        /// -------------------------------------------------------------------
        /// <summary>
        /// Construct the provider.
        /// </summary>
        /// -------------------------------------------------------------------
        public TestSecretProvider()
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
            return "xxxx";
        }
        #endregion
    }
}
