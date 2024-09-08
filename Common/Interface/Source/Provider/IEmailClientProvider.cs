//-----------------------------------------------------------------------------
// <copyright file="IEmailClientProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Model;

    /// -----------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for communication.
    /// </summary>
    /// -----------------------------------------------------------------------
    public interface IEmailClientProvider : IProvider
    {
        #region Properties
        /// -------------------------------------------------------------------
        /// <summary>
        /// Returns the client threshold for email sending.
        /// </summary>
        /// -------------------------------------------------------------------
        Int32 EmailFailingThreshold { get; }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Returns the system email address.
        /// </summary>
        /// -------------------------------------------------------------------
        String SystemEmailAddress { get; }

        /// -------------------------------------------------------------------
        /// <summary>
        /// Returns the system email name.
        /// </summary>
        /// -------------------------------------------------------------------
        String SystemEmailName { get; }
        #endregion

        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Send email message.
        /// </summary>
        /// -------------------------------------------------------------------
        void Send(
            SmtpMessage emailMessage);
        #endregion
    }
}
