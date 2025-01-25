//-----------------------------------------------------------------------------
// <copyright file="SqliteAccess.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using Codev.Core.Base;
    using Microsoft.Data.Sqlite;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides a common data access utility for making calls to the
    /// repository store.  This utility should be able to interface with both
    /// ADO and CE related stacks.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class SqliteAccess
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the static members of the class.
        /// </summary>
        ///--------------------------------------------------------------------
        static SqliteAccess()
        {
            SqliteAccess.SqlErrorMappings = new Dictionary<Int32, Int32>();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the mapping dictionary.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Dictionary<Int32, Int32> SqlErrorMappings { get; set; }
        #endregion

        #region Methods (Statements)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke a call to the ADO store with a setup and worker callback.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void CallStatement(
            String             commandText,
            IDataSource        dataSource,
            Action<IDbCommand> setupCallback,
            Action<IDbCommand> workerCallback)
        {
            SqliteAccess.CallWorker(
                CommandType.Text,
                commandText,
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
        /// Invoke the call to the ADO store where the worker can pull the
        /// reader results.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void CallStatement(
            String                          commandText,
            IDataSource                     dataSource,
            Action<IDbCommand>              setupCallback,
            Action<IDbCommand, IDataReader> workerCallback)
        {
            SqliteAccess.CallWorker(
                CommandType.Text,
                commandText,
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
        /// Invoke a call to the ADO store with parameters, but also returns
        /// the rows affected.
        /// </summary>
        ///--------------------------------------------------------------------
        public static void CallStatement(
            String                    commandText,
            IDataSource               dataSource,
            Action<IDbCommand>        setupCallback,
            Action<IDbCommand, Int32> workerCallback)
        {            
            SqliteAccess.CallWorker(
                CommandType.Text,
                commandText,
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
        private static void CallWorker(
            CommandType        commandType,
            String             commandText,
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
                SqliteAccess.DoCall(work, commandType, commandText, setupCallback, workerCallback);
            }
            else
            {
                using (IUnitOfWork localWork = new UnitOfWorkSqlite(dataSource))
                {
                    SqliteAccess.DoCall(localWork, commandType, commandText, setupCallback, workerCallback);

                    localWork.Commit();
                }
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Make the call using the unit of work.
        /// </summary>
        ///--------------------------------------------------------------------
        private static void DoCall(
            IUnitOfWork        work,
            CommandType        commandType,
            String             commandText,
            Action<IDbCommand> setupCallback,
            Action<IDbCommand> workerCallback)
        {
            // Perform the work.
            // 
            try
            {
                using (IDbCommand command = work.CreateCommand())
                {
                    command.CommandType = commandType;
                    command.CommandText = commandText;

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
            catch (SqliteException se)
            {
                // Log to the diagnostics.
                //
                Int32 errorCode = HandleSqlException(se);

                throw new CoreDataException(CoreErrorCode.InternalFailure, se.InnerException != null ? se.InnerException : se);
            }
            catch (Exception e)
            {
                throw new CoreDataException(CoreErrorCode.InternalFailure, e);
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
        /// This will analyze the SQL exception to determine what error
        /// codes to send back in the throw.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Int32 HandleSqlException(
            SqliteException exception)
        {
            Int32 exceptionError = 0;

            SqliteAccess.SqlErrorMappings.TryGetValue(exception.ErrorCode, out exceptionError);

            return exceptionError;
        }
        #endregion
    }
}
