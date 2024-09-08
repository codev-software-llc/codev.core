//-----------------------------------------------------------------------------
// <copyright file="BlobContentRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Repository.Sqlite
{
    using System;
    using System.Data;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the repository for blob storage.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BlobContentRepository : BaseRepository, IBlobContentRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public BlobContentRepository(
            IDataSource     dataSource,
            IBlobRepository blobRepository) : base(dataSource)
        {
            this.BlobRepository = blobRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the blob repository.
        /// </summary>
        ///--------------------------------------------------------------------
        private IBlobRepository BlobRepository { get; set; }
        #endregion

        #region Methods (IRepository)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new entity to the store.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            BlobContentEntity entity)
        {
            ValidateRepository<BlobContentEntity>.Add(entity);

            SqliteAccess.CallStatement(
                "INSERT INTO [BlobContents] ([IsActive], [Flags], [DateCreated], [DateModified], [BlobId], [MimeType], [Size], [Content]) VALUES (1, ?, ?, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<BlobContentFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>         ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>         ("DateModified", entity.DateModified);
                        command.AddInputParameter<Int32>           ("BlobId"      , entity.Blob.Id     );
                        command.AddInputParameter<String>          ("MimeType"    , entity.MimeType    );
                        command.AddInputParameter<Int64>           ("Size"        , entity.Size        );
                        command.AddInputParameter<Byte[]>          ("Content"     , entity.Content     );
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
            BlobContentEntity entity)
        {
            ValidateRepository<BlobContentEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [BlobContents] SET [IsActive] = 0 WHERE [RowId] = ?",
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
            BlobContentEntity entity)
        {
            ValidateRepository<BlobContentEntity>.Purge(entity);

            SqliteAccess.CallStatement(
                "DELETE FROM [BlobContents] WHERE [RowId] = ?",
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
        public EntityCollection<BlobContentEntity> GetAll()
        {
            ValidateRepository<BlobContentEntity>.GetAll();

            EntityCollection<BlobContentEntity> entities = new EntityCollection<BlobContentEntity>();

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [BlobId], [MimeType], [Size], [Content] FROM [BlobContents]",
                this.DataSource,
                (command) =>
                    {
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            BlobContentEntity entity = this.LoadEntity(reader);

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
        public BlobContentEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<BlobContentEntity>.GetById(entityId);

            BlobContentEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [BlobId], [MimeType], [Size], [Content] FROM [BlobContents] WHERE [RowId] = ?",
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
            BlobContentEntity entity)
        {
            ValidateRepository<BlobContentEntity>.Update(entity);

            SqliteAccess.CallStatement(
                "UPDATE [BlobContents] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [IdentityId] = ?, [MimeType] = ?, [Size] = ?, [Content] = ? WHERE [RowId] = @RowId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<BlobContentFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>         ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>         ("DateModified", entity.DateModified);
                        command.AddInputParameter<Int32>           ("BlobId"      , entity.Blob.Id     );
                        command.AddInputParameter<String>          ("MimeType"    , entity.MimeType    );
                        command.AddInputParameter<Int64>           ("Size"        , entity.Size        );
                        command.AddInputParameter<Byte[]>          ("Content"     , entity.Content     );
                        command.AddInputParameter<Int32>           ("RowId"       , entity.Id          );
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
        /// Retrieve the entity by it's partial name.  This is the same as a
        /// "Contains".
        /// </summary>
        ///-------------------------------------------------------------------- 
        public EntityCollection<BlobContentEntity> GetAllByBlob(
            BlobEntity blobEntity)
        {
            EntityCollection<BlobContentEntity> entities = new EntityCollection<BlobContentEntity>();

            String query = String.Empty;

                query = String.Format("SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [BlobId], [MimeType], [Size], [Content] FROM [BlobContents] WHERE [BlobId] = ?", blobEntity.Id);

            SqliteAccess.CallStatement(
                query,
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("BlobId", blobEntity.Id);
                    },
                (command, reader) =>
                    {
                        while (reader.Read())
                        {
                            BlobContentEntity entity = this.LoadEntity(reader);

                            entities.Add(entity);
                        }
                    });

            return entities;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the current blob contents.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public BlobContentEntity GetCurrent(
            BlobEntity blobEntity)
        {
            BlobContentEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [BlobId], [MimeType], [Size], [Content] FROM [BlobContents] WHERE ([RowId] = ?) AND (([Flags] & 1) <> 0)",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("BlobId", blobEntity.Id);
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
        /// set the current blob.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public void SetCurrent(
            BlobContentEntity blobContentEntity)
        {
            SqliteAccess.CallStatement(
                "UPDATE [BlobContents] SET [Flags] = 1 WHERE [RowId] = @RowId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<Int32>("RowId", blobContentEntity.Id);
                    },
                (command) =>
                    {
                    });
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Load the entity..
        /// </summary>
        ///--------------------------------------------------------------------
        private BlobContentEntity LoadEntity(
            IDataReader reader)
        {
            BlobContentEntity entity = new BlobContentEntity(
                reader.GetBaseEntity())
                    {
                        Flags    = reader.GetValue<BlobContentFlags>("Flags")   ,
                        MimeType = reader.GetValue<String>          ("MimeType"),
                        Size     = reader.GetValue<Int64>           ("Size")    ,
                        Content  = reader.GetValue<Byte[]>          ("Content")
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
            BlobContentEntity entity)
        {
            entity.LazyBlob = new Lazy<BlobEntity>(() =>
                {
                    return this.BlobRepository.GetByBlobContent(entity);
                });
        }
        #endregion
    }
}
