//-----------------------------------------------------------------------------
// <copyright file="ScheduledTaskService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Task
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains the explicit interface hooks that we will use to perform
    /// call validation on the parameters.  Each of these methods will in-turn
    /// call the actual implementations.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ScheduledTaskService : IScheduledTaskService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Cancel task.
        /// </summary>
        ///--------------------------------------------------------------------
        void IScheduledTaskService.Cancel(
            ScheduledTask task)
        {
            try
            {
                Validation.ValidateParameter<ScheduledTask>("task", task);

                using (this.UnitOfWork.Begin())
                {
                    this.Cancel(task);

                    this.UnitOfWork.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the task by the identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        ScheduledTask IScheduledTaskService.Get(
            Int32 id)
        {
            try
            {
                Validation.ValidateParameter<Int32>("id", id);

                ScheduledTask task = null;

                using (this.UnitOfWork.Begin())
                {
                    task = this.Get(id);

                    this.UnitOfWork.Commit();
                }

                return task;
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the tasks for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ScheduledTask> IScheduledTaskService.GetAll(
            Identity identity)
        {
            try
            {
                List<ScheduledTask> tasks = new List<ScheduledTask>();

                Validation.ValidateParameter<Identity>("identity", identity);

                using (this.UnitOfWork.Begin())
                {
                    tasks = this.GetAll(identity);

                    this.UnitOfWork.Commit();
                }

                return tasks;
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Schedule a new task.
        /// </summary>
        ///--------------------------------------------------------------------
        void IScheduledTaskService.Schedule(
            Identity                  identity,
            LocalDateTime             nextAttentionAt,
            ScheduledTaskPriorityType priority,
            String                    category,
            String                    detailTypeName,
            String                    serializedDetail)
        {
            try
            {
                Validation.ValidateParameter<Identity>     ("identity"        , identity        );
                Validation.ValidateParameter<LocalDateTime>("nextAttentionAt" , nextAttentionAt );
                Validation.ValidateParameter<String>       ("category"        , category        );
                Validation.ValidateParameter<String>       ("detailTypeName"  , detailTypeName  );
                Validation.ValidateParameter<String>       ("serializedDetail", serializedDetail);

                using (this.UnitOfWork.Begin())
                {
                    this.Schedule(identity, nextAttentionAt, priority, category, detailTypeName, serializedDetail);

                    this.UnitOfWork.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Run tasks.  The caller will be invoked upon the running of the
        /// task.
        /// </summary>
        ///--------------------------------------------------------------------
        void IScheduledTaskService.Run(
            ScheduledTaskPriorityType                     priority,
            Action<ScheduledTask, ScheduledTaskEventArgs> clientWorker)
        {
            try
            {
                Validation.ValidateParameter<Action<ScheduledTask, ScheduledTaskEventArgs>>("clientWorkder", clientWorker);

                using (this.UnitOfWork.Begin())
                {
                    this.Run(priority, clientWorker);

                    this.UnitOfWork.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Run tasks.  The caller will be invoked upon the running of the
        /// task.
        /// </summary>
        ///--------------------------------------------------------------------
        void IScheduledTaskService.Run(
            String                                        category,
            ScheduledTaskPriorityType                     priority,
            Action<ScheduledTask, ScheduledTaskEventArgs> clientWorker)
        {
            try
            {
                Validation.ValidateParameter<String>                                       ("category"     , category    );
                Validation.ValidateParameter<Action<ScheduledTask, ScheduledTaskEventArgs>>("clientWorkder", clientWorker);

                using (this.UnitOfWork.Begin())
                {
                    this.Run(category, priority, clientWorker);

                    this.UnitOfWork.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Clean up any tasks that are obsolete, complete or cancelled.
        /// </summary>
        ///--------------------------------------------------------------------
        void IScheduledTaskService.GarbageCollect()
        {
            try
            {
                using (this.UnitOfWork.Begin())
                {
                    this.GarbageCollect();

                    this.UnitOfWork.Commit();
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion
    }
}
