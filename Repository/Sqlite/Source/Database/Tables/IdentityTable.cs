//-----------------------------------------------------------------------------
// <copyright file="IdentityTable.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Data;
    using System.Data.Common;
    using Codev.Core.Interface;
    using Microsoft.Data.Sqlite;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the table generation support for the identity table.
    /// </summary>
    ///------------------------------------------------------------------------
    public class IdentityTable : IDatabaseTable
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// This defines the identity table.
        /// </summary>
        ///--------------------------------------------------------------------
        private static String tableDefinition =
            @"CREATE TABLE [Identities] 
              (
                  [RowId]                INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
                  [RowVersion]           TEXT                              NOT NULL, 
                  [IsActive]             INTEGER                           NOT NULL, 
                  [Flags]                INTEGER                           NOT NULL, 
                  [DateCreated]          TEXT                              NOT NULL, 
                  [DateModified]         TEXT                              NOT NULL, 
                  [ConfirmationAttempts] INTERGER                          NOT NULL, 
                  [DateTimeZoneId]       TEXT                              NOT NULL
              )";
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the table object.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityTable(
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
            if (this.Database.DoesTableExist("Identities") == false)
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
            if (this.Database.DoesTableExist("Identities"))
            {
                using (DbConnection connection = new SqliteConnection(this.Database.ConnectionString))
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = "DELETE FROM [Identities]";

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
