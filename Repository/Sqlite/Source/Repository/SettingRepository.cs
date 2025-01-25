//-----------------------------------------------------------------------------
// <copyright file="SettingRepository.cs" company="Codev Software, LLC">
// Copyright © 2025
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
    /// This implements the setting repository interface for key-value pair
    /// storage.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SettingRepository : BaseRepository, ISettingRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public SettingRepository(
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
            SettingEntity entity)
        {
            ValidateRepository<SettingEntity>.Add(entity);

            SqliteAccess.CallStatement(
                "INSERT INTO [Settings] ([IsActive], [Flags], [DateCreated], [DateModified], [Name], [Value]) VALUES (1, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<SettingFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>     ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>     ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>      ("Name"        , entity.Name        );
                        command.AddInputParameter<String>      ("Value"       , entity.Value       );
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
            SettingEntity entity)
        {
            ValidateRepository<SettingEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Settings] SET [IsActive] = 0 WHERE [RowId] = ?",
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
            SettingEntity entity)
        {
            ValidateRepository<SettingEntity>.Purge(entity);

            SqliteAccess.CallStatement(
                "DELETE FROM [Settings] WHERE [RowId] = ?",
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
        public EntityCollection<SettingEntity> GetAll()
        {
            ValidateRepository<SettingEntity>.GetAll();

            EntityCollection<SettingEntity> entities = new EntityCollection<SettingEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [Name], [Value] FROM [Settings]",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            SettingEntity entity = this.LoadEntity(reader);

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
        public SettingEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<SettingEntity>.GetById(entityId);

            SettingEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [Name], [Value] FROM [Settings] WHERE [RowId] = ?",
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
            SettingEntity entity)
        {
            ValidateRepository<SettingEntity>.Update(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Settings] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [Name] = ?, [Value] = ? WHERE [RowId] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<SettingFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>     ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>     ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>      ("Name"        , entity.Name        );
                        command.AddInputParameter<String>      ("Value"       , entity.Value       );
                        command.AddInputParameter<Int32>       ("RowId"       , entity.Id          );

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
        /// Return the entity by the name.
        /// </summary>
        ///--------------------------------------------------------------------
        public SettingEntity GetByName(
            String name)
        {
            SettingEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [Name], [Value] FROM [Settings] WHERE [Name] = ?",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String>("Name", name);
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
        private SettingEntity LoadEntity(
            IDataReader reader)
        {
            DateTime rowVersion   = reader.GetValue<DateTime>("RowVersion"  );
            Int64    isActive     = reader.GetValue<Int64>   ("IsActive"    ); 
            Int64    flags        = reader.GetValue<Int64>   ("Flags"       );
            String   dateCreated  = reader.GetValue<String>  ("DateCreated" );
            String   dateModified = reader.GetValue<String>  ("DateModified");
            String   name         = reader.GetValue<String>  ("Name"        );
            String   value        = reader.GetValue<String>  ("Value"       );

            SettingEntity entity = new SettingEntity(
                reader.GetBaseEntity())
                    {
                        Flags = (SettingFlags)flags,
                        Name  = name               ,
                        Value = value
                    };

            return entity;
        }
        #endregion
    }
}
