//-----------------------------------------------------------------------------
// <copyright file="CommunicationRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
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
    /// This implements the repository for blob storage.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CommunicationRepository : BaseRepository, ICommunicationRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public CommunicationRepository(
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
            CommunicationEntity entity)
        {
            ValidateRepository<CommunicationEntity>.Add(entity);

            SqliteAccess.CallStatement(
                "INSERT INTO [Communications] ([IsActive], [Flags], [DateCreated], [DateModified], [Application], [Category], [Subcategory], [Name], [EmailAddress], [Comments]) VALUES (1, ?, ?, ?, ?, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<CommunicationFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>           ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>           ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>            ("Application" , entity.Application );
                        command.AddInputParameter<String>            ("Category"    , entity.Category    );
                        command.AddInputParameter<String>            ("Subcategory" , entity.Subcategory );
                        command.AddInputParameter<String>            ("Name"        , entity.Name        );
                        command.AddInputParameter<String>            ("EmailAddress", entity.EmailAddress);
                        command.AddInputParameter<String>            ("Comments"    , entity.Comments    );
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
            CommunicationEntity entity)
        {
            ValidateRepository<CommunicationEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Communications] SET [IsActive] = 0 WHERE [RowId] = ?",
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
            CommunicationEntity entity)
        {
            ValidateRepository<CommunicationEntity>.Purge(entity);

            SqliteAccess.CallStatement(
                "DELETE FROM [Communications] WHERE [RowId] = ?",
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
        public EntityCollection<CommunicationEntity> GetAll()
        {
            ValidateRepository<CommunicationEntity>.GetAll();

            EntityCollection<CommunicationEntity> entities = new EntityCollection<CommunicationEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [Application], [Category], [Subcategory], [Name], [EmailAddress], [Comments] FROM [Communications]",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            CommunicationEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the entity by name.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public CommunicationEntity GetById(
            Int32   entityId)
        {
            ValidateRepository<CommunicationEntity>.GetById(entityId);

            CommunicationEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [Application], [Category], [Subcategory], [Name], [EmailAddress], [Comments] FROM [Communications] WHERE [RowId] = ?",
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
        /// This will update the entity information.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            CommunicationEntity entity)
        {
            ValidateRepository<CommunicationEntity>.Update(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Communications] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [Application] = ?, [Category] = ?, [Subcategory] = ?, [Name] = ?, [EmailAddress] = ?, [Comments] = ? WHERE [RowId] = @RowId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<CommunicationFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>           ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>           ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>            ("Application" , entity.Application );
                        command.AddInputParameter<String>            ("Category"    , entity.Category    );
                        command.AddInputParameter<String>            ("Subcategory" , entity.Subcategory );
                        command.AddInputParameter<String>            ("Name"        , entity.Name        );
                        command.AddInputParameter<String>            ("EmailAddress", entity.EmailAddress);
                        command.AddInputParameter<String>            ("Comments"    , entity.Comments    );
                        command.AddInputParameter<Int32>             ("RowId"       , entity.Id          );
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

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private CommunicationEntity LoadEntity(
            IDataReader reader)
        {
            CommunicationEntity entity = new CommunicationEntity(
                reader.GetBaseEntity())
                    {
                        Flags        = reader.GetValue<CommunicationFlags>("Flags")       ,
                        Application  = reader.GetValue<String>            ("Application") ,
                        Category     = reader.GetValue<String>            ("Category")    ,
                        Subcategory  = reader.GetValue<String>            ("Subcategory") ,
                        Name         = reader.GetValue<String>            ("Name")        ,
                        EmailAddress = reader.GetValue<String>            ("EmailAddress"),
                        Comments     = reader.GetValue<String>            ("Comments")
                    };

            return entity;
        }
        #endregion
    }
}
