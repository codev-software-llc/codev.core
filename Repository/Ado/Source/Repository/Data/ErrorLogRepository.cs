//-----------------------------------------------------------------------------
// <copyright file="ErrorLogRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
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
    /// This implements the error logging repository.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ErrorLogRepository : BaseRepository, IErrorLogRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public ErrorLogRepository(
            ICoreDataSource     dataSource,
            IIdentityRepository identityRepository) : base(dataSource)
        {
            this.IdentityRepository = identityRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------    
        /// <summary>
        /// Add the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            ErrorLogEntity entity)
        {
            ValidateRepository<ErrorLogEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "ErrorLog_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>                  ("IdentityId"   , entity.Identity.Id  );
                        command.AddInputParameter<ErrorLogFlags>          ("Flags"        , entity.Flags        );
                        command.AddInputParameter<Instant>                ("DateCreated"  , entity.DateCreated  );
                        command.AddInputParameter<Instant>                ("DateModified" , entity.DateModified );
                        command.AddInputParameter<DiagnosticComponentType>("ComponentType", entity.ComponentType);
                        command.AddInputParameter<DiagnosticSeverityType> ("SeverityType" , entity.SeverityType );
                        command.AddInputParameter<String>                 ("Message"      , entity.Message      );
                        command.AddInputParameter<String>                 ("ServerName"   , entity.ServerName   );
                        command.AddInputParameter<String>                 ("TrackingTag"  , entity.TrackingTag  );
                        command.AddInputParameter<String>                 ("StackTrace"   , entity.StackTrace   );
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
            ErrorLogEntity entity)
        {
            ValidateRepository<ErrorLogEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "ErrorLog_Delete",
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
        /// Remove the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Purge(
            ErrorLogEntity entity)
        {
            ValidateRepository<ErrorLogEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "ErrorLog_Purge",
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
        public EntityCollection<ErrorLogEntity> GetAll()
        {
            ValidateRepository<ErrorLogEntity>.GetAll();

            EntityCollection<ErrorLogEntity> entities = new EntityCollection<ErrorLogEntity>();

            AdoAccess.CallProcedure(
                "ErrorLog_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            ErrorLogEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the entity by the unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public ErrorLogEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<ErrorLogEntity>.GetById(entityId);

            ErrorLogEntity entity = null;

            AdoAccess.CallProcedure(
                "ErrorLog_GetById",
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
            ErrorLogEntity entity)
        {
            ValidateRepository<ErrorLogEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "ErrorLog_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>                  ("RowId"        , entity.Id           );
                        command.AddInputParameter<Byte[]>                 ("RowVersion"   , entity.Version      );
                        command.AddInputParameter<Int32>                  ("IdentityId"   , entity.Identity.Id  );
                        command.AddInputParameter<ErrorLogFlags>          ("Flags"        , entity.Flags        );
                        command.AddInputParameter<Instant>                ("DateCreated"  , entity.DateCreated  );
                        command.AddInputParameter<Instant>                ("DateModified" , entity.DateModified );
                        command.AddInputParameter<DiagnosticComponentType>("ComponentType", entity.ComponentType);
                        command.AddInputParameter<DiagnosticSeverityType> ("SeverityType" , entity.SeverityType );
                        command.AddInputParameter<String>                 ("Message"      , entity.Message      );
                        command.AddInputParameter<String>                 ("ServerName"   , entity.ServerName   );
                        command.AddInputParameter<String>                 ("TrackingTag"  , entity.TrackingTag  );
                        command.AddInputParameter<String>                 ("StackTrace"   , entity.StackTrace   );
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
        /// Return errors that are within a date range.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<ErrorLogEntity> GetAllByDateRange(
            IdentityEntity      identity,
            Nullable<LocalDate> dateStart,
            Nullable<LocalDate> dateEnd)
        {
            EntityCollection<ErrorLogEntity> entities = new EntityCollection<ErrorLogEntity>();

            Instant instantStart;
            Instant instantEnd;

            if (dateStart == null)
            {
                instantStart = NodaExtensions.InstantMinValue;
            }
            else
            {
                instantStart = ((LocalDate)dateStart).ToStartOfDay().ToInstant(identity.TimeZone);
            }

            if (dateEnd == null)
            {
                instantEnd = NodaExtensions.InstantMaxValue;
            }
            else
            {
                instantEnd = ((LocalDate)dateEnd).ToEndOfDay().ToInstant(identity.TimeZone);
            }

            AdoAccess.CallProcedure(
                "ErrorLog_GetAllByDateRange",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Instant>("DateStart", instantStart);
                        command.AddInputParameter<Instant>("DateEnd"  , instantEnd  );
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            ErrorLogEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return errors that are common.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<ErrorLogEntity> GetStatistics(
            IdentityEntity      identity,
            Nullable<LocalDate> dateStart,
            Nullable<LocalDate> dateEnd)
        {
            EntityCollection<ErrorLogEntity> entities = new EntityCollection<ErrorLogEntity>();

            Instant instantStart;
            Instant instantEnd;

            if (dateStart == null)
            {
                instantStart = NodaExtensions.InstantMinValue;
            }
            else
            {
                instantStart = ((LocalDate)dateStart).ToStartOfDay().ToInstant(identity.TimeZone);
            }

            if (dateEnd == null)
            {
                instantEnd = NodaExtensions.InstantMaxValue;
            }
            else
            {
                instantEnd = ((LocalDate)dateEnd).ToEndOfDay().ToInstant(identity.TimeZone);
            }

            AdoAccess.CallProcedure(
                "ErrorLog_GetStatistics",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Instant>("DateStart", instantStart);
                        command.AddInputParameter<Instant>("DateEnd"  , instantEnd  );
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            ErrorLogEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove all entities.
        /// </summary>
        ///--------------------------------------------------------------------
        public void PurgeAll()
        {
            AdoAccess.CallProcedure(
                "ErrorLog_PurgeAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, rowsAffected) =>
                    {
                        Boolean isValid = (rowsAffected > 0);
                    });
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private ErrorLogEntity LoadEntity(
            IDataReader reader)
        {
            ErrorLogEntity entity = new ErrorLogEntity(reader.GetBaseEntity())
                {
                    Flags         = reader.GetValue<ErrorLogFlags>          ("Flags")        ,
                    ComponentType = reader.GetValue<DiagnosticComponentType>("ComponentType"),
                    SeverityType  = reader.GetValue<DiagnosticSeverityType> ("SeverityType") ,
                    Message       = reader.GetValue<String>                 ("Message")      ,
                    ServerName    = reader.GetValue<String>                 ("ServerName")   ,
                    TrackingTag   = reader.GetValue<String>                 ("TrackingTag")  ,
                    StackTrace    = reader.GetValue<String>                 ("StackTrace")
                };

            this.InitializeLazyLoading(entity);

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the lazy loading of the Contact entity properties.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeLazyLoading(
            ErrorLogEntity entity)
        {
            entity.LazyIdentity = new Lazy<IdentityEntity>(() =>
                {
                    return this.IdentityRepository.GetByErrorLog(entity);
                });
        }
        #endregion
    }
}
