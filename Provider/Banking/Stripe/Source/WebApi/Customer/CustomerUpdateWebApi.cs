//-----------------------------------------------------------------------------
// <copyright file="CustomerUpdateWebApi.cs" company="Codev Software, LLC">
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
    /// This is the web api call to update the customer information.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CustomerUpdateWebApi : BaseWebApi<UpdateCustomerResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public CustomerUpdateWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api for a customer update.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            UpdateCustomerRequest request)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            parameters.Add("id"         , request.Id);
            parameters.Add("description", request.Name);
            parameters.Add("email"      , request.Email);

            if (String.IsNullOrWhiteSpace(request.DefaultCard) == false)
            {
                parameters.Add("default_source", request.DefaultCard);
            }

            await this.Post(String.Format("v1/customers/{0})", request.Id), parameters);
        }
        #endregion
    }
}