//-----------------------------------------------------------------------------
// <copyright file="PlanDeleteWebApi.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Threading.Tasks;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call to delete a subscription plan in stripe.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PlanDeleteWebApi : BaseWebApi<DeletePlanResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public PlanDeleteWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api to delete.  For Stripe, existing subscriptions
        /// are not affected.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            DeletePlanRequest request)
        {
            await this.Delete(String.Format("v1/plans/{0}", request.Id));
        }
        #endregion
    }
}