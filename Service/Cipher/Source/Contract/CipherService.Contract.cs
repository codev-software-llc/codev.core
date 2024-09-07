//-----------------------------------------------------------------------------
// <copyright file="CipherService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Cipher
{
    using System;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;
    using Codev.Core.Repository.Ado;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the Cipher Service.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class CipherService : ICipherService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Encrypt a clear-text string into the encrypted string.
        /// </summary>
        ///--------------------------------------------------------------------
        String ICipherService.EncryptString(
            String clearText)
        {
            try
            {
                Validation.ValidateParameter<String>("clearText", clearText);

                String value = String.Empty;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    value = this.EncryptString(clearText);

                    work.Commit();
                }

                return value;
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
        /// This will decrypt the encrypted string back to clear text.
        /// </summary>
        ///--------------------------------------------------------------------
        String ICipherService.DecryptString(
            String encryptedText)
        {
            try
            {
                Validation.ValidateParameter<String>("encryptedText", encryptedText);

                String value = String.Empty;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    value = this.DecryptString(encryptedText);

                    work.Commit();
                }

                return value;
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
