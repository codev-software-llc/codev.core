//-----------------------------------------------------------------------------
// <copyright file="ICipherProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the supported calls through the cipher
    /// component.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ICipherProvider : IProvider
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Encrypt a clear-text string into the encrypted string.
        /// </summary>
        ///--------------------------------------------------------------------
        String EncryptString(
            String inputString);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will decrypt the encrypted string back to clear text.
        /// </summary>
        ///--------------------------------------------------------------------
        String DecryptString(
            String encryptedString);
        #endregion
    }
}
