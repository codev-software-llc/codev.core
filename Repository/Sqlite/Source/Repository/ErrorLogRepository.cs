//-----------------------------------------------------------------------------
// <copyright file="ErrorLogRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
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
            IDataSource         dataSource,
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

            SqliteAccess.CallStatement(
                "INSERT INTO [ErrorLog] ([IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [ComponentType], [SeverityType], [Message], [ServerName], [TrackingTag], [StackTrace]) VALUES (1, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<ErrorLogFlags>          ("Flags"        , entity.Flags        );
                        command.AddInputParameter<Instant>                ("DateCreated"  , entity.DateCreated  );
                        command.AddInputParameter<Instant>                ("DateModified" , entity.DateModified );
                        command.AddInputParameter<Int32>                  ("IdentityId"   , entity.Identity.Id  );
                        command.AddInputParameter<DiagnosticComponentType>("ComponentType", entity.ComponentType);
                        command.AddInputParameter<DiagnosticSeverityType> ("SeverityType" , entity.SeverityType );
                        command.AddInputParameter<String>                 ("Message"      , entity.Message      );
                        command.AddInputParameter<String>                 ("ServerName"   , entity.ServerName   );
                        command.AddInputParameter<String>                 ("TrackingTag"  , entity.TrackingTag  );
                        command.AddInputParameter<String>                 ("StackTrace"   , entity.StackTrace   );
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
            ErrorLogEntity entity)
        {
            ValidateRepository<ErrorLogEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [ErrorLog] SET [IsActive] = 0 WHERE [RowId] = ?",
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

            SqliteAccess.CallStatement(
                "DELETE FROM [ErrorLog] WHERE [RowId] = ?",
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

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [ComponentType], [SeverityType], [Message], [ServerName], [TrackingTag], [StackTrace] FROM [ErrorLog]",
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

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [ComponentType], [SeverityType], [Message], [ServerName], [TrackingTag], [StackTrace] FROM [ErrorLog] WHERE [RowId] = ?",
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

            SqliteAccess.CallStatement(
                "UPDATE [ErrorLog] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [IdentityId] = ?, [ComponentType] = ?, [SeverityType] = ?, [Message] = ?, [ServerName] = ?, [TrackingTag] = ?, [StackTrace] = ? WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<ErrorLogFlags>          ("Flags"        , entity.Flags        );
                        command.AddInputParameter<Instant>                ("DateCreated"  , entity.DateCreated  );
                        command.AddInputParameter<Instant>                ("DateModified" , entity.DateModified );
                        command.AddInputParameter<Int32>                  ("IdentityId"   , entity.Identity.Id  );
                        command.AddInputParameter<DiagnosticComponentType>("ComponentType", entity.ComponentType);
                        command.AddInputParameter<DiagnosticSeverityType> ("SeverityType" , entity.SeverityType );
                        command.AddInputParameter<String>                 ("Message"      , entity.Message      );
                        command.AddInputParameter<String>                 ("ServerName"   , entity.ServerName   );
                        command.AddInputParameter<String>                 ("TrackingTag"  , entity.TrackingTag  );
                        command.AddInputParameter<String>                 ("StackTrace"   , entity.StackTrace   );
                        command.AddInputParameter<Int32>                  ("RowId"        , entity.Id           );
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

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [ComponentType], [SeverityType], [Message], [ServerName], [TrackingTag], [StackTrace] FROM [ErrorLog] WHERE (([DateCreated] >= ?) AND ([DateCreated] <= ?)) ORDER BY [DateCreated]",
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
        /// Return the count breakdown of errors.
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

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [ComponentType], [SeverityType], [Message], [ServerName], [TrackingTag], [StackTrace] FROM [ErrorLog] WHERE (([DateCreated] >= ?) AND ([DateCreated] <= ?)) ORDER BY [DateCreated]",
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
            SqliteAccess.CallStatement(
                "DELETE FROM [ErrorLog]",
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
            ErrorLogEntity entity = new ErrorLogEntity(
                reader.GetBaseEntity())
                    {
                        Flags         = reader.GetValue<ErrorLogFlags>          ("Flags")        ,
                        ComponentType = reader.GetValue<DiagnosticComponentType>("ComponentType"),
                        SeverityType  = reader.GetValue<DiagnosticSeverityType> ("SeverityType") ,
                        Message       = reader.GetValue<String>                 ("Message")      ,
                        ServerName    = reader.GetValue<String>                 ("ServerName")   ,
                        TrackingTag   = reader.GetValue<String>                 ("TrackingTag")  ,
                        StackTrace    = reader.GetValue<String>                 ("StackTrace")
                    };

            return entity;
        }
        #endregion
    }
}
