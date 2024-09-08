//-----------------------------------------------------------------------------
// <copyright file="PaymentMethodRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
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
    /// This implements the repository for task persistence.
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
            PaymentMethodEntity entity)
        {
            ValidateRepository<PaymentMethodEntity>.Add(entity);

            String data = JsonSerializer.Serialize(entity.Token);

            SqliteAccess.CallStatement(
                "INSERT INTO [PaymentMethods] ([IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Expiration], [OffuscatedNumber], [SerializedData]) VALUES (1, ?, ?, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {                    
                        command.AddInputParameter<PaymentMethodFlags>("Flags"           , entity.Flags           );
                        command.AddInputParameter<Instant>           ("DateCreated"     , entity.DateCreated     );
                        command.AddInputParameter<Instant>           ("DateModified"    , entity.DateModified    );
                        command.AddInputParameter<Int32>             ("IdentityId"      , entity.Identity.Id     );
                        command.AddInputParameter<String>            ("Expiration"      , entity.Expiration      );
                        command.AddInputParameter<String>            ("OffuscatedNumber", entity.OffuscatedNumber);
                        command.AddInputParameter<String>            ("SerializedData"  , data                   );
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
            PaymentMethodEntity entity)
        {
            ValidateRepository<PaymentMethodEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [PaymentMethods] SET [IsActive] = 0 WHERE [RowId] = ?",
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
            PaymentMethodEntity entity)
        {
            ValidateRepository<PaymentMethodEntity>.Purge(entity);

            SqliteAccess.CallStatement(
                "DELETE FROM [PaymentMethods] WHERE [RowId] = ?",
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

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Expiration], [OffuscatedNumber], [SerializedData] FROM [PaymentMethods]",
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
        /// Return the entity by its unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethodEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<PaymentMethodEntity>.GetById(entityId);

            PaymentMethodEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Expiration], [OffuscatedNumber], [SerializedData] FROM [PaymentMethods] WHERE [RowId] = ?",
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
            PaymentMethodEntity entity)
        {
            ValidateRepository<PaymentMethodEntity>.Update(entity);

            String data = JsonSerializer.Serialize(entity.Token);

            SqliteAccess.CallStatement(
                "UPDATE [PaymentMethods] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [IdentityId] = ?, [Expiration] = ?, [OffuscatedNumber] = ?, [SerializedData] = ? WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<PaymentMethodFlags>("Flags"           , entity.Flags           );
                        command.AddInputParameter<Instant>           ("DateCreated"     , entity.DateCreated     );
                        command.AddInputParameter<Instant>           ("DateModified"    , entity.DateModified    );
                        command.AddInputParameter<Int32>             ("IdentityId"      , entity.Identity.Id     );
                        command.AddInputParameter<String>            ("Expiration"      , entity.Expiration      );
                        command.AddInputParameter<String>            ("OffuscatedNumber", entity.OffuscatedNumber);
                        command.AddInputParameter<String>            ("SerializedData"  , data                   );
                        command.AddInputParameter<Int32>             ("RowId"           , entity.Id              );
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
        /// Return entities by the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<PaymentMethodEntity> GetAllByIdentity(
            IdentityEntity identity)
        {
            EntityCollection<PaymentMethodEntity> entities = new();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Expiration], [OffuscatedNumber], [SerializedData] WHERE [IdentityId] = ?",
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
        /// Return payment method from the payment entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentMethodEntity GetByPayment(
            PaymentEntity payment)
        {
            PaymentMethodEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Expiration], [OffuscatedNumber], [SerializedData] WHERE [PaymentId] = ?",
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

            PaymentMethodEntity entity = new(
                reader.GetBaseEntity())
                    {
                        Expiration        = reader.GetValue<String> ("Expiration")      ,
                        OffuscatedNumber  = reader.GetValue<String> ("OffuscatedNumber"),
                        Token             = data
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
