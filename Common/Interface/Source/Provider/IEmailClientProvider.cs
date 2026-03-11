//-----------------------------------------------------------------------------
// <copyright file="IEmailClientProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System.Threading.Tasks;
    using Codev.Core.Model;

    /// -----------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for communication.
    /// </summary>
    /// -----------------------------------------------------------------------
    public interface IEmailClientProvider : IProvider
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Send email message.
        /// </summary>
        /// -------------------------------------------------------------------
        Task SendAsync(
            SmtpMessage emailMessage);
        #endregion
    }
}
