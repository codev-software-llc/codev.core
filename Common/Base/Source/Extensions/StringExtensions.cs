//-----------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Extends the String class.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class StringExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return an offuscated card number.
        /// </summary>
        ///--------------------------------------------------------------------
        public static String OffuscatedCardNumber(
            this String value)
        {
            String[] split = value.Split(new Char[] { ' ', '-', '/', '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length > 0)
            {
                String number = split[split.Length - 1];

                if (number.Length >= 4)
                {
                    return String.Format("**** {0}", number.Substring(number.Length - 4, 4));
                }
            }

            return value;
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Return a flatten number from basic phone formatting.
        /// </summary>
        ///------------------------------------------------------------------------
        public static String FlattenPhone(
            this String phoneNumber)
        {
            // Strip and flatten the number.
            //
            return phoneNumber.Trim().Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "");
        }

        ///------------------------------------------------------------------------
        /// <summary>
        /// Return a formated phone-number "(xxx) yyy-zzzz".
        /// </summary>
        ///------------------------------------------------------------------------
        public static String ToFormatedPhone(
            this String phoneNumber)
        {
            // Strip and flatten the number.
            //
            phoneNumber = phoneNumber.FlattenPhone();

            if (phoneNumber.Length == 10)
            {
                String area   = phoneNumber.Substring(0, 3);
                String digit3 = phoneNumber.Substring(3, 3);
                String digit4 = phoneNumber.Substring(6, 4);

                phoneNumber = String.Format("({0}) {1}-{2}", area, digit3, digit4);
            }

            return phoneNumber;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return a trimmed value without spaces.
        /// </summary>
        ///--------------------------------------------------------------------
        public static String TrimSpaces(
            this String value)
        {
            if (String.IsNullOrWhiteSpace(value) == false)
            {
                return value.Trim();
            }

            return String.Empty;
        }
        #endregion
    }
}
