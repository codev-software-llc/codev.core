//-----------------------------------------------------------------------------
// <copyright file="ISmsClientProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
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
    public interface ISmsClientProvider : IProvider
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Send sms message.
        /// </summary>
        /// -------------------------------------------------------------------
        void Send(
            SmsMessage smsMessage);
        #endregion
    }
}
