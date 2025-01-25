//-----------------------------------------------------------------------------
// <copyright file="PasswordHelper.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains helper routines for generating maintaining passwords.
    /// </summary>
    ///-----------------------------------------------------------------------
    public static class PasswordHelper
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a random salt value we can use for the authentication
        /// password.  The first 4 bytes are the desired number of iterations 
        /// for hashing; the remaining 32 are actual salt.
        /// </summary>
        ///--------------------------------------------------------------------   
        public static Byte[] GenerateRandomSalt(
            Int32 iterations)
        {
            Byte[] saltValue = new Byte[32];

            using (RandomNumberGenerator cryptoProvider = RandomNumberGenerator.Create())
            {
                cryptoProvider.GetNonZeroBytes(saltValue);
            }

            Byte[] iterationsBytes = BitConverter.GetBytes(iterations);

            Byte[] returnValue = new Byte[iterationsBytes.Length + saltValue.Length];

            iterationsBytes.CopyTo(returnValue, 0);

            saltValue.CopyTo(returnValue, iterationsBytes.Length);

            return returnValue;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a salted-hash password.
        /// The incoming salt has 4 bytes of iteration-count and 32 bytes of 
        /// actual salt.
        /// </summary>
        ///--------------------------------------------------------------------   
        public static Byte[] GenerateSaltedPasswordHash(
            String passwordText,
            Byte[] iterationsAndSalt)
        {
            Byte[] passwordTextBytes = UTF8Encoding.UTF8.GetBytes(passwordText);

            Int32 iterations = BitConverter.ToInt32(iterationsAndSalt, 0);

            Byte[] saltBytes = iterationsAndSalt.Skip(4).ToArray();

            using (Rfc2898DeriveBytes pbkdf2 = new(passwordTextBytes, saltBytes, iterations, HashAlgorithmName.SHA1))
            {
                return pbkdf2.GetBytes(64);  // storing 64-byte hashes
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a salted-hash password.
        /// </summary>
        ///--------------------------------------------------------------------   
        public static Boolean ValidatePassword(
            String passwordText,
            Byte[] passwordHashValid,
            Byte[] iterationsAndSalt)
        {
            Byte[] saltedHashComputed = PasswordHelper.GenerateSaltedPasswordHash(passwordText, iterationsAndSalt);

            return saltedHashComputed.SequenceEqual<Byte>(passwordHashValid);
        }
        #endregion
    }
}
