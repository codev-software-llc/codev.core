//-----------------------------------------------------------------------------
// <copyright file="CoreDataSource.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ICoreDataSource interface which defines an ADO
    /// database connection.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CoreDataSource : BaseDataSourceAdo, ICoreDataSource
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the data source.
        /// </summary>
        ///--------------------------------------------------------------------
        public CoreDataSource(
            String  source,
            String  catalog,
            String  username,
            String  password,
            String  schemaName,
            String  applicationName,
            Int32   poolSize,
            Int32   timeOutConnection,
            Int32   timeOutCall,
            Boolean doEncrypt,
            Boolean trustCertificate) : base(source, catalog, username, password, schemaName, applicationName, poolSize, timeOutConnection, timeOutCall, doEncrypt, trustCertificate)
        {
        }
        #endregion
    }
}
