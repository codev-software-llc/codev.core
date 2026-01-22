//-----------------------------------------------------------------------------
// <copyright file="CustomerCreateWebApi.cs" company="Codev Software, LLC">
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
    /// This is the web api call create a new customer in stripe.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CustomerCreateWebApi : BaseWebApi<CreateCustomerResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public CustomerCreateWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api for a customer creation.  If we want to add
        /// a default sourc (card), this must be done through an update.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            CreateCustomerRequest request)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            parameters.Add("description", request.Name );
            parameters.Add("email"      , request.Email);

            await this.Post("v1/customers", parameters);
        }
        #endregion
    }
}