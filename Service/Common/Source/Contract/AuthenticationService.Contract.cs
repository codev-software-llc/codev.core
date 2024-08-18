//-----------------------------------------------------------------------------
// <copyright file="AuthenticationService.Contract.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using System.Collections.Generic;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Model;
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
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
                Validation.ValidateParameter<String>             ("address"          , address          );

                Destination destination = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
            try
            {
                Validation.ValidateParameter<Instant>("instant", instant);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    this.ClearSessions(instant);

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
            try
            {
                Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);

                Destination destination = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        destination = this.ConfirmRequest(destinationEntity);
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
            try
            {
                Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);
                Validation.ValidateParameter<String>                ("confirmationSecret"  , confirmationSecret  );
                Validation.ValidateParameter<Duration>              ("expiration"          , expiration          );

                Session session = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        session = this.Confirm(destinationEntity, confirmationSecret, expiration);
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

            Destination destination = null;

            try
            {
                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(destinationAddress, DestinationType.Any);

                    if (destinationEntity != null)
                    {
                        destination = this.Get(destinationEntity);
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

            return destination;
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

            Identity identity = null;

            try
            {
                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        identity = this.GetIdentity(identityEntity);
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

            return identity;
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

            Destination destination = null;

            try
            {
                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        destination = this.GetDestination(destinationEntity);
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

            return destination;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity of the confirmation.
        /// </summary>
        ///--------------------------------------------------------------------
        Identity IAuthenticationService.GetIdentityByConfirmationSecret(
            String confirmationSecret)
        {
            try
            {
                Validation.ValidateParameter<String>("confirmationSecret", confirmationSecret);

                Identity identity = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    identity = this.GetIdentityByConfirmationSecret(confirmationSecret);

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
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

                List<Destination> destinations = new List<Destination>();

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
            try
            {
                Validation.ValidateParameter<String>("sessionSecret", sessionSecret);

                Identity identity = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    SessionEntity sessionEntity = this.SessionRepository.GetBySessionSecret(sessionSecret);

                    if (sessionEntity != null)
                    {
                        identity = this.GetIdentityBySessionSecret(sessionEntity);
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
            try
            {
                Validation.ValidateParameter<String>  ("address"        , address        );
                Validation.ValidateParameter<String>  ("applicationName", applicationName);
                Validation.ValidateParameter<Duration>("expiration"     , expiration     );

                Destination destination = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetByAddress(address, destinationType);

                    if (destinationEntity != null)
                    {
                        destination = this.Login(destinationEntity, expiration);
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
            try
            {
                Validation.ValidateParameter<String>("sessionSecret", sessionSecret);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    SessionEntity sessionEntity = this.SessionRepository.GetBySessionSecret(sessionSecret);

                    if (sessionEntity != null)
                    {
                        this.Logout(sessionEntity, logoutAll);
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
            try
            {
                Validation.ValidateParameter<String>  ("emailAddress", emailAddress);
                Validation.ValidateParameter<Duration>("expiration"  , expiration  );

                Destination destination = null;

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    destination = this.Register(emailAddress, expiration);

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
            try
            {
                Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        this.RemoveDestination(destinationEntity);
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
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    IdentityEntity identityEntity = this.IdentityRepository.GetById(identityReference.Id);

                    if (identityEntity != null)
                    {
                        this.RemoveIdentity(identityEntity);
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
            try
            {
                Validation.ValidateParameter<Reference<Destination>>("destinationReference", destinationReference);

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
                {
                    DestinationEntity destinationEntity = this.DestinationRepository.GetById(destinationReference.Id);

                    if (destinationEntity != null)
                    {
                        this.SetPrimary(destinationEntity);
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
            try
            {
                Validation.ValidateParameter<Reference<Identity>>("identityReference", identityReference);
                Validation.ValidateParameter<DateTimeZone>       ("timeZone"         , timeZone         );

                using (IUnitOfWork work = new UnitOfWork(this.DataSource))
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
