//-----------------------------------------------------------------------------
// <copyright file="BlobExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the Blob entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class BlobExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Blob ToModel(
            this BlobEntity entity)
        {
            return new Blob()
                {
                    Id           = entity.Id,
                    DateCreated  = entity.DateCreated.ToLocalDateTime(entity.Identity.TimeZone),
                    DateModified = entity.DateModified.ToLocalDateTime(entity.Identity.TimeZone),
                    MimeType     = entity.MimeType,
                    Name         = entity.Name,
                    Size         = entity.Size
                };
        }
        #endregion
    }
}
