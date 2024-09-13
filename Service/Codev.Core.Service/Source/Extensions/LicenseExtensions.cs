//-----------------------------------------------------------------------------
// <copyright file="LicenseExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the License entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class LicenseExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static License ToModel(
            this LicenseEntity entity)
        {
            License license = new License()
                {
                    Id           = entity.Id,
                    DateCreated  = entity.DateCreated.ToLocalDateTime(entity.Identity.TimeZone),
                    DateModified = entity.DateModified.ToLocalDateTime(entity.Identity.TimeZone),
                    Application  = entity.Application,
                    Name         = entity.Name,
                    Cost         = entity.Cost
                };

            entity.Features.ForEach(x => license.Features.Add(x));

            return license;
        }
        #endregion
    }
}
