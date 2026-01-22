//-----------------------------------------------------------------------------
// <copyright file="PlanUpdateWebApi.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call to update subscription plan in stripe.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PlanUpdateWebApi : BaseWebApi<UpdatePlanResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public PlanUpdateWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api for a plan update.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            UpdatePlanRequest request)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            parameters.Add("id"                  , request.Id);
            parameters.Add("name"                , request.Name);
            parameters.Add("statement_descriptor", String.Format("{0} plan for {1}", request.Name, request.Source));
            parameters.Add("currency"            , request.Cost.IsoCode.ToLower());
            parameters.Add("amount"              , request.Cost.Amount.ToString());
            parameters.Add("interval"            , request.Interval.ToString().ToLower());

            await this.Post(String.Format("v1/plans/{0})", request.Id), parameters);
        }
        #endregion
    }
}