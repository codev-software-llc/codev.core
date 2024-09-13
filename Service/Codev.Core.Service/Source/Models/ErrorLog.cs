//-----------------------------------------------------------------------------
// <copyright file="ErrorLog.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the ErrorLog model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ErrorLog : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public ErrorLog() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date created.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateCreated { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date modified.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime DateModified { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the component type.
        /// </summary>
        ///--------------------------------------------------------------------
        public DiagnosticComponentType ComponentType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the severity type.
        /// </summary>
        ///--------------------------------------------------------------------
        public DiagnosticSeverityType SeverityType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the message.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Message { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the server name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ServerName { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the tracking tag.
        /// </summary>
        ///--------------------------------------------------------------------
        public String TrackingTag { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the stack trace.
        /// </summary>
        ///--------------------------------------------------------------------
        public String StackTrace { get; set; }
        #endregion
    }
}
