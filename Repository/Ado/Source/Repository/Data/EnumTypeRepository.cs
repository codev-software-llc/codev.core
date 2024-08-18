//-----------------------------------------------------------------------------
// <copyright file="EnumTypeRepository.cs" company="Codev Software, LLC">
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
            ICoreDataSource dataSource) : base(dataSource)
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

            AdoAccess.CallProcedure(
                "EnumType_Add",
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
            EnumTypeEntity entity)
        {
            ValidateRepository<EnumTypeEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "EnumType_Delete",
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

            AdoAccess.CallProcedure(
                "EnumType_Purge",
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

            AdoAccess.CallProcedure(
                "EnumType_GetAll",
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

            AdoAccess.CallProcedure(
                "EnumType_GetById",
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

            AdoAccess.CallProcedure(
                "EnumType_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>        ("RowId"       , entity.Id          );
                        command.AddInputParameter<Byte[]>       ("RowVersion"  , entity.Version     );
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
            EnumTypeEntity entity = new EnumTypeEntity(reader.GetBaseEntity())
                {
                    Flags       = reader.GetValue<EnumTypeFlags>("Flags")      ,
                    IsFlag      = reader.GetValue<Boolean>      ("IsFlag")     ,
                    IsBig       = reader.GetValue<Boolean>      ("IsBig")      ,
                    Application = reader.GetValue<String>       ("Application"),
                    Name        = reader.GetValue<String>       ("Name")       ,
                    EnumKey     = reader.GetValue<String>       ("EnumKey")    ,
                    Value       = reader.GetValue<Int64>        ("Value")      ,
                    Comment     = reader.GetValue<String>       ("Comment")
                };

            return entity;
        }
        #endregion
    }
}
