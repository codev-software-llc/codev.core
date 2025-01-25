//-----------------------------------------------------------------------------
// <copyright file="DesCipherProvider.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Cipher
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using Codev.Core.Base;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ICipher interface for encryption/decryption type
    /// functionality.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class DesCipherProvider : ICipherProvider
    {
        #region Constants
        /// -------------------------------------------------------------------
        /// <summary>
        /// Sets a default hash.
        /// </summary>
        /// -------------------------------------------------------------------
        private const String DefaultHash = "e1c146b6-7ff5-4aa3-ae8d-2e328e8dcc26";
        #endregion

        #region Constructors
        /// -------------------------------------------------------------------
        /// <summary>
        /// Construct the Cipher object.
        /// </summary>
        /// -------------------------------------------------------------------
        public DesCipherProvider(
            String keyHash = DefaultHash)
        {
            this.Cipher = DES.Create();

            keyHash = String.IsNullOrWhiteSpace(keyHash) ? String.Empty : keyHash;

            this.Cipher.Key = this.GenerateKey(keyHash);
            this.Cipher.IV  = this.GenerateIV();
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Construct the Cipher object using username and password.
        /// </summary>
        /// -------------------------------------------------------------------
        public DesCipherProvider(
            String userName,
            String password) : this(String.Format("{0}:{1}", String.IsNullOrWhiteSpace(userName) ? String.Empty : userName, String.IsNullOrWhiteSpace(password) ? String.Empty : password))
        {
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Construct the Cipher object using a GUID.
        /// </summary>
        /// -------------------------------------------------------------------
        public DesCipherProvider(
            Guid keyGuid) : this(keyGuid.ToString())
        {
        }
        #endregion

        #region Properties
        /// -------------------------------------------------------------------
        /// <summary>
        /// Defines the cipher object that will perform the encrypt or
        /// decrypt actions.
        /// </summary>
        /// -------------------------------------------------------------------
        private DES Cipher { get; set; }
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
            String encryptedText = String.Empty;

            try
            {
                using (ICryptoTransform ct = this.Cipher.CreateEncryptor(this.Cipher.Key, this.Cipher.IV))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            Byte[] bt = Encoding.UTF8.GetBytes(clearText);

                            cs.Write(bt, 0, bt.Length);

                            cs.FlushFinalBlock();

                            cs.Close();

                            encryptedText = Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new CoreProviderException(CoreErrorCode.InternalFailure, e);
            }

            return encryptedText;
        }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Decrypt an encrypted string blob to it's clear-text string.
        /// </summary>
        /// -------------------------------------------------------------------
        public String DecryptString(
            String cipherText)
        {
            try
            {
                String decryptedText = String.Empty;

                using (ICryptoTransform ct = this.Cipher.CreateDecryptor(this.Cipher.Key, this.Cipher.IV))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            Byte[] bt = Convert.FromBase64String(cipherText);

                            cs.Write(bt, 0, bt.Length);

                            cs.FlushFinalBlock();

                            cs.Close();

                            decryptedText = Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }

                return decryptedText;
            }
            catch (Exception e)
            {
                throw new CoreProviderException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion

        #region Methods (Private)
        /// -------------------------------------------------------------------
        /// <summary>
        /// Build the initialization vector.
        /// </summary>
        /// -------------------------------------------------------------------
        private Byte[] GenerateIV()
        {
            return new Byte[] { 38, 55, 206, 48, 28, 64, 20, 16 };
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Build the key using the private key and the combination of our 
        /// general key.
        /// </summary>
        ///--------------------------------------------------------------------
        private Byte[] GenerateKey(
            String keyHash)
        {
            Byte[] keySalt = Encoding.UTF8.GetBytes("Quorent, Inc Salt Value");

            Rfc2898DeriveBytes encoder = new Rfc2898DeriveBytes(keyHash, keySalt, 1000, HashAlgorithmName.SHA1);

            return encoder.GetBytes(8);
        }
        #endregion
    }
}
