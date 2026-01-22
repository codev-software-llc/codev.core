//-----------------------------------------------------------------------------
// <copyright file="CustomerDeleteWebApi.cs" company="Codev Software, LLC">
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
    /// This is the web api call to delete a customer in stripe.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CustomerDeleteWebApi : BaseWebApi<DeleteCustomerResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public CustomerDeleteWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api to delete 
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            DeleteCustomerRequest request)
        {
            await this.Delete(String.Format("v1/customers/{0}", request.Id));
        }
        #endregion
    }
}