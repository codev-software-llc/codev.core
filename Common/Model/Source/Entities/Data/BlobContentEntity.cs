//-----------------------------------------------------------------------------
// <copyright file="BlobContentEntity.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the Blob Content entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BlobContentEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public BlobContentEntity(
            Instant instantNow) : base(instantNow)
        {
            this.Initialize();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public BlobContentEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
            this.Initialize();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the entity flags.
        /// </summary>
        ///--------------------------------------------------------------------
        public BlobContentFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the size of the content.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int64 Size { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set mime type of the content.
        /// </summary>
        ///--------------------------------------------------------------------
        public String MimeType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set blob content.
        /// </summary>
        ///--------------------------------------------------------------------
        public Byte[] Content { get; set; }
        #endregion

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the blob reference.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual BlobEntity Blob
        {
            get
            {
                return this.LazyBlob.Value;
            }

            set
            {
                this.LazyBlob = new Lazy<BlobEntity>(() => value);
            }
        }
        #endregion

        #region Properties (Lazy Loading)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the lazy loading reference for the blob entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Lazy<BlobEntity> LazyBlob { get; set; }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Initialize the entity.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Initialize()
        {
            this.LazyBlob = new Lazy<BlobEntity>();
        }
        #endregion
    }
}
