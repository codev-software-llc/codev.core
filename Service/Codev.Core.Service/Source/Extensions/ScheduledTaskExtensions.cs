//-----------------------------------------------------------------------------
// <copyright file="ScheduledTaskExtensions.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the extensions for the ScheduledTask entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class ScheduledTaskExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to model.
        /// </summary>
        ///--------------------------------------------------------------------
        public static TaskJob ToModel(
            this ScheduledTaskEntity entity)
        {
            DateTimeZone dtz = NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault();

            return new TaskJob()
                {
                    Id              = entity.Id,
                    DateCreated     = entity.DateCreated.ToLocalDateTime(dtz),
                    DateModified    = entity.DateModified.ToLocalDateTime(dtz),
                    NextAttentionAt = entity.AttentionAt.ToLocalDateTime(dtz),
                    Priority        = entity.Priority,
                    Category        = entity.Category,
                    DetailType      = entity.DetailType,
                    Detail          = entity.Detail
                };
        }
        #endregion
    }
}
