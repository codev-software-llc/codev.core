//-----------------------------------------------------------------------------
// <copyright file="IMerchantTransactionRepository.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System;
    using Codev.Core.Base;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface is provided for storing transactions.  Typically, this
    /// is used for testing purpose only.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IMerchantTransactionRepository : IRepository<MerchantTransactionEntity>
    {
        #region Methods
        ///--------------------------------------------------------------------   
        /// <summary>
        /// Locate the transaction information by its transaction group.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<MerchantTransactionEntity> GetAllByGroup(
            Guid groupId);

        ///--------------------------------------------------------------------   
        /// <summary>
        /// Locate the transaction information by its transaction group.
        /// </summary>
        ///--------------------------------------------------------------------
        EntityCollection<MerchantTransactionEntity> GetAllByCard(
            MerchantCardEntity card);
        #endregion
    }
}
