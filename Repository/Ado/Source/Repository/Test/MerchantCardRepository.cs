//-----------------------------------------------------------------------------
// <copyright file="MerchantCardRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado.Test
{
    using System;
    using System.Data;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IMerchantTokenRepository for tokenization to 
    /// SQL Store.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class MerchantCardRepository : BaseRepository, IMerchantCardRepository
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantCardRepository(
            ICoreDataSource             dataSource,
            IMerchantCustomerRepository customerRepository) : base(dataSource)
        {
            this.MerchantCustomerRepository = customerRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the customer repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantCustomerRepository MerchantCustomerRepository { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            MerchantCardEntity entity)
        {
            ValidateRepository<MerchantCardEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "MerchantCard_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<MerchantCardFlags>("Flags"          , entity.Flags               );
                        command.AddInputParameter<Instant>          ("DateCreated"    , entity.DateCreated         );
                        command.AddInputParameter<Instant>          ("DateModified"   , entity.DateModified        );
                        command.AddInputParameter<String>           ("Name"           , entity.Name                );
                        command.AddInputParameter<String>           ("Type"           , entity.Type                );
                        command.AddInputParameter<String>           ("Number"         , entity.Number              );
                        command.AddInputParameter<String>           ("Code"           , entity.Code                );
                        command.AddInputParameter<Int32>            ("ExpirationYear" , entity.DateExpiration.Year );
                        command.AddInputParameter<Int32>            ("ExpirationMonth", entity.DateExpiration.Month);
                        command.AddInputParameter<Int32>            ("CustomerId"     , entity.Customer.Id         );
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
            MerchantCardEntity entity)
        {
            ValidateRepository<MerchantCardEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "MerchantCard_Delete",
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
        /// Delete the entity permanently
        /// </summary>
        ///--------------------------------------------------------------------
        public void Purge(
            MerchantCardEntity entity)
        {
            ValidateRepository<MerchantCardEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "MerchantCard_Purge",
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
        public EntityCollection<MerchantCardEntity> GetAll()
        {
            ValidateRepository<MerchantCardEntity>.GetAll();

            EntityCollection<MerchantCardEntity> entities = new EntityCollection<MerchantCardEntity>();

            AdoAccess.CallProcedure(
                "MerchantCard_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            MerchantCardEntity entity = this.LoadEntity(reader);

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
        public MerchantCardEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<MerchantCardEntity>.GetById(entityId);

            MerchantCardEntity entity = null;

            AdoAccess.CallProcedure(
                "MerchantCard_GetById",
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
        /// Update the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            MerchantCardEntity entity)
        {
            ValidateRepository<MerchantCardEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "MerchantCard_Update",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>            ("RowId"          , entity.Id                  );
                        command.AddInputParameter<Byte[]>           ("RowVersion"     , entity.Version             );
                        command.AddInputParameter<MerchantCardFlags>("Flags"          , entity.Flags               );
                        command.AddInputParameter<Instant>          ("DateCreated"    , entity.DateCreated         );
                        command.AddInputParameter<Instant>          ("DateModified"   , entity.DateModified        );
                        command.AddInputParameter<String>           ("Name"           , entity.Name                );
                        command.AddInputParameter<String>           ("Type"           , entity.Type                );
                        command.AddInputParameter<String>           ("Number"         , entity.Number              );
                        command.AddInputParameter<String>           ("Code"           , entity.Code                );
                        command.AddInputParameter<Int32>            ("ExpirationYear" , entity.DateExpiration.Year );
                        command.AddInputParameter<Int32>            ("ExpirationMonth", entity.DateExpiration.Month);
                        command.AddInputParameter<Int32>            ("CustomerId"     , entity.Customer.Id         );
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
        /// Return the card by the transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantCardEntity GetByTransaction(
            MerchantTransactionEntity transaction)
        {
            MerchantCardEntity entity = null;

            AdoAccess.CallProcedure(
                "MerchantCard_GetByTransactionId",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>("TransactionId", transaction.Id);
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
        /// Return all cards for the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<MerchantCardEntity> GetAllByCustomer(
            MerchantCustomerEntity customer)
        {
            EntityCollection<MerchantCardEntity> entities = new EntityCollection<MerchantCardEntity>();

            AdoAccess.CallProcedure(
                "MerchantCard_GetAllByCustomerId",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>("CustomerId", customer.Id);
                    },
                (command, reader) => 
                    {
                        while (reader.Read()) 
                        {
                            MerchantCardEntity entity = this.LoadEntity(reader);
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
        private MerchantCardEntity LoadEntity(
            IDataReader reader)
        {
            Int32 expirationYear  = reader.GetValue<Int32>("ExpirationYear" );
            Int32 expirationMonth = reader.GetValue<Int32>("ExpirationMonth");

            LocalDateTime dateTimeExpiration = new LocalDate(expirationYear, expirationMonth, 1).ToEndOfMonth();

            MerchantCardEntity entity = new MerchantCardEntity(reader.GetBaseEntity())
                {
                    Flags          = reader.GetValue<MerchantCardFlags>("Flags") ,
                    Name           = reader.GetValue<String>           ("Name")  ,
                    Type           = reader.GetValue<String>           ("Type")  ,
                    Number         = reader.GetValue<String>           ("Number"),
                    Code           = reader.GetValue<String>           ("Code")  ,
                    DateExpiration = dateTimeExpiration.Date
                };

            this.InitializeLazyLoading(entity);

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the lazy loading of the User entity properties.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeLazyLoading(
            MerchantCardEntity entity)
        {
            entity.LazyCustomer = new Lazy<MerchantCustomerEntity>(() =>
                {
                    return this.MerchantCustomerRepository.GetByCard(entity);
                });
        }
        #endregion
    }
}
