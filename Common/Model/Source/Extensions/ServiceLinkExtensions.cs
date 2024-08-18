//-----------------------------------------------------------------------------
// <copyright file="ServiceLinkExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using Codev.Core.Common.Base;

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
        public static ServiceLink ToModel(
            this ServiceLinkEntity entity)
        {
            return new ServiceLink()
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
