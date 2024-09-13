//-----------------------------------------------------------------------------
// <copyright file="LicenseRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text.Json;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for license store.
    /// </summary>
    ///------------------------------------------------------------------------
    public class LicenseRepository : BaseRepository, ILicenseRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public LicenseRepository(
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
        /// Add a new entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            LicenseEntity entity)
        {
            ValidateRepository<LicenseEntity>.Add(entity);

            String detailSerialized = JsonSerializer.Serialize(entity.Features);

            SqliteAccess.CallStatement(
                "INSERT INTO [Licenses] ([IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Application], [Features]) VALUES (1, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<LicenseFlags>("Flags"         , entity.Flags            );
                        command.AddInputParameter<Instant>     ("DateCreated"   , entity.DateCreated      );
                        command.AddInputParameter<Instant>     ("DateModified"  , entity.DateModified     );
                        command.AddInputParameter<Int32>       ("IdentityId"    , entity.Identity.Id      );
                        command.AddInputParameter<String>      ("Application"   , entity.Application      );
                        command.AddInputParameter<String>      ("Name"          , entity.Name             );
                        command.AddInputParameter<String>      ("CurrencyCode"  , entity.Cost.IsoCode);
                        command.AddInputParameter<Decimal>     ("Amount"        , entity.Cost.Amount      );
                        command.AddInputParameter<String>      ("Features"      , detailSerialized        );
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
            LicenseEntity entity)
        {
            ValidateRepository<LicenseEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Licenses] SET [IsActive] = 0 WHERE [RowId] = ?",
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
            LicenseEntity entity)
        {
            ValidateRepository<LicenseEntity>.Purge(entity);

            SqliteAccess.CallStatement(
                "DELETE FROM [Licenses] WHERE [RowId] = ?",
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
        public EntityCollection<LicenseEntity> GetAll()
        {
            ValidateRepository<LicenseEntity>.GetAll();

            EntityCollection<LicenseEntity> entities = new();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Application], [Name], [CurrencyType], [Amount], [Features] FROM [Licenses]",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            LicenseEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the blob by name.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public LicenseEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<LicenseEntity>.GetById(entityId);

            LicenseEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Application], [Name], [CurrencyType], [Amount], [Features] FROM [Licenses] WHERE [RowId] = ?",
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
        /// This will update the blob information.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            LicenseEntity entity)
        {
            ValidateRepository<LicenseEntity>.Update(entity);

            String detailSerialized = JsonSerializer.Serialize(entity.Features);

            SqliteAccess.CallStatement(
                "UPDATE [Licenses] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [IdentityId] = ?, [Application] = ?, [Features] = ? WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<LicenseFlags>("Flags"       , entity.Flags            );
                        command.AddInputParameter<Instant>     ("DateCreated" , entity.DateCreated      );
                        command.AddInputParameter<Instant>     ("DateModified", entity.DateModified     );
                        command.AddInputParameter<Int32>       ("IdentityId"  , entity.Identity.Id      );
                        command.AddInputParameter<String>      ("Name"        , entity.Name             );
                        command.AddInputParameter<String>      ("CurrencyCode", entity.Cost.IsoCode);
                        command.AddInputParameter<Decimal>     ("Amount"      , entity.Cost.Amount      );
                        command.AddInputParameter<String>      ("Application" , entity.Application      );
                        command.AddInputParameter<String>      ("Features"    , detailSerialized        );
                        command.AddInputParameter<Int32>       ("RowId"       , entity.Id               );
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
        /// Retrieve the entities by the application name.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public EntityCollection<LicenseEntity> GetAllByApplication(
            String applicationName)
        {
            EntityCollection<LicenseEntity> entities = new();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Application], [Name], [CurrencyType], [Amount], [Features] FROM [Licenses] WHERE [Application] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String>("Application", applicationName);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            LicenseEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the license by the subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        public LicenseEntity GetBySubscription(
            SubscriptionEntity subscription)
        {
            LicenseEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT e.[RowId], e.[RowVersion], e.[IsActive], e.[Flags], e.[DateCreated], e.[DateModified], e.[IdentityId], e.[Application], e.[Name], e.[CurrencyType], e.[Amount], e.[Features] FROM [Licenses] e JOIN [Subscriptions] s on s.[LicenseId] = e.[RowId] WHERE s.[RowId] = ?",
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
        private LicenseEntity LoadEntity(
            IDataReader reader)
        {
            List<LicenseFeature> features = JsonSerializer.Deserialize<List<LicenseFeature>>(reader.GetValue<String>("Features"));

            Token data = JsonSerializer.Deserialize<Token>(reader.GetValue<String>("SerializedData"));

            PaymentAmount cost = new(reader.GetValue<String>("CurrencyCode"), reader.GetValue<Decimal>("Amount"));

            LicenseEntity entity = new(
                reader.GetBaseEntity())
                    {
                        Flags       = reader.GetValue<LicenseFlags>("Flags")      ,
                        Application = reader.GetValue<String>      ("Application"),
                        Name        = reader.GetValue<String>      ("Name")       ,
                        Cost        = cost                                        ,
                        Features    = features                                    ,
                        Token       = data
                    };

            return entity;
        }
        #endregion
    }
}
