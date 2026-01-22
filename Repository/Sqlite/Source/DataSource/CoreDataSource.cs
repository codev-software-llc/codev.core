//-----------------------------------------------------------------------------
// <copyright file="CoreDataSource.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Data;
    using System.Reflection;
    using Codev.Core.Interface;
    using Microsoft.Data.Sqlite;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements data source for a local SQLite core database.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CoreDataSource : BaseDataSourceSqlite, ICoreDataSource
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the data source.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreDataSource(
            String location,
            String catalog,
            String username,
            String password,
            String schemaName,
            Int32  timeOutConnection,
            Int32  timeOutCommand) : base(location, catalog, username, password, timeOutConnection, timeOutCommand)
        {
            this.SchemaName = this.SchemaName;

            IDatabase database = new Database(location, catalog);

            database.Initialize(Assembly.GetExecutingAssembly());
        }
        #endregion

        #region Properties
        ///------------------------------------------------------------------------
        /// <summary>
        /// Get or set the schema name.
        /// </summary>
        ///------------------------------------------------------------------------
        public String SchemaName { get; private set; }
        #endregion

        #region Methods
        ///------------------------------------------------------------------------
        /// <summary>
        /// This will open a connection to the data source.
        /// </summary>
        ///------------------------------------------------------------------------
        public IDbConnection OpenConnection()
        {
            return new SqliteConnection(this.ConnectionString);
        }
        #endregion
    }
}
