//-----------------------------------------------------------------------------
// <copyright file="BlobRepository.cs" company="Codev Software, LLC">
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
    /// This implements the repository for blob storage.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BlobRepository : BaseRepository, IBlobRepository
    {
        #region Constructors
        ///--------------------------------------------------------------
        /// <summary>
        /// Instantiate the repository object.
        /// </summary>
        ///--------------------------------------------------------------
        public BlobRepository(
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
        /// Add a new entity to the store.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Add(
            BlobEntity entity)
        {
            ValidateRepository<BlobEntity>.Add(entity);

            SqliteAccess.CallStatement(
                "INSERT INTO [Blobs] ([IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Name], [MimeType], [Size]) VALUES (1, ?, ?, ?, ?, ?, ?, ?)",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<BlobFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>  ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>  ("DateModified", entity.DateModified);
                        command.AddInputParameter<Int32>    ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<String>   ("Name"        , entity.Name        );
                        command.AddInputParameter<String>   ("MimeType"    , entity.MimeType    );
                        command.AddInputParameter<Int64>    ("Size"        , entity.Size        );
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
            BlobEntity entity)
        {
            ValidateRepository<BlobEntity>.Delete(entity);

            SqliteAccess.CallStatement(
                "UPDATE [Blobs] SET [IsActive] = 0 WHERE [RowId] = ?",
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

            SqliteAccess.CallStatement(
                "DELETE FROM [Blobs] WHERE [RowId] = ?",
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

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Name], [MimeType], [Size], [Content] FROM [Blobs]",
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
        /// Retrieve the entity by name.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public BlobEntity GetById(
            Int32 entityId)
        {
            ValidateRepository<BlobEntity>.GetById(entityId);

            BlobEntity entity = null;

            SqliteAccess.CallStatement(
                "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Name], [MimeType], [Size], [Content] FROM [Blobs] WHERE [RowId] = ?",
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

            SqliteAccess.CallStatement(
                "UPDATE [Blobs] SET [Flags] = ?, [DateCreated] = ?, [DateModified] = ?, [IdentityId] = ?, [Name] = ?, [MimeType] = ?, [Size] = ? WHERE [RowId] = @RowId",
                this.DataSource,
                (command) =>
                    {
                        command.AddInputParameter<BlobFlags>("Flags"       , entity.Flags       );
                        command.AddInputParameter<Instant>  ("DateCreated" , entity.DateCreated );
                        command.AddInputParameter<Instant>  ("DateModified", entity.DateModified);
                        command.AddInputParameter<Int32>    ("IdentityId"  , entity.Identity.Id );
                        command.AddInputParameter<String>   ("Name"        , entity.Name        );
                        command.AddInputParameter<String>   ("MimeType"    , entity.MimeType    );
                        command.AddInputParameter<Int64>    ("Size"        , entity.Size        );
                        command.AddInputParameter<Int32>    ("RowId"       , entity.Id          );
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
        /// Retrieve the current blob contents.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public void CancelDrafts(
            BlobEntity blobEntity)
        {
            SqliteAccess.CallStatement(
                "DELETE FROM [BlobContents] WHERE ([BlobId] = ?) AND (([Flags] & 1) = 0)",
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
        /// Retrieve the entity by its unique name.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public BlobEntity GetByName(
            String name)
        {
            BlobEntity blob = null;

            String query = "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Name] FROM [Blobs] WHERE [Name] = @Name";

            SqliteAccess.CallStatement(
                query,
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
        /// Retrieve the entity by its content.
        /// </summary>
        ///-------------------------------------------------------------------- 
        public BlobEntity GetByBlobContent(
            BlobContentEntity blobContent)
        {
            BlobEntity blob = null;

            String query = "SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Name] FROM [Blobs] WHERE [] = @Name";

            SqliteAccess.CallStatement(
                query,
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
        /// Retrieve the entity by it's partial name.  This is the same as a
        /// "Contains".
        /// </summary>
        ///-------------------------------------------------------------------- 
        public EntityCollection<BlobEntity> GetAllByPartialName(
            String partialName)
        {
            EntityCollection<BlobEntity> entities = new EntityCollection<BlobEntity>();

            String query = String.Format("SELECT [RowId], [RowVersion], [IsActive], [Flags], [DateCreated], [DateModified], [IdentityId], [Name] FROM [Blobs] WHERE [Name] LIKE '%{0}%'", partialName);

            SqliteAccess.CallStatement(
                query,
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
        /// Delete all blobs for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void PurgeAllByIdentity(
            IdentityEntity entity)
        {
            SqliteAccess.CallStatement(
                "DELETE FROM [Blobs] WHERE [IdentityId] = ?",
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
        /// Load the entity..
        /// </summary>
        ///--------------------------------------------------------------------
        private BlobEntity LoadEntity(
            IDataReader reader)
        {
            BlobEntity entity = new BlobEntity(
                reader.GetBaseEntity())
                    {
                        Flags    = reader.GetValue<BlobFlags>("Flags")   ,
                        Name     = reader.GetValue<String>   ("Name")    ,
                        MimeType = reader.GetValue<String>   ("MimeType"),
                        Size     = reader.GetValue<Int64>    ("Size")
                    };

            return entity;
        }
        #endregion
    }
}
