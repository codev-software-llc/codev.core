//-----------------------------------------------------------------------------
// <copyright file="CommunicationRepository.cs" company="Codev Software, LLC">
// Copyright © 2025
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
    /// This implements the repository for communication entries.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CommunicationRepository : BaseRepository, ICommunicationRepository
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------------
        public CommunicationRepository(
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
            CommunicationEntity entity)
        {
            ValidateRepository<CommunicationEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "Communication_Add",
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
            CommunicationEntity entity)
        {
            ValidateRepository<CommunicationEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "Communication_Delete",
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

            AdoAccess.CallProcedure(
                "Communication_Purge",
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
        public EntityCollection<CommunicationEntity> GetAll()
        {
            ValidateRepository<CommunicationEntity>.GetAll();

            EntityCollection<CommunicationEntity> entities = new EntityCollection<CommunicationEntity>();

            AdoAccess.CallProcedure(
                "Communication_GetAll",
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
        /// Retrieve the entity by its unique identifier.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public CommunicationEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<CommunicationEntity>.GetById(entityId);

            CommunicationEntity entity = null;

            AdoAccess.CallProcedure(
                "Communication_GetById",
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
            CommunicationEntity entity)
        {
            ValidateRepository<CommunicationEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "Communication_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>             ("RowId"       , entity.Id          );
                        command.AddInputParameter<Byte[]>            ("RowVersion"  , entity.Version     );
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
            CommunicationEntity entity = new CommunicationEntity(reader.GetBaseEntity())
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
