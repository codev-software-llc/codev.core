//-----------------------------------------------------------------------------
// <copyright file="DestinationRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Data;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for the authentication destinations.
    /// </summary>
    ///------------------------------------------------------------------------
    public class DestinationRepository : BaseRepository, IDestinationRepository
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------------
        public DestinationRepository(
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
            DestinationEntity entity)
        {
            ValidateRepository<DestinationEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "Destination_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>           ("IdentityId"                , entity.Identity.Id            );
                        command.AddInputParameter<DestinationFlags>("Flags"                     , entity.Flags                  );
                        command.AddInputParameter<Instant>         ("DateCreated"               , entity.DateCreated            );
                        command.AddInputParameter<Instant>         ("DateModified"              , entity.DateModified           );
                        command.AddInputParameter<Instant>         ("ConfirmationExpirationDate", entity.DateConfirmationExpires);
                        command.AddInputParameter<String>          ("Address"                   , entity.Address                );
                        command.AddInputParameter<DestinationType> ("Type"                      , entity.DestinationType        );
                        command.AddInputParameter<String>          ("ConfirmationSecret"        , entity.ConfirmationSecret     );
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
            DestinationEntity entity)
        {
            ValidateRepository<DestinationEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "Destination_Delete",
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
            DestinationEntity entity)
        {
            ValidateRepository<DestinationEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "Destination_Purge",
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
        /// Retrieve all the entities.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<DestinationEntity> GetAll()
        {
            ValidateRepository<DestinationEntity>.GetAll();

            EntityCollection<DestinationEntity> entities = new EntityCollection<DestinationEntity>();

            AdoAccess.CallProcedure(
                "Destination_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            DestinationEntity entity = this.LoadEntity(reader);

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
        public DestinationEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<DestinationEntity>.GetById(entityId);

            DestinationEntity entity = null;

            AdoAccess.CallProcedure(
                "Destination_GetById",
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
            DestinationEntity entity)
        {
            ValidateRepository<DestinationEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "Destination_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>           ("RowId"                     , entity.Id                     );
                        command.AddInputParameter<Byte[]>          ("RowVersion"                , entity.Version                );
                        command.AddInputParameter<Int32>           ("IdentityId"                , entity.Identity.Id            );
                        command.AddInputParameter<DestinationFlags>("Flags"                     , entity.Flags                  );
                        command.AddInputParameter<Instant>         ("DateCreated"               , entity.DateCreated            );
                        command.AddInputParameter<Instant>         ("DateModified"              , entity.DateModified           );
                        command.AddInputParameter<Instant>         ("ConfirmationExpirationDate", entity.DateConfirmationExpires);
                        command.AddInputParameter<String>          ("Address"                   , entity.Address                );
                        command.AddInputParameter<DestinationType> ("Type"                      , entity.DestinationType        );
                        command.AddInputParameter<String>          ("ConfirmationSecret"        , entity.ConfirmationSecret     );
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
        /// Return destinations associated to the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<DestinationEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<DestinationEntity> entities = new EntityCollection<DestinationEntity>();

            AdoAccess.CallProcedure(
                "Destination_GetAllByIdentityId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("IdentityId", identity.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            DestinationEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the destination by the name and type.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public DestinationEntity GetByAddress(
            String          address,
            DestinationType destinationType)
        {
            DestinationEntity entity = null;

            Nullable<DestinationType> type = destinationType == DestinationType.Any ? null : destinationType;

            AdoAccess.CallProcedure(
                "Destination_GetByAddress",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String>                   ("Address", address);
                        command.AddInputParameter<Nullable<DestinationType>>("Type"   , type   );
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
        public DestinationEntity GetByConfirmationSecret(
            String  confirmationSecret)
        {
           DestinationEntity entity = null;

            AdoAccess.CallProcedure(
                "Destination_GetByConfirmationSecret",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String> ("ConfirmationSecret", confirmationSecret);
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
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private DestinationEntity LoadEntity(
            IDataReader reader)
        {
            DestinationEntity entity = new DestinationEntity(reader.GetBaseEntity())
                {
                    Flags                   = reader.GetValue<DestinationFlags>("Flags")                     ,
                    DateConfirmationExpires = reader.GetValue<Instant>         ("ConfirmationExpirationDate"),
                    Address                 = reader.GetValue<String>          ("Address")                   ,
                    DestinationType         = reader.GetValue<DestinationType> ("Type")                      ,
                    ConfirmationSecret      = reader.GetValue<String>          ("ConfirmationSecret")
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
            DestinationEntity entity)
        {
            entity.LazyIdentity = new Lazy<IdentityEntity>(() =>
                {
                    return this.IdentityRepository.GetByDestination(entity);
                });
        }
        #endregion
    }
}
