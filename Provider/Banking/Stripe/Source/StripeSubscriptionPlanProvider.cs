//-----------------------------------------------------------------------------
// <copyright file="StripeSubscriptionPlanProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Linq;
    using System.Net;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call for a connect.
    /// </summary>
    ///------------------------------------------------------------------------
    public class StripeSubscriptionPlanProvider : ISubscriptionPlanProvider
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// Thi is the base api for calls to the stripe service.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String BaseUrl = "https://api.stripe.com/";
        #endregion

        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public StripeSubscriptionPlanProvider(
            String accessToken)
        {
            this.AccessToken = accessToken;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the access token to work with stripe.
        /// </summary>
        ///--------------------------------------------------------------------
        private String AccessToken { get; set; }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Create a new plan.
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
            // Retrieve all plans to make sure we're not adding a duplicate.
            //
            PlanListWebApi listWebApi = new PlanListWebApi(BaseUrl, this.AccessToken);

            listWebApi.Invoke().Wait();

            WebApiResponse<ListPlansResponse> response = listWebApi.Response;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (this.DoesPlanExist(response.Value, source, name) == false)
                {
                    PlanCreateWebApi createWebApi = new PlanCreateWebApi(BaseUrl, this.AccessToken);

                    CreatePlanRequest request = new CreatePlanRequest()
                        {
                            Name          = name,
                            Source        = source,
                            Cost          = cost,
                            Interval      = interval,
                            IntervalCount = intervalCount,
                            TrialDays     = trialDays
                        };

                    createWebApi.Invoke(request).Wait();

                    if ((createWebApi.Response.StatusCode == HttpStatusCode.OK) || (createWebApi.Response.StatusCode == HttpStatusCode.Created))
                    {
                        return createWebApi.Response.Value.ToToken();
                    }
                    else
                    {
                        throw new CoreProviderException(CoreErrorCode.ServiceFailure, response.ErrorMessage);
                    }
                }
                else
                {
                    throw new CoreProviderException(CoreErrorCode.ServiceFailure, "Plan already exists");
                }
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.ServiceFailure, response.ErrorMessage);
            }
        }

        ///--------------------------------------------------------------------     
        /// <summary>
        /// Delete the subscription plan.  This inherently will remove all
        /// subscriptions that are associate to the plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Delete(
            Token token)
        {
            PlanDeleteWebApi deleteWebApi = new PlanDeleteWebApi(BaseUrl, this.AccessToken);

            DeletePlanRequest request = new DeletePlanRequest()
                {
                    Id = token.ToId()
                };

            deleteWebApi.Invoke(request).Wait();

            if (deleteWebApi.Response.StatusCode == HttpStatusCode.OK)
            {

            }
            else
            {

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
            // Retrieve all plans to make sure we're not adding a duplicate.
            //
            PlanListWebApi listWebApi = new PlanListWebApi(BaseUrl, this.AccessToken);

            listWebApi.Invoke().Wait();

            WebApiResponse<ListPlansResponse> response = listWebApi.Response;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (this.DoesPlanExist(response.Value, name) == false)
                { 
                    PlanUpdateWebApi updateWebApi = new PlanUpdateWebApi(BaseUrl, this.AccessToken);

                    UpdatePlanRequest request = new UpdatePlanRequest()
                        {
                            Id   = token.ToId(),
                            Name = name
                        };

                    updateWebApi.Invoke(request).Wait();

                    if ((updateWebApi.Response.StatusCode == HttpStatusCode.OK) || (updateWebApi.Response.StatusCode == HttpStatusCode.Created))
                    {
                        return updateWebApi.Response.Value.ToToken();
                    }
                    else
                    {
                        throw new CoreProviderException(CoreErrorCode.ServiceFailure, response.ErrorMessage);
                    }
                }
                else
                {
                    throw new CoreProviderException(CoreErrorCode.ServiceFailure, "Plan already exists");
                }
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.ServiceFailure, response.ErrorMessage);
            }
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Subscribe a customer to a subscription plan.
        /// </summary>
        ///--------------------------------------------------------------------
        public Token Subscribe(
            Token   planToken,
            Token   customerToken)
        {
            SubscriptionCreateWebApi webApi = new SubscriptionCreateWebApi(BaseUrl, this.AccessToken);

            CreateSubscriptionRequest request = new CreateSubscriptionRequest()
                {
                    PlanId     = planToken.ToId(),
                    CustomerId = customerToken.ToId()
                };

            webApi.Invoke(request).Wait();

            if ((webApi.Response.StatusCode == HttpStatusCode.OK) || (webApi.Response.StatusCode == HttpStatusCode.Created))
            {
                return webApi.Response.Value.ToToken();
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.ServiceFailure, webApi.Response.ErrorMessage);
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
            SubscriptionCancelWebApi webApi = new SubscriptionCancelWebApi(BaseUrl, this.AccessToken);

            CancelSubscriptionRequest request = new CancelSubscriptionRequest()
                {
                    Id = token.ToId()
                };

            webApi.Invoke(request).Wait();

            if (webApi.Response.StatusCode == HttpStatusCode.OK)
            {

            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.ServiceFailure, webApi.Response.ErrorMessage);
            }
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------      
        /// <summary>
        /// Determine if the souce already exists.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean DoesPlanExist(
            ListPlansResponse response,
            String            source,
            String            name)
        {
            source = source.ToLower();
            name   = name.ToLower();

            GetPlanResponse found = response.Data.Where(x => x.Name.ToLower() == name).FirstOrDefault();

            return (found != null);
        }

        ///--------------------------------------------------------------------      
        /// <summary>
        /// Determine if the souce already exists.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean DoesPlanExist(
            ListPlansResponse response,
            String            name)
        {
            name = name.ToLower();

            GetPlanResponse found = response.Data.Where(x => x.Name.ToLower() == name).FirstOrDefault();

            return (found != null);
        }
        #endregion
    }
}