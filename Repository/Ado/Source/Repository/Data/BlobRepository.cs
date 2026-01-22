//-----------------------------------------------------------------------------
// <copyright file="BlobRepository.cs" company="Codev Software, LLC">
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
    /// This implements the repository for blob storage.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BlobRepository : BaseRepository, IBlobRepository
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------------
        public BlobRepository(
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
        /// Add a new Entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            BlobEntity entity)
        {
            ValidateRepository<BlobEntity>.Add(entity);

            AdoAccess.CallProcedure(
                "Blob_Add",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>    ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<BlobFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>  ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>  ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>   ("Name"        , entity.Name        );
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
            BlobEntity entity)
        {
            ValidateRepository<BlobEntity>.Delete(entity);

            AdoAccess.CallProcedure(
                "Blob_Delete",
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
            BlobEntity entity)
        {
            ValidateRepository<BlobEntity>.Purge(entity);

            AdoAccess.CallProcedure(
                "Blob_Purge",
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
        public EntityCollection<BlobEntity> GetAll()
        {
            ValidateRepository<BlobEntity>.GetAll();

            EntityCollection<BlobEntity> entities = new EntityCollection<BlobEntity>();

            AdoAccess.CallProcedure(
                "Blob_GetAll",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            BlobEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the entity by its identifier.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public BlobEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<BlobEntity>.GetById(entityId);

            BlobEntity entity = null;

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
        /// This will update the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            BlobEntity entity)
        {
            ValidateRepository<BlobEntity>.Update(entity);

            AdoAccess.CallProcedure(
                "Blob_Update",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>    ("RowId"       , entity.Id          );
                        command.AddInputParameter<Byte[]>   ("RowVersion"  , entity.Version     );
                        command.AddInputParameter<Int32>    ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<BlobFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>  ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>  ("DateModified", entity.DateModified);
                        command.AddInputParameter<String>   ("Name"        , entity.Name        );
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
        /// Cancel all draft blob contents.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public void CancelDrafts(
            BlobEntity blobEntity)
        {
            AdoAccess.CallProcedure(
                "Blob_CancelDrafts",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("BlobId", blobEntity.Id);
                    },
                (command) =>
                    {
                    });
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the entity by the name.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public BlobEntity GetByName(
            String name)
        {
            BlobEntity blob = null;

            AdoAccess.CallProcedure(
                "Blob_GetByName",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String>("Name", name);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            blob = this.LoadEntity(reader);
                        }
                    });

            return blob;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the entity by the name.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public BlobEntity GetByBlobContent(
            BlobContentEntity blobContent)
        {
            BlobEntity blob = null;

            AdoAccess.CallProcedure(
                "Blob_GetByBlobContentId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("BlobContentId", blobContent.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            blob = this.LoadEntity(reader);
                        }
                    });

            return blob;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the blob by it's partial name.  This is the same as a
        /// "Contains".
        /// </summary>
        ///-------------------------------------------------------------------- 
        public EntityCollection<BlobEntity> GetAllByPartialName(
            String partialName)
        {
            EntityCollection<BlobEntity> entities = new EntityCollection<BlobEntity>();

            AdoAccess.CallProcedure(
                "Blob_GetAllByPartialName",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<String>("PartialName", partialName);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            BlobEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete all blobs by the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void PurgeAllByIdentity(
            IdentityEntity entity)
        {
            AdoAccess.CallProcedure(
                "Blob_PurgeAllByIdentityId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("IdentityId", entity.Id);
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
        private BlobEntity LoadEntity(
            IDataReader reader)
        {
            BlobEntity entity = new BlobEntity(reader.GetBaseEntity())
                {
                    Flags    = reader.GetValue<BlobFlags>("Flags")   ,
                    Name     = reader.GetValue<String>   ("Name")    ,
                    MimeType = reader.GetValue<String>   ("MimeType"),
                    Size     = reader.GetValue<Int64>    ("Size")
                };

            this.InitializeLazyLoading(entity);

            return entity;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the lazy loading of the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private void InitializeLazyLoading(
            BlobEntity entity)
        {
            entity.LazyIdentity = new Lazy<IdentityEntity>(() =>
                {
                    return this.IdentityRepository.GetByBlob(entity);
                });
        }
        #endregion
    }
}
