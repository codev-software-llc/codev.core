//-----------------------------------------------------------------------------
// <copyright file="MerchantSubscriptionRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado.Test
{
    using System;
    using System.Data;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the subscription repository.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class MerchantSubscriptionRepository : BaseRepository, IMerchantSubscriptionRepository
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantSubscriptionRepository(
            ICoreDataSource             dataSource,
            IMerchantCustomerRepository customerRepository,
            IMerchantPlanRepository     planRepository) : base(dataSource)
        {
            this.MerchantCustomerRepository = customerRepository;
            this.MerchantPlanRepository     = planRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the customer repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantCustomerRepository MerchantCustomerRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the plan repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantPlanRepository MerchantPlanRepository { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            MerchantSubscriptionEntity entity)
        {
            ValidateRepository<MerchantSubscriptionEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "MerchantSubscription_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<MerchantSubscriptionFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>                  ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>                  ("DateModified", entity.DateModified);
                        command.AddInputParameter<Int32>                    ("PlanId"      , entity.Plan.Id     );
                        command.AddInputParameter<Int32>                    ("CustomerId"  , entity.Customer.Id );
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
            MerchantSubscriptionEntity entity)
        {
            ValidateRepository<MerchantSubscriptionEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "MerchantSubscription_Delete",
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
            MerchantSubscriptionEntity entity)
        {
            ValidateRepository<MerchantSubscriptionEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "MerchantSubscription_Purge",
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
        public EntityCollection<MerchantSubscriptionEntity> GetAll()
        {
            ValidateRepository<MerchantSubscriptionEntity>.GetAll();

            EntityCollection<MerchantSubscriptionEntity> entities = new EntityCollection<MerchantSubscriptionEntity>();

            AdoAccess.CallProcedure(
                "MerchantSubscription_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            MerchantSubscriptionEntity entity = this.LoadEntity(reader);

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
        public MerchantSubscriptionEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<MerchantSubscriptionEntity>.GetById(entityId);

            MerchantSubscriptionEntity entity = null;

            AdoAccess.CallProcedure(
                "MerchantSubscription_GetById",
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
            MerchantSubscriptionEntity entity)
        {
            ValidateRepository<MerchantSubscriptionEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "MerchantSubscription_Update",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>                    ("RowId"       , entity.Id          );
                        command.AddInputParameter<Byte[]>                   ("RowVersion"  , entity.Version     );
                        command.AddInputParameter<MerchantSubscriptionFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>                  ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>                  ("DateModified", entity.DateModified);
                        command.AddInputParameter<Int32>                    ("PlanId"      , entity.Plan.Id     );
                        command.AddInputParameter<Int32>                    ("CustomerId"  , entity.Customer.Id );
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
        /// Return all the subscriptions for the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<MerchantSubscriptionEntity> GetAllByPlan(
            MerchantPlanEntity plan)
        {
            EntityCollection<MerchantSubscriptionEntity> entities = new EntityCollection<MerchantSubscriptionEntity>();

            AdoAccess.CallProcedure(
                "MerchantSubscription_GetAllByPlanId",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>("PlanId", plan.Id);
                    },
                (command, reader) => 
                    {
                        while (reader.Read()) 
                        {
                            MerchantSubscriptionEntity entity = this.LoadEntity(reader);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Return all subscriptions for the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<MerchantSubscriptionEntity> GetAllByCustomer(
            MerchantCustomerEntity customer)
        {
            EntityCollection<MerchantSubscriptionEntity> entities = new EntityCollection<MerchantSubscriptionEntity>();

            AdoAccess.CallProcedure(
                "MerchantSubscription_GetAllByCustomerId",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>("CustomerId", customer.Id);
                    },
                (command, reader) => 
                    {
                        while (reader.Read()) 
                        {
                            MerchantSubscriptionEntity entity = this.LoadEntity(reader);
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
        private MerchantSubscriptionEntity LoadEntity(
            IDataReader reader)
        {
            MerchantSubscriptionEntity entity = new MerchantSubscriptionEntity(reader.GetBaseEntity())
                {
                    Flags = reader.GetValue<MerchantSubscriptionFlags>("Flags")
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
            MerchantSubscriptionEntity entity)
        {
            entity.LazyPlan = new Lazy<MerchantPlanEntity>(() =>
                {
                    return this.MerchantPlanRepository.GetBySubscription(entity);
                });

            entity.LazyCustomer = new Lazy<MerchantCustomerEntity>(() =>
                {
                    return this.MerchantCustomerRepository.GetBySubscription(entity);
                });
        }
        #endregion
    }
}
