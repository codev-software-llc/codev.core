//-----------------------------------------------------------------------------
// <copyright file="IServiceLinkRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the additional CRUD methods for a service link.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IServiceLinkRepository : IRepository<ServiceLinkEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the service links that share the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<ServiceLinkEntity> GetAllByIdentity(
            IdentityEntity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the service link using the tiny url.
        /// </summary>
        ///--------------------------------------------------------------------
        ServiceLinkEntity GetByTinyUrl(
            String tinyUrl);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove all service links that share the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        void PurgeAllByIdentity(
            IdentityEntity identity);
        #endregion
    }
}
