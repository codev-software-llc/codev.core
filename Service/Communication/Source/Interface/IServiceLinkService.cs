//-----------------------------------------------------------------------------
// <copyright file="IServiceLinkService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Communication
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;

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
        ServiceLink Add(
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
        ServiceLink GetByTinyUrl(
            String tinyUrl);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get all service links.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ServiceLink> GetAll();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the service links by identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<ServiceLink> GetAll(
            Identity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        void Remove(
            ServiceLink serviceLink);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the service link.
        /// </summary>
        ///--------------------------------------------------------------------
        void Update(
            ServiceLink serviceLink,
            String      detailTypeName,
            String      serializedDetail);
        #endregion
    }
}
