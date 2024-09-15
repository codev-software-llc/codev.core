//-----------------------------------------------------------------------------
// <copyright file="ExceptionMessaget.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the messages we pass through to exceptions.
    /// </summary>
    ///------------------------------------------------------------------------
    public struct ExceptionMessage
    {
        ///--------------------------------------------------------------------
        /// <summary>
        /// These are the error messages attached to exceptions.
        /// </summary>
        ///--------------------------------------------------------------------
        public const String IdentityDoesNotExist = "Identity does not exist";
        public const String IdentityAlreadyExist = "Identity already exists";

        public const String DestinationDoesNotExistMessage = "Address does not exist.  Registration is required before signing in.";
        public const String DestinationAlreadyExistMessage = "Address already exists";
        public const String DestinationNonConfirmedMessage = "Address is not confirmed";
        public const String DestinationIsLockedMessage     = "Address cannot be removed";

        public const String ConfirmationCodeInvalidMessage = "Invalid confirmation code";

        public const String SessionDoesNotExistMessage = "Session does not exist";

        public const String BlobDoesNotExistMessage = "Blob does not exist";
        public const String BlobAlreadyExistMessage = "Blob already exists";

        public const String LicenseDoesNotExistMessage     = "License does not exist";
        public const String LicenseHasSubscriptionsMessage = "License has subscriptions";

        public const String PaymentMethodDoesNotExistMessage = "Payment Method does not exist";

        public const String ServiceLinkDoesNotExistMessage = "Service Link does not exist";

        public const String ScheduledTaskDoesNotExist = "Scheduled Task does not exist";

        public const String SettingDoesNotExist = "Setting does not exist";

        public const String SubscriptionDoesNotExist = "Subscription does not exist";
    }
}
