//-----------------------------------------------------------------------------
// <copyright file="JWTHelper.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Microsoft.IdentityModel.Tokens;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This will generate a JWT (token).
    /// </summary>
    ///------------------------------------------------------------------------
    public static class JWTHelper
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a JWT Token.
        /// </summary>
        ///--------------------------------------------------------------------
        public static String CreateJWT(
            SymmetricSecurityKey signingKey,
            String               identityId,
            String               emailAddress,
            String               sessionId,
            String               issuer,
            String               audience)
        {
            Claim[] claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub  , identityId),
                    new Claim(JwtRegisteredClaimNames.Email, emailAddress),
                    new Claim("sid"                        , sessionId),
                    new Claim("role"                       , "User")
                };

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(20),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
