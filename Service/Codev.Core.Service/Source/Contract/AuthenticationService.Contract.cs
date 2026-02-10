//-----------------------------------------------------------------------------
// <copyright file="AuthenticationService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Authentication
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Base;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the contract layer for the Authentication Service.
    /// </summary>
    ///------------------------------------------------------------------------
    public partial class AuthenticationService : IAuthenticationService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination IAuthenticationService.AddDestination(
            Reference<Identity> identityReference,
            String              address,
            DestinationType     destinationType,
            Boolean             isPrimary)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
            Validation.ValidateParameter<String>             ("address"          , address          );

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(address, destinationType);

                    if (destinationEntity == null)
                    {
                        return this.AddDestination(identityEntity, address, destinationType, isPrimary);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.Duplicate, "Destination already exists");
                    }
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Clear all sessions that are prior to the specified instant.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAuthenticationService.ClearSessions(
            Instant instant)
        {
            Validation.ValidateParameter<Instant>("instant", instant);

            try
            {
                this.SessionRepository.PurgeAllExpiredSessions(instant);
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// This will mark the destination as needing a confirmation.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination IAuthenticationService.ConfirmRequest(
            Reference<Destination> destinationReference)
        {
            Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);

            try
            {
                DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                if (destinationEntity != null)
                {
                    return this.ConfirmRequest(destinationEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.DestinationDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Confirm the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        Session IAuthenticationService.Confirm(
            Reference<Destination> destinationReference,
            String                 confirmationSecret,
            Duration               expiration)
        {
            Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);
            Validation.ValidateParameter<String>                ("confirmationSecret"  , confirmationSecret  );
            Validation.ValidateParameter<Duration>              ("expiration"          , expiration          );

            try
            {
                DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                if (destinationEntity != null)
                {
                    return this.Confirm(destinationEntity, confirmationSecret, expiration);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.DestinationDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination IAuthenticationService.Get(
            String destinationAddress)
        {
            Validation.ValidateParameter<String>("destinationAddress", destinationAddress);

            try
            {
                DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(destinationAddress, DestinationType.Any);

                if (destinationEntity != null)
                {
                    return this.GetDestination(destinationEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.DestinationDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        Identity IAuthenticationService.GetIdentity(
            Reference<Identity> identityReference)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    return identityEntity.ToModel();
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination IAuthenticationService.GetDestination(
            Reference<Destination> destinationReference)
        {
            Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);

            try
            {
                DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                if (destinationEntity != null)
                {
                    return this.GetDestination(destinationEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.DestinationDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity of the confirmation.
        /// </summary>
        ///--------------------------------------------------------------------
        Identity IAuthenticationService.GetIdentityByConfirmationSecret(
            String confirmationSecret)
        {
            Validation.ValidateParameter<String>("confirmationSecret", confirmationSecret);

            try
            {
                DestinationEntity destinationEntity = this.DestinationRepository.GetByConfirmationSecret(confirmationSecret);

                if (destinationEntity != null)
                {
                    return destinationEntity.Identity.ToModel();
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.DestinationDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the sesssion by the unique identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        Session IAuthenticationService.GetSession(
            Guid sessionId)
        {
            try
            {
                SessionEntity sessionEntity = this.SessionRepository.GetBySessionId(sessionId);

                if (sessionEntity != null)
                {
                    return sessionEntity.ToModel();
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.SessionDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all destinations for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        List<Destination> IAuthenticationService.GetIdentityDestinations(
            Reference<Identity> identityReference)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    return this.GetIdentityDestinations(identityEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity of the session.
        /// </summary>
        ///--------------------------------------------------------------------
        Identity IAuthenticationService.GetIdentityBySessionSecret(
            String sessionSecret)
        {
            Validation.ValidateParameter<String>("sessionSecret", sessionSecret);

            try
            {
                SessionEntity sessionEntity = this.SessionRepository.GetBySessionSecret(sessionSecret);
                
                if (sessionEntity != null)
                {
                    return sessionEntity.Identity.ToModel();
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.SessionDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Login the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination IAuthenticationService.Login(
            String          address,
            DestinationType destinationType,
            String          applicationName,
            Duration        expiration)
        {
            Validation.ValidateParameter<String>  ("address"        , address        );
            Validation.ValidateParameter<String>  ("applicationName", applicationName);
            Validation.ValidateParameter<Duration>("expiration"     , expiration     );

            try
            {
                DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(address, destinationType);

                if (destinationEntity != null)
                {
                    return this.Login(destinationEntity, expiration);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.DestinationDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Logout the session.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAuthenticationService.Logout(
            String  sessionSecret,
            Boolean logoutAll)
        {
            Validation.ValidateParameter<String>("sessionSecret", sessionSecret);

            try
            {
                SessionEntity sessionEntity = this.SessionRepository.GetBySessionSecret(sessionSecret);

                if (sessionEntity != null)
                {
                    this.Logout(sessionEntity, logoutAll);
                }
                else
                {
                    //
                    // Don't throw any exceptions.  Assume we are logged
                    // out.
                    //
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Register a destination.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination IAuthenticationService.Register(
            String   emailAddress,
            Duration expiration,
            String   serializedData)
        {
            Validation.ValidateParameter<String>  ("emailAddress", emailAddress);
            Validation.ValidateParameter<Duration>("expiration"  , expiration  );

            serializedData = Validation.ValidateDefault<String>("serializedData", serializedData, String.Empty);

            try
            {
                DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(emailAddress, DestinationType.Email);

                if (destinationEntity == null)
                {
                    return this.Register(emailAddress, expiration, serializedData);
                }

                return this.Register(destinationEntity, expiration);
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAuthenticationService.RemoveDestination(
            Reference<Destination> destinationReference)
        {
            Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);

            try
            {
                DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                if (destinationEntity != null)
                {
                    this.RemoveDestination(destinationEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.DestinationDoesNotExistMessage);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAuthenticationService.RemoveIdentity(
            Reference<Identity> identityReference)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    this.IdentityRepository.Purge(identityEntity);
                }
                else
                {
                    //
                    // Do not throw the exception, as we will treat a non-
                    // existent entity as being removed.
                    //
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the destination as primary.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAuthenticationService.SetPrimary(
            Reference<Destination> destinationReference)
        {
            Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);

            try
            {
                DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                if (destinationEntity != null)
                {
                    this.SetPrimary(destinationEntity);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the time zone for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAuthenticationService.SetTimeZone(
            Reference<Identity> identityReference,
            DateTimeZone        timeZone)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
            Validation.ValidateParameter<DateTimeZone>       ("timeZone"         , timeZone         );

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    this.SetTimeZone(identityEntity, timeZone);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Update the serialized data for the identity.  This is application
        /// defined.
        /// </summary>
        ///--------------------------------------------------------------------
        void IAuthenticationService.UpdateSerializedData(
            Reference<Identity> identityReference,
            String              serializedData)
        {
            Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

            serializedData = Validation.ValidateDefault<String>("serializedData", serializedData, String.Empty);

            try
            {
                IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                if (identityEntity != null)
                {
                    this.UpdateSerializedData(identityEntity, serializedData);
                }
                else
                {
                    throw new CoreLogicException(CoreErrorCode.DoesNotExist, ExceptionMessage.IdentityDoesNotExist);
                }
            }
            catch (CoreDataException cde)
            {
                throw new CoreServiceException(cde.ErrorCode, cde);
            }
            catch (CoreProviderException cpe)
            {
                throw new CoreServiceException(cpe.ErrorCode, cpe);
            }
            catch (CoreLogicException cle)
            {
                throw new CoreServiceException(cle.ErrorCode, cle);
            }
            catch (Exception e)
            {
                throw new CoreServiceException(CoreErrorCode.InternalFailure, e);
            }
        }
        #endregion
    }
}
