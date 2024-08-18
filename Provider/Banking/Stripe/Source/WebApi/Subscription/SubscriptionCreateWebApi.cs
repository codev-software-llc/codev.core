//-----------------------------------------------------------------------------
// <copyright file="SubscriptionCreateWebApi.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call create a new plan subscription.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SubscriptionCreateWebApi : BaseWebApi<CreateSubscriptionResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public SubscriptionCreateWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api for a subscription creation.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            CreateSubscriptionRequest request)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            parameters.Add("customer"      , request.CustomerId);
            parameters.Add("items[0][plan]", request.PlanId    );

            await this.Post("v1/subscriptions", parameters);
        }
        #endregion
    }
}