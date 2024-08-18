//-----------------------------------------------------------------------------
// <copyright file="DestinationExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using Codev.Core.Common.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the Destination entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class DestinationExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Destination ToModel(
            this DestinationEntity entity)
        {
            return new Destination()
                {
                    Id                         = entity.Id,
                    DateCreated                = entity.DateCreated.ToLocalDateTime(entity.Identity.TimeZone),
                    DateModified               = entity.DateModified.ToLocalDateTime(entity.Identity.TimeZone),
                    Address                    = entity.Address,
                    DestinationType            = entity.DestinationType,
                    IsPrimary                  = (entity.Flags & DestinationFlags.Primary)    != 0,
                    IsConfirmed                = (entity.Flags & DestinationFlags.Registered) != 0,
                    CanDelete                  = (entity.Flags & DestinationFlags.CanDelete)  != 0,
                    ConfirmationExpirationDate = entity.DateConfirmationExpires.ToLocalDateTime(entity.Identity.TimeZone),
                    ConfirmationSecret         = entity.ConfirmationSecret,
                    Identity                   = entity.Identity.ToModel()
                };
        }
        #endregion
    }
}
