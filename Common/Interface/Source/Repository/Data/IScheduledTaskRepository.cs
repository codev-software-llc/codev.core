//-----------------------------------------------------------------------------
// <copyright file="IScheduledTaskRepository.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using Codev.Core.Base;
    using Codev.Core.Model;

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
