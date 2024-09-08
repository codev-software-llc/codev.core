//-----------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Transactions;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements a unit of work on a repository.
    /// </summary>
    ///------------------------------------------------------------------------
    public class UnitOfWork : IUnitOfWork
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Name of the thread local storage slot.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String UnitOfWorkName = "UnitOfWork";
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the data source.
        /// </summary>
        ///--------------------------------------------------------------------
        public UnitOfWork(
            IDataSource dataSource)
        {
            this.DataSource = dataSource;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Called when the object is disposed.
        /// </summary>
        ///--------------------------------------------------------------------
        ~UnitOfWork()
        {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the data source for the connection.
        /// </summary>
        ///--------------------------------------------------------------------
        public IDataSource DataSource { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the SQL connection.
        /// </summary>
        ///--------------------------------------------------------------------
        public IDbConnection Connection { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether we have already disposed of the object.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean IsDisposed { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether we're a nested unit of work.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean IsNested { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the transaction scope to use.
        /// </summary>
        ///--------------------------------------------------------------------
        private TransactionScope TransactionScope { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Begin the unit of work.
        /// </summary>
        ///--------------------------------------------------------------------
        public IUnitOfWork Begin()
        {
            // Put the unit of work into the TLS if it is not already
            // there.  If there is a global Unit-Of-Work, then we will
            // add (accumulate) this unit of wor to that.
            //
            Stack<IUnitOfWork> unitOfWork = this.GetUnitOfWorkFromThread();

            if (unitOfWork.Count == 0)
            {
                // Create a transaction scope on this base work item.
                //
                this.TransactionScope = new TransactionScope(TransactionScopeOption.Required);
                this.Connection       = this.OpenConnection(this.DataSource);
                this.IsNested         = false;
            }
            else
            {
                IUnitOfWork data = unitOfWork.Peek();

                this.Connection = data.Connection;
                this.IsNested   = true;
            }

            // Push this unit of work onto the thread stack object.
            //
            unitOfWork.Push(this);

            return this;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a command that can be used
        /// </summary>
        ///--------------------------------------------------------------------
        public IDbCommand CreateCommand()
        {
            if (this.Connection == null)
            {
                this.Connection = this.OpenConnection();
            }

            if (this.Connection.State == ConnectionState.Closed)
            {
                this.Connection.Open();
            }

            // Create the command.  This can only be done if a connection
            // is established.
            //
            IDbCommand command = this.Connection.CreateCommand();

            command.CommandTimeout = this.DataSource.TimeoutCommand;

            return command;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Commit the transaction.  This is only performed on the top level
        /// unit of work.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Commit()
        {
            if (this.IsNested == false)
            {
                this.CloseConnection(true);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Rollback the transaction.  This is only performed on the top level
        /// unit of work.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Rollback()
        {
            if (this.IsNested == false)
            {
                this.CloseConnection(false);
            }
        }
        #endregion

        #region IDisposable
        ///--------------------------------------------------------------------
        /// <summary>
        /// Dispose of the resources allocated on behalf of this class.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Dispose()
        {
            this.Dispose(true);

            // Suppressing the finalize will prevent our Dispose from
            // potentially being called twice.
            //
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new connection through the data source.
        /// </summary>
        ///--------------------------------------------------------------------
        private IDbConnection OpenConnection(
            IDataSource dataSource)
        {
            IDbConnection connection = dataSource.OpenConnection();

            return connection;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new connection through the data source.
        /// </summary>
        ///--------------------------------------------------------------------
        private IDbConnection OpenConnection()
        {
            IDbConnection connection = this.DataSource.OpenConnection();

            return connection;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new connection through the data source.
        /// </summary>
        ///--------------------------------------------------------------------
        private void CloseConnection(
            Boolean completeTransaction)
        {
            if (this.Connection != null)
            {
                this.Connection.Close();

                this.Connection = null;
            }

            if (this.TransactionScope != null)
            {
                if (completeTransaction)
                {
                    this.TransactionScope.Complete();
                }

                this.TransactionScope.Dispose();

                this.TransactionScope = null;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the stack of units of work.
        /// </summary>
        ///--------------------------------------------------------------------
        private Stack<IUnitOfWork> GetUnitOfWorkFromThread()
        {
            LocalDataStoreSlot slot = Thread.GetNamedDataSlot(UnitOfWorkName);

            if (slot == null)
            {
                slot = Thread.AllocateNamedDataSlot(UnitOfWorkName);
            }

            Stack<IUnitOfWork> stack = (Stack<IUnitOfWork>)Thread.GetData(slot);

            if (stack == null)
            {
                stack = new Stack<IUnitOfWork>();

                Thread.SetData(slot, stack);
            }

            return stack;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will handle the disposal of our work.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Dispose(
            Boolean isDisposing)
        {
            if (isDisposing && (this.IsDisposed == false))
            {
                this.Rollback();

                Stack<IUnitOfWork> unitOfWork = this.GetUnitOfWorkFromThread();

                // If we have reached the end of the work, then we will want
                // to free the thread-local storage.
                //
                IUnitOfWork work = unitOfWork.Pop();

                if (work == null)
                {
                    Thread.FreeNamedDataSlot(UnitOfWorkName);
                }

                this.IsDisposed = true;
            }
        }
        #endregion
    }
}
