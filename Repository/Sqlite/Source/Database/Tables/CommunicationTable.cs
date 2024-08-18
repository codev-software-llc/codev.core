//-----------------------------------------------------------------------------
// <copyright file="CommunicationTable.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Data;
    using System.Data.Common;
    using Codev.Core.Common.Interface;
    using Microsoft.Data.Sqlite;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the table generation support for the Communication table.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CommunicationTable : IDatabaseTable
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// This defines the destination table.
        /// </summary>
        ///--------------------------------------------------------------------
        private static String tableDefinition =
            @"CREATE TABLE [Communicationss] 
              (
                  [RowId]        INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
                  [RowVersion]   TEXT                              NOT NULL, 
                  [IsActive]     INTEGER                           NOT NULL, 
                  [Flags]        INTETER                           NOT NULL, 
                  [DateCreated]  TEXT                              NOT NULL, 
                  [DateModified] TEXT                              NOT NULL, 
                  [Application]  TEXT                              NOT NULL, 
                  [Category]     TEXT                              NOT NULL,
                  [Subcategory]  TEXT                              NOT NULL, 
                  [Name]         TEXT                              NOT NULL, 
                  [EmailAddress] TEXT                              NOT NULL, 
                  [Comments]     TEXT                              NOT NULL
              )";
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the table object.
        /// </summary>
        ///--------------------------------------------------------------------
        public CommunicationTable(
            Database database)
        {
            this.Database = database;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the database.
        /// </summary>
        ///--------------------------------------------------------------------
        private Database Database { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the component has been initialized.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean IsNew { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create the table for the project.
        /// </summary>
        ///--------------------------------------------------------------------
        public void CreateTable()
        {
            if (this.Database.DoesTableExist("Destinations") == false)
            {
                this.IsNew = true;

                using (DbConnection connection = new SqliteConnection(this.Database.ConnectionString))
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = tableDefinition;

                        connection.Open();

                        Int32 rowsEffected = command.ExecuteNonQuery();

                        command.Connection.Close();
                    }

                    connection.Close();
                }
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Clear the table.
        /// </summary>
        ///--------------------------------------------------------------------
        public void ClearTable()
        {
            if (this.Database.DoesTableExist("Destinations"))
            {
                using (DbConnection connection = new SqliteConnection(this.Database.ConnectionString))
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "DELETE FROM [Communications]";

                        connection.Open();

                        Int32 rowsEffected = command.ExecuteNonQuery();

                        command.Connection.Close();
                    }

                    connection.Close();
                }
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create the keys for the table.
        /// </summary>
        ///--------------------------------------------------------------------
        public void CreatePrimaryKey()
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create any foreign keys for the table.
        /// </summary>
        ///--------------------------------------------------------------------
        public void CreateForeignKeys()
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create indexes for the table.
        /// </summary>
        ///--------------------------------------------------------------------
        public void CreateIndexes()
        {
        }
        #endregion
    }
}
