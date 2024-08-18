//-----------------------------------------------------------------------------
// <copyright file="IdentityRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Ado
{
    using System;
    using System.Data;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for identitites.
    /// </summary>
    ///------------------------------------------------------------------------
    public class IdentityRepository : BaseRepository, IIdentityRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public IdentityRepository(
            ICoreDataSource dataSource) : base(dataSource)
        {
        }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new entity to the store.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            IdentityEntity entity)
        {
            ValidateRepository<IdentityEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "Identity_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<IdentityFlags>("Flags"               , entity.Flags               );
                        command.AddInputParameter<Instant>      ("DateCreated"         , entity.DateCreated         );
                        command.AddInputParameter<Instant>      ("DateModified"        , entity.DateModified        );
                        command.AddInputParameter<Int32>        ("ConfirmationAttempts", entity.ConfirmationAttempts);
                        command.AddInputParameter<String>       ("DateTimeZoneId"      , entity.TimeZone.Id         );
                        command.AddInputParameter<String>       ("SerializedData"      , String.Empty               );
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
            IdentityEntity entity)
        {
            ValidateRepository<IdentityEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "Identity_Delete",
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
            IdentityEntity entity)
        {
            ValidateRepository<IdentityEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "Identity_Purge",
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
        public EntityCollection<IdentityEntity> GetAll()
        {
            ValidateRepository<IdentityEntity>.GetAll();

            EntityCollection<IdentityEntity> entities = new EntityCollection<IdentityEntity>();

            AdoAccess.CallProcedure(
                "Identity_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            IdentityEntity entity = this.LoadEntity(reader);

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
        public IdentityEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<IdentityEntity>.GetById(entityId);

            IdentityEntity entity = null;

            AdoAccess.CallProcedure(
                "Identity_GetById",
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
            IdentityEntity entity)
        {
            ValidateRepository<IdentityEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "Identity_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>        ("RowId"               , entity.Id                  );
                        command.AddInputParameter<Byte[]>       ("RowVersion"          , entity.Version             );
                        command.AddInputParameter<IdentityFlags>("Flags"               , entity.Flags               );
                        command.AddInputParameter<Instant>      ("DateCreated"         , entity.DateCreated         );
                        command.AddInputParameter<Instant>      ("DateModified"        , entity.DateModified        );
                        command.AddInputParameter<Int32>        ("ConfirmationAttempts", entity.ConfirmationAttempts);
                        command.AddInputParameter<String>       ("DateTimeZoneId"      , entity.TimeZone.Id         );
                        command.AddInputParameter<String>       ("SerializedData"      , String.Empty               );
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
        /// Return the identity by the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByDestination(
            DestinationEntity destination)
        {
            IdentityEntity identity = null;

            AdoAccess.CallProcedure(
                "Identity_GetByDestinationId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("DestinationId", destination.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            identity = this.LoadEntity(reader);
                        }
                    });

            return identity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the error log.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByErrorLog(
            ErrorLogEntity errorLog)
        {
            IdentityEntity identity = null;

            AdoAccess.CallProcedure(
                "Identity_GetByErrorLogId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("ErrorLogId", errorLog.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            identity = this.LoadEntity(reader);
                        }
                    });

            return identity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByBlob(
            BlobEntity blob)
        {
            IdentityEntity identity = null;

            AdoAccess.CallProcedure(
                "Identity_GetByBlobId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("BlobId", blob.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            identity = this.LoadEntity(reader);
                        }
                    });

            return identity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the license.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByLicense(
            LicenseEntity license)
        {
            IdentityEntity identity = null;

            AdoAccess.CallProcedure(
                "Identity_GetByLicenseId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("LicenseId", license.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            identity = this.LoadEntity(reader);
                        }
                    });

            return identity;
        }
                
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByPaymentMethod(
            PaymentMethodEntity paymentMethod)
        {
            IdentityEntity identity = null;

            AdoAccess.CallProcedure(
                "Identity_GetByPaymentMethodId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("PaymentMethodId", paymentMethod.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            identity = this.LoadEntity(reader);
                        }
                    });

            return identity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByServiceLink(
            ServiceLinkEntity serviceLink)
        {
            IdentityEntity identity = null;

            AdoAccess.CallProcedure(
                "Identity_GetByServiceLinkId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("ServiceLinkId", serviceLink.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            identity = this.LoadEntity(reader);
                        }
                    });

            return identity;
        }
        
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the scheduled task.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByScheduledTask(
            ScheduledTaskEntity scheduledTask)
        {
            IdentityEntity identity = null;

            AdoAccess.CallProcedure(
                "Identity_GetByScheduledTaskId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("ScheduledTaskId", scheduledTask.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            identity = this.LoadEntity(reader);
                        }
                    });

            return identity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the session.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetBySession(
            SessionEntity session)
        {
            IdentityEntity identity = null;

            AdoAccess.CallProcedure(
                "Identity_GetBySessionId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("SessionId", session.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            identity = this.LoadEntity(reader);
                        }
                    });

            return identity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the license subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetBySubscription(
            SubscriptionEntity subscription)
        {
            IdentityEntity identity = null;

            AdoAccess.CallProcedure(
                "Identity_GetBySubscriptionId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("SubscriptionId", subscription.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            identity = this.LoadEntity(reader);
                        }
                    });

            return identity;
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private IdentityEntity LoadEntity(
            IDataReader reader)
        {
            DateTimeZone dateTimeZone;

            String timeZoneInfoId = reader.GetValue<String>("DateTimeZoneId");

            if (String.IsNullOrWhiteSpace(timeZoneInfoId))
            {
                dateTimeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
            }
            else
            {
                dateTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZoneInfoId);

                if (dateTimeZone == null)
                {
                    dateTimeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
                }
            }

            IdentityEntity entity = new IdentityEntity(reader.GetBaseEntity())
                {
                    Flags                = reader.GetValue<IdentityFlags>("Flags")               ,
                    ConfirmationAttempts = reader.GetValue<Int32>        ("ConfirmationAttempts"),
                    TimeZone             = dateTimeZone
                };

            return entity;
        }
        #endregion
    }
}
