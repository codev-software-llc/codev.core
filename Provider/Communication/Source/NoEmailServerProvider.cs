//-----------------------------------------------------------------------------
// <copyright file="NoEmailSererProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Communication
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IEMailServer interface for POP3 email processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public class NoEmailServerProvider : IEmailServerProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the server object.
        /// </summary>
        ///--------------------------------------------------------------------
        public NoEmailServerProvider()
        {
        }
        #endregion

        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Retrieve email messages from server.
        /// </summary>
        /// -------------------------------------------------------------------
        public IEnumerable<PopMessage> GetBouncedMessages(
            Boolean deleteAfterFetch,
            String  senderAddressFilter)
        {
            return new List<PopMessage>();
        }
        #endregion
    }
}
