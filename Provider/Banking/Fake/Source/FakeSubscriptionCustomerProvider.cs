//-----------------------------------------------------------------------------
// <copyright file="FakeSubscriptionCustomerProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Fake
{
    using System;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Common.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call for a connect.
    /// </summary>
    ///------------------------------------------------------------------------
    public class FakeSubscriptionCustomerProvider : ISubscriptionCustomerProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the provider.
        /// </summary>
        ///--------------------------------------------------------------------
        public FakeSubscriptionCustomerProvider(
            IMerchantCardRepository   cardRepository,
            IMerchantCustomerRepository customerRepository)
        {
            this.CustomerRepository = customerRepository;
            this.CardRepository     = cardRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for storing customers.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantCustomerRepository CustomerRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for storing cards.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantCardRepository CardRepository { get; set; }
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
            EntityCollection<MerchantCustomerEntity> allCustomers = this.CustomerRepository.GetAll();

            MerchantCustomerEntity entity = allCustomers.Where(x => x.EmailAddress.ToLower() == email.ToLower()).FirstOrDefault();

            if (entity == null)
            {
                entity = new MerchantCustomerEntity(instantNow)
                    {
                        Flags        = MerchantCustomerFlags.None,
                        Name         = name,
                        EmailAddress = email
                    };

                this.CustomerRepository.Add(entity);

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
            MerchantCustomerEntity entity = this.CustomerRepository.GetById(token.ToId());

            if (entity != null)
            {
                this.CustomerRepository.Delete(entity);
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Customer does not exists");
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
            MerchantCustomerEntity entity = this.CustomerRepository.GetById(token.ToId());

            if (entity != null)
            {
                EntityCollection<MerchantCustomerEntity> allCustomers = this.CustomerRepository.GetAll();

                MerchantCustomerEntity duplicate = allCustomers.Where(x => x.EmailAddress.ToLower() == email.ToLower()).FirstOrDefault();

                if (duplicate == null)
                {
                    entity.DateModified = instantNow;
                    entity.Name         = name;
                    entity.EmailAddress = email;

                    this.CustomerRepository.Update(entity);
                }
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Customer does not exists");
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
            MerchantCustomerEntity customerEntity = this.CustomerRepository.GetById(customerToken.ToId());

            if (customerEntity != null)
            {
                MerchantCardEntity cardEntity = new MerchantCardEntity(instantNow)
                    {
                        Flags          = MerchantCardFlags.None,
                        Name           = creditCard.Name,
                        Type           = creditCard.Type,
                        Number         = creditCard.Number,
                        Code           = creditCard.Code,
                        DateExpiration = creditCard.DateExpriation,
                        Customer       = customerEntity
                   
                    };

                this.CardRepository.Add(cardEntity);

                return cardEntity.ToToken();
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Customer does not exists");
            }
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Remove a card from the customer.
        /// </summary>
        ///--------------------------------------------------------------------
        public void RemoveCard(
            Token cardToken)
        {
            MerchantCardEntity entity = this.CardRepository.GetById(cardToken.ToId());

            if (entity != null)
            {
                this.CardRepository.Delete(entity);
            }
        }
        #endregion
    }
}