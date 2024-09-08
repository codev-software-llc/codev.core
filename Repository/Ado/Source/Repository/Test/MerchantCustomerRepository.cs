//-----------------------------------------------------------------------------
// <copyright file="MerchantCustomerRepository.cs" company="Codev Software, LLC">
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
    /// This implements the subscription customer repository.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class MerchantCustomerRepository : BaseRepository, IMerchantCustomerRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public MerchantCustomerRepository(
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
            MerchantCustomerEntity entity)
        {
            ValidateRepository<MerchantCustomerEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "MerchantCustomer_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<MerchantCustomerFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>              ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>              ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>               ("Name"        , entity.Name        );
                        command.AddInputParameter<String>               ("EmailAddress", entity.EmailAddress);
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
            MerchantCustomerEntity entity)
        {
            ValidateRepository<MerchantCustomerEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "MerchantCustomer_Delete",
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
            MerchantCustomerEntity entity)
        {
            ValidateRepository<MerchantCustomerEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "MerchantCustomer_Purge",
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
        public EntityCollection<MerchantCustomerEntity> GetAll()
        {
            ValidateRepository<MerchantCustomerEntity>.GetAll();

            EntityCollection<MerchantCustomerEntity> entities = new EntityCollection<MerchantCustomerEntity>();

            AdoAccess.CallProcedure(
                "MerchantCustomer_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            MerchantCustomerEntity entity = this.LoadEntity(reader);

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
        public MerchantCustomerEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<MerchantCustomerEntity>.GetById(entityId);

            MerchantCustomerEntity entity = null;

            AdoAccess.CallProcedure(
                "MerchantCustomer_GetById",
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
            MerchantCustomerEntity entity)
        {
            ValidateRepository<MerchantCustomerEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "MerchantCustomer_Update",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>                ("RowId"       , entity.Id          );
                        command.AddInputParameter<Byte[]>               ("RowVersion"  , entity.Version     );
                        command.AddInputParameter<MerchantCustomerFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>              ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>              ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>               ("Name"        , entity.Name        );
                        command.AddInputParameter<String>               ("EmailAddress", entity.EmailAddress);
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
        /// Return the entity from the card.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantCustomerEntity GetByCard(
            MerchantCardEntity card)
        {
            MerchantCustomerEntity entity = null;

            AdoAccess.CallProcedure(
                "MerchantCustomer_GetByCardId",
                this.DataSource,
                (command) => 
                    {
                        command.AddInputParameter<Int32>("CardId", card.Id);
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
        /// Return the entity from the subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        public MerchantCustomerEntity GetBySubscription(
            MerchantSubscriptionEntity subscription)
        {
            MerchantCustomerEntity entity = null;

            AdoAccess.CallProcedure(
                "MerchantCustomer_GetBySubscriptionId",
                this.DataSource,
                ((command) => 
                    {
                        command.AddInputParameter<Int32>("SubscriptionId", subscription.Id);
                    }),
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
        private MerchantCustomerEntity LoadEntity(
            IDataReader reader)
        {
            MerchantCustomerEntity entity = new MerchantCustomerEntity(reader.GetBaseEntity())
                {
                    Flags        = reader.GetValue<MerchantCustomerFlags>("Flags"),
                    Name         = reader.GetValue<String>               ("Name") ,
                    EmailAddress = reader.GetValue<String>               ("EmailAddress")
                };

            return entity;
        }
        #endregion
    }
}
