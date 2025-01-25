//-----------------------------------------------------------------------------
// <copyright file="NoSmsProvider.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Communication
{
    using Codev.Core.Interface;
    using Codev.Core.Model;

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
