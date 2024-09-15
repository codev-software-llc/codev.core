//-----------------------------------------------------------------------------
// <copyright file="Destination.cs" company="Codev Software, LLC">
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
    /// This defines the Destination model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Destination : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Destination() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the destination is primary.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsPrimary { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the destination is confirmed as valid.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsConfirmed { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the destination is registered.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean IsRegistered { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the destination can be deleted.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean CanDelete { get; set; }

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
        /// Get or set the destination confirmation date expiration.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDateTime ConfirmationExpirationDate { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the destination address.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Address { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the confirmation secret.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ConfirmationSecret { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the destination type.
        /// </summary>
        ///--------------------------------------------------------------------
        public DestinationType DestinationType { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Identity Identity { get; set; }
        #endregion
    }
}
