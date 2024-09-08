//-----------------------------------------------------------------------------
// <copyright file="ErrorLogExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the ErrorLog entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class ErrorLogExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static ErrorLog ToModel(
            this ErrorLogEntity entity)
        {
            return new ErrorLog()
                {
                    Id            = entity.Id,
                    DateCreated   = entity.DateCreated.ToLocalDateTime(entity.Identity.TimeZone),
                    DateModified  = entity.DateModified.ToLocalDateTime(entity.Identity.TimeZone),
                    ComponentType = entity.ComponentType,
                    SeverityType  = entity.SeverityType,
                    ServerName    = entity.ServerName,
                    Message       = entity.Message,
                    TrackingTag   = entity.TrackingTag,
                    StackTrace    = entity.StackTrace
                };
        }
        #endregion
    }
}
