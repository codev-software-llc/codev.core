//-----------------------------------------------------------------------------
// <copyright file="PlanCreateWebApi.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call create a new subscription plan in stripe.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PlanCreateWebApi : BaseWebApi<CreatePlanResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public PlanCreateWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api for a plan creation.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            CreatePlanRequest request)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            parameters.Add("id"                  , String.Format("{0}_{1}", request.Source, Guid.NewGuid()));
            parameters.Add("name"                , request.Name);
            parameters.Add("statement_descriptor", String.Format("{0} plan for {1}", request.Name, request.Source));
            parameters.Add("currency"            , request.Cost.IsoCode.ToLower());
            parameters.Add("amount"              , request.Cost.Amount.ToString());
            parameters.Add("interval"            , request.Interval.ToString().ToLower());
            parameters.Add("interval_count"      , request.IntervalCount.ToString());
            parameters.Add("trial_period_days"   , request.TrialDays.ToString());

            await this.Post("v1/plans", parameters);
        }
        #endregion
    }
}