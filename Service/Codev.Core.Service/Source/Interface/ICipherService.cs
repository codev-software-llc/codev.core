//-----------------------------------------------------------------------------
// <copyright file="ICipherService.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Cipher
{
    using System;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This service provides for cipher encryption/decryption.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ICipherService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Encrypt a clear-text string into the encrypted string.
        /// </summary>
        ///--------------------------------------------------------------------
        String EncryptString(
            String clearText);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will decrypt the encrypted string back to clear text.
        /// </summary>
        ///--------------------------------------------------------------------
        String DecryptString(
            String encryptedText);
        #endregion
    }
}
