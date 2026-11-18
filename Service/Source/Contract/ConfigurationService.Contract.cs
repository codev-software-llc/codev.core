//-----------------------------------------------------------------------------
// <copyright file="ConfigurationService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Configuration
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the Configuration Service.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ConfigurationService : IConfigurationService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the application version.
        /// </summary>
        ///--------------------------------------------------------------------
        String IConfigurationService.GetVersion()
        {
            try
            {
                return this.GetVersion();
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set a value.  If this doesn't exist it will be added to the store.
        /// </summary>
        ///--------------------------------------------------------------------
        void IConfigurationService.SetValue<T>(
            String settingName,
            T      settingValue)
        {
            Validation.ValidateParameter<T>     ("settingValue", settingValue);
            Validation.ValidateParameter<String>("settingName" , settingName );

            try
            {
                this.SetValue<T>(settingName, settingValue);
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the setting value.
        /// </summary>
        ///--------------------------------------------------------------------
        T IConfigurationService.GetValue<T>(
            String settingName)
        {
            Validation.ValidateParameter<String>("settingName", settingName);

            try
            {
                return this.GetValue<T>(settingName);
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all known enumerations types.
        /// </summary>
        ///--------------------------------------------------------------------
        List<EnumType> IConfigurationService.GetAllEnumTypes(
            String applicationName)
        {
            Validation.ValidateParameter<String>("applicationName", applicationName);

            try
            {
                return this.GetAllEnumTypes(applicationName);
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion
    }
}
