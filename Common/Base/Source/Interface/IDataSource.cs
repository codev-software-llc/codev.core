//-----------------------------------------------------------------------------
// <copyright file="IDataSource.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Data;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the data source to a store repository.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IDataSource
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the timeout for a connection.
        /// </summary>
        ///--------------------------------------------------------------------
        Int32 TimeoutConnection { get; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the timeout for a command.
        /// </summary>
        ///--------------------------------------------------------------------
        Int32 TimeoutCommand { get; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the connection string.
        /// </summary>
        ///--------------------------------------------------------------------
        String ConnectionString { get; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the schema name.
        /// </summary>
        ///--------------------------------------------------------------------
        String SchemaName { get; }
        #endregion

        #region Methods
        ///------------------------------------------------------------------------
        /// <summary>
        /// This will open a connection to the data source.
        /// </summary>
        ///------------------------------------------------------------------------
        IDbConnection OpenConnection();
        #endregion
    }
}