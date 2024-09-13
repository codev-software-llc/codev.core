//-----------------------------------------------------------------------------
// <copyright file="ICommunicationService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Communication
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    /// -----------------------------------------------------------------------
    /// <summary>
    /// This defines the interface for tracking communications.
    /// </summary>
    /// -----------------------------------------------------------------------
    public interface ICommunicationService : IService
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Get all the supported communication types (Email, Sms, ...)
        /// </summary>
        /// -------------------------------------------------------------------
        List<String> GetCommunicationTypes();

        /// -------------------------------------------------------------------
        /// <summary>
        /// Get all the unprocessed communications.
        /// </summary>
        /// -------------------------------------------------------------------
        List<Message> GetUnprocessed();

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
        /// Mark as processed.
        /// </summary>
        /// -------------------------------------------------------------------
        void SetProcessed(
            Message communiction);
        #endregion
    }
}
