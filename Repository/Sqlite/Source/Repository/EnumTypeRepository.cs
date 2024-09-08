//-----------------------------------------------------------------------------
// <copyright file="EnumTypeRepository.cs" company="Codev Software, LLC">
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
    /// This implements the repository for flag access.
    /// </summary>
    ///------------------------------------------------------------------------
    public class EnumTypeRepository : BaseRepository, IEnumTypeRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public EnumTypeRepository(
            IDataSource dataSource) : base(dataSource)
        {
        }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add entity to repository
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            EnumTypeEntity entity)
        {
            ValidateRepository<EnumTypeEntity>.Add(entity);

            SqliteAccess.CallStatement(
                "INSERT INTO [EnumTypes] ([IsActive], [Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment]) VALUES (1, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<EnumTypeFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>      ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>      ("DateModified", entity.DateModified);
                        command.AddInputParameter<Boolean>      ("IsFlag"      , entity.IsFlag      );
                        command.AddInputParameter<Boolean>      ("IsBig"       , entity.IsBig       );
                        command.AddInputParameter<String>       ("Application" , entity.Application );
                        command.AddInputParameter<String>       ("Name"        , entity.Name        );
                        command.AddInputParameter<String>       ("EnumKey"     , entity.EnumKey     );
                        command.AddInputParameter<Int64>        ("Value"       , entity.Value       );
                        command.AddInputParameter<String>       ("Comment"     , entity.Comment     );
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
            EnumTypeEntity entity)
        {
            ValidateRepository<EnumTypeEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [EnumTypes] SET [IsActive] = 0 WHERE [RowId] = ?",
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
        /// Remove the entity permanently.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Purge(
            EnumTypeEntity entity)
        {
            ValidateRepository<EnumTypeEntity>.Purge(entity);

            SqliteAccess.CallStatement(
                "DELETE FROM [EnumTypes] WHERE [RowId] = ?",
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
        public EntityCollection<EnumTypeEntity> GetAll()
        {
            ValidateRepository<EnumTypeEntity>.GetAll();

            EntityCollection<EnumTypeEntity> entities = new EntityCollection<EnumTypeEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment] FROM [EnumTypes]",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            EnumTypeEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the entity by its unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public EnumTypeEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<EnumTypeEntity>.GetById(entityId);

            EnumTypeEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [[Flags], [DateCreated], [DateModified], [IsFlag], [IsBig], [Application], [Name], [EnumKey], [Value], [Comment] FROM [EnumTypes] WHERE [RowId] = ?",
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
            EnumTypeEntity entity)
        {
            ValidateRepository<EnumTypeEntity>.Update(entity);

            SqliteAccess.CallStatement(
                "UPDATE [EnumTypes] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [IsFlag] = ?, [IsFlag] = ?, [Application] = ?, [Name] = ?, [EnumKey] = ?, [Value] = ?, [Comment] = ? WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<EnumTypeFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>      ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>      ("DateModified", entity.DateModified);
                        command.AddInputParameter<Boolean>      ("IsFlag"      , entity.IsFlag      );
                        command.AddInputParameter<Boolean>      ("IsBig"       , entity.IsBig       );
                        command.AddInputParameter<String>       ("Application" , entity.Application );
                        command.AddInputParameter<String>       ("Name"        , entity.Name        );
                        command.AddInputParameter<String>       ("EnumKey"     , entity.EnumKey     );
                        command.AddInputParameter<Int64>        ("Value"       , entity.Value       );
                        command.AddInputParameter<String>       ("Comment"     , entity.Comment     );
                        command.AddInputParameter<Int32>        ("RowId"       , entity.Id          );
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
        /// Load an entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private EnumTypeEntity LoadEntity(
            IDataReader reader)
        {
            EnumTypeEntity entity = new EnumTypeEntity(
                reader.GetBaseEntity())
                    {
                        Flags        = reader.GetValue<EnumTypeFlags>("Flags")             ,
                        IsFlag       = reader.GetValue<Int32>        ("IsFlag").ToBoolean(),
                        IsBig        = reader.GetValue<Int32>        ("IsBig").ToBoolean() ,
                        Application  = reader.GetValue<String>       ("Application")       ,
                        Name         = reader.GetValue<String>       ("Name")              ,
                        EnumKey      = reader.GetValue<String>       ("EnumKey")           , 
                        Value        = reader.GetValue<Int64>        ("Value")             ,
                        Comment      = reader.GetValue<String>       ("Comment")
                    };

            return entity;
        }
        #endregion
    }
}
