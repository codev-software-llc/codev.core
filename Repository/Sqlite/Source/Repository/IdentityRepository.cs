//-----------------------------------------------------------------------------
// <copyright file="IdentityRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
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
    /// This implements the repository for identities.
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
            IDataSource dataSource) : base(dataSource)
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

            SqliteAccess.CallStatement(
                "INSERT INTO [Identities] ([IsActive], [Flags], [DateCreated], [DateModified], [ConfirmationAttempts], [DateTimeZoneId], [SerializedData]) VALUES (1, ?, ?, ?, ?, ?, ?)",
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
            IdentityEntity entity)
        {
            ValidateRepository<IdentityEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Identities] SET [IsActive] = 0 WHERE [RowId] = ?",
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

            SqliteAccess.CallStatement(
                "DELETE FROM [Identities] WHERE [RowId] = ?",
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
            ValidateRepository<BlobEntity>.GetAll();

            EntityCollection<IdentityEntity> entities = new();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [ConfirmationAttempts], [DateTimeZoneId], [SerializedData] FROM [Identities]",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            IdentityEntity entity = LoadEntity(reader);

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

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [ConfirmationAttempts], [DateTimeZoneId], [SerializedData] FROM [Identities] WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("RowId", entityId);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will update the entity information.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            IdentityEntity entity)
        {
            ValidateRepository<IdentityEntity>.Update(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Identies] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [ConfirmationAttempts] = ?, [DateTimeZoneId] = ?, [SerializedData] = ? WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<IdentityFlags>("Flags"               , entity.Flags               );
                        command.AddInputParameter<Instant>      ("DateCreated"         , entity.DateCreated         );
                        command.AddInputParameter<Instant>      ("DateModified"        , entity.DateModified        );
                        command.AddInputParameter<Int32>        ("ConfirmationAttempts", entity.ConfirmationAttempts);
                        command.AddInputParameter<String>       ("DateTimeZoneId"      , entity.TimeZone.Id         );
                        command.AddInputParameter<String>       ("SerializedData"      , String.Empty               );
                        command.AddInputParameter<Int32>        ("RowId"               , entity.Id                  );
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
            IdentityEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[ConfirmationAttempts], e.[DateTimeZoneId], e.[SerializedData] FROM [Identities] e JOIN [Destinations] d on d.[IdentityId] = e.[RowId] WHERE d.[RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("DestinationId", destination.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the error log.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByErrorLog(
            ErrorLogEntity errorLog)
        {
            IdentityEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[ConfirmationAttempts], e.[DateTimeZoneId], e.[SerializedData] FROM [Identities] e JOIN [ErrorLog] l on l.[IdentityId] = e.[RowId] WHERE l.[RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("ErrorLogId", errorLog.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByBlob(
            BlobEntity blob)
        {
            IdentityEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[ConfirmationAttempts], e.[DateTimeZoneId], e.[SerializedData] FROM [Identities] e JOIN [Blobs] b on b.[IdentityId] = e.[RowId] WHERE b.[RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("BlobId", blob.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the license.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByLicense(
            LicenseEntity license)
        {
            IdentityEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[ConfirmationAttempts], e.[DateTimeZoneId], e.[SerializedData] FROM [Identities] e JOIN [Licenses] l on l.[IdentityId] = e.[RowId] WHERE l.[RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("LicenseId", license.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the payment method.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByPaymentMethod(
            PaymentMethodEntity paymentMethod)
        {
            IdentityEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[ConfirmationAttempts], e.[DateTimeZoneId], e.[SerializedData] FROM [Identities] e JOIN [PaymentMethods] p on p.[IdentityId] = e.[RowId] WHERE p.[RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("PaymentMethodId", paymentMethod.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByServiceLink(
            ServiceLinkEntity serviceLink)
        {
            IdentityEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[ConfirmationAttempts], e.[DateTimeZoneId], e.[SerializedData] FROM [Identities] e JOIN [ServiceLinks] s on s.[IdentityId] = e.[RowId] WHERE s.[RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("ServiceLinkId", serviceLink.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the scheduled task.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetByScheduledTask(
            ScheduledTaskEntity scheduledTask)
        {
            IdentityEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[ConfirmationAttempts], e.[DateTimeZoneId], e.[SerializedData] FROM [Identities] e JOIN [ScheduledTasks] s on s.[IdentityId] = e.[RowId] WHERE s.[RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("ScheduledTaskId", scheduledTask.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the session.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetBySession(
            SessionEntity session)
        {
            IdentityEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[ConfirmationAttempts], e.[DateTimeZoneId], e.[SerializedData] FROM [Identities] e JOIN [Sessions] s on s.[IdentityId] = e.[RowId] WHERE s.[RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("SessionId", session.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the identity by the subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        public IdentityEntity GetBySubscription(
            SubscriptionEntity subscription)
        {
            IdentityEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[ConfirmationAttempts], e.[DateTimeZoneId], e.[SerializedData] FROM [Identities] e JOIN [Subscriptions] s on s.[IdentityId] = e.[RowId] WHERE s.[RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("SubscriptionId", subscription.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            entity = LoadEntity(reader);
                        }
                    });

            return entity;
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the entity..
        /// </summary>
        ///--------------------------------------------------------------------
        private static IdentityEntity LoadEntity(
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
                    dateTimeZone = NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault();
                }
            }

            IdentityEntity entity = new(
                reader.GetBaseEntity())
                    {
                        Flags                = reader.GetValue<IdentityFlags>("Flags")               ,
                        ConfirmationAttempts = reader.GetValue<Int32>        ("ConfirmationAttempts"),
                        TimeZone             = dateTimeZone                                          ,
                     //   Token                = data
                    };

            return entity;
        }
        #endregion
    }
}
