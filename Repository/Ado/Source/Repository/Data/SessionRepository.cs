//-----------------------------------------------------------------------------
// <copyright file="SessionRepository.cs" company="Codev Software, LLC">
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
    /// This implements the repository for tracking user sessions.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SessionRepository : BaseRepository, ISessionRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public SessionRepository(
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
        /// Add a new entity to the store.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            SessionEntity entity)
        {
            ValidateRepository<SessionEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "Session_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>       ("IdentityId"    , entity.Identity.Id   );
                        command.AddInputParameter<SessionFlags>("Flags"         , entity.Flags         );
                        command.AddInputParameter<Instant>     ("DateCreated"   , entity.DateCreated   );
                        command.AddInputParameter<Instant>     ("DateModified"  , entity.DateModified  );
                        command.AddInputParameter<Instant>     ("DateExpiration", entity.DateExpiration);
                        command.AddInputParameter<String>      ("Secret"        , entity.Secret        );
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
            SessionEntity entity)
        {
            ValidateRepository<SessionEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "Session_Delete",
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
        /// Delete the entity permanently.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public void Purge(
            SessionEntity entity)
        {
            ValidateRepository<SessionEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "Session_Purge",
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
        /// Retrieve all blobs.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<SessionEntity> GetAll()
        {
            ValidateRepository<SessionEntity>.GetAll();

            EntityCollection<SessionEntity> entities = new EntityCollection<SessionEntity>();

            AdoAccess.CallProcedure(
                "Session_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            SessionEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the entity by its unique identifier.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public SessionEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<SessionEntity>.GetById(entityId);

            SessionEntity entity = null;

            AdoAccess.CallProcedure(
                "Session_GetById",
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
        /// This will update the entity information.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            SessionEntity entity)
        {
            ValidateRepository<SessionEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "Session_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>       ("RowId"         , entity.Id            );
                        command.AddInputParameter<Byte[]>      ("RowVersion"    , entity.Version       );
                        command.AddInputParameter<Int32>       ("IdentityId"    , entity.Identity.Id   );
                        command.AddInputParameter<SessionFlags>("Flags"         , entity.Flags         );
                        command.AddInputParameter<Instant>     ("DateCreated"   , entity.DateCreated   );
                        command.AddInputParameter<Instant>     ("DateModified"  , entity.DateModified  );
                        command.AddInputParameter<Instant>     ("DateExpiration", entity.DateExpiration);
                        command.AddInputParameter<String>      ("Secret"        , entity.Secret        );
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
        /// Return sessions associated to the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<SessionEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<SessionEntity> entities = new EntityCollection<SessionEntity>();

            AdoAccess.CallProcedure(
                "Session_GetAllByIdentityId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("IdentityId", identity.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            SessionEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the entity by the unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public SessionEntity GetBySessionId(
            Guid sessionId)
        {
            SessionEntity entity = null;

            AdoAccess.CallProcedure(
                "Session_GetBySessionId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Guid>("SessionId", sessionId);
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
        /// Return the entity by the secret.
        /// </summary>
        ///--------------------------------------------------------------------
        public SessionEntity GetBySessionSecret(
            String secret)
        {
            SessionEntity entity = null;

            AdoAccess.CallProcedure(
                "Session_GetBySecret",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String>("Secret", secret);
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
        /// Purge all expired sessions.
        /// </summary>
        ///--------------------------------------------------------------------
        public void PurgeAllExpiredSessions(
            Instant instant)
        {
            AdoAccess.CallProcedure(
                "Session_PurgeAllExpired",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Instant>("DateExpired", instant);
                    },
                (command, rowsAffected) =>
                    {
                        Boolean isValid = (rowsAffected > 0);
                    });
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private SessionEntity LoadEntity(
            IDataReader reader)
        {
            SessionEntity entity = new SessionEntity(reader.GetBaseEntity())
                {
                    Flags          = reader.GetValue<SessionFlags>("Flags")         ,
                    DateExpiration = reader.GetValue<Instant>     ("DateExpiration"),
                    SessionId      = reader.GetValue<Guid>        ("SessionId")     ,
                    Secret         = reader.GetValue<String>      ("Secret")
                };

            this.InitializeLazyLoading(entity);

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the entity lazy loading properties.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeLazyLoading(
            SessionEntity entity)
        {
            entity.LazyIdentity = new Lazy<IdentityEntity>(() =>
                {
                    return this.IdentityRepository.GetBySession(entity);
                });
        }
        #endregion
    }
}
