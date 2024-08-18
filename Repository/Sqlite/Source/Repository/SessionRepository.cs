//-----------------------------------------------------------------------------
// <copyright file="SessionRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Data;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for session persistence.
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
            IDataSource         dataSource,
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
            SessionEntity entity)
        {
            ValidateRepository<SessionEntity>.Add(entity);

            SqliteAccess.CallStatement(
                "INSERT INTO [Sessions] ([IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [DateExpiration], [Secret]) VALUES (1, ?, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {                    
                        command.AddInputParameter<SessionFlags>("Flags"         , entity.Flags         );
                        command.AddInputParameter<Instant>     ("DateCreated"   , entity.DateCreated   );
                        command.AddInputParameter<Instant>     ("DateModified"  , entity.DateModified  );
                        command.AddInputParameter<Int32>       ("IdentityId"    , entity.Identity.Id   );
                        command.AddInputParameter<Instant>     ("DateExpiration", entity.DateExpiration);
                        command.AddInputParameter<String>      ("Secret"        , entity.Secret        );
                    },
                (command) =>
                    {
                    });

            RowVersionHelper.UpdateIdentifier(this.DataSource, entity);
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

            SqliteAccess.CallStatement(
                "UPDATE [Sessions] SET [IsActive] = 0 WHERE [RowId] = ?",
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
            SessionEntity entity)
        {
            ValidateRepository<SessionEntity>.Purge(entity);

            SqliteAccess.CallStatement(
                "DELETE FROM [Sessions] WHERE [RowId] = ?",
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
        public EntityCollection<SessionEntity> GetAll()
        {
            ValidateRepository<SessionEntity>.GetAll();

            EntityCollection<SessionEntity> entities = new EntityCollection<SessionEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [DateExpiration], [Secret] FROM [Sessions]",
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
        /// Return the entity by its unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public SessionEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<SessionEntity>.GetById(entityId);

            SessionEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [DateExpiration], [Secret] FROM [Sessions] WHERE [RowId] = ?",
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
            SessionEntity entity)
        {
            ValidateRepository<SessionEntity>.Update(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Sessions] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [IdentityId] = ?, [DateExpiration] = ?, [Secret] = ? WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<SessionFlags>("Flags"         , entity.Flags         );
                        command.AddInputParameter<Instant>     ("DateCreated"   , entity.DateCreated   );
                        command.AddInputParameter<Instant>     ("DateModified"  , entity.DateModified  );
                        command.AddInputParameter<Int32>       ("IdentityId"    , entity.Identity.Id   );
                        command.AddInputParameter<Instant>     ("DateExpiration", entity.DateExpiration);
                        command.AddInputParameter<String>      ("Secret"        , entity.Secret        );
                        command.AddInputParameter<Int32>       ("RowId"         , entity.Id            );
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
        /// Return the sessions that share the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<SessionEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<SessionEntity> entities = new EntityCollection<SessionEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [DateExpiration], [Secret] FROM [Sessions] WHERE [IdentityId] = ?",
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
        /// Return the session using the secret.
        /// </summary>
        ///--------------------------------------------------------------------
        public SessionEntity GetBySessionSecret(
            String secret)
        {
            SessionEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [DateExpiration], [Secret] FROM [Sessions] WHERE [Secret] = ?",
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
        /// Remove all expired sessions.
        /// </summary>
        ///--------------------------------------------------------------------
        public void PurgeAllExpiredSessions(
            Instant instant)
        {
            SqliteAccess.CallStatement(
                "DELETE FROM [Sessionss] WHERE [DateExpiration] > ?",
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
            SessionEntity entity = new SessionEntity(
                reader.GetBaseEntity())
                    {
                        Flags          = reader.GetValue<SessionFlags>("Flags")         ,
                        DateExpiration = reader.GetValue<Instant>     ("DateExpiration"),
                        Secret         = reader.GetValue<String>      ("Secret")
                    };

            this.InitializeLazyLoading(entity);

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the lazy loading of the entity.
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
