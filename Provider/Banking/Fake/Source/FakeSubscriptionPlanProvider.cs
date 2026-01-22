//-----------------------------------------------------------------------------
// <copyright file="FakeSubscriptionPlanProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Fake
{
    using System;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call for a connect.
    /// </summary>
    ///------------------------------------------------------------------------
    public class FakeSubscriptionPlanProvider : ISubscriptionPlanProvider
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public FakeSubscriptionPlanProvider(
            IMerchantPlanRepository         planRepository,
            IMerchantCustomerRepository     customerRepository,
            IMerchantSubscriptionRepository subscriptionRepository)
        {
            this.PlanRepository         = planRepository;
            this.CustomerRepository     = customerRepository;
            this.SubscriptionRepository = subscriptionRepository;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for storing plans.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantPlanRepository PlanRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for storing customers.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantCustomerRepository CustomerRepository { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the repository for storing subscriptions.
        /// </summary>
        ///--------------------------------------------------------------------
        private IMerchantSubscriptionRepository SubscriptionRepository { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Creeate a new plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Create(
            String               source,
            String               name,
            PaymentAmount        cost,
            SubscriptionInterval interval,
            Int32                intervalCount,
            Int32                trialDays)
        {
            EntityCollection<MerchantPlanEntity> allPlans = this.PlanRepository.GetAll();

            MerchantPlanEntity entity = allPlans.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();

            if (entity == null)
            {
                Instant instantNow = NodaTime.SystemClock.Instance.GetCurrentInstant();

                entity = new MerchantPlanEntity(
                    new BaseEntity(0, null, true, instantNow, instantNow))
                    {
                        Flags         = MerchantPlanFlags.None,
                        Source        = source,
                        Name          = name,
                        Interval      = interval,
                        IntervalCount = intervalCount,
                        Cost          = cost,
                        TrialDays     = trialDays
                    };

                this.PlanRepository.Add(entity);

                return entity.ToToken();
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.Duplicate, "Plan already exists");
            }
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Delete the subscription plan.  This inherently will remove all
        /// subscriptions that are associate to the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public void  Delete(
            Token token)
        {
            MerchantPlanEntity entity = this.PlanRepository.GetById(token.ToId());

            if (entity != null)
            {
                this.PlanRepository.Delete(entity);
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Plan does not exists");
            }
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Update the subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Update(
            Token  token,
            String name)
        {
            MerchantPlanEntity entity = this.PlanRepository.GetById(token.ToId());

            if (entity != null)
            {
                EntityCollection<MerchantPlanEntity> allPlans = this.PlanRepository.GetAll();

                MerchantPlanEntity duplicate = allPlans.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();

                if (duplicate == null)
                {
                    Instant instantNow = NodaTime.SystemClock.Instance.GetCurrentInstant();

                    entity.DateModified  = instantNow;
                    entity.Name          = name;

                    this.PlanRepository.Update(entity);

                    return entity.ToToken();
                }
                else
                {
                    throw new CoreProviderException(CoreErrorCode.Duplicate, "Plan esists with this name");
                }
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Plan does not exists");
            }
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Subscribe a customer to a subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Subscribe(
            Token planToken,
            Token customerToken)
        {
            MerchantPlanEntity planEntity = this.PlanRepository.GetById(planToken.ToId());

            if (planEntity != null)
            {
                MerchantCustomerEntity customerEntity = this.CustomerRepository.GetById(customerToken.ToId());

                if (customerEntity != null)
                {
                    Instant instantNow = NodaTime.SystemClock.Instance.GetCurrentInstant();

                    MerchantSubscriptionEntity subscription = new MerchantSubscriptionEntity(instantNow)
                        {
                            Flags    = MerchantSubscriptionFlags.None,
                            Plan     = planEntity,
                            Customer = customerEntity
                        };

                    this.SubscriptionRepository.Add(subscription);

                    return subscription.ToToken();
                }
                else
                {
                    throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Customer does not exists");
                }
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Plan does not exists");
            }
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Remove a customer from the subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Unsubscribe(
            Token token)
        {
            MerchantSubscriptionEntity entity = this.SubscriptionRepository.GetById(token.ToId());

            if (entity != null)
            {
                this.SubscriptionRepository.Purge(entity);
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Subscription does not exist");
            }
        }
        #endregion
    }
}