//-----------------------------------------------------------------------------
// <copyright file="BlobContent.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using System;
    using Codev.Core.Common.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the Blob content model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BlobContent : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the model.
        /// </summary>
        ///--------------------------------------------------------------------
        public BlobContent() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date created.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateCreated { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date modified.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateModified { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether this is our current copy of the contents.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsCurrent { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the size of the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int64 Size { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the blob mime type.
        /// </summary>
        ///--------------------------------------------------------------------
        public String MimeType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the contents of the blob.
        /// </summary>
        ///--------------------------------------------------------------------
        public Byte[] Content { get; set; }
        #endregion
    }
}
