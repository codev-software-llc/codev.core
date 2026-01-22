//-----------------------------------------------------------------------------
// <copyright file="IServiceLinkService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.ServiceLink
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the means to manage service links.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IServiceLinkService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a service link.
        /// </summary>
        ///--------------------------------------------------------------------
        ServiceJob Add(
            Identity identity,
            String   detailTypeName,
            String   serializedDetail);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Delete all service links by the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        void DeleteAll(
            Identity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the service link by tiny url.
        /// </summary>
        ///--------------------------------------------------------------------
        ServiceJob GetByTinyUrl(
            String tinyUrl);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get all service links.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ServiceJob> GetAll();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the service links by identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ServiceJob> GetAll(
            Identity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        void Remove(
            ServiceJob serviceLink);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        void Update(
            ServiceJob serviceLink,
            String     detailTypeName,
            String     serializedDetail);
        #endregion
    }
}
