//-----------------------------------------------------------------------------
// <copyright file="SettingRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
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
            SettingEntity entity)
        {
            ValidateRepository<SettingEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "Setting_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<SettingFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>     ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>     ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>      ("Name"        , entity.Name        );
                        command.AddInputParameter<String>      ("Value"       , entity.Value       );
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
            SettingEntity entity)
        {
            ValidateRepository<SettingEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "Setting_Delete",
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

            AdoAccess.CallProcedure(
                "Setting_Purge",
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
        /// Retrieve all blobs.
        /// </summary>
        ///--------------------------------------------------------------------
        public EntityCollection<SettingEntity> GetAll()
        {
            ValidateRepository<SettingEntity>.GetAll();

            EntityCollection<SettingEntity> entities = new EntityCollection<SettingEntity>();

            AdoAccess.CallProcedure(
                "Setting_GetAll",
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

            AdoAccess.CallProcedure(
                "Blob_GetById",
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

            AdoAccess.CallProcedure(
                "Setting_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>       ("RowId"       , entity.Id          );
                        command.AddInputParameter<Byte[]>      ("RowVersion"  , entity.Version     );
                        command.AddInputParameter<SettingFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>     ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>     ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>      ("Name"        , entity.Name        );
                        command.AddInputParameter<String>      ("Value"       , entity.Value       );
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

            AdoAccess.CallProcedure(
                "Setting_GetByName",
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
            SettingEntity entity = new SettingEntity(reader.GetBaseEntity())
                {
                    Flags = reader.GetValue<SettingFlags>("Flags"),
                    Name  = reader.GetValue<String>      ("Name") ,
                    Value = reader.GetValue<String>      ("Value")
                };

            return entity;
        }
        #endregion
    }
}
