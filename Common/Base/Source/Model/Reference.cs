//-----------------------------------------------------------------------------
// <copyright file="Reference.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Text.Json.Serialization;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is used as a reference to an object.  This supports will support
    /// JSON serialization by making sure the constructor is tagged.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Reference<T>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the model.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonConstructor]
        public Reference(
            Int32 id)
        {
            this.Id            = id;
            this.ReferenceType = typeof(T);
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the base identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 Id { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the base identifier.  If this is deserialized, it will
        /// be done so with the constructor, so this will be set.
        /// </summary>
        ///--------------------------------------------------------------------
        [JsonIgnore]
        public Type ReferenceType { get; private set; }
        #endregion
    }
}