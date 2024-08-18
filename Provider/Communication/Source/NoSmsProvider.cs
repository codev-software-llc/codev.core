//-----------------------------------------------------------------------------
// <copyright file="NoSmsProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider
{
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the ISmsClientProvider interface for SMS processing.
    /// </summary>
    ///------------------------------------------------------------------------
    public class NoSmsProvider : ISmsClientProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the server object.
        /// </summary>
        ///--------------------------------------------------------------------
        public NoSmsProvider()
        {
        }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Send email.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Send(
            SmsMessage message)
        {
            //
            // DO NOTHING.
            //
        }
        #endregion
    }
}
