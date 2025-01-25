//-----------------------------------------------------------------------------
// <copyright file="IdentityExtensions.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the Identity entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class IdentityExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Identity ToModel(
            this IdentityEntity entity)
        {
            return new Identity()
                {
                    Id       = entity.Id,
                    TimeZone = entity.TimeZone
                };
        }
        #endregion
    }
}
