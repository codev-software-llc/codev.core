//-----------------------------------------------------------------------------
// <copyright file="AuthenticationService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
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
                Destination destination = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(address, destinationType);

                        if (destinationEntity == null)
                        {
                            destination = this.AddDestination(identityEntity, address, destinationType, isPrimary);
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

                    work.Commit();
                }

                return destination;
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    this.SessionRepository.PurgeAllExpiredSessions(instant);

                    work.Commit();
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
        /// This will mark the destination as needing a confirmation.
        /// </summary>
        ///--------------------------------------------------------------------
        Destination IAuthenticationService.ConfirmRequest(
            Reference<Destination> destinationReference)
        {
            Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);

            try
            {
                Destination destination = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByDestination(destinationEntity);

                        if (identityEntity != null)
                        {
                            destinationEntity.Identity = identityEntity;

                            destination = this.ConfirmRequest(destinationEntity);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Destination does not exist");
                    }

                    work.Commit();
                }

                return destination;
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
                Session session = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByDestination(destinationEntity);

                        if (identityEntity != null)
                        {
                            destinationEntity.Identity = identityEntity;

                            session = this.Confirm(destinationEntity, confirmationSecret, expiration);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Destination does not exist");
                    }

                    work.Commit();
                }

                return session;
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
                Destination destination = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(destinationAddress, DestinationType.Any);

                    if (destinationEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByDestination(destinationEntity);

                        if (identityEntity != null)
                        {
                            destinationEntity.Identity = identityEntity;

                            destination = this.GetDestination(destinationEntity);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Destination does not exist");
                    }

                    work.Commit();
                }

                return destination;
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
                Identity identity = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        identity = identityEntity.ToModel();
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                    }

                    work.Commit();
                }

                return identity;
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
                Destination destination = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetById(destinationReference.Id);

                        if (identityEntity != null)
                        {
                            destinationEntity.Identity = identityEntity;

                            destination = this.GetDestination(destinationEntity);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Destination does not exist");
                    }

                    work.Commit();
                }

                return destination;
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
                Identity identity = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetByConfirmationSecret(confirmationSecret);

                    if (destinationEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByDestination(destinationEntity);

                        if (identityEntity != null)
                        {
                            identity = identityEntity.ToModel();
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Destination does not exist");
                    }

                    work.Commit();
                }

                return identity;
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
                List<Destination> destinations = new List<Destination>();

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        destinations = this.GetIdentityDestinations(identityEntity);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                    }

                    work.Commit();
                }

                return destinations;
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
                Identity identity = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    SessionEntity sessionEntity = this.SessionRepository.GetBySessionSecret(sessionSecret);

                    if (sessionEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetBySession(sessionEntity);

                        if (identityEntity != null)
                        {
                            return identityEntity.ToModel();
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Session does not exist");
                    }

                    work.Commit();
                }

                return identity;
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
                Destination destination = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(address, destinationType);

                    if (destinationEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByDestination(destinationEntity);

                        if (identityEntity != null)
                        {
                            destinationEntity.Identity = identityEntity;

                            destination = this.Login(destinationEntity, expiration);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Destination does not exist");
                    }

                    work.Commit();
                }

                return destination;
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
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

                    work.Commit();
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
            Duration expiration)
        {
            Validation.ValidateParameter<String>  ("emailAddress", emailAddress);
            Validation.ValidateParameter<Duration>("expiration"  , expiration  );

            try
            {
                Destination destination = null;

                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(emailAddress, DestinationType.Email);

                    if (destinationEntity == null)
                    {
                        destination = this.Register(emailAddress, expiration);
                    }
                    else
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByDestination(destinationEntity);

                        if (identityEntity == null)
                        {
                            destinationEntity.Identity = identityEntity;

                            destination = this.Register(destinationEntity, expiration);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                        }
                    }

                    work.Commit();
                }

                return destination;
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByDestination(destinationEntity);

                        if (identityEntity != null)
                        {
                            destinationEntity.Identity = identityEntity;

                            this.RemoveDestination(destinationEntity);
                        }
                        else
                        {
                            throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Destination does not exist");
                    }

                    work.Commit();
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
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

                    work.Commit();
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        IdentityEntity identityEntity = this.IdentityRepository.GetByDestination(destinationEntity);

                        if (identityEntity != null)
                        {
                            destinationEntity.Identity = identityEntity;

                            this.SetPrimary(destinationEntity);
                        }
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                    }

                    work.Commit();
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
                using (IUnitOfWork work = this.UnitOfWork.Begin())
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        this.SetTimeZone(identityEntity, timeZone);
                    }
                    else
                    {
                        throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Identity does not exist");
                    }

                    work.Commit();
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
