//-----------------------------------------------------------------------------
// <copyright file="IScheduledTaskRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for a scheduled task repository.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IScheduledTaskRepository : IRepository<ScheduledTaskEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the scheduled tasks that share the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<ScheduledTaskEntity> GetAllByIdentity(
            IdentityEntity identity);
        #endregion
    }
}
