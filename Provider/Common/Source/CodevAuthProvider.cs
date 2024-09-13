//-----------------------------------------------------------------------------
// <copyright file="CodevAuthProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Authentication
{
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IProvider interface for the authentication
    /// provider.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class CodevAuthProvider : IAuthenticationProvider
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the authentication provider.
        /// </summary>
        ///---------------------------------------------------------------
        public CodevAuthProvider(
            IIdentityRepository identityRepository)
        {
            this.IdentityRepository = identityRepository;
        }
        #endregion

        #region Properties
        /// -------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for storing identities.
        /// </summary>
        /// -------------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }
        #endregion
    }
}
