//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Data;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for service link persistence.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ServiceLinkRepository : BaseRepository, IServiceLinkRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public ServiceLinkRepository(
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
            ServiceLinkEntity entity)
        {
            ValidateRepository<ServiceLinkEntity>.Add(entity);

            SqliteAccess.CallStatement(
                "INSERT INTO [ServiceLinks] ([IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [TinyUrl], [DetailType], [Detail]) VALUES (1, ?, ?, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {                    
                        command.AddInputParameter<ServiceLinkFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>         ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>         ("DateModified", entity.DateModified);
                        command.AddInputParameter<Int32>           ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<String>          ("TinyUrl"     , entity.TinyUrl     );
                        command.AddInputParameter<String>          ("DetailType"  , entity.DetailType  );
                        command.AddInputParameter<String>          ("Detail"      , entity.Detail      );
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
            ServiceLinkEntity entity)
        {
            ValidateRepository<ServiceLinkEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [ServiceLinks] SET [IsActive] = 0 WHERE [RowId] = ?",
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
            ServiceLinkEntity entity)
        {
            ValidateRepository<ServiceLinkEntity>.Purge(entity);

            SqliteAccess.CallStatement(
                "DELETE FROM [ServiceLinks] WHERE [RowId] = ?",
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
        public EntityCollection<ServiceLinkEntity> GetAll()
        {
            ValidateRepository<ServiceLinkEntity>.GetAll();

            EntityCollection<ServiceLinkEntity> entities = new EntityCollection<ServiceLinkEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [TinyUrl], [DetailType], [Detail] FROM [ServiceLinks]",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            ServiceLinkEntity entity = this.LoadEntity(reader);

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
        public ServiceLinkEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<ServiceLinkEntity>.GetById(entityId);

            ServiceLinkEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [TinyUrl], [DetailType], [Detail] FROM [ServiceLinks] WHERE [RowId] = ?",
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
            ServiceLinkEntity entity)
        {
            ValidateRepository<ServiceLinkEntity>.Update(entity);

            SqliteAccess.CallStatement(
                "UPDATE [ServiceLinks] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [IdentityId] = ?, [TinyUrl] = ?, [DetailType] = ?, [Detail] = ? WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<ServiceLinkFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>         ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>         ("DateModified", entity.DateModified);
                        command.AddInputParameter<Int32>           ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<String>          ("TinyUrl"     , entity.TinyUrl     );
                        command.AddInputParameter<String>          ("DetailType"  , entity.DetailType  );
                        command.AddInputParameter<String>          ("Detail"      , entity.Detail      );
                        command.AddInputParameter<Int32>           ("RowId"       , entity.Id          );
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
        /// Return the service links that share the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<ServiceLinkEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<ServiceLinkEntity> entities = new EntityCollection<ServiceLinkEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [TinyUrl], [DetailType], [Detail] FROM [ServiceLinks] WHERE [IdentityId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("IdentityId", identity.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            ServiceLinkEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the service link using the tiny url.
        /// </summary>
        ///--------------------------------------------------------------------
        public ServiceLinkEntity GetByTinyUrl(
            String tinyUrl)
        {
            ServiceLinkEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [TinyUrl], [DetailType], [Detail] FROM [ServiceLinks] WHERE [TinyUrl] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String>("TinyUrl", tinyUrl);
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
        /// Remove all service links that share the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void PurgeAllByIdentity(
            IdentityEntity identity)
        {
            SqliteAccess.CallStatement(
                "DELETE FROM [ServiceLinks] WHERE [IdentityId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("RowId", identity.Id);
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
        private ServiceLinkEntity LoadEntity(
            IDataReader reader)
        {
            ServiceLinkEntity entity = new ServiceLinkEntity(
                reader.GetBaseEntity())
                    {
                        TinyUrl    = reader.GetValue<String> ("TinyUrl")   ,
                        DetailType = reader.GetValue<String> ("DetailType"),
                        Detail     = reader.GetValue<String> ("Detail")
                    };

            this.InitializeLazyLoading(entity);

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the lazy loading of the Contact entity properties.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeLazyLoading(
            ServiceLinkEntity entity)
        {
            entity.LazyIdentity = new Lazy<IdentityEntity>(() =>
                {
                    return this.IdentityRepository.GetByServiceLink(entity);
                });
        }
        #endregion
    }
}
