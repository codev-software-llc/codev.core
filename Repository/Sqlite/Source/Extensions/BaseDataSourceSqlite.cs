//-----------------------------------------------------------------------------
// <copyright file="BaseDataSourceSqlite.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IDataSource interface which defines a SQLite
    /// database connection.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BaseDataSourceSqlite
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Name of the file for the local database.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String DatabaseFileFormat = @"{0}.db";
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the data source.
        /// </summary>
        ///--------------------------------------------------------------------
        protected BaseDataSourceSqlite(
            String location,
            String catalog,
            String username,
            String password,
            Int32  timeOutConnection,
            Int32  timeOutCommand)
        {
            this.Location          = location.TrimEnd(new Char[] { '\\' });
            this.Catalog           = String.Format(DatabaseFileFormat, catalog);
            this.TimeoutConnection = timeOutConnection;
            this.TimeoutCommand    = 0;

            this.ConnectionString = this.BuildConnectionString(username, password);
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the database location (directory).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Location { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the database name (file).
        /// </summary>
        ///--------------------------------------------------------------------
        public String Catalog { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the timeout for a connection.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 TimeoutConnection { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the timeout for a command.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 TimeoutCommand { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the connection string.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ConnectionString { get; private set; }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Build the connection string for the CE file.
        /// </summary>
        ///--------------------------------------------------------------------
        private String BuildConnectionString(
            String username,
            String password)
        {
            return String.Format("Data Source={0}\\{1}", this.Location, this.Catalog);
        }
        #endregion
    }
}