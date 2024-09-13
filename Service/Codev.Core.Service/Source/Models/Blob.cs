//-----------------------------------------------------------------------------
// <copyright file="Blob.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the Blob model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Blob : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the model.
        /// </summary>
        ///--------------------------------------------------------------------
        public Blob() : base()
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
        /// Get or set the blob name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

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
        #endregion
    }
}
