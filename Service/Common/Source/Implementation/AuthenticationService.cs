//-----------------------------------------------------------------------------
// <copyright file="AuthenticationService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codev.Core.Common.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Common.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the security authentication service.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class AuthenticationService : BaseService, IAuthenticationService
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Limit the password iteration count.
        /// </summary>
        ///-------------------------------------------------------------------
        private const Int32 PasswordIterationLimit = 7;

        ///--------------------------------------------------------------------
        /// <summary>
        /// Default timeout in minutes for confirmation secrets.
        /// </summary>
        ///-------------------------------------------------------------------
        private const Int32 ConfirmationSecretTimeoutMinutes = 15;

        ///--------------------------------------------------------------------
        /// <summary>
        /// Default session secret length.  This is of base-32.
        /// </summary>
        ///-------------------------------------------------------------------
        private const Int32 SessionSecretLength = 64;

        ///--------------------------------------------------------------------
        /// <summary>
        /// Default timeout in minutes for session secrets.  Our default is
        /// (3) months.
        /// </summary>
        ///--------------------------------------------------------------------
        private const Int32 SessionSecretTimeoutMinutes = 90 * 24 * 60;
        #endregion

        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the service.
        /// </summary>
        ///---------------------------------------------------------------
        public AuthenticationService(
            ICoreUnitOfWork        unitOfWork,
            ISecretProvider        secretProvider,
            IIdentityRepository    identityRepository,
            IDestinationRepository destinationRepository,
            ISessionRepository     sessionRepository,
            IClockService          clockService) : base(unitOfWork)
        {
            Validation.ValidateParameter<ISecretProvider>("secretProvider", secretProvider);

            Validation.ValidateParameter<IIdentityRepository>   ("identityRepository"   , identityRepository   );
            Validation.ValidateParameter<IDestinationRepository>("destinationRepository", destinationRepository);
            Validation.ValidateParameter<ISessionRepository>    ("sessionRepository"    , sessionRepository    );
            Validation.ValidateParameter<IClockService>         ("clockService"         , clockService         );

            this.SecretProvider        = secretProvider;
            this.IdentityRepository    = identityRepository;
            this.DestinationRepository = destinationRepository;
            this.SessionRepository     = sessionRepository;
            this.ClockService          = clockService;
        }
        #endregion

        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for the identities.
        /// </summary>
        ///---------------------------------------------------------------
        private IIdentityRepository IdentityRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for the destinations.
        /// </summary>
        ///---------------------------------------------------------------
        private IDestinationRepository DestinationRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for the sessions.
        /// </summary>
        ///---------------------------------------------------------------
        private ISessionRepository SessionRepository { get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the provider for the authentication secret
        /// generation.
        /// </summary>
        ///---------------------------------------------------------------
        private ISecretProvider SecretProvider{ get; set; }

        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the clock service.
        /// </summary>
        ///---------------------------------------------------------------
        private IClockService ClockService { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Add a new destination to the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Destination AddDestination(
            IdentityEntity  identityEntity,
            String          address,
            DestinationType destinationType,
            Boolean         isPrimary)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            // Clear out the primary destinations if this is the new
            // one.
            //
            DestinationFlags flags = DestinationFlags.None;

            if (isPrimary)
            {
                this.ClearPrimaryDestinations(identityEntity, destinationType);

                flags |= DestinationFlags.Primary;
                flags |= DestinationFlags.Registered;
            }

            if ((destinationType == DestinationType.Phone) || ((flags & DestinationFlags.Primary) == 0))
            {
                flags |= DestinationFlags.CanDelete;
            }

            // Add the destination.
            //
            DestinationEntity destinationEntity = new DestinationEntity(instantNow)
                {
                    Identity                = identityEntity,
                    Flags                   = flags,
                    Address                 = address,
                    DestinationType         = destinationType,
                    ConfirmationSecret      = this.GenerateConfirmationCode(),
                    DateConfirmationExpires = this.GenerateExpiration(Duration.FromDays(30))
                };

            this.DestinationRepository.Add(destinationEntity);

            return destinationEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Clear all sessions prior to the instant.
        /// </summary>
        ///--------------------------------------------------------------------
        public void ClearSessions(
            Instant instant)
        {
            this.SessionRepository.PurgeAllExpiredSessions(instant);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Request a confirmation of the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        public Destination ConfirmRequest(
            DestinationEntity destinationEntity)
        {
            Instant instantNow = this.ClockService.GetCurrentInstant();

            // Update the confirmation code and touch the entity.
            //
            destinationEntity.ConfirmationSecret      = this.GenerateConfirmationCode();
            destinationEntity.DateConfirmationExpires = this.GenerateExpiration(Duration.FromDays(30));

            this.DestinationRepository.Update(destinationEntity);

            return destinationEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Confirm the identity and return the session that is 
        /// established.
        /// </summary>
        ///--------------------------------------------------------------------
        public Session Confirm(
            DestinationEntity destinationEntity,
            String            confirmationSecret,
            Duration          expiratrion)
        {
            if (String.Compare(confirmationSecret, destinationEntity.ConfirmationSecret, StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                Instant instantNow = this.ClockService.GetCurrentInstant();

                // Tag the destination as registered.
                //
                destinationEntity.Flags |= DestinationFlags.Registered;

                destinationEntity.ConfirmationSecret      = String.Empty;
                destinationEntity.DateConfirmationExpires = Instant.MaxValue;

                this.DestinationRepository.Update(destinationEntity);

                // Create a session.
                //
                SessionEntity entity = new SessionEntity(instantNow)
                    {
                        Identity       = destinationEntity.Identity,
                        Flags          = SessionFlags.None,
                        Secret         = this.GenerationSessionSecret(),
                        DateExpiration = this.GenerateExpiration(expiratrion)
                    };

                this.SessionRepository.Add(entity);

                return entity.ToModel();
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.AccessDenied, "Invalid confirmation code");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        public Destination Get(
            DestinationEntity destinationEntity)
        {
             return destinationEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Identity GetIdentity(
            IdentityEntity identityEntity)
        {
            return identityEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the destination.
        /// </summary>
        ///--------------------------------------------------------------------
        public Destination GetDestination(
            DestinationEntity destinationEntity)
        {
            return destinationEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity by the confirmation secret.
        /// </summary>
        ///--------------------------------------------------------------------
        public Identity GetIdentityByConfirmationSecret(
            String confirmationSecret)
        {
            DestinationEntity destination = this.DestinationRepository.GetByConfirmationSecret(confirmationSecret);

            if (destination != null)
            {
                return destination.Identity.ToModel();
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.DoesNotExist, "Destination does not exist");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve the identity by the session.
        /// </summary>
        ///--------------------------------------------------------------------
        public Identity GetIdentityBySessionSecret(
            SessionEntity sessionEntity)
        {
            return sessionEntity.Identity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Retrieve all destinations for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<Destination> GetIdentityDestinations(
            IdentityEntity identityEntity)
        {
            EntityCollection<DestinationEntity> destinations = this.DestinationRepository.GetAllByIdentity(identityEntity);

            return destinations.Select(x => x.ToModel()).ToList();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Login the destination and return the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Destination Login(
            DestinationEntity destinationEntity,
            Duration          expiration)
        {
            if ((destinationEntity.Flags & DestinationFlags.Registered) != 0)
            {
                Instant instantNow = this.ClockService.GetCurrentInstant();

                destinationEntity.DateModified            = instantNow;
                destinationEntity.ConfirmationSecret      = this.GenerateConfirmationCode();
                destinationEntity.DateConfirmationExpires = this.GenerateExpiration(expiration);
           
                this.DestinationRepository.Update(destinationEntity);

                return destinationEntity.ToModel();
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.InvalidOperation, "Destination is not confirmed");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Logout the session.  If the session is permanent then we do
        /// not remove it.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Logout(
            SessionEntity sessionEntity,
            Boolean logoutAll)
        {
            if ((sessionEntity.Flags & SessionFlags.Permanent) == 0)
            {
                this.SessionRepository.Purge(sessionEntity);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Register a destination.  We must register an email primarily as
        /// the main account communication.
        /// </summary>
        ///--------------------------------------------------------------------
        public Destination Register(
            String   emailAddress,
            Duration expiration)
        {
            // Get the email destination.  If it arleady exsits, then make
            // sure that it is not in use.
            //
            DestinationEntity emailDestinationEntity = this.DestinationRepository.GetByAddress(emailAddress, DestinationType.Email);

            if ((emailDestinationEntity != null) && ((emailDestinationEntity.Flags & DestinationFlags.Registered) != 0))
            {
                throw new CoreLogicException(CoreErrorCode.Duplicate, "Email address already exists");
            }

            Instant instantNow = this.ClockService.GetCurrentInstant();

            // If we already have an identity and destination, make sure
            // it's not registered.  If it's not, then we can reuse the entry.
            //
            if (emailDestinationEntity != null)
            {
                if ((emailDestinationEntity.Flags & DestinationFlags.Registered) != 0)
                {
                    throw new CoreLogicException(CoreErrorCode.Duplicate, "Destination already exists");
                }
                else
                {
                    emailDestinationEntity.ConfirmationSecret      = this.GenerateConfirmationCode();
                    emailDestinationEntity.DateConfirmationExpires = this.GenerateExpiration(expiration);
                    emailDestinationEntity.DateModified            = instantNow;

                    this.DestinationRepository.Update(emailDestinationEntity);
                }
            }
            else
            {
                // Add the identity.
                //
                IdentityEntity identity = new IdentityEntity(instantNow)
                    {
                        Flags                = IdentityFlags.None,
                        ConfirmationAttempts = 0,
                        TimeZone             = NodaTime.DateTimeZoneProviders.Tzdb.GetSystemDefault()
                    };

                this.IdentityRepository.Add(identity);

                // Add the destination.  Since this is the first addition,
                // we will make it primary.
                //
                emailDestinationEntity = new DestinationEntity(instantNow)
                    {
                        Identity                = identity,
                        Flags                   = DestinationFlags.Primary,
                        Address                 = emailAddress,
                        DestinationType         = DestinationType.Email,
                        ConfirmationSecret      = this.GenerateConfirmationCode(),
                        DateConfirmationExpires = this.GenerateExpiration(expiration)
                    };

                this.DestinationRepository.Add(emailDestinationEntity);
            }

            return emailDestinationEntity.ToModel();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the destination.  We will not allow the deletion of the
        /// primary destination, or if it's the only destination.
        /// </summary>
        ///--------------------------------------------------------------------
        public void RemoveDestination(
            DestinationEntity destinationEntity)
        {
            if ((destinationEntity.Flags & DestinationFlags.CanDelete) != 0)
            {
                this.DestinationRepository.Purge(destinationEntity);
            }
            else
            {
                throw new CoreLogicException(CoreErrorCode.InvalidOperation, "Destination is locked");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Remove the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void RemoveIdentity(
            IdentityEntity identityEntity)
        {
            // Purge the entity from the system.
            //
            this.IdentityRepository.Purge(identityEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the destination as primary.
        /// </summary>
        ///--------------------------------------------------------------------
        public void SetPrimary(
            DestinationEntity destinationEntity)
        {
            // Clear out the primary on all destinations of the type.
            //
            this.ClearPrimaryDestinations(destinationEntity.Identity, destinationEntity.DestinationType);

            // Refetch our entity as it has now been versioned from 
            // the above call.
            //
            destinationEntity = this.DestinationRepository.GetById(destinationEntity.Id);

            destinationEntity.Flags |= DestinationFlags.Primary;

            if (destinationEntity.DestinationType == DestinationType.Email)
            {
                destinationEntity.Flags &= ~DestinationFlags.CanDelete;
            }

            this.DestinationRepository.Update(destinationEntity);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the time zone for the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        public void SetTimeZone(
            IdentityEntity identityEntity,
            DateTimeZone   timeZone)
        {
            identityEntity.TimeZone     = timeZone;
            identityEntity.DateModified = this.ClockService.GetCurrentInstant();

            this.IdentityRepository.Update(identityEntity);
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate the expiration from the duration.
        /// </summary>
        ///--------------------------------------------------------------------
        private Instant GenerateExpiration(
            Duration expiratoin)
        {
            Instant instantNow        = this.ClockService.GetCurrentInstant();
            Instant instantExpiration = instantNow.Plus(expiratoin);

            return instantExpiration;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a session secret.
        /// </summary>
        ///--------------------------------------------------------------------
        private String GenerationSessionSecret()
        {
            return SecretHelper.GenerateUnique(SessionSecretLength, (pathName => this.SessionRepository.GetBySessionSecret(pathName) != null));
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Generate a confirmation code.
        /// </summary>
        ///--------------------------------------------------------------------
        private String GenerateConfirmationCode()
        {
            return this.SecretProvider.GenerateSecret();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Clear all primary destinations.
        /// </summary>
        ///--------------------------------------------------------------------
        private void ClearPrimaryDestinations(
            IdentityEntity  identityEntity,
            DestinationType destinationType)
        {
            // Retrieve all the destinations.
            //
            EntityCollection<DestinationEntity> destinations = this.DestinationRepository.GetAllByIdentity(identityEntity);

            // Get all the destinations by the DestinationType we're working
            // with.
            //
            List<DestinationEntity> filteredDestinations = destinations.Where(x => (x.DestinationType == destinationType)).ToList();

            foreach (DestinationEntity destination in filteredDestinations)
            {
                destination.Flags &= ~DestinationFlags.Primary;

                destination.Flags |= DestinationFlags.CanDelete;

                this.DestinationRepository.Update(destination);
            }
        }
        #endregion
    }
}
