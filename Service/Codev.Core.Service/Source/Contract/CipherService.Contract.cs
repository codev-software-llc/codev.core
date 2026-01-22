//-----------------------------------------------------------------------------
// <copyright file="CipherService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Cipher
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;

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
            Validation.ValidateParameter<String>("clearText", clearText);

            try
            {
                return this.EncryptString(clearText);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
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
            Validation.ValidateParameter<String>("encryptedText", encryptedText);

            try
            {
                return this.DecryptString(encryptedText);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion
    }
}
