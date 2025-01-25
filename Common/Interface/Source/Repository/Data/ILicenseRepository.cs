//-----------------------------------------------------------------------------
// <copyright file="ILicenseRepository.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the licensing access.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ILicenseRepository : IRepository<LicenseEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return all licenses for the application.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<LicenseEntity> GetAllByApplication(
            String  applicationName);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the license by the license subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        LicenseEntity GetBySubscription(
            SubscriptionEntity subscription);
        #endregion
    }
}
