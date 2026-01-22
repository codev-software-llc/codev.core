//-----------------------------------------------------------------------------
// <copyright file="AdoAccess.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Threading;
    using System.Transactions;
    using Codev.Core.Base;
    using Microsoft.Data.SqlClient;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides a common data access utility for making calls to the
    /// repository store.  This utility should be able to interface with both
    /// ADO and CE related stacks.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class AdoAccess
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Name of the thread local storage slot.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String UnitOfWorkFormat = "UnitOfWork_{0}";
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// This will load up the list of detailed errors we can use for our
        /// exception handling.
        /// </summary>
        ///--------------------------------------------------------------------
        static AdoAccess()
        {
            Errors = new()
                {
                    { 2601, CoreErrorCode.Duplicate},
                    { 2627, CoreErrorCode.Duplicate}
                };
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the list of errors.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Dictionary<Int32, CoreErrorCode> Errors { get; set; }
        #endregion

        #region Methods
        public static void PopUnitOfWork()
        {
            String slotName = GetSlotName();

            LocalDataStoreSlot slot = Thread.GetNamedDataSlot(slotName);

            if (slot != null)
            {
                Stack<IUnitOfWork> stack = (Stack<IUnitOfWork>)Thread.GetData(slot);

                if ((stack != null) && (stack.Count > 0))
                {
                    stack.Pop();

                    if (stack.Count == 0)
                    {
                        Thread.FreeNamedDataSlot(slotName);
                    }
                }
                else
                {
                    Thread.FreeNamedDataSlot(slotName);
                }
            }
        }

        private static String GetSlotName()
        {
            return String.Format(UnitOfWorkFormat, Environment.CurrentManagedThreadId);
        }

        public static void AddUnitOfWorkToThread(
            IUnitOfWork unitOfWork)
        {
            String slotName = GetSlotName();

            LocalDataStoreSlot slot = Thread.GetNamedDataSlot(slotName);

            if (slot == null)
            {
                slot = Thread.AllocateNamedDataSlot(slotName);
            }

            Stack<IUnitOfWork> stack = (Stack<IUnitOfWork>)Thread.GetData(slot);

            if (stack == null)
            {
                stack = new Stack<IUnitOfWork>();

                Thread.SetData(slot, stack);
            }

            stack.Push(unitOfWork);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the stack of units of work.
        /// </summary>
        ///--------------------------------------------------------------------
        public static IUnitOfWork GetUnitOfWorkFromThread()
        {
            String slotName = GetSlotName();

            LocalDataStoreSlot slot = Thread.GetNamedDataSlot(slotName);

            if (slot == null)
            {
                slot = Thread.AllocateNamedDataSlot(slotName);
            }

            Stack<IUnitOfWork> stack = (Stack<IUnitOfWork>)Thread.GetData(slot);

            if (stack == null)
            {
                stack = new Stack<IUnitOfWork>();

                Thread.SetData(slot, stack);
            }

            return stack.Count > 0 ? stack.Peek() : null;
        }
        #endregion

        #region Methods (Procedures)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke a call to the ADO store with a setup and worker callback.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void CallProcedure(
            String             procedureName,
            IDataSource        dataSource,
            Action<IDbCommand> setupCallback,
            Action<IDbCommand> workerCallback)
        {
            AdoAccess.CallProcedureWorker(
                procedureName,
                dataSource,
                setupCallback,
                (command) =>
                    {
                        // Make the procedure call.
                        // 
                        command.ExecuteNonQuery();

                        // Notify the caller after the work has been 
                        // completed.
                        //
                        if (workerCallback != null)
                        {
                            workerCallback(command);
                        }
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke a call to the ADO store with parameters, but also returns
        /// the rows affected.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void CallProcedure(
            String                    procedureName,
            IDataSource               dataSource,
            Action<IDbCommand>        setupCallback,
            Action<IDbCommand, Int32> workerCallback)
        {            
            AdoAccess.CallProcedureWorker(
                procedureName,
                dataSource,
                setupCallback,
                (command) =>
                    {
                        // Make the procedure call.
                        // 
                        Int32 rowsAffected = command.ExecuteNonQuery();

                        // Notify the caller after the work has been 
                        // completed.
                        //
                        if (workerCallback != null)
                        {
                            workerCallback(command, rowsAffected);
                        }
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the call to the ADO store where the worker can pull the
        /// reader results.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void CallProcedure(
            String                          procedureName,
            IDataSource                     dataSource,
            Action<IDbCommand>              setupCallback,
            Action<IDbCommand, IDataReader> workerCallback)
        {
            AdoAccess.CallProcedureWorker(
                procedureName,
                dataSource,
                setupCallback,
                (command) =>
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            // Notify the caller so that they may process the
                            // reader and command response.
                            //
                            if (workerCallback != null)
                            {
                                workerCallback(command, reader);
                            }
                        }
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke a call to the DB store to retrieve a table result.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void GetTable(
            String             procedureName,
            IDataSource        dataSource,
            Action<IDbCommand> setupCallback,
            Action<DataTable>  workerCallback)
        {
            AdoAccess.CallTableWorker(
                procedureName,
                dataSource,
                setupCallback,
                (command) =>
                    {
                        using (DataSet dataSet = new())
                        {          
                            using (DbDataAdapter dataAdapter = new SqlDataAdapter(command as SqlCommand))
                            {
                                dataAdapter.SelectCommand = (SqlCommand)command;
                    
                                dataAdapter.Fill(dataSet);
                            }
                    
                            // Notify the caller so that they may process the
                            // response of the call.
                            //
                            if (workerCallback != null)
                            {
                                DataTable table = dataSet.Tables.Count > 0 ? dataSet.Tables[0] : null;
                    
                                workerCallback(table);
                            }
                        }
                    });
        }           
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// This provides the worker code that wraps the custom Ado client
        /// implementations.  Its goal is to provide the common aspects of
        /// open and closing of connections as well as doing the common
        /// exception handling.
        /// </summary>
        ///--------------------------------------------------------------------
        private static void CallProcedureWorker(
            String             procedureName,
            IDataSource        dataSource,
            Action<IDbCommand> setupCallback,
            Action<IDbCommand> workerCallback)
        {
            procedureName = String.Format("[{0}].[{1}]", dataSource.SchemaName, procedureName);

            // Pull the unit of work collection from the thread local
            // storage.  We will work of the current unit-of-work from
            // this stack.
            //
            IUnitOfWork unitOfWork = GetUnitOfWorkFromThread();

            if (unitOfWork != null)
            {
                using (IDbCommand command = unitOfWork.CreateCommand())
                {
                    AdoAccess.CallCommand(command, procedureName, setupCallback, workerCallback);
                }
            }
            else
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    using (IDbConnection connection = dataSource.OpenConnection())
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        using (IDbCommand command = connection.CreateCommand())
                        {
                            AdoAccess.CallCommand(command, procedureName, setupCallback, workerCallback);
                        }
                    }

                    scope.Complete();
                }
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Make the call using the unit of work.
        /// </summary>
        ///--------------------------------------------------------------------
        private static void CallCommand(
            IDbCommand         command,
            String             procedureName,
            Action<IDbCommand> setupCallback,
            Action<IDbCommand> workerCallback)
        {
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;

                // This will make a callback to the caller so that they
                // may initialize the command with parameters.
                //
                if (setupCallback != null)
                {
                    setupCallback(command);
                }

                // Open the connection and perform the callback to do
                // the actual ADO call.
                //
                workerCallback(command);
            }
            catch (SqlException se)
            {
                CoreErrorCode errorCode = MapErrorCode(se);

                throw new CoreDataException(errorCode, se.InnerException != null ? se.InnerException : se);
            }
            catch (Exception e)
            {
                throw new CoreDataException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This provides the worker code that wraps the custom Ado client
        /// implementations.  Its goal is to provide the common aspects of
        /// open and closing of connections as well as doing the common
        /// exception handling.
        /// </summary>
        ///--------------------------------------------------------------------
        private static void CallTableWorker(
            String             procedureName,
            IDataSource        dataSource,
            Action<IDbCommand> setupCallback,
            Action<IDbCommand> workerCallback)
        {
            // Pull the unit of work collection from the thread local
            // storage.  We will work of the current unit-of-work from
            // this stack.
            //
            IUnitOfWork unitOfWork = GetUnitOfWorkFromThread();

            if (unitOfWork != null)
            {
                using (IDbCommand command = unitOfWork.CreateCommand())
                {
                    command.CommandType    = CommandType.StoredProcedure;
                    command.CommandText    = String.Format("[{0}].[{1}]", dataSource.SchemaName, procedureName);
                    command.CommandTimeout = dataSource.TimeoutCommand;

                    // This will make a callback to the caller so that they
                    // may initialize the command with parameters.
                    //
                    if (setupCallback != null)
                    {
                        setupCallback(command);
                    }

                    workerCallback(command);
                }
            }
            else
            {
                throw new NotSupportedException("Command is not in a transaction scope");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Map the SqlError code to that of our known Core error codes.
        /// </summary>
        ///--------------------------------------------------------------------
        private static CoreErrorCode MapErrorCode(
            SqlException ex)
        {
            foreach (SqlError code in ex.Errors)
            {
                if (Errors.TryGetValue(code.Number, out CoreErrorCode errorCode))
                {
                    return errorCode;
                }
            }

            return CoreErrorCode.InternalFailure;
        }
        #endregion
    }
}
