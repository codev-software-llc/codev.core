//-----------------------------------------------------------------------------
// <copyright file="BlobBehavior.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides the custom behavior for a blob entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class BlobBehavior
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return whether the blob is a transient (temporary) entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Boolean IsTemporary(
            this BlobEntity entity)
        {
            return ((entity.Flags & BlobFlags.Temporary) != 0);
        }
        #endregion
    }
}
