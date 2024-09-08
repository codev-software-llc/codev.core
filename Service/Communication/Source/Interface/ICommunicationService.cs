//-----------------------------------------------------------------------------
// <copyright file="ICommunicationService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Communication
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;

    /// -----------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for communication.
    /// </summary>
    /// -----------------------------------------------------------------------
    public interface ICommunicationService : IService
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
        /// Receive a communication message.
        /// </summary>
        /// -------------------------------------------------------------------
        void Receive(
            String application,
            String category,
            String subcategory,
            String name,
            String emailAddress,
            String comments);

        /// -------------------------------------------------------------------
        /// <summary>
        /// Get all the unprocessed communications.
        /// </summary>
        /// -------------------------------------------------------------------
        List<Message> GetUnprocessed();

        /// -------------------------------------------------------------------
        /// <summary>
        /// Get all the supported communication types (Email, Sms, ...)
        /// </summary>
        /// -------------------------------------------------------------------
        List<String> GetCommunicationTypes();

        /// -------------------------------------------------------------------
        /// <summary>
        /// Mark as processed.
        /// </summary>
        /// -------------------------------------------------------------------
        void SetProcessed(
            Message communiction);

        /// -------------------------------------------------------------------
        /// <summary>
        /// Send email message.
        /// </summary>
        /// -------------------------------------------------------------------
        void Send(
            SmtpMessage emailMessage);

        /// -------------------------------------------------------------------
        /// <summary>
        /// Send sms message.
        /// </summary>
        /// -------------------------------------------------------------------
        void Send(
            SmsMessage smsMessage);

        /// -------------------------------------------------------------------
        /// <summary>
        /// Retrieve email messages from server that have bounced.
        /// </summary>
        /// -------------------------------------------------------------------
        List<PopMessage> GetBouncedMessages(
            Boolean deleteAfterFetch,
            String  senderAddressFilter);
        #endregion
    }
}
