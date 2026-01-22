//-----------------------------------------------------------------------------
// <copyright file="ISettingRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Model;

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
