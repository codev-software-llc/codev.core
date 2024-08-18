//-----------------------------------------------------------------------------
// <copyright file="PlanGetWebApi.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Threading.Tasks;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call to retrieve plan details.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PlanGetWebApi : BaseWebApi<GetPlanResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public PlanGetWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api to retreive.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            GetPlanRequest request)
        {
            await this.Get(String.Format("v1/plans/{0}", request.Id));
        }
        #endregion
    }
}