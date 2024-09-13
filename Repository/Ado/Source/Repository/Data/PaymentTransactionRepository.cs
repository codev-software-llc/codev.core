//-----------------------------------------------------------------------------
// <copyright file="PaymentTransactionRepository.cs" company="Codev Software, LLC">
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
    /// This implements the repository for payment transactions.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PaymentTransactionRepository : BaseRepository, IPaymentTransactionRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public PaymentTransactionRepository(
            ICoreDataSource    dataSource,
            IPaymentRepository paymentRepository) : base(dataSource)
        {
            this.PaymentRepository = paymentRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IPaymentRepository PaymentRepository { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            PaymentTransactionEntity entity)
        {
            ValidateRepository<PaymentTransactionEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "PaymentTransaction_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<PaymentTransactionFlags>("Flags"          , entity.Flags                );
                        command.AddInputParameter<Instant>                ("DateCreated"    , entity.DateCreated          );
                        command.AddInputParameter<Instant>                ("DateModified"   , entity.DateModified         );
                        command.AddInputParameter<TransactionType>        ("TransactionType", entity.TransactionType      );
                        command.AddInputParameter<String>                 ("CurrencyCode"   , entity.PaymentAmount.IsoCode);
                        command.AddInputParameter<Decimal>                ("Amount"         , entity.PaymentAmount.Amount );
                        command.AddInputParameter<Int32>                  ("PaymentId"      , entity.Payment.Id           );
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
            PaymentTransactionEntity entity)
        {
            ValidateRepository<PaymentTransactionEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "PaymentTransaction_Delete",
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
            PaymentTransactionEntity entity)
        {
            ValidateRepository<PaymentTransactionEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "PaymentTransaction_Purge",
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
        public EntityCollection<PaymentTransactionEntity> GetAll()
        {
            ValidateRepository<PaymentTransactionEntity>.GetAll();

            EntityCollection<PaymentTransactionEntity> entities = new EntityCollection<PaymentTransactionEntity>();

            AdoAccess.CallProcedure(
                "PaymentTransaction_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            PaymentTransactionEntity entity = this.LoadEntity(reader);

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
        public PaymentTransactionEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<PaymentTransactionEntity>.GetById(entityId);

            PaymentTransactionEntity entity = null;

            AdoAccess.CallProcedure(
                "PaymentTransaction_GetById",
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
            PaymentTransactionEntity entity)
        {
            ValidateRepository<PaymentTransactionEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "PaymentTransaction_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>                  ("RowId"          , entity.Id                   );
                        command.AddInputParameter<Byte[]>                 ("RowVersion"     , entity.Version              );
                        command.AddInputParameter<PaymentTransactionFlags>("Flags"          , entity.Flags                );
                        command.AddInputParameter<Instant>                ("DateCreated"    , entity.DateCreated          );
                        command.AddInputParameter<Instant>                ("DateModified"   , entity.DateModified         );
                        command.AddInputParameter<TransactionType>        ("TransactionType", entity.TransactionType      );
                        command.AddInputParameter<String>                 ("CurrencyCode"   , entity.PaymentAmount.IsoCode);
                        command.AddInputParameter<Decimal>                ("Amount"         , entity.PaymentAmount.Amount );
                        command.AddInputParameter<Int32>                  ("PaymentId"      , entity.Payment.Id           );
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

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private PaymentTransactionEntity LoadEntity(
            IDataReader reader)
        {
            String  currencyCode = reader.GetValue<String>("CurrencyCode");
            Decimal amount       = reader.GetValue<Decimal>("Amount");

            PaymentTransactionEntity entity = new PaymentTransactionEntity(reader.GetBaseEntity())
                {
                    Flags           = reader.GetValue<PaymentTransactionFlags>("Flags")          ,
                    TransactionType = reader.GetValue<TransactionType>        ("TransactionType"),
                    PaymentAmount   = new PaymentAmount(currencyCode, amount)
                };

            return entity;
        }
        #endregion
    }
}
