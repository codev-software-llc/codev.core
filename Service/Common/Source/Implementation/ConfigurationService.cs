//-----------------------------------------------------------------------------
// <copyright file="ConfigurationService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IConfigurationService.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ConfigurationService : BaseService, IConfigurationService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the service.
        /// </summary>
        ///---------------------------------------------------------------
        public ConfigurationService(
            ICoreUnitOfWork     unitOfWork,
            ISettingRepository  settingRepository,
            IEnumTypeRepository enumTypeRepository,
            IClockService       clockService) : base(unitOfWork)
        {
            Validation.ValidateParameter<ISettingRepository> ("settingRepository" , settingRepository );
            Validation.ValidateParameter<IEnumTypeRepository>("enumTypeRepository", enumTypeRepository);
            Validation.ValidateParameter<IClockService>      ("clockService"      , clockService      );

            this.SettingRepository  = settingRepository;
            this.EnumTypeRepository = enumTypeRepository;
            this.ClockService       = clockService;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the repository for persisting settings.
        /// </summary>
        ///--------------------------------------------------------------------
        private ISettingRepository SettingRepository { get; set; }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Gets or sets the repository for enumerations.
        /// </summary>
        ///--------------------------------------------------------------------
        private IEnumTypeRepository EnumTypeRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the service for clock.
        /// </summary>
        ///--------------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the core version.
        /// </summary>
        ///--------------------------------------------------------------------
        public String GetVersion()
        {
            String version = String.Empty;

            SettingEntity setting = this.SettingRepository.GetByName("Version");

            version = setting.Value;

            return version;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set a value.  If this doesn't exist it will be added to the store.
        /// </summary>
        ///--------------------------------------------------------------------
        public void SetValue<T>(
            String settingName,
            T      settingValue)
        {
            SettingEntity setting = this.SettingRepository.GetByName(settingName);
           
            String settingValueString = settingValue.ToString();

            Instant instantNow = this.ClockService.GetCurrentInstant();

            if (setting != null)
            {
                setting.Value = settingValueString;
           
                this.SettingRepository.Update(setting);
            }
            else
            {
                setting = new SettingEntity(instantNow)
                    {
                        Flags = SettingFlags.None,
                        Name  = settingName,
                        Value = settingValueString
                    };
           
                this.SettingRepository.Add(setting);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the setting value.
        /// </summary>
        ///--------------------------------------------------------------------
        public T GetValue<T>(
            String settingName)
        {
            T settingValue = default(T);
            
            SettingEntity settings = this.SettingRepository.GetByName(settingName);
            
            if (settings != null)
            {
                settingValue = (T)Convert.ChangeType(settings.Value, typeof(T));
            }
            
            return settingValue;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all known enumerations types.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<EnumType> GetAllEnumTypes(
            String applicationName)
        {
            List<EnumType> enumTypes = new List<EnumType>();

            List<EnumTypeEntity> entities = this.EnumTypeRepository.GetAll().Where(x => x.Application == applicationName).ToList();

            enumTypes = entities.Select(x => x.ToModel()).ToList();

            return enumTypes;
        }
        #endregion
    }
}
