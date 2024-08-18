//-----------------------------------------------------------------------------
// <copyright file="IConfigurationService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;

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
