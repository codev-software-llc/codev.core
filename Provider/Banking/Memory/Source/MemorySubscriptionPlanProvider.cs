//-----------------------------------------------------------------------------
// <copyright file="MemorySubscriptionPlanProvider.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call for a connect.
    /// </summary>
    ///------------------------------------------------------------------------
    public class MemorySubscriptionPlanProvider : ISubscriptionPlanProvider
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
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public MemorySubscriptionPlanProvider(
            ISubscriptionCustomerProvider customers)
        {
            this.Customers = Customers;

            this.Plans         = new Dictionary<Int32, MerchantPlanEntity>();
            this.Subscriptions = new Dictionary<Int32, List<MerchantSubscriptionEntity>>();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Keep a list of subscriptions.
        /// </summary>
        ///--------------------------------------------------------------------
        private ISubscriptionCustomerProvider Customers { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Keep a list of plans.
        /// </summary>
        ///--------------------------------------------------------------------
        private Dictionary<Int32, MerchantPlanEntity> Plans { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Keep a list of customers in the subscriptions.
        /// </summary>
        ///--------------------------------------------------------------------
        private Dictionary<Int32, List<MerchantSubscriptionEntity>> Subscriptions { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Creeate a new subscription plan.
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
            MerchantPlanEntity entity = this.LookupPlanByName(source, name);

            if (entity == null)
            {
                Instant instantNow = NodaTime.SystemClock.Instance.GetCurrentInstant();

                entity = new MerchantPlanEntity(
                    new BaseEntity(this.GenerateUniqueId(), new Byte[] { }, true, instantNow, instantNow))
                    {
                        Flags         = MerchantPlanFlags.None,
                        Source        = source,
                        Name          = name,
                        Cost          = cost,
                        Interval      = interval, 
                        IntervalCount = intervalCount,
                        TrialDays     = trialDays
                    };

                this.Plans.Add(entity.Id, entity);

                this.Subscriptions.Add(entity.Id, new List<MerchantSubscriptionEntity>());

                return entity.ToToken();
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.Duplicate, "Plan already exists");
            }
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Delete the subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public void  Delete(
            Token token)
        {
            MerchantPlanEntity entity;

            Int32 rowId = token.ToId();

            if (this.Plans.TryGetValue(rowId, out entity))
            {
                // Remove the subscriptions.
                //
                List<MerchantSubscriptionEntity> subscriptions;

                if (this.Subscriptions.TryGetValue(rowId, out subscriptions))
                {
                    this.Subscriptions.Remove(rowId);
                }

                // Remove the plan.
                //
                this.Plans.Remove(entity.Id);
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
            MerchantPlanEntity entity;

            if (this.Plans.TryGetValue(token.ToId(), out entity))
            {
                MerchantPlanEntity duplicate = this.LookupPlanByName(entity.Source, name);

                if ((duplicate == null) || (String.Compare(duplicate.Name, name, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    Instant instantNow = NodaTime.SystemClock.Instance.GetCurrentInstant();

                    entity.DateModified  = instantNow;
                    entity.Name          = name;

                    return entity.ToToken();
                }
                else
                {
                    throw new CoreProviderException(CoreErrorCode.Duplicate, "Plan already exists with name");
                }
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Plan does not exist");
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
            MerchantPlanEntity planEntity;

            Int32 planRowId     = planToken.ToId();
            Int32 customerRowId = customerToken.ToId();

            if (this.Plans.TryGetValue(planRowId, out planEntity))
            {
                List<MerchantSubscriptionEntity> subscriptions;

                if (this.Subscriptions.TryGetValue(planRowId, out subscriptions))
                {
                    MerchantSubscriptionEntity subscriptionEntity = subscriptions.Where(x => x.Id == customerRowId).FirstOrDefault();

                    if (subscriptionEntity == null)
                    {
                        Instant instantNow = NodaTime.SystemClock.Instance.GetCurrentInstant();

                        MerchantCustomerEntity customerEntity = new MerchantCustomerEntity(
                            new BaseEntity(customerToken.ToId(), new Byte[] { }, true, instantNow, instantNow))
                            {
                               
                            };

                        subscriptionEntity = new MerchantSubscriptionEntity(
                            new BaseEntity(this.GenerateUniqueId(), new Byte[] { }, true, instantNow, instantNow))
                            {
                                Flags    = MerchantSubscriptionFlags.None,
                                Plan     = planEntity,
                                Customer = customerEntity
                            };

                        subscriptions.Add(subscriptionEntity);
                    }

                    return subscriptionEntity.ToToken();
                }
                else
                {
                    throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Plan subscriptions does not exist");
                }
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Plan does not exist");
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
            List<MerchantSubscriptionEntity> subscriptions;

            Int32 subscriptionRowId = token.ToId();

            if (this.Subscriptions.TryGetValue(token.ToId(), out subscriptions))
            {
                MerchantSubscriptionEntity subscription = subscriptions.Where(x => x.Id == subscriptionRowId).FirstOrDefault();

                if (subscription != null)
                {
                    subscriptions.Remove(subscription);
                }
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Plan does not exist");
            }
        }
        #endregion

        #region Methods (Private)
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

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Look up plan by the source and name.
        /// </summary>
        ///--------------------------------------------------------------------
        private MerchantPlanEntity LookupPlanByName(
            String source,
            String name)
        {
            foreach (KeyValuePair<Int32, MerchantPlanEntity> kvp in this.Plans)
            {
                if ((String.Compare(kvp.Value.Name, name, StringComparison.OrdinalIgnoreCase) == 0) &&
                    (String.Compare(kvp.Value.Source, source, StringComparison.OrdinalIgnoreCase) == 0))
                {
                    return kvp.Value;
                }
            }

            return null;
        }
        #endregion
    }
}