//-----------------------------------------------------------------------------
// <copyright file="INotifyService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Notify
{
    using System.Threading.Tasks;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    /// -----------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for notifications.
    /// </summary>
    /// -----------------------------------------------------------------------
    public interface INotifyService : IService
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Send email message.
        /// </summary>
        /// -------------------------------------------------------------------
        Task SendAsync(
            SmtpMessage message);

        /// -------------------------------------------------------------------
        /// <summary>
        /// Send sms (text) message.
        /// </summary>
        /// -------------------------------------------------------------------
        void Send(
            SmsMessage smsMessage);
        #endregion
    }
}
