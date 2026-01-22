//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkExtensions.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the ServiceLink entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class ServiceLinkExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static ServiceJob ToModel(
            this ServiceLinkEntity entity)
        {
            return new ServiceJob()
                {
                    Id           = entity.Id,
                    DateCreated  = entity.DateCreated.ToLocalDateTime(entity.Identity.TimeZone),
                    DateModified = entity.DateModified.ToLocalDateTime(entity.Identity.TimeZone),
                    TinyUrl      = entity.TinyUrl,
                    DetailType   = entity.DetailType,
                    Detail       = entity.Detail
                };
        }
        #endregion
    }
}
