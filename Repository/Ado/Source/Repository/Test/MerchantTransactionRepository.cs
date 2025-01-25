//-----------------------------------------------------------------------------
// <copyright file="MerchantTransactionRepository.cs" company="Codev Software, LLC">
// Copyright © 2025
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
    /// This implements the IMerchantTransactionRepository for transaction 
    /// storing to a SQL store.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class MerchantTransactionRepository : BaseRepository, IMerchantTransactionRepository
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantTransactionRepository(
            ICoreDataSource         dataSource,
            IMerchantCardRepository cardRepository) : base(dataSource)
        {
            this.MerchantCardRepository = cardRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the card repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantCardRepository MerchantCardRepository { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add new entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            MerchantTransactionEntity entity)
        {
            ValidateRepository<MerchantTransactionEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "MerchantTransaction_Add",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<MerchantTransactionFlags>("Flags"           , entity.Flags               );
                        command.AddInputParameter<Instant>                 ("DateCreated"     , entity.DateCreated         );
                        command.AddInputParameter<Instant>                 ("DateModified"    , entity.DateModified        );
                        command.AddInputParameter<Guid>                    ("TransactionGroup", entity.TransactionGroup    );
                        command.AddInputParameter<TransactionType>         ("TransactionType" , entity.TransactionType     );
                        command.AddInputParameter<String>                  ("CurrencyCode"    , entity.Payment.IsoCode);
                        command.AddInputParameter<Decimal>                 ("Amount"          , entity.Payment.Amount      );
                        command.AddInputParameter<String>                  ("OrderNumber"     , entity.OrderNumber         );
                        command.AddInputParameter<Int32>                   ("CardId"          , entity.Card.Id             );
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
            MerchantTransactionEntity entity)
        {
            ValidateRepository<MerchantTransactionEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "MerchantTransaction_Delete",
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
            MerchantTransactionEntity entity)
        {
            ValidateRepository<MerchantTransactionEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "MerchantTransaction_Purge",
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
        public EntityCollection<MerchantTransactionEntity> GetAll()
        {
            ValidateRepository<MerchantTransactionEntity>.GetAll();

            EntityCollection<MerchantTransactionEntity> entities = new EntityCollection<MerchantTransactionEntity>();

            AdoAccess.CallProcedure(
                "MerchantTransaction_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            MerchantTransactionEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Locate the entity by its unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantTransactionEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<MerchantTransactionEntity>.GetById(entityId);

            MerchantTransactionEntity entity = null;

            AdoAccess.CallProcedure(
                "MerchantTransaction_GetById",
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
            MerchantTransactionEntity entity)
        {
            ValidateRepository<MerchantTransactionEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "MerchantTransaction_Update",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>                   ("RowId"           , entity.Id                  );
                        command.AddInputParameter<Byte[]>                  ("RowVersion"      , entity.Version             );
                        command.AddInputParameter<MerchantTransactionFlags>("Flags"           , entity.Flags               );
                        command.AddInputParameter<Instant>                 ("DateCreated"     , entity.DateCreated         );
                        command.AddInputParameter<Instant>                 ("DateModified"    , entity.DateModified        );
                        command.AddInputParameter<Guid>                    ("TransactionGroup", entity.TransactionGroup    );
                        command.AddInputParameter<TransactionType>         ("TransactionType" , entity.TransactionType     );
                        command.AddInputParameter<String>                  ("CurrencyCode"    , entity.Payment.IsoCode);
                        command.AddInputParameter<Decimal>                 ("Amount"          , entity.Payment.Amount      );
                        command.AddInputParameter<String>                  ("OrderNumber"     , entity.OrderNumber         );
                        command.AddInputParameter<Int32>                   ("CardId"          , entity.Card.Id             );                    },
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
        /// Locate the entities by a grouping.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<MerchantTransactionEntity> GetAllByGroup(
            Guid groupId)
        {
            EntityCollection<MerchantTransactionEntity> transactions = new EntityCollection<MerchantTransactionEntity>();

            AdoAccess.CallProcedure(
                "MerchantTransaction_GetAllByGroupId",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Guid>("TransactionGroup", groupId);
                    },
                (command, reader) => 
                    {
                        while (reader.Read()) 
                        {
                            MerchantTransactionEntity transactionEntity = this.LoadEntity(reader);

                            transactions.Add(transactionEntity);
                        }
                    });

            return transactions;
        }

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Locate the entities by a grouping.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<MerchantTransactionEntity> GetAllByCard(
            MerchantCardEntity card)
        {
            EntityCollection<MerchantTransactionEntity> transactions = new EntityCollection<MerchantTransactionEntity>();

            AdoAccess.CallProcedure(
                "MerchantTransaction_GetAllByCardId",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>("CardId", card.Id);
                    },
                (command, reader) => 
                    {
                        while (reader.Read()) 
                        {
                            MerchantTransactionEntity transactionEntity = this.LoadEntity(reader);

                            transactions.Add(transactionEntity);
                        }
                    });

            return transactions;
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the merchant transaction.
        /// </summary>
        ///--------------------------------------------------------------------
        private MerchantTransactionEntity LoadEntity(
            IDataReader reader)
        {
            PaymentAmount payment = new PaymentAmount(reader.GetValue<String>("CurrencyCode"), reader.GetValue<Decimal>("Amount"));

            MerchantTransactionEntity entity = new MerchantTransactionEntity(
                reader.GetBaseEntity())
                    {
                        Flags            = reader.GetValue<MerchantTransactionFlags>("Flags")           ,
                        TransactionGroup = reader.GetValue<Guid>                    ("TransactionGroup"),
                        TransactionType  = reader.GetValue<TransactionType>         ("TransactionType") ,
                        OrderNumber      = reader.GetValue<String>                  ("OrderNumber")     ,
                        Payment          = payment
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
            MerchantTransactionEntity entity)
        {
            entity.LazyCard = new Lazy<MerchantCardEntity>(() =>
                {
                    return this.MerchantCardRepository.GetByTransaction(entity);
                });
        }
        #endregion
    }
}
