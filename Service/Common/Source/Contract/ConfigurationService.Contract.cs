//-----------------------------------------------------------------------------
// <copyright file="ConfigurationService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;
    using Codev.Core.Repository.Ado;

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
                String version = String.Empty;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    version = this.GetVersion();

                    work.Commit();
                }

                return version;
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
            try
            {
                Validation.ValidateParameter<String>("settingName" , settingName );
                Validation.ValidateParameter<T>     ("settingValue", settingValue);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.SetValue<T>(settingName, settingValue);

                    work.Commit();
                }
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
            try
            {
                Validation.ValidateParameter<String>("settingName", settingName);

                T settingValue = default(T);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    settingValue = this.GetValue<T>(settingName);

                    work.Commit();
                }

                return settingValue;
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
            try
            {
                Validation.ValidateParameter<String>("applicationName", applicationName);

                List<EnumType> enumTypes = new List<EnumType>();

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    enumTypes = this.GetAllEnumTypes(applicationName);

                    work.Commit();
                }

                return enumTypes;
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
