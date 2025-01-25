//-----------------------------------------------------------------------------
// <copyright file="ISecretProvider.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the calls for authentication secret generator.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ISecretProvider : IProvider
    {
        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Generate a secret key.
        /// </summary>
        ///---------------------------------------------------------------
        String GenerateSecret();
        #endregion
    }
}
