//-----------------------------------------------------------------------------
// <copyright file="IEmailServerProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Model;

    /// -----------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for communication.
    /// </summary>
    /// -----------------------------------------------------------------------
    public interface IEmailServerProvider : IProvider
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Retrieve email messages from server that have bounced.
        /// </summary>
        /// -------------------------------------------------------------------
        IEnumerable<PopMessage> GetBouncedMessages(
            Boolean deleteAfterFetch,
            String  senderAddressFilter);
        #endregion
    }
}
