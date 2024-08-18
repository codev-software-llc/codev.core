//-----------------------------------------------------------------------------
// <copyright file="DestinationRepository.cs" company="Codev Software, LLC">
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
    /// This implements the repository for user security.
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
        /// Add a new entity to the store.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            DestinationEntity entity)
        {
            ValidateRepository<DestinationEntity>.Add(entity);

            SqliteAccess.CallStatement(
                "INSERT INTO [Destinations] ([IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Address], [Type], [ConfirmationSecret], [ConfirmationExpirationDate]) VALUES (1, ?, ?, ?, ?, ?, ?, ?, ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<DestinationFlags>("Flags"                     , entity.Flags                  );
                        command.AddInputParameter<Instant>         ("DateCreated"               , entity.DateCreated            );
                        command.AddInputParameter<Instant>         ("DateModified"              , entity.DateModified           );
                        command.AddInputParameter<Int32>           ("IdentityId"                , entity.Identity.Id            );
                        command.AddInputParameter<String>          ("Address"                   , entity.Address                );
                        command.AddInputParameter<DestinationType> ("Type"                      , entity.DestinationType        );
                        command.AddInputParameter<String>          ("ConfirmationSecret"        , entity.ConfirmationSecret     );
                        command.AddInputParameter<Instant>         ("ConfirmationExpirationDate", entity.DateConfirmationExpires);
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
            DestinationEntity entity)
        {
            ValidateRepository<DestinationEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Destinations] SET [IsActive] = 0 WHERE [RowId] = ?",
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

            SqliteAccess.CallStatement(
                "DELETE FROM [Destinations] WHERE [RowId] = ?",
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
        public EntityCollection<DestinationEntity> GetAll()
        {
            ValidateRepository<DestinationEntity>.GetAll();

            EntityCollection<DestinationEntity> entities = new EntityCollection<DestinationEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Address], [Type], [ConfirmationSecret], [ConfirmationExpirationDate] FROM [Destinations]",
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

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Address], [Type], [ConfirmationSecret], [ConfirmationExpirationDate] FROM [Destinations] WHERE [RowId] = ?",
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
            DestinationEntity entity)
        {
            ValidateRepository<DestinationEntity>.Update(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Destinations] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [IdentityId] = ?, [Address] = ?, [Type] = ?, [ConfirmationSecret] = ?, [ConfirmationExpirationDate] = ? WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<DestinationFlags>("Flags"                     , entity.Flags                  );
                        command.AddInputParameter<Instant>         ("DateCreated"               , entity.DateCreated            );
                        command.AddInputParameter<Instant>         ("DateModified"              , entity.DateModified           );
                        command.AddInputParameter<Int32>           ("IdentityId"                , entity.Identity.Id            );
                        command.AddInputParameter<String>          ("Address"                   , entity.Address                );
                        command.AddInputParameter<DestinationType> ("Type"                      , entity.DestinationType        );
                        command.AddInputParameter<String>          ("ConfirmationSecret"        , entity.ConfirmationSecret     );
                        command.AddInputParameter<Instant>         ("ConfirmationExpirationDate", entity.DateConfirmationExpires);
                        command.AddInputParameter<Int32>           ("RowId"                     , entity.Id                     );
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
        /// Return destinations for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<DestinationEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<DestinationEntity> entities = new EntityCollection<DestinationEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Address], [Type], [ConfirmationSecret], [ConfirmationExpirationDate] FROM [Destinations] WHERE [IdentityId] = ?",
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
        /// Retrieve the destination by the name and type combination.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public DestinationEntity GetByAddress(
            String          address,
            DestinationType destinationType)
        {
            DestinationEntity entity = null;

            Nullable<DestinationType> type = destinationType == DestinationType.Any ? null : destinationType;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Address], [Type], [ConfirmationSecret], [ConfirmationExpirationDate] FROM [Destinations] WHERE ([Address] = ?) AND ([Type] = ?)",
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
            String confirmationSecret)
        {
            DestinationEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Address], [Type], [ConfirmationSecret], [ConfirmationExpirationDate] FROM [Destinations] WHERE [ConfirmationSecret] = ?)",
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
            DestinationEntity entity = new DestinationEntity(
                reader.GetBaseEntity())
                    {
                        Flags                   = reader.GetValue<DestinationFlags>("Flags")             ,
                        Address                 = reader.GetValue<String>          ("Address")           ,
                        DestinationType         = reader.GetValue<DestinationType> ("Type")              ,
                        ConfirmationSecret      = reader.GetValue<String>          ("ConfirmationSecret"),
                        DateConfirmationExpires = reader.GetValue<Instant>         ("DateConfirmationExpires")
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
