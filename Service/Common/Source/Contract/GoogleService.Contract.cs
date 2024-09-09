//-----------------------------------------------------------------------------
// <copyright file="GoogleService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
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
    public partial class GoogleService : IGoogleService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new destination.
        /// </summary>
        ///--------------------------------------------------------------------
        String IGoogleService.CreateToken(
            String           impersonatedUser,
            String           serviceAccount,
            X509Certificate2 certificate,
            List<String>     claims)
        {
            try
            {
                Validation.ValidateParameter<String>          ("impersonatedUser", impersonatedUser);
                Validation.ValidateParameter<String>          ("serviceAccount"  , serviceAccount  );
                Validation.ValidateParameter<X509Certificate2>("certificate"     , certificate     );
                Validation.ValidateParameter<List<String>>    ("claims"          , claims          );

                String token = String.Empty;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    token = this.CreateToken(impersonatedUser, serviceAccount, certificate, claims);

                    work.Commit();
                }

                return token;
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
