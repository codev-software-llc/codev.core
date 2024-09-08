//-----------------------------------------------------------------------------
// <copyright file="IDestinationRepository.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the repository for managing destinations.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IDestinationRepository : IRepository<DestinationEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the destinations by the identity.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<DestinationEntity> GetAllByIdentity(
            IdentityEntity identity);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the entity by the name nd type combination.
        /// </summary>
        ///--------------------------------------------------------------------
        DestinationEntity GetByAddress(
            String          address,
            DestinationType destinationType);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the entity by the secret.
        /// </summary>
        ///--------------------------------------------------------------------
        DestinationEntity GetByConfirmationSecret(
            String  confirmationSecret);
        #endregion
    }
}
