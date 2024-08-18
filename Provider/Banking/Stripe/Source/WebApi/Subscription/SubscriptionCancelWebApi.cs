//-----------------------------------------------------------------------------
// <copyright file="SubscriptionCancelWebApi.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Threading.Tasks;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call to cancel a subscription in stripe.
    /// </summary>
    ///------------------------------------------------------------------------
    public class SubscriptionCancelWebApi : BaseWebApi<CancelSubscriptionResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public SubscriptionCancelWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api to delete the subscription.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            CancelSubscriptionRequest request)
        {
            await this.Delete(String.Format("v1/subscriptions/{0}", request.Id));
        }
        #endregion
    }
}