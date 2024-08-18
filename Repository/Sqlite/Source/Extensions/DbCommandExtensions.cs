//-----------------------------------------------------------------------------
// <copyright file="DbCommandExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.Data.Sqlite;
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
            TypeMappings = new Dictionary<Type, SqliteType>();

            TypeMappings.Add(typeof(Boolean)                 , SqliteType.Integer);
            TypeMappings.Add(typeof(Decimal)                 , SqliteType.Real);
            TypeMappings.Add(typeof(Int16)                   , SqliteType.Integer);
            TypeMappings.Add(typeof(Int32)                   , SqliteType.Integer);
            TypeMappings.Add(typeof(Int64)                   , SqliteType.Integer);
            TypeMappings.Add(typeof(DateTime)                , SqliteType.Text);
            TypeMappings.Add(typeof(DateTimeOffset)          , SqliteType.Integer);
            TypeMappings.Add(typeof(Byte[])                  , SqliteType.Blob);

            TypeMappings.Add(typeof(Nullable<Boolean>)       , SqliteType.Integer);
            TypeMappings.Add(typeof(Nullable<Decimal>)       , SqliteType.Real);
            TypeMappings.Add(typeof(Nullable<Int16>)         , SqliteType.Integer);
            TypeMappings.Add(typeof(Nullable<Int32>)         , SqliteType.Integer);
            TypeMappings.Add(typeof(Nullable<Int64>)         , SqliteType.Integer);
            TypeMappings.Add(typeof(Nullable<DateTime>)      , SqliteType.Text);
            TypeMappings.Add(typeof(Nullable<DateTimeOffset>), SqliteType.Integer);
            TypeMappings.Add(typeof(Nullable<Byte>[])        , SqliteType.Blob);

            TypeMappings.Add(typeof(Guid)  , SqliteType.Blob);
            TypeMappings.Add(typeof(String), SqliteType.Text);            
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the type mappings from C-Types to SQL-Types.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Dictionary<Type, SqliteType> TypeMappings { get; set; }
        #endregion

        #region Public Methods (SQLITE)
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

            if (!typeof(T).IsValueType || (parameterValue == null))
            {
                isNullable = true;
            }

            if (typeof(T) == typeof(Instant))
            {
                Instant instantValue = (Instant)(Object)parameterValue;

                String value = instantValue.ToString();

                sqlCommand.Parameters.Add(
                    new SqliteParameter()
                        {
                            ParameterName = "@" + parameterName,
                            Value         = value,
                            SqliteType    = GetDbType<String>(),
                            IsNullable    = isNullable,
                            Direction     = ParameterDirection.Input
                        });
            }
            else
            {
                sqlCommand.Parameters.Add(
                    new SqliteParameter()
                        {
                            ParameterName = "@" + parameterName,
                            Value         = parameterValue,
                            SqliteType    = GetDbType<T>(),
                            IsNullable    = isNullable,
                            Direction     = ParameterDirection.Input
                        });
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Add an input parameter of a large blob type.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void AddInputParameterTextBlob<T>(
            this IDbCommand sqlCommand,
                 String     parameterName,
                 T          parameterValue)
        {
            // If the type is a nullable, then we will want to make sure
            // we specify this in the parameter build out.
            //
            Boolean isNullable = false;

            if (!typeof(T).IsValueType || (parameterValue == null))
            {
                isNullable = true;
            }

            sqlCommand.Parameters.Add(
                new SqliteParameter()
                    {
                        ParameterName = "@" + parameterName,
                        Value         = parameterValue,
                        DbType        = DbType.String,
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
                new SqliteParameter("@" + parameterName, GetDbType<T>(), 2048)
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
                new SqliteParameter("@" + parameterName, DbType.VarNumeric)
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
            if (((SqliteParameter)sqlCommand.Parameters["@" + parameterName]).Value.ToString().Length == 0)
            {
                return default(T);
            }

            return (T)((SqliteParameter)sqlCommand.Parameters["@" + parameterName]).Value;
        }
        #endregion

        #region Private Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// This will return the SQL type from the C-Type.
        /// </summary>
        ///--------------------------------------------------------------------
        private static SqliteType GetDbType<T>()
        {
            SqliteType sqlType;

            if (TypeMappings.TryGetValue(typeof(T), out sqlType))
            {
                return sqlType;
            }

            // Check for an enum, which well treat as an integer.
            //
            if (IsTypeEnum<T>())
            {
                return SqliteType.Integer;
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
