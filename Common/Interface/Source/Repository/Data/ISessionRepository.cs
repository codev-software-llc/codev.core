//-----------------------------------------------------------------------------
// <copyright file="ISessionRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the repository for persisting session
    /// information.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ISessionRepository : IRepository<SessionEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all the sessions by the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<SessionEntity> GetAllByIdentity(
            IdentityEntity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the session by the session secret.
        /// </summary>
        ///--------------------------------------------------------------------
        SessionEntity GetBySessionSecret(
            String secret);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Purge all expired sessions.
        /// </summary>
        ///--------------------------------------------------------------------
        void PurgeAllExpiredSessions(
            Instant instant);
        #endregion
    }
}
