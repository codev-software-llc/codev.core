//-----------------------------------------------------------------------------
// <copyright file="DbCommandExtensions.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Extends the command class.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class DbCommandExtensions
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the ADO dictionaries and resources.
        /// </summary>
        ///--------------------------------------------------------------------
        static DbCommandExtensions()
        {
            TypeMappings = new Dictionary<Type, SqlDbType>();

            TypeMappings.Add(typeof(Boolean)                 , SqlDbType.Bit);
            TypeMappings.Add(typeof(Decimal)                 , SqlDbType.Decimal);
            TypeMappings.Add(typeof(Int16)                   , SqlDbType.SmallInt);
            TypeMappings.Add(typeof(Int32)                   , SqlDbType.Int);
            TypeMappings.Add(typeof(Int64)                   , SqlDbType.BigInt);
            TypeMappings.Add(typeof(DateTime)                , SqlDbType.DateTime);
            TypeMappings.Add(typeof(Instant)                 , SqlDbType.DateTime);
            TypeMappings.Add(typeof(LocalDate)               , SqlDbType.Date);
            TypeMappings.Add(typeof(Duration)                , SqlDbType.BigInt);
            TypeMappings.Add(typeof(DateTimeOffset)          , SqlDbType.DateTimeOffset);
            TypeMappings.Add(typeof(Byte[])                  , SqlDbType.VarBinary);

            TypeMappings.Add(typeof(Nullable<Boolean>)       , SqlDbType.Bit);
            TypeMappings.Add(typeof(Nullable<Decimal>)       , SqlDbType.Decimal);
            TypeMappings.Add(typeof(Nullable<Int16>)         , SqlDbType.SmallInt);
            TypeMappings.Add(typeof(Nullable<Int32>)         , SqlDbType.Int);
            TypeMappings.Add(typeof(Nullable<Int64>)         , SqlDbType.BigInt);
            TypeMappings.Add(typeof(Nullable<DateTime>)      , SqlDbType.DateTime);
            TypeMappings.Add(typeof(Nullable<Instant>)       , SqlDbType.DateTime);
            TypeMappings.Add(typeof(Nullable<LocalDate>)     , SqlDbType.Date);
            TypeMappings.Add(typeof(Nullable<Duration>)      , SqlDbType.BigInt);
            TypeMappings.Add(typeof(Nullable<DateTimeOffset>), SqlDbType.DateTimeOffset);
            TypeMappings.Add(typeof(Nullable<Byte>[])        , SqlDbType.VarBinary);

            TypeMappings.Add(typeof(Guid)  , SqlDbType.UniqueIdentifier);
            TypeMappings.Add(typeof(String), SqlDbType.NVarChar);            
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the type mappings from C-Types to SQL-Types.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Dictionary<Type, SqlDbType> TypeMappings { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add an input parameter of a generic type.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void AddInputParameter<T>(
            this IDbCommand sqlCommand,
                 String     parameterName,
                 T          parameterValue)
        {
            // If the type is a nullable, then we will want to make sure
            // we specify this in the parameter build out.
            //
            Boolean isNullable = false;
            Object  sqlValue   = null;

            if (!typeof(T).IsValueType || (parameterValue == null))
            {
                isNullable = true;
            }

            if ((typeof(T) == typeof(Instant)) || (typeof(T) == typeof(Nullable<Instant>)))
            {
                Instant? instantValue = (Instant)(Object)parameterValue;

                if (instantValue != null)
                {
                    sqlValue = ((Instant)instantValue).ToDateTimeUtc();
                }
            }
            else if ((typeof(T) == typeof(LocalDate)) || (typeof(T) == typeof(Nullable<LocalDate>)))
            {
                LocalDate? localDateValue = (LocalDate)(Object)parameterValue;

                if (localDateValue != null)
                {
                    sqlValue = String.Format("{0}-{1:00}-{2:00}", ((LocalDate)localDateValue).Year, ((LocalDate)localDateValue).Month, ((LocalDate)localDateValue).Day);
                }
            }
            else if ((typeof(T) == typeof(Duration)) || (typeof(T) == typeof(Nullable<Duration>)))
            {
                Duration? durationValue = (Duration)(Object)parameterValue;

                if (durationValue != null)
                {
                    sqlValue = Convert.ToInt64(((Duration)durationValue).TotalSeconds);
                }
            }
            else
            {
                sqlValue = parameterValue;
            }

            sqlCommand.Parameters.Add(
                new SqlParameter()
                    {
                        ParameterName = "@" + parameterName,
                        Value         = sqlValue,
                        SqlDbType     = GetSqlType<T>(),
                        IsNullable    = isNullable,
                        Direction     = ParameterDirection.Input
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Add output parameter for a generic type.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void AddOutputParameter<T>(
            this IDbCommand sqlCommand,
                 String     parameterName)
        {
            sqlCommand.Parameters.Add(
                new SqlParameter("@" + parameterName, GetSqlType<T>(), 2048) 
                    {                
                        Direction = ParameterDirection.Output
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Add an output parameter for a variant type.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void AddOutputParameterVariant(
            this IDbCommand sqlCommand,
                 String     parameterName)
        {
            sqlCommand.Parameters.Add(
                new SqlParameter("@" + parameterName, SqlDbType.Variant) 
                    {
                        Direction = ParameterDirection.Output
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve a generic type return value from a command.
        /// </summary>
        ///--------------------------------------------------------------------
        public static T GetReturnValue<T>(
            this IDbCommand sqlCommand,
                 String     parameterName)
        {          
            if (((SqlParameter)sqlCommand.Parameters["@" + parameterName]).Value.ToString().Length == 0) 
            {
                return default;
            }

            return (T)((SqlParameter)sqlCommand.Parameters["@" + parameterName]).Value;
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// This will return the SQL type from the C-Type.
        /// </summary>
        ///--------------------------------------------------------------------
        private static SqlDbType GetSqlType<T>()
        {
            if (TypeMappings.TryGetValue(typeof(T), out SqlDbType sqlType))
            {
                return sqlType;
            }

            // Check for an enum, which well treat as an integer.
            //
            if (IsTypeEnum<T>())
            {
                return SqlDbType.Int;
            }

            throw new NotSupportedException();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Returns whether the type is an enumeration.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Boolean IsTypeEnum<T>()
        {
            if (typeof(T).IsEnum)
            {
                return true;
            }

            Type underlyingType = Nullable.GetUnderlyingType(typeof(T));

            if (underlyingType.IsEnum)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
