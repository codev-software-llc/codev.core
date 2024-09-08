//-----------------------------------------------------------------------------
// <copyright file="MerchantTransactionRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ITransactionRepository for fake transaction 
    /// processing to a fake store.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class MerchantTransactionRepository : BaseRepository, IMerchantTransactionRepository
    {
        #region Private Members
        ///--------------------------------------------------------------------
        /// <summary>
        /// This is used to lock access to critical sections of code.
        /// </summary>
        ///--------------------------------------------------------------------
        private readonly Object callLock = new Object();

        ///--------------------------------------------------------------------
        /// <summary>
        /// This is the unique number incremented on each transaction entry.
        /// </summary>
        ///--------------------------------------------------------------------
        private Int32 rowId;
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct our fake transaction repository.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantTransactionRepository(
            IDataSource dataSource) : base(dataSource)
        {
            this.InitializeTransactionTracking();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// This contains the dictionaries that we use to track transactions.
        /// </summary>
        ///--------------------------------------------------------------------
        public IQueryable<MerchantTransactionEntity> Transactions
        {
            get
            {
                return this.TransactionsStore.Values.AsQueryable<MerchantTransactionEntity>();
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This contains the dictionaries that we use to track transactions.
        /// </summary>
        ///--------------------------------------------------------------------
        private Dictionary<Int32, MerchantTransactionEntity> TransactionsStore { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a transaction entry.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            MerchantTransactionEntity entity)
        {
            ValidateRepository<MerchantTransactionEntity>.Add(entity);

            lock (this.callLock)
            {
                Interlocked.Increment(ref this.rowId);

                MerchantTransactionEntity transaction = new MerchantTransactionEntity(
                    new BaseEntity(this.rowId, new Byte[] {}, true, entity.DateCreated, entity.DateModified))
                        {
                            Flags            = entity.Flags           ,
                            TransactionGroup = entity.TransactionGroup,
                            TransactionType  = entity.TransactionType ,
                            Payment          = entity.Payment         ,
                            OrderNumber      = entity.OrderNumber     ,
                            Card             = entity.Card
                        };
                     
                this.TransactionsStore.Add(this.rowId, transaction);
            }
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

            lock (this.callLock)
            {
                MerchantTransactionEntity foundTransaction;

                if (this.TransactionsStore.TryGetValue(entity.Id, out foundTransaction))
                {
                    MerchantTransactionEntity remove = new MerchantTransactionEntity(
                        new BaseEntity(foundTransaction.Id, foundTransaction.Version, false, entity.DateCreated, entity.DateModified));

                    this.TransactionsStore.Remove(foundTransaction.Id);

                    this.TransactionsStore.Add(remove.Id, remove);
                }
                else
                {
                    //
                    // Throw not found.
                    //
                }
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete the transaction. 
        /// </summary>
        ///--------------------------------------------------------------------
        public void Purge(
            MerchantTransactionEntity entity)
        {
            ValidateRepository<MerchantTransactionEntity>.Purge(entity);

            lock (this.callLock)
            {
                this.TransactionsStore.Remove(entity.Id);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all transactions.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<MerchantTransactionEntity> GetAll()
        {
            ValidateRepository<MerchantTransactionEntity>.GetAll();

            throw new NotSupportedException();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Locate the transaction from the key information.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantTransactionEntity GetById(
            Int32   entityId)
        {
            ValidateRepository<MerchantTransactionEntity>.GetById(entityId);

            MerchantTransactionEntity transactionEntity = null;

            lock (this.callLock)
            {
                this.TransactionsStore.TryGetValue(entityId, out transactionEntity);
            }

            return transactionEntity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the transaction information.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            MerchantTransactionEntity entity)
        {
            ValidateRepository<MerchantTransactionEntity>.Update(entity);

            lock (this.callLock)
            {
                MerchantTransactionEntity foundTransaction;

                if (this.TransactionsStore.TryGetValue(entity.Id, out foundTransaction))
                {
                    this.TransactionsStore[foundTransaction.Id] = entity;
                }
                else
                {
                    //
                    // Throw not found.
                    //
                }
            }
        }
        #endregion

        #region Methods (Additional)
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Locate the transaction information by its transaction group.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<MerchantTransactionEntity> GetAllByGroup(
            Guid transactionGroupId)
        {
            List<MerchantTransactionEntity> transactions = this.TransactionsStore.Values.Where(c => c.TransactionGroup == transactionGroupId).ToList();

            EntityCollection<MerchantTransactionEntity> response = new EntityCollection<MerchantTransactionEntity>();

            foreach (MerchantTransactionEntity transaction in transactions)
            {
                response.Add(transaction);
            }

            return response;
        }

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Return all the transactions by the card.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<MerchantTransactionEntity> GetAllByCard(
            MerchantCardEntity card)
        {
            List<MerchantTransactionEntity> transactions = this.TransactionsStore.Values.Where(c => c.Card.Id == card.Id).ToList();

            EntityCollection<MerchantTransactionEntity> response = new EntityCollection<MerchantTransactionEntity>();

            foreach (MerchantTransactionEntity transaction in transactions)
            {
                response.Add(transaction);
            }

            return response;
        }
        #endregion

        #region Private Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the dictionaries that will be tracking transaction
        /// information.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeTransactionTracking()
        {
            this.TransactionsStore = new Dictionary<Int32, MerchantTransactionEntity>();
        }
        #endregion
    }
}
