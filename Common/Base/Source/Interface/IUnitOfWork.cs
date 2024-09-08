//-----------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Data;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for doing unit-of-work repository
    /// persistence.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IUnitOfWork : IDisposable
    {
        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the data connection.
        /// </summary>
        ///--------------------------------------------------------------------
        IDbConnection Connection { get; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Commit the unit of work.
        /// </summary>
        ///--------------------------------------------------------------------
        IUnitOfWork Begin();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Commit the unit of work.
        /// </summary>
        ///--------------------------------------------------------------------
        void Commit();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        void Rollback();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a command object to use.
        /// </summary>
        ///--------------------------------------------------------------------
        IDbCommand CreateCommand();
        #endregion
    }
}
