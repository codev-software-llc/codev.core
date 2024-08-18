//-----------------------------------------------------------------------------
// <copyright file="BlobContentExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using Codev.Core.Common.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the BlobContent entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class BlobContentExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static BlobContent ToModel(
            this BlobContentEntity entity)
        {
            return new BlobContent()
                {
                    Id           = entity.Id,
                    DateCreated  = entity.DateCreated.ToLocalDateTime(entity.Blob.Identity.TimeZone),
                    DateModified = entity.DateModified.ToLocalDateTime(entity.Blob.Identity.TimeZone),
                    IsCurrent    = (entity.Flags & BlobContentFlags.Current) != 0,
                    MimeType     = entity.MimeType,
                    Size         = entity.Size,
                    Content      = entity.Content
                };
        }
        #endregion
    }
}
