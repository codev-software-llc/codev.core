//-----------------------------------------------------------------------------
// <copyright file="SecretHelper.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains support routines for generation of random secrets.
    /// 
    /// </summary>
    ///------------------------------------------------------------------------
    public class SecretHelper
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// These are the base characters to use for the random strings.  They
        /// are all eye-friendly, URL- and filename-safe characters. (For 
        /// example, symbols, 0/O, and l/1/I are not included to make manual 
        /// transcription and transmission more easily accurate.)
        /// </summary> 
        ///--------------------------------------------------------------------
        private const String BaseChars = "23456789abcdefghijkmnpqrstuvwxyz"; // "friendly base-32"

        ///--------------------------------------------------------------------
        /// <summary>
        /// The random number generator object we utilize to generate URLs.
        /// </summary> 
        ///--------------------------------------------------------------------
        private static readonly Random Rand = new();
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generates a random tiny-URL-style string composed of 
        /// eye-friendly, URL- and filename-safe characters.
        /// </summary>
        /// <param name="length">length of resulting string</param>
        /// <returns>the URL component</returns>
        ///--------------------------------------------------------------------
        public static String Generate(
            Int32 length)
        {
            Char[] tinyURL = new Char[length];

            for (Int32 i = 0; i < length; i++)
            {
                tinyURL[i] = BaseChars[Rand.Next() % BaseChars.Length];
            }

            return new String(tinyURL);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a random tinyURL, ensuring that it is not already in use.
        /// The requested minimum length is automatically increased 
        /// by one every three tries until an unused string is found.
        /// </summary>
        /// <param name="minLength">minimum length of resulting string</param>
        /// <param name="isInUse">predicate for whether a URL is in use</param>
        /// <returns>the tiny URL text</returns>
        ///--------------------------------------------------------------------
        public static String GenerateUnique(
            Int32             minLength,
            Predicate<String> isInUse)
        {
            String newURL;
            Int32  tries = 0;

            do
            {
                newURL = Generate(minLength);

                tries++;

                if (tries >= 3)
                {
                    tries = 0;

                    minLength++;
                }
            } 
            while (isInUse(newURL));

            return newURL;
        }
        #endregion
    }
}
