//-----------------------------------------------------------------------------
// <copyright file="NoSmtpProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Communication
{
    using System;
    using System.Threading.Tasks;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IEMailClient interface for POP3 email processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public class NoSmtpProvider : IEmailClientProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the server object.
        /// </summary>
        ///--------------------------------------------------------------------
        public NoSmtpProvider()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the From address.
        /// </summary>
        ///--------------------------------------------------------------------
        private String FromAddress { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Ger or set the From name.
        /// </summary>
        ///--------------------------------------------------------------------
        private String FromName { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Send email.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task SendAsync(
            SmtpMessage message)
        {
            await Task.CompletedTask;

            //
            // DO NOTHING.
            //
        }
        #endregion
    }
}
