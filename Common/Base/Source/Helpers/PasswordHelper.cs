//-----------------------------------------------------------------------------
// <copyright file="PasswordHelper.cs" company="Codev Software, LLC">
// Copyright © 2026
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
        public static String GenerateSalt(
            Int32 iterations)
        {
            if (iterations > 0)
            {
                Byte[] saltValue = GenerateRandomSalt(iterations);

                return Convert.ToBase64String(saltValue);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(iterations), "The number of iterations must be greater than zero.");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a salted-hash password.
        /// The incoming salt has 4 bytes of iteration-count and 32 bytes of
        /// actual salt.
        /// </summary>
        ///--------------------------------------------------------------------
        public static String GenerateHash(
            String password,
            String salt)
        {
            if (String.IsNullOrWhiteSpace(password) == false)
            {
                if (String.IsNullOrWhiteSpace(salt) == false)
                {
                    Byte[] saltBytes = Convert.FromBase64String(salt);

                    Byte[] passwordTextBytes = GenerateSaltedPasswordHash(password, saltBytes);

                    return Convert.ToBase64String(passwordTextBytes);
                }
                else
                {
                    throw new ArgumentException("The salt cannot be null or empty.", nameof(salt));
                }
            }
            else
            {
                throw new ArgumentException("The password cannot be null or empty.", nameof(password));
            }
        }

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

            Byte[] encoder = Rfc2898DeriveBytes.Pbkdf2(passwordTextBytes, saltBytes, iterations, HashAlgorithmName.SHA256, 64);

            return encoder;
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
