//-----------------------------------------------------------------------------
// <copyright file="MerchantPlanRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
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
    /// This implements the subscription plans repository.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class MerchantPlanRepository : BaseRepository, IMerchantPlanRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public MerchantPlanRepository(
            ICoreDataSource dataSource) : base(dataSource)
        {
        }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            MerchantPlanEntity entity)
        {
            ValidateRepository<MerchantPlanEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "MerchantPlan_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<MerchantPlanFlags>   ("Flags"        , entity.Flags            );
                        command.AddInputParameter<Instant>             ("DateCreated"  , entity.DateCreated      );
                        command.AddInputParameter<Instant>             ("DateModified" , entity.DateModified     );
                        command.AddInputParameter<String>              ("Source"       , entity.Source           );
                        command.AddInputParameter<String>              ("Name"         , entity.Name             );
                        command.AddInputParameter<SubscriptionInterval>("Interval"     , entity.Interval         );
                        command.AddInputParameter<Int32>               ("IntervalCount", entity.IntervalCount    );
                        command.AddInputParameter<String>              ("CurrencyCode" , entity.Cost.IsoCode);
                        command.AddInputParameter<Decimal>             ("Amount"       , entity.Cost.Amount      );
                        command.AddInputParameter<Int32>               ("TrialDays"    , entity.TrialDays        );
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
            MerchantPlanEntity entity)
        {
            ValidateRepository<MerchantPlanEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "MerchantPlan_Delete",
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
            MerchantPlanEntity entity)
        {
            ValidateRepository<MerchantPlanEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "MerchantPlan_Purge",
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
        public EntityCollection<MerchantPlanEntity> GetAll()
        {
            ValidateRepository<MerchantPlanEntity>.GetAll();

            EntityCollection<MerchantPlanEntity> entities = new EntityCollection<MerchantPlanEntity>();

            AdoAccess.CallProcedure(
                "MerchantPlan_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            MerchantPlanEntity entity = this.LoadEntity(reader);

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
        public MerchantPlanEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<MerchantPlanEntity>.GetById(entityId);

            MerchantPlanEntity entity = null;

            AdoAccess.CallProcedure(
                "MerchantPlan_GetById",
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
            MerchantPlanEntity entity)
        {
            ValidateRepository<MerchantPlanEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "MerchantPlan_Update",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>               ("RowId"        , entity.Id               );
                        command.AddInputParameter<Byte[]>              ("RowVersion"   , entity.Version          );
                        command.AddInputParameter<MerchantPlanFlags>   ("Flags"        , entity.Flags            );
                        command.AddInputParameter<Instant>             ("DateCreated"  , entity.DateCreated      );
                        command.AddInputParameter<Instant>             ("DateModified" , entity.DateModified     );
                        command.AddInputParameter<String>              ("Source"       , entity.Source           );
                        command.AddInputParameter<String>              ("Name"         , entity.Name             );
                        command.AddInputParameter<SubscriptionInterval>("Interval"     , entity.Interval         );
                        command.AddInputParameter<Int32>               ("IntervalCount", entity.IntervalCount    );
                        command.AddInputParameter<String>              ("CurrencyCode" , entity.Cost.IsoCode);
                        command.AddInputParameter<Decimal>             ("Amount"       , entity.Cost.Amount      );
                        command.AddInputParameter<Int32>               ("TrialDays"    , entity.TrialDays        );
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
        /// Return the entity from the subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantPlanEntity GetBySubscription(
            MerchantSubscriptionEntity subscription)
        {
            MerchantPlanEntity entity = null;

            AdoAccess.CallProcedure(
                "MerchantPlan_GetBySubscriptionId",
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
        private MerchantPlanEntity LoadEntity(
            IDataReader reader)
        {
            PaymentAmount cost = new PaymentAmount(reader.GetValue<String>("CurrencyCode"), reader.GetValue<Decimal>("Amount"));

            MerchantPlanEntity entity = new MerchantPlanEntity(reader.GetBaseEntity())
                {
                    Flags         = reader.GetValue<MerchantPlanFlags>   ("Flags")        ,
                    Source        = reader.GetValue<String>              ("source")       ,
                    Name          = reader.GetValue<String>              ("Name")         ,
                    Interval      = reader.GetValue<SubscriptionInterval>("Interval")     ,
                    IntervalCount = reader.GetValue<Int32>               ("IntervalCount"),
                    TrialDays     = reader.GetValue<Int32>               ("TrialDays")    ,
                    Cost          = cost
                };

            return entity;
        }
        #endregion
    }
}
