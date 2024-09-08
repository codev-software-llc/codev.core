//-----------------------------------------------------------------------------
// <copyright file="PaymentRepository.cs" company="Codev Software, LLC">
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
    /// This implements the repository for payments.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public PaymentRepository(
            ICoreDataSource          dataSource,
            IPaymentMethodRepository paymentMethodRepository) : base(dataSource)
        {
            this.PaymentMethodRepository = paymentMethodRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the payment method repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IPaymentMethodRepository PaymentMethodRepository { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            PaymentEntity entity)
        {
            ValidateRepository<PaymentEntity>.Add(entity);

            String data = JsonSerializer.Serialize(entity.Token);

            AdoAccess.CallProcedure(
                "Payment_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<PaymentFlags> ("Flags"          , entity.Flags                );
                        command.AddInputParameter<Instant>      ("DateCreated"    , entity.DateCreated          );
                        command.AddInputParameter<Instant>      ("DateModified"   , entity.DateModified         );
                        command.AddInputParameter<String>       ("OrderNumber"    , entity.OrderNumber          );
                        command.AddInputParameter<PaymentStatus>("PaymentStatus"  , entity.PaymentStatus        );
                        command.AddInputParameter<String>       ("CurrencyCode"   , entity.PaymentAmount.IsoCode);
                        command.AddInputParameter<Decimal>      ("Amount"         , entity.PaymentAmount.Amount );
                        command.AddInputParameter<Int32>        ("PaymentMethodId", entity.PaymentMethod.Id     );
                        command.AddInputParameter<String>       ("SerializedData" , data                        );
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
            PaymentEntity entity)
        {
            ValidateRepository<PaymentEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "Payment_Delete",
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
            PaymentEntity entity)
        {
            ValidateRepository<PaymentEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "Payment_Purge",
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
        public EntityCollection<PaymentEntity> GetAll()
        {
            ValidateRepository<PaymentEntity>.GetAll();

            EntityCollection<PaymentEntity> entities = new EntityCollection<PaymentEntity>();

            AdoAccess.CallProcedure(
                "Payment_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            PaymentEntity entity = this.LoadEntity(reader);

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
        public PaymentEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<PaymentEntity>.GetById(entityId);

            PaymentEntity entity = null;

            AdoAccess.CallProcedure(
                "Payment_GetById",
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
            PaymentEntity entity)
        {
            ValidateRepository<PaymentEntity>.Update(entity);

            String data = JsonSerializer.Serialize(entity.Token);

            AdoAccess.CallProcedure(
                "Payment_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>        ("RowId"          , entity.Id                   );
                        command.AddInputParameter<Byte[]>       ("RowVersion"     , entity.Version              );
                        command.AddInputParameter<PaymentFlags> ("Flags"          , entity.Flags                );
                        command.AddInputParameter<Instant>      ("DateCreated"    , entity.DateCreated          );
                        command.AddInputParameter<Instant>      ("DateModified"   , entity.DateModified         );
                        command.AddInputParameter<String>       ("OrderNumber"    , entity.OrderNumber          );
                        command.AddInputParameter<PaymentStatus>("PaymentStatus"  , entity.PaymentStatus        );
                        command.AddInputParameter<String>       ("CurrencyCode"   , entity.PaymentAmount.IsoCode);
                        command.AddInputParameter<Decimal>      ("Amount"         , entity.PaymentAmount.Amount );
                        command.AddInputParameter<Int32>        ("PaymentMethodId", entity.PaymentMethod.Id     );
                        command.AddInputParameter<String>       ("SerializedData" , data                        );
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
        /// Return payment method for the payment.
        /// </summary>
        ///--------------------------------------------------------------------
        public PaymentEntity GetByPaymentTransaction(
            PaymentTransactionEntity paymentTransaction)
        {
            PaymentEntity entity = null;

            AdoAccess.CallProcedure(
                "Payment_GetByPaymentTransactionId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("PaymentTransactionId", paymentTransaction.Id);
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
        private PaymentEntity LoadEntity(
            IDataReader reader)
        {
            Token data = JsonSerializer.Deserialize<Token>(reader.GetValue<String>("SerializedData"));

            String  currencyCode = reader.GetValue<String>("CurrencyCode");
            Decimal amount       = reader.GetValue<Decimal>("Amount");

            PaymentEntity entity = new(reader.GetBaseEntity())
                {
                    Flags         = reader.GetValue<PaymentFlags> ("Flags")        ,
                    OrderNumber   = reader.GetValue<String>       ("OrderNumber")  ,
                    PaymentStatus = reader.GetValue<PaymentStatus>("PaymentStatus"),
                    PaymentAmount = new PaymentAmount(currencyCode, amount),
                    Token         = data
                };

            this.InitializeLazyLoading(entity);

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the lazy loading of the entity references.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeLazyLoading(
            PaymentEntity entity)
        {
            entity.LazyPaymentMethod = new Lazy<PaymentMethodEntity>(() =>
                {
                    return this.PaymentMethodRepository.GetByPayment(entity);
                });
        }
        #endregion
    }
}
