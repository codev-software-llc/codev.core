//-----------------------------------------------------------------------------
// <copyright file="ScheduledTaskRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Data;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for task persistence.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ScheduledTaskRepository : BaseRepository, IScheduledTaskRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public ScheduledTaskRepository(
            ICoreDataSource     dataSource,
            IIdentityRepository identityRepository) : base(dataSource)
        {
            this.IdentityRepository = identityRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            ScheduledTaskEntity entity)
        {
            ValidateRepository<ScheduledTaskEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "ScheduledTask_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>                    ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<ScheduledTaskFlags>       ("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>                  ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>                  ("DateModified", entity.DateModified);
                        command.AddInputParameter<Instant>                  ("AttentionAt" , entity.AttentionAt );
                        command.AddInputParameter<ScheduledTaskPriorityType>("Priority"    , entity.Priority    );
                        command.AddInputParameter<String>                   ("Category"    , entity.Category    );
                        command.AddInputParameter<String>                   ("DetailType"  , entity.DetailType  );
                        command.AddInputParameter<String>                   ("Detail"      , entity.Detail      );
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            this.UpdateEntityRow(reader, entity);
                        }
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Deactivate the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Delete(
            ScheduledTaskEntity entity)
        {
            ValidateRepository<ScheduledTaskEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "ScheduledTask_Delete",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("RowId", entity.Id);
                    },
                (command, rowsAffected) =>
                    {
                        Boolean isValid = (rowsAffected > 0);
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Purge(
            ScheduledTaskEntity entity)
        {
            ValidateRepository<ScheduledTaskEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "ScheduledTask_Purge",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("RowId", entity.Id);
                    },
                (command, rowsAffected) =>
                    {
                        Boolean isValid = (rowsAffected > 0);
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all entities.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<ScheduledTaskEntity> GetAll()
        {
            ValidateRepository<ScheduledTaskEntity>.GetAll();

            EntityCollection<ScheduledTaskEntity> entities = new EntityCollection<ScheduledTaskEntity>();

            AdoAccess.CallProcedure(
                "ScheduledTask_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            ScheduledTaskEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the entity by its unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public ScheduledTaskEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<ScheduledTaskEntity>.GetById(entityId);

            ScheduledTaskEntity entity = null;

            AdoAccess.CallProcedure(
                "ScheduledTask_GetById",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("RowId", entityId);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = this.LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the entity information.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            ScheduledTaskEntity entity)
        {
            ValidateRepository<ScheduledTaskEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "ScheduledTask_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>                    ("RowId"       , entity.Id          );
                        command.AddInputParameter<Byte[]>                   ("RowVersion"  , entity.Version     );
                        command.AddInputParameter<Int32>                    ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<ScheduledTaskFlags>       ("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>                  ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>                  ("DateModified", entity.DateModified);
                        command.AddInputParameter<Instant>                  ("AttentionAt" , entity.AttentionAt );
                        command.AddInputParameter<ScheduledTaskPriorityType>("Priority"    , entity.Priority    );
                        command.AddInputParameter<String>                   ("Category"    , entity.Category    );
                        command.AddInputParameter<String>                   ("DetailType"  , entity.DetailType  );
                        command.AddInputParameter<String>                   ("Detail"      , entity.Detail      );
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            this.UpdateEntityRow(reader, entity);
                        }
                    });
        }
        #endregion

        #region Methods (Additional)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the scheduled tasks shared by the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<ScheduledTaskEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<ScheduledTaskEntity> entities = new EntityCollection<ScheduledTaskEntity>();

            AdoAccess.CallProcedure(
                "ScheduledTask_GetAllByIdentityId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("IdentityId", identity.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            ScheduledTaskEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private ScheduledTaskEntity LoadEntity(
            IDataReader reader)
        {
            ScheduledTaskEntity entity = new ScheduledTaskEntity(reader.GetBaseEntity())
                {
                    AttentionAt = reader.GetValue<Instant>                  ("AttentionAt"),
                    Priority    = reader.GetValue<ScheduledTaskPriorityType>("Priority")   ,
                    Category    = reader.GetValue<String>                   ("Category")   ,
                    DetailType  = reader.GetValue<String>                   ("DetailType") ,
                    Detail      = reader.GetValue<String>                   ("Detail")
                };

            this.InitializeLazyLoading(entity);

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the lazy loading of the entity properties.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeLazyLoading(
            ScheduledTaskEntity entity)
        {
            entity.LazyIdentity = new Lazy<IdentityEntity>(() =>
                {
                    return this.IdentityRepository.GetByScheduledTask(entity);
                });
        }
        #endregion
    }
}
