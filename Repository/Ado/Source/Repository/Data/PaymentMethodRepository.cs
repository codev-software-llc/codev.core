//-----------------------------------------------------------------------------
// <copyright file="PaymentMethodRepository.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Data;
    using System.Text.Json;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for payment methods.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PaymentMethodRepository : BaseRepository, IPaymentMethodRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public PaymentMethodRepository(
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
            PaymentMethodEntity entity)
        {
            ValidateRepository<PaymentMethodEntity>.Add(entity);

            String data = JsonSerializer.Serialize(entity.Token);

            AdoAccess.CallProcedure(
                "PaymentMethod_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<PaymentMethodFlags>("Flags"           , entity.Flags           );
                        command.AddInputParameter<Instant>           ("DateCreated"     , entity.DateCreated     );
                        command.AddInputParameter<Instant>           ("DateModified"    , entity.DateModified    );
                        command.AddInputParameter<String>            ("Expiration"      , entity.Expiration      );
                        command.AddInputParameter<String>            ("OffuscatedNumber", entity.OffuscatedNumber);
                        command.AddInputParameter<Int32>             ("IdentityId"      , entity.Identity.Id     );
                        command.AddInputParameter<String>            ("SerializedData"  , data                   );
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
            PaymentMethodEntity entity)
        {
            ValidateRepository<PaymentMethodEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "PaymentMethod_Delete",
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
            PaymentMethodEntity entity)
        {
            ValidateRepository<PaymentMethodEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "PaymentMethod_Purge",
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
        public EntityCollection<PaymentMethodEntity> GetAll()
        {
            ValidateRepository<PaymentMethodEntity>.GetAll();

            EntityCollection<PaymentMethodEntity> entities = new();

            AdoAccess.CallProcedure(
                "PaymentMethod_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            PaymentMethodEntity entity = this.LoadEntity(reader);

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
        public PaymentMethodEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<PaymentMethodEntity>.GetById(entityId);

            PaymentMethodEntity entity = null;

            AdoAccess.CallProcedure(
                "PaymentMethod_GetById",
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
            PaymentMethodEntity entity)
        {
            ValidateRepository<PaymentMethodEntity>.Update(entity);

            String data = JsonSerializer.Serialize(entity.Token);

            AdoAccess.CallProcedure(
                "PaymentMethod_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>             ("RowId"           , entity.Id              );
                        command.AddInputParameter<Byte[]>            ("RowVersion"      , entity.Version         );
                        command.AddInputParameter<PaymentMethodFlags>("Flags"           , entity.Flags           );
                        command.AddInputParameter<Instant>           ("DateCreated"     , entity.DateCreated     );
                        command.AddInputParameter<Instant>           ("DateModified"    , entity.DateModified    );
                        command.AddInputParameter<String>            ("Expiration"      , entity.Expiration      );
                        command.AddInputParameter<String>            ("OffuscatedNumber", entity.OffuscatedNumber);
                        command.AddInputParameter<Int32>             ("IdentityId"      , entity.Identity.Id     );
                        command.AddInputParameter<String>            ("SerializedData"  , data                   );
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
        /// Retrieve all the identity payment methods.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public EntityCollection<PaymentMethodEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<PaymentMethodEntity> entities = new();

            AdoAccess.CallProcedure(
                "PaymentMethod_GetAllByIdentityId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("IdentityId", identity.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            PaymentMethodEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return payment method for the payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethodEntity GetByPayment(
            PaymentEntity payment)
        {
            PaymentMethodEntity entity = null;

            AdoAccess.CallProcedure(
                "PaymentMethod_GetByPaymentId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("PaymentId", payment.Id);
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
        private PaymentMethodEntity LoadEntity(
            IDataReader reader)
        {
            Token data = JsonSerializer.Deserialize<Token>(reader.GetValue<String>("SerializedData"));

            PaymentMethodEntity entity = new(reader.GetBaseEntity())
                {
                    Flags            = reader.GetValue<PaymentMethodFlags>("Flags")           ,
                    Expiration       = reader.GetValue<String>            ("Expiration")      ,
                    OffuscatedNumber = reader.GetValue<String>            ("OffuscatedNumber"),
                    Token            = data
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
            PaymentMethodEntity entity)
        {
            entity.LazyIdentity = new Lazy<IdentityEntity>(() =>
                {
                    return this.IdentityRepository.GetByPaymentMethod(entity);
                });
        }
        #endregion
    }
}
