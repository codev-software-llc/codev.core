//-----------------------------------------------------------------------------
// <copyright file="ScheduledTaskService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.ScheduledTask
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
            Reference<TaskJob> scheduledTaskReference)
        {
            Validation.ValidateParameter<Reference<TaskJob>>("scheduledTaskReference", scheduledTaskReference);

            try
            {
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    ScheduledTaskEntity scheduledTaskEntity = this.TaskRepository.GetById(scheduledTaskReference.Id);

                    if (scheduledTaskEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByScheduledTask(scheduledTaskEntity);

                        if (identityEntity != null)
                        {
                            scheduledTaskEntity.Identity = identityEntity;

                            this.Cancel(scheduledTaskEntity);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.ScheduledTaskDoesNotExist);
                    }

                    work.Commit();
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
        TaskJob IScheduledTaskService.Get(
            Reference<TaskJob> scheduledTaskReference)
        {
            Validation.ValidateParameter<Reference<TaskJob>>("scheduledTaskReference", scheduledTaskReference);

            try
            {
                TaskJob scheduledTask = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    ScheduledTaskEntity scheduledTaskEntity = this.TaskRepository.GetById(scheduledTaskReference.Id);

                    if (scheduledTaskEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByScheduledTask(scheduledTaskEntity);

                        if (identityEntity != null)
                        {
                            scheduledTaskEntity.Identity = identityEntity;

                            this.Get(scheduledTaskEntity);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.ScheduledTaskDoesNotExist);
                    }
                }

                return scheduledTask;
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
        List<TaskJob> IScheduledTaskService.GetAll(
            Reference<Identity> identityReference)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

            try
            {
                List<TaskJob> scheduledTasks = new List<TaskJob>();

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        EntityCollection<ScheduledTaskEntity> entities = this.TaskRepository.GetAllByIdentity(identityEntity);

                        foreach (ScheduledTaskEntity entity in entities)
                        {
                            entity.Identity = identityEntity;

                            scheduledTasks.Add(entity.ToModel());
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
                }

                return scheduledTasks;
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
            Reference<Identity>       identityReference,
            LocalDateTime             nextAttentionAt,
            ScheduledTaskPriorityType priority,
            String                    category,
            String                    detailTypeName,
            String                    serializedDetail)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
            Validation.ValidateParameter<LocalDateTime>      ("nextAttentionAt"  , nextAttentionAt  );
            Validation.ValidateParameter<String>             ("category"         , category         );
            Validation.ValidateParameter<String>             ("detailTypeName"   , detailTypeName   );
            Validation.ValidateParameter<String>             ("serializedDetail" , serializedDetail );

            try
            {
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        this.Schedule(identityEntity, nextAttentionAt, priority, category, detailTypeName, serializedDetail);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExistMessage);
                    }

                    work.Commit();
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
            ScheduledTaskPriorityType               priority,
            Action<TaskJob, ScheduledTaskEventArgs> clientWorker)
        {
            Validation.ValidateParameter<Action<TaskJob, ScheduledTaskEventArgs>>("clientWorkder", clientWorker);

            try
            {
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    this.Run(priority, clientWorker);

                    work.Commit();
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
            String                                  category,
            ScheduledTaskPriorityType               priority,
            Action<TaskJob, ScheduledTaskEventArgs> clientWorker)
        {
            Validation.ValidateParameter<String>                                 ("category"     , category    );
            Validation.ValidateParameter<Action<TaskJob, ScheduledTaskEventArgs>>("clientWorkder", clientWorker);

            try
            {
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    this.Run(category, priority, clientWorker);

                    work.Commit();
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    this.GarbageCollect();

                    work.Commit();
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
