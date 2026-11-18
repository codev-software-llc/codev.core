//-----------------------------------------------------------------------------
// <copyright file="IConfigurationService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Configuration
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines for managing configurations.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IConfigurationService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the application version.
        /// </summary>
        ///--------------------------------------------------------------------
        String GetVersion();

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set a value.  If this doesn't exist it will be added to the store.
        /// </summary>
        ///--------------------------------------------------------------------
        void SetValue<T>(
            String settingName,
            T     settingValue);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the setting value.
        /// </summary>
        ///--------------------------------------------------------------------
        T GetValue<T>(
            String settingName);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all known enumerations types.
        /// </summary>
        ///--------------------------------------------------------------------
        List<EnumType> GetAllEnumTypes(
            String applicationName);
        #endregion
    }
}
