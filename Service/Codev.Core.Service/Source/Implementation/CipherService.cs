//-----------------------------------------------------------------------------
// <copyright file="CipherService.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Cipher
{
    using System;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ICipherService interface for encryption/decryption
    /// type functionality.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class CipherService : ICipherService
    {
        #region Constructors
        /// -------------------------------------------------------------------
        /// <summary>
        /// Construct the Cipher service.
        /// </summary>
        /// -------------------------------------------------------------------
        public CipherService(
            ICipherProvider cipherProvider)
        {
            Validation.ValidateParameter<ICipherProvider>("cipherProvider", cipherProvider);

            this.CipherProvider = cipherProvider;
        }
        #endregion

        #region Properties
        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set the provider for the cipher we'll be implementing.
        /// </summary>
        /// -------------------------------------------------------------------
        private ICipherProvider CipherProvider { get; set; }
        #endregion

        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Generate an encrypted string from a clear-text string.
        /// </summary>
        /// -------------------------------------------------------------------
        public String EncryptString(
            String clearText)
        {
            return this.CipherProvider.EncryptString(clearText);
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Decrypt an encrypted string blob to it's clear-text string.
        /// </summary>
        /// -------------------------------------------------------------------
        public String DecryptString(
            String encryptedText)
        {
            return this.CipherProvider.DecryptString(encryptedText);
        }
        #endregion
    }
}
