//-----------------------------------------------------------------------------
// <copyright file="TokenService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Token
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the google api.
    /// </summary>
    ///------------------------------------------------------------------------
    public partial class TokenService : ITokenService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new destination.
        /// </summary>
        ///--------------------------------------------------------------------
        String ITokenService.CreateToken(
            String           impersonatedUser,
            String           serviceAccount,
            X509Certificate2 certificate,
            List<String>     claims)
        {
            Validation.ValidateParameter<String>          ("impersonatedUser", impersonatedUser);
            Validation.ValidateParameter<String>          ("serviceAccount"  , serviceAccount  );
            Validation.ValidateParameter<X509Certificate2>("certificate"     , certificate     );
            Validation.ValidateParameter<List<String>>    ("claims"          , claims          );

            try
            {
                return this.CreateToken(impersonatedUser, serviceAccount, certificate, claims);
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
