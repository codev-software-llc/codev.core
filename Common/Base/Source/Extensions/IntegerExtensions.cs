//-----------------------------------------------------------------------------
// <copyright file="IntegerExtensions.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Extends the integer class.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class IntegerExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the boolean type of integer.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Boolean ToBoolean(
            this Int32 value)
        {
            return value == 0 ? false : true;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the boolean type of integer.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Boolean ToBoolean(
            this Int64 value)
        {
            return value == 0 ? false : true;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the flags from the integer.
        /// </summary>
        ///--------------------------------------------------------------------
        public static T ToEnum<T>(
            this Int32 value)
        {
            Type type = typeof(T);

            if (type.IsEnum)
            {
                if (type.IsEnumDefined(value))
                {
                    return (T)Enum.ToObject(type, value);
                }

                throw new ArgumentException("Enum value is not defined");
            }

            throw new ArgumentException("Type is not an enum");
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the flags from the integer.
        /// </summary>
        ///--------------------------------------------------------------------
        public static T ToEnum<T>(
            this Int64 value)
        {
            Type type = typeof(T);

            if (type.IsEnum)
            {
                if (type.IsEnumDefined(value))
                {
                    return (T)Enum.ToObject(type, value);
                }

                throw new ArgumentException("Enum value is not defined");
            }

            throw new ArgumentException("Type is not an enum");
        }
        #endregion
    }
}
