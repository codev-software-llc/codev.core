//-----------------------------------------------------------------------------
// <copyright file="CustomerGetWebApi.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Threading.Tasks;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call to retrieve customer details.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CustomerGetWebApi : BaseWebApi<GetCustomerResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public CustomerGetWebApi(
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
            GetCustomerRequest request)
        {
            await this.Get(String.Format("v1/customers/{0}", request.Id));
        }
        #endregion
    }
}