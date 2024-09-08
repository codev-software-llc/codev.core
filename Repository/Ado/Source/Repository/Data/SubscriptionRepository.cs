//-----------------------------------------------------------------------------
// <copyright file="SubscriptionRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Data;
    using System.Text.Json;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for license subscriptions.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------------
        public SubscriptionRepository(
            ICoreDataSource     dataSource,
            IIdentityRepository identityRepository,
            ILicenseRepository  licenseRepository) : base(dataSource)
        {
            this.IdentityRepository = identityRepository;
            this.LicenseRepository  = licenseRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private ILicenseRepository LicenseRepository { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            SubscriptionEntity entity)
        {
            ValidateRepository<SubscriptionEntity>.Add(entity);

            String data = JsonSerializer.Serialize(entity.Token);

            AdoAccess.CallProcedure(
                "Subscription_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<SubscriptionFlags>("Flags"         , entity.Flags         );
                        command.AddInputParameter<Instant>          ("DateCreated"   , entity.DateCreated   );
                        command.AddInputParameter<Instant>          ("DateModified"  , entity.DateModified  );
                        command.AddInputParameter<Instant>          ("DateExpiration", entity.DateExpiration);
                        command.AddInputParameter<Int32>            ("LicenseId"     , entity.License.Id    );
                        command.AddInputParameter<Int32>            ("IdentityId"    , entity.Identity.Id   );
                        command.AddInputParameter<String>           ("SerializedData", data                 );
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
            SubscriptionEntity entity)
        {
            ValidateRepository<SubscriptionEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "Subscription_Delete",
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
            SubscriptionEntity entity)
        {
            ValidateRepository<SubscriptionEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "Subscription_Purge",
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
        public EntityCollection<SubscriptionEntity> GetAll()
        {
            ValidateRepository<SubscriptionEntity>.GetAll();

            EntityCollection<SubscriptionEntity> entities = new();

            AdoAccess.CallProcedure(
                "Subscription_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            SubscriptionEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the entity by the unique identifier.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public SubscriptionEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<SubscriptionEntity>.GetById(entityId);

            SubscriptionEntity entity = null;

            AdoAccess.CallProcedure(
                "Subscription_GetById",
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
            SubscriptionEntity entity)
        {
            ValidateRepository<SubscriptionEntity>.Update(entity);

            String data = JsonSerializer.Serialize(entity.Token);

            AdoAccess.CallProcedure(
                "Subscription_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>            ("RowId"         , entity.Id            );
                        command.AddInputParameter<Byte[]>           ("RowVersion"    , entity.Version       );
                        command.AddInputParameter<SubscriptionFlags>("Flags"         , entity.Flags         );
                        command.AddInputParameter<Instant>          ("DateCreated"   , entity.DateCreated   );
                        command.AddInputParameter<Instant>          ("DateModified"  , entity.DateModified  );
                        command.AddInputParameter<Instant>          ("DateExpiration", entity.DateExpiration);
                        command.AddInputParameter<Int32>            ("LicenseId"     , entity.License.Id    );
                        command.AddInputParameter<Int32>            ("IdentityId"    , entity.Identity.Id   );
                        command.AddInputParameter<String>           ("SerializedData", data                 );
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
        /// Return all the subscriptions for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<SubscriptionEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<SubscriptionEntity> entities = new();

            AdoAccess.CallProcedure(
                "Subscription_GetAllByIdentityId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("IdentityId", identity.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            SubscriptionEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return all the subscriptions for the license.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<SubscriptionEntity> GetAllByLicense(
            LicenseEntity license)
        {
            EntityCollection<SubscriptionEntity> entities = new();

            AdoAccess.CallProcedure(
                "Subscription_GetAllByLicenseId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("LicenseId", license.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            SubscriptionEntity entity = this.LoadEntity(reader);

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
        private SubscriptionEntity LoadEntity(
            IDataReader reader)
        {
            Token data = JsonSerializer.Deserialize<Token>(reader.GetValue<String>("SerializedData"));

            SubscriptionEntity entity = new(reader.GetBaseEntity())
                {
                    Flags          = reader.GetValue<SubscriptionFlags>("Flags")         ,
                    DateExpiration = reader.GetValue<Instant>          ("DateExpiration"),
                    Token          = data
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
            SubscriptionEntity entity)
        {
            entity.LazyLicense = new Lazy<LicenseEntity>(() =>
                {
                    return this.LicenseRepository.GetBySubscription(entity);
                });

            entity.LazyIdentity = new Lazy<IdentityEntity>(() =>
                {
                    return this.IdentityRepository.GetBySubscription(entity);
                });
        }
        #endregion
    }
}
