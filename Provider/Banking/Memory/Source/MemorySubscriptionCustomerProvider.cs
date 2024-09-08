//-----------------------------------------------------------------------------
// <copyright file="MemorySubscriptionCustomerProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call for a connect.
    /// </summary>
    ///------------------------------------------------------------------------
    public class MemorySubscriptionCustomerProvider : ISubscriptionCustomerProvider
    {
        #region Member Variables
        ///--------------------------------------------------------------------
        /// <summary>
        /// This represents the unique row identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        private static Int32 m_Index = 0;
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the provider.
        /// </summary>
        ///--------------------------------------------------------------------
        public MemorySubscriptionCustomerProvider()
        {
            this.Customers = new Dictionary<Int32, MerchantCustomerEntity>();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Keep a list of customers.
        /// </summary>
        ///--------------------------------------------------------------------
        private Dictionary<Int32, MerchantCustomerEntity> Customers { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Creeate a new customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Create(
            Instant instantNow,
            String  name,
            String  email)
        {
            MerchantCustomerEntity entity = this.LookupCustomerByEmail(email);

            if (entity == null)
            {
                entity = new MerchantCustomerEntity(
                    new BaseEntity(this.GenerateUniqueId(), new Byte[] { }, true, instantNow, instantNow))
                    {
                        Flags        = MerchantCustomerFlags.None,
                        Name         = name,
                        EmailAddress = email
                    };

                this.Customers.Add(entity.Id, entity);

                return entity.ToToken();
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.Duplicate, "Customer already exists");
            }
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Delete the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void  Delete(
            Token token)
        {
            MerchantCustomerEntity entity;

            if (this.Customers.TryGetValue(token.ToId(), out entity))
            {
                this.Customers.Remove(entity.Id);
            }
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Update the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Update(
            Token   token,
            Instant instantNow,
            String  name,
            String  email)
        {
            MerchantCustomerEntity entity;

            if (this.Customers.TryGetValue(token.ToId(), out entity))
            {
                MerchantCustomerEntity duplicate = this.LookupCustomerByEmail(email);

                if ((duplicate == null) || (String.Compare(duplicate.EmailAddress, email, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    entity.DateModified = instantNow;
                    entity.Name         = name;
                    entity.EmailAddress = email;
                }
                else
                {
                    throw new CoreProviderException(CoreErrorCode.Duplicate, "Customer already exists with email");
                }
            }
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Add a card to the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token AddCard(
            Token             customerToken,
            PaymentCreditCard creditCard,
            Instant           instantNow)
        {
            throw new NotImplementedException();
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Remove a card from the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void RemoveCard(
            Token cardToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------      
        /// <summary>
        /// Look up customer based on email.
        /// </summary>
        ///--------------------------------------------------------------------
        private MerchantCustomerEntity LookupCustomerByEmail(
            String email)
        {
            foreach (KeyValuePair<Int32, MerchantCustomerEntity> kvp in this.Customers)
            {
                if (String.Compare(kvp.Value.EmailAddress, email, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return kvp.Value;
                }

            }

            return null;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get a unique server id to use for our local entities.
        /// </summary>
        ///--------------------------------------------------------------------
        private Int32 GenerateUniqueId()
        {
            Int32 index = Interlocked.Decrement(ref m_Index);

            return index;
        }
        #endregion
    }
}