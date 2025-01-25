//-----------------------------------------------------------------------------
// <copyright file="PlanListWebApi.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Threading.Tasks;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call to retrieve all plans.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PlanListWebApi : BaseWebApi<ListPlansResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public PlanListWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api to retreive all the plans.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task<WebApiResponse<ListPlansResponse>> Invoke()
        {
            return await this.Get("v1/plans?limit=100");
        }
        #endregion
    }
}