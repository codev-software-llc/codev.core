//-----------------------------------------------------------------------------
// <copyright file="Database.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Codev.Core.Common.Interface;
    using Microsoft.Data.Sqlite;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the database object for interfacing with the CE tables.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Database : IDatabase
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Name of the file for the local database.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String DatabaseFile = @"{0}\{1}.db";

        ///--------------------------------------------------------------------
        /// <summary>
        /// This defines the data source connection string.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String ConnectionStringFormat = @"Data Source={0}";
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the table object.
        /// </summary>
        ///--------------------------------------------------------------------
        public Database(
            String locationPath,
            String catalogName)
        {
            SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();

            connectionStringBuilder.DataSource            = String.Format(DatabaseFile, locationPath, catalogName);
         //   connectionStringBuilder.JournalMode           = SQLiteJournalModeEnum.Wal;

            this.ConnectionString = connectionStringBuilder.ToString();

            //this.ConnectionString = this.BuildConnectionString(locationPath, catalogName);

            this.Initialize(locationPath, catalogName);
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the connection string for the database.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ConnectionString { get; private set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the database.  Make sure the tables exists.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Initialize(
            Assembly assembly)
        {
            // From the core standpoint, create the tables from the current
            // executing assembly.
            //
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            this.CreateTables(currentAssembly);

            // Create the tables from the specified assembly instance.
            //
            this.CreateTables(assembly);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the database.  Make sure the tables exists.
        /// </summary>
        ///--------------------------------------------------------------------
        public IDbConnection OpenConnection()
        {
            return new SqliteConnection(this.ConnectionString);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Check whether a table exists.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean DoesTableExist(
            String tableName)
        {
            Boolean doesExist = false;

            using (DbConnection connection = new SqliteConnection(this.ConnectionString))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    String query = String.Format("SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '{0}'", tableName);

                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    connection.Open();

                    Object result = command.ExecuteScalar();

                    doesExist = (Convert.ToInt32(result) > 0);

                    command.Connection.Close();
                }

                connection.Close();
            }

            return doesExist;
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// This will initialize the database (source) to make sure that the
        /// SDF file is fully built and qualified.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Initialize(
            String locationPath,
            String catalogName)
        {
            String fileName = String.Format(DatabaseFile, locationPath, catalogName);

            if (Directory.Exists(locationPath) == false)
            {
                Directory.CreateDirectory(locationPath);
            }

            if (this.DoesDatabaseExist(fileName) == false)
            {
                // Create the database.
                //
                this.CreateDatabase(fileName);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Build the connection string for the CE file.
        /// </summary>
        ///--------------------------------------------------------------------
        private String BuildConnectionString(
            String locationPath,
            String catalogName)
        {
            String databaseFile = String.Format(Database.DatabaseFile, locationPath, catalogName);

            return String.Format(Database.ConnectionStringFormat, databaseFile);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Check for the existence of the database.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean DoesDatabaseExist(
            String relativePath)
        {
            String directory = Assembly.GetExecutingAssembly().Location;

            String path = Path.Combine(Path.GetDirectoryName(directory), relativePath);

            return File.Exists(path);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will establish a new database file.
        /// </summary>
        ///--------------------------------------------------------------------
        private void CreateDatabase(
            String fileName)
        {
            throw new NotImplementedException();
           // SqliteConnection //CreateFile(fileName);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will create the tables.
        /// </summary>
        ///--------------------------------------------------------------------
        private void CreateTables(
            Assembly assembly)
        {
            var instances = from t in assembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(IDatabaseTable))
                            select Activator.CreateInstance(t, this) as IDatabaseTable;

            List<IDatabaseTable> tables = instances.ToList();

            // Create the tables.
            //
            foreach (var instance in tables)
            {
                instance.CreateTable();
            }

            // Create the keys.
            //
            foreach (var instance in tables)
            {
                instance.CreatePrimaryKey();
            }

            // Create the foreign keys.
            //
            foreach (var instance in tables)
            {
                instance.CreateForeignKeys();
            }

            // Create the indexes.
            //
            foreach (var instance in tables)
            {
                instance.CreateIndexes();
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will clear all the tables.
        /// </summary>
        ///--------------------------------------------------------------------
        private void ClearTables(
            Assembly assembly)
        {
            var instances = from t in assembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(IDatabaseTable))
                            select Activator.CreateInstance(t, this) as IDatabaseTable;

            List<IDatabaseTable> tables = instances.ToList();

            // Create the tables.
            //
            foreach (var instance in tables)
            {
                instance.ClearTable();
            }
        }
        #endregion
    }
}
