//-----------------------------------------------------------------------------
// <copyright file="BaseDataSourceAdo.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Data;
    using Microsoft.Data.SqlClient;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IDataSource interface which defines an ADO
    /// database connection.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BaseDataSourceAdo
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the data source.
        /// </summary>
        ///--------------------------------------------------------------------
        protected BaseDataSourceAdo(
            String  source,
            String  catalog,
            String  userName,
            String  password,
            String  schemaName,
            String  applicationName,
            Int32   poolSize,
            Int32   timeOutConnection,
            Int32   timeOutCommand,
            Boolean doEncrypt,
            Boolean trustCertificate)
        {
            this.TimeoutConnection = timeOutConnection;
            this.TimeoutCommand    = timeOutCommand;
            this.SchemaName        = schemaName;

            catalog  = catalog.Replace("$", "").Replace("(", "%").Replace(")", "%");
            source   = source.Replace("$", "").Replace("(", "%").Replace(")", "%");
            userName = userName.Replace("$", "").Replace("(", "%").Replace(")", "%");
            password = password.Replace("$", "").Replace("(", "%").Replace(")", "%");

            this.BuildConnectionString(
                Environment.ExpandEnvironmentVariables(source),
                Environment.ExpandEnvironmentVariables(catalog),
                Environment.ExpandEnvironmentVariables(userName), 
                Environment.ExpandEnvironmentVariables(password), 
                applicationName, 
                poolSize, 
                doEncrypt, 
                trustCertificate);
        }
        #endregion

        #region Properties
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

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the schema name.
        /// </summary>
        ///--------------------------------------------------------------------
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
            return new SqlConnection(this.ConnectionString);
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Build the connection string that corresponds to an ADO provider.
        /// </summary>
        ///--------------------------------------------------------------------
        private void BuildConnectionString(
            String  source,
            String  catalog,
            String  userName,
            String  password,
            String  applicationName,
            Int32   poolSize,
            Boolean doEncrypt,
            Boolean trustCertificate)
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder() 
                {
                    InitialCatalog         = catalog,
                    ConnectTimeout         = this.TimeoutConnection,
                    DataSource             = source,
                    UserID                 = userName,
                    Password               = password,
                    ApplicationName        = applicationName,
                    MaxPoolSize            = poolSize,
                    Encrypt                = doEncrypt,
                    TrustServerCertificate = trustCertificate,
                    IntegratedSecurity     = false
                };

            this.ConnectionString = scsb.ToString();
        }
        #endregion
    }
}
