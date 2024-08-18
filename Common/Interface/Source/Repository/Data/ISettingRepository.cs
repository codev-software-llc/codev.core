//-----------------------------------------------------------------------------
// <copyright file="ISettingRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using System;
    using Codev.Core.Common.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the means to do key-value pair configuration
    /// persistence.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface ISettingRepository : IRepository<SettingEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the setting value.
        /// </summary>
        ///--------------------------------------------------------------------
        SettingEntity GetByName(
            String settingName);
        #endregion
    }
}
