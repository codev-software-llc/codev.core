//-----------------------------------------------------------------------------
// <copyright file="DataReaderExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Base
{
    using System;
    using System.Data;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Extends the IDataReader to provide data access.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class DataReaderExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the value from the reader in the type specified.
        /// </summary>
        ///--------------------------------------------------------------------
        public static T GetValue<T>(
            this IDataReader reader,
                 String      columnName)
        {
            Int32 index = reader.GetOrdinal(columnName);

            if (reader.IsDBNull(index))
            {
                if (typeof(T) == typeof(String))
                {
                    Object obj;

                    obj = String.Empty;

                    return (T)obj;
                }
                else
                {
                    return default(T);
                }
            }

            if (typeof(T) == typeof(Instant))
            {
                DateTime dateValue = (DateTime)((Object)DateTime.SpecifyKind((DateTime)reader[index], DateTimeKind.Utc));

                return (T)((Object)Instant.FromDateTimeUtc(dateValue));
            }
            else if (typeof(T) == typeof(LocalDate))
            {
                DateTime dateValue = (DateTime)((Object)DateTime.SpecifyKind((DateTime)reader[index], DateTimeKind.Local));

                LocalDate dateConvert = new LocalDate(dateValue.Year, dateValue.Month, dateValue.Day);

                return (T)(Object)dateConvert;
            }
            else if (typeof(T) == typeof(Duration))
            {
                Int64 durationValue = (Int64)reader[index];

                return (T)(Object)Duration.FromSeconds(durationValue);
            }
            else if (typeof(T) == typeof(DateTime))
            {
                return (T)((Object)DateTime.SpecifyKind((DateTime)reader[index], DateTimeKind.Utc));
            }
            else
            {
                return (T)reader[index];
            }
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Load up the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public static BaseEntity GetBaseEntity(
            this IDataReader reader)
        {
            return new BaseEntity(
                reader.GetIdentifier<Int32>("RowId"),
                reader.GetValue<Byte[]>    ("RowVersion"),
                reader.GetValue<Boolean>   ("IsActive"),
                reader.GetValue<Instant>   ("DateCreated"),
                reader.GetValue<Instant>   ("DateModified"));
        }

        ///-------------------------------------------------------------------
        /// <summary>
        /// Return the identifier.  This retrieves the identifier as the type.
        /// </summary>
        ///--------------------------------------------------------------------
        public static T GetIdentifier<T>(
            this IDataReader reader,
                 String      columnName)
        {
            Int32 index = reader.GetOrdinal(columnName);

            if (reader.IsDBNull(index))
            {
                return default(T);
            }
            else
            {
                Int32 value = Convert.ToInt32(reader[index]);

                return (T)(Object)value;
            }
        }
        #endregion
    }
}
