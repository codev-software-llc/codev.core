//-----------------------------------------------------------------------------
// <copyright file="SessionExtensions.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the Session entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class SessionExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Session ToModel(
            this SessionEntity entity)
        {
            return new Session()
                {
                    Id             = entity.Id,
                    DateCreated    = entity.DateCreated.ToLocalDateTime(entity.Identity.TimeZone),
                    DateExpiration = entity.DateExpiration.ToLocalDateTime(entity.Identity.TimeZone),
                    Identity       = entity.Identity.ToModel(),
                    Secret         = entity.Secret
                };
        }
        #endregion
    }
}
