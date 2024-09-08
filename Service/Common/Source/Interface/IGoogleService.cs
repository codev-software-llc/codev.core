//-----------------------------------------------------------------------------
// <copyright file="IGoogleService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface manages google interactions.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IGoogleService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a JSON Web Token.
        /// </summary>
        ///--------------------------------------------------------------------
        String CreateToken(
            String           impersonatedUser,
            String           serviceAccoiunt,
            X509Certificate2 certificate,
            List<String>     claims);
        #endregion
    }
}
