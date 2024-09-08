//-----------------------------------------------------------------------------
// <copyright file="LicenseRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text.Json;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for blob storage.
    /// </summary>
    ///------------------------------------------------------------------------
    public class LicenseRepository : BaseRepository, ILicenseRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public LicenseRepository(
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
            LicenseEntity entity)
        {
            ValidateRepository<LicenseEntity>.Add(entity);

            String features = JsonSerializer.Serialize(entity.Features);
            String data     = JsonSerializer.Serialize(entity.Token);

            AdoAccess.CallProcedure(
                "License_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<LicenseFlags>("Flags"         , entity.Flags       );
                        command.AddInputParameter<Instant>     ("DateCreated"   , entity.DateCreated );
                        command.AddInputParameter<Instant>     ("DateModified"  , entity.DateModified);
                        command.AddInputParameter<Int32>       ("IdentityId"    , entity.Identity.Id );
                        command.AddInputParameter<String>      ("Application"   , entity.Application );
                        command.AddInputParameter<String>      ("Name"          , entity.Name        );
                        command.AddInputParameter<String>      ("CurrencyCode"  , entity.Cost.IsoCode);
                        command.AddInputParameter<Decimal>     ("Amount"        , entity.Cost.Amount );
                        command.AddInputParameter<String>      ("Features"      , features           );
                        command.AddInputParameter<String>      ("SerializedData", data               );
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
            LicenseEntity entity)
        {
            ValidateRepository<LicenseEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "License_Delete",
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
            LicenseEntity entity)
        {
            ValidateRepository<LicenseEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "License_Purge",
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
        public EntityCollection<LicenseEntity> GetAll()
        {
            ValidateRepository<LicenseEntity>.GetAll();

            EntityCollection<LicenseEntity> entities = new();

            AdoAccess.CallProcedure(
                "License_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            LicenseEntity entity = this.LoadEntity(reader);

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
        public LicenseEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<LicenseEntity>.GetById(entityId);

            LicenseEntity entity = null;

            AdoAccess.CallProcedure(
                "License_GetById",
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
            LicenseEntity entity)
        {
            ValidateRepository<LicenseEntity>.Update(entity);

            String features = JsonSerializer.Serialize(entity.Features);
            String data     = JsonSerializer.Serialize(entity.Token);

            AdoAccess.CallProcedure(
                "License_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>       ("RowId"         , entity.Id          );
                        command.AddInputParameter<Byte[]>      ("RowVersion"    , entity.Version     );
                        command.AddInputParameter<Int32>       ("IdentityId"    , entity.Identity.Id );
                        command.AddInputParameter<LicenseFlags>("Flags"         , entity.Flags       );
                        command.AddInputParameter<Instant>     ("DateCreated"   , entity.DateCreated );
                        command.AddInputParameter<Instant>     ("DateModified"  , entity.DateModified);
                        command.AddInputParameter<String>      ("Application"   , entity.Application );
                        command.AddInputParameter<String>      ("Name"          , entity.Name        );
                        command.AddInputParameter<String>      ("CurrencyCode"  , entity.Cost.IsoCode);
                        command.AddInputParameter<Decimal>     ("Amount"        , entity.Cost.Amount );
                        command.AddInputParameter<String>      ("Features"      , features           );
                        command.AddInputParameter<String>      ("SerializedData", data               );
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
        /// Retrieve all entities by the application name.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public EntityCollection<LicenseEntity> GetAllByApplication(
            String applicationName)
        {
            EntityCollection<LicenseEntity> entities = new();

            AdoAccess.CallProcedure(
                "License_GetAllByApplication",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String>("Application", applicationName);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            LicenseEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }
                
        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the entity by the subscription.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public LicenseEntity GetBySubscription(
            SubscriptionEntity subscription)
        {
            LicenseEntity entity = null;

            AdoAccess.CallProcedure(
                "License_GetBySubscriptionId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("SubscriptionId", subscription.Id);
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
        private LicenseEntity LoadEntity(
            IDataReader reader)
        {
            List<LicenseFeature> features = JsonSerializer.Deserialize<List<LicenseFeature>>(reader.GetValue<String>("Features"));

            Token data = JsonSerializer.Deserialize<Token>(reader.GetValue<String>("SerializedData"));

            PaymentAmount cost = new(reader.GetValue<String>("CurrencyCode"), reader.GetValue<Decimal>("Amount"));

            LicenseEntity entity = new(reader.GetBaseEntity())
                {
                    Flags       = reader.GetValue<LicenseFlags>("Flags")      ,
                    Application = reader.GetValue<String>      ("Application"),
                    Name        = reader.GetValue<String>      ("Name")       ,
                    Cost        = cost                                        ,
                    Features    = features                                    ,
                    Token       = data
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
            LicenseEntity entity)
        {
            entity.LazyIdentity = new Lazy<IdentityEntity>(() =>
                {
                    return this.IdentityRepository.GetByLicense(entity);
                });
        }
        #endregion
    }
}
