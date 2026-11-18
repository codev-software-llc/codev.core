//-----------------------------------------------------------------------------
// <copyright file="TokenService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Token
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography.X509Certificates;
    using Codev.Core.Service.Clock;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the service for managing JWT tokens.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class TokenService : ITokenService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the service.
        /// </summary>
        ///---------------------------------------------------------------
        public TokenService(
            IClockService clockService)
        {
            this.ClockService = clockService;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the clock service.
        /// </summary>
        ///--------------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a JSON Web Token.
        /// </summary>
        ///--------------------------------------------------------------------
        public String CreateToken(
            String           impersonatedUser,
            String           serviceAccount,
            X509Certificate2 certificate,
            List<String>     claims)
        {
            throw new NotImplementedException();

            /*
            String token = String.Empty;

            // Specify the header.
            //
            var header = new
                {
                    typ = "JWT",
                    alg = "RS256"
                };

            // Specify the expiration time.
            //
            Int32[] times = this.GetExpiryAndIssueDate();

            var claimset = new
                {
                    sub    = impersonatedUser,
                    iss    = serviceAccount,
                    scope  = String.Join(" ", claims),
                    aud    = "https://www.googleapis.com/oauth2/v4/token",
                    iat    = times[0],
                    exp    = times[1],
                };

            // Encoded header.
            //
            var headerSerialized = JsonHelper.Serialize(header);
            var headerBytes      = Encoding.UTF8.GetBytes(headerSerialized);
            var headerEncoded    = Convert.ToBase64String(headerBytes);

            // Encoded claimset.
            //
            var claimsetSerialized = JsonHelper.Serialize(claimset);
            var claimsetBytes      = Encoding.UTF8.GetBytes(claimsetSerialized);
            var claimsetEncoded    = Convert.ToBase64String(claimsetBytes);

            // Input.
            //
            var input      = headerEncoded + "." + claimsetEncoded;
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Signature signing.
            //
            var rsa = certificate.PrivateKey as RSACryptoServiceProvider;

            var cspParam = new CspParameters
                {
                    KeyContainerName = rsa.CspKeyContainerInfo.KeyContainerName,
                    KeyNumber        = rsa.CspKeyContainerInfo.KeyNumber == KeyNumber.Exchange ? 1 : 2
                };

            var aescsp           = new RSACryptoServiceProvider(cspParam) { PersistKeyInCsp = false };
            var signatureBytes   = aescsp.SignData(inputBytes, "SHA256");
            var signatureEncoded = Convert.ToBase64String(signatureBytes);

            // This is the token.
            //
            String jwt = headerEncoded + "." + claimsetEncoded + "." + signatureEncoded;

            return jwt;
            */
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the expiration values for the token.
        /// </summary>
        ///--------------------------------------------------------------------
        /*
        private Int32[] GetExpiryAndIssueDate()
        {
            Instant instantBase = Instant.FromUtc(1970, 1, 1, 0, 0, 0);
            Instant instantNow  = this.ClockService.GetCurrentInstant();

            Duration duration = instantNow.Minus(instantBase);

            Int32 iat = Convert.ToInt32(duration.TotalSeconds);
            Int32 exp = Convert.ToInt32(instantNow.Plus(Duration.FromSeconds(55)).Minus(instantBase).TotalSeconds);

            return new[] { iat, exp };
        }
        */
        #endregion
    }
}
