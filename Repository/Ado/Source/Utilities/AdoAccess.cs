//-----------------------------------------------------------------------------
// <copyright file="AdoAccess.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Threading;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides a common data access utility for making calls to the
    /// repository store.  This utility should be able to interface with both
    /// ADO and CE related stacks.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class AdoAccess
    {
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
                    { 2601, CoreErrorCode.Duplicate}
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
            // Pull the unit of work collection from the thread local
            // storage.  We will work of the current unit-of-work from
            // this stack.
            //
            Stack<IUnitOfWork> stack = GetUnitOfWorkFromThread();

            // If we don't have a unit of work in the stack, then 
            // create a local scope unit of work that we will use for
            // the call.
            //
            IUnitOfWork unitOfWork = (stack.Count > 0 ? stack.Peek() : null);

            procedureName = String.Format("[{0}].[{1}]", dataSource.SchemaName, procedureName);

            if (unitOfWork != null)
            {
                AdoAccess.DoProcedureCall(unitOfWork, procedureName, setupCallback, workerCallback);
            }
            else
            {
                using (IUnitOfWork localWork = new UnitOfWork(dataSource))
                {
                    using (IUnitOfWork work = localWork.Begin())
                    {
                        AdoAccess.DoProcedureCall(work, procedureName, setupCallback, workerCallback);

                        work.Commit();
                    }
                }
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Make the call using the unit of work.
        /// </summary>
        ///--------------------------------------------------------------------
        private static void DoProcedureCall(
            IUnitOfWork        work,
            String             procedureName,
            Action<IDbCommand> setupCallback,
            Action<IDbCommand> workerCallback)
        {
            try
            {
                using (IDbCommand command = work.CreateCommand())
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
            Stack<IUnitOfWork> unitOfWork = GetUnitOfWorkFromThread();

            // If we don't have a unit of work in the stack, then 
            // create a local scope unit of work that we will use for
            // the call.
            //
            IUnitOfWork work = (unitOfWork.Count > 0 ? unitOfWork.Peek() : null);

            if (work != null)
            {
                using (IDbCommand command = work.CreateCommand())
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
        /// Get the stack of units of work.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Stack<IUnitOfWork> GetUnitOfWorkFromThread()
        {
            LocalDataStoreSlot slot = Thread.GetNamedDataSlot("UnitOfWork");

            if (slot == null)
            {
                slot = Thread.AllocateNamedDataSlot("UnitOfWork");
            }

            return (Stack<IUnitOfWork>)Thread.GetData(slot);
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
