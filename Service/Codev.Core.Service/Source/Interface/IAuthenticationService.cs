//-----------------------------------------------------------------------------
// <copyright file="IAuthenticationService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Authentication
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface manages the security and authentication of users.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IAuthenticationService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new destination.  This will create a confirmation secret
        /// good for a login or register.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination AddDestination(
            Reference<Identity> identityReference,
            String              address,
            DestinationType     destinationType,
            Boolean             isPrimary);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Clear sessions that are prior to the instant.
        /// </summary>
        ///--------------------------------------------------------------------
        void ClearSessions(
            Instant instant);

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will mark the destination as needing a confirmation.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination ConfirmRequest(
            Reference<Destination> destinationReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Confirm the identity.  Thir removes the confirmation secret and
        /// creates a session for the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Session Confirm(
            Reference<Destination> destinationReference,
            String                 confirmationCode,
            Duration               expiration);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination Get(
            String destinationAddress);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        Identity GetIdentity(
            Reference<Identity> identityReferece);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination GetDestination(
            Reference<Destination> destinationReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity of the confirmation.
        /// </summary>
        ///--------------------------------------------------------------------
        Identity GetIdentityByConfirmationSecret(
            String confirmationSecret);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all destinations for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<Destination> GetIdentityDestinations(
            Reference<Identity> identityReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity of the session.
        /// </summary>
        ///--------------------------------------------------------------------
        Identity GetIdentityBySessionSecret(
            String sessionSecret);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the session by it's unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        Session GetSession(
            Guid sessionId);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Login the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination Login(
            String          address,
            DestinationType destinationType,
            String          applicationName,
            Duration        expiration);

        ///---------------------------------------------------------------------
        /// <summary>
        /// Logout the session.
        /// </summary>
        ///--------------------------------------------------------------------
        void Logout(
            String sessionSecret,
            Boolean  logoutAll);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Register a destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination Register(
            String   emailAddress,
            Duration expiration,
            String   serializedData);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        void RemoveDestination(
            Reference<Destination> destinationReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        void RemoveIdentity(
            Reference<Identity> identityReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the destination as primary.
        /// </summary>
        ///--------------------------------------------------------------------
        void SetPrimary(
            Reference<Destination> destinationReference);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the time zone for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        void SetTimeZone(
            Reference<Identity> identityReference,
            DateTimeZone timeZone);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update application data.
        /// </summary>
        ///--------------------------------------------------------------------
        void UpdateSerializedData(
            Reference<Identity> identityReference,
            String              serializedData);
        #endregion
    }
}
