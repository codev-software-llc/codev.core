//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
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
    /// This implements the repository for a service link.
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
        /// Add a new entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            ServiceLinkEntity entity)
        {
            ValidateRepository<ServiceLinkEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "ServiceLink_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>           ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<ServiceLinkFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>         ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>         ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>          ("TinyUrl"     , entity.TinyUrl     );
                        command.AddInputParameter<String>          ("DetailType"  , entity.DetailType  );
                        command.AddInputParameter<String>          ("Detail"      , entity.Detail      );
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
            ServiceLinkEntity entity)
        {
            ValidateRepository<ServiceLinkEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "ServiceLink_Delete",
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
            ServiceLinkEntity entity)
        {
            ValidateRepository<ServiceLinkEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "ServiceLink_Purge",
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

            AdoAccess.CallProcedure(
                "ServiceLink_GetAll",
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
        /// Retrieve the entity by its unique identifier.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public ServiceLinkEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<ServiceLinkEntity>.GetById(entityId);

            ServiceLinkEntity entity = null;

            AdoAccess.CallProcedure(
                "ServiceLink_GetById",
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
        /// This will update the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            ServiceLinkEntity entity)
        {
            ValidateRepository<ServiceLinkEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "ServiceLink_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>           ("RowId"       , entity.Id          );
                        command.AddInputParameter<Byte[]>          ("RowVersion"  , entity.Version     );
                        command.AddInputParameter<Int32>           ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<ServiceLinkFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>         ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>         ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>          ("TinyUrl"     , entity.TinyUrl     );
                        command.AddInputParameter<String>          ("DetailType"  , entity.DetailType  );
                        command.AddInputParameter<String>          ("Detail"      , entity.Detail      );
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
        /// Return all service links that share the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<ServiceLinkEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<ServiceLinkEntity> entities = new EntityCollection<ServiceLinkEntity>();

            AdoAccess.CallProcedure(
                "ServiceLink_GetAllByIdentityId",
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
        /// Return service link using the tiny url.
        /// </summary>
        ///--------------------------------------------------------------------
        public ServiceLinkEntity GetByTinyUrl(
            String tinyUrl)
        {
            ServiceLinkEntity entity = null;

            AdoAccess.CallProcedure(
                "ServiceLink_GetByTinyUrl",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String>("TinyUrL", tinyUrl);
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
            AdoAccess.CallProcedure(
                "ServiceLink_PurgeAllByIdentity",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("IdentityId", identity.Id);
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
            ServiceLinkEntity entity = new ServiceLinkEntity(reader.GetBaseEntity())
                {
                    Flags      = reader.GetValue<ServiceLinkFlags>("Flags")     ,
                    TinyUrl    = reader.GetValue<String>          ("TinyURL")   ,
                    DetailType = reader.GetValue<String>          ("DetailType"),
                    Detail     = reader.GetValue<String>          ("Detail")
                };

            return entity;
        }
        #endregion
    }
}
