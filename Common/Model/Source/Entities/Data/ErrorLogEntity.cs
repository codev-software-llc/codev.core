//-----------------------------------------------------------------------------
// <copyright file="ErrorLogEntity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Error log entity.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ErrorLogEntity : BaseEntity
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public ErrorLogEntity(
            Instant instantNow) : base(instantNow)
        {
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public ErrorLogEntity(
            BaseEntity baseEntity) : base(baseEntity)
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the entity flags.
        /// </summary>
        ///--------------------------------------------------------------------
        public ErrorLogFlags Flags { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the component type.
        /// </summary>
        ///--------------------------------------------------------------------
        public DiagnosticComponentType ComponentType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the severity of the error.
        /// </summary>
        ///--------------------------------------------------------------------
        public DiagnosticSeverityType SeverityType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the error message.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Message { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the server who logged the error.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ServerName { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set a tag that can be used to group log entries.
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

        #region Properties (Reference)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public virtual IdentityEntity Identity { get; set; }
        #endregion
    }
}
