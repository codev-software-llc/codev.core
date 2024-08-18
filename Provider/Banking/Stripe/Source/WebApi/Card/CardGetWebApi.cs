//-----------------------------------------------------------------------------
// <copyright file="CardGetWebApi.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Threading.Tasks;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call to retrieve card details.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CardGetWebApi : BaseWebApi<GetCardResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public CardGetWebApi(
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
            GetCardRequest request)
        {
            await this.Get(String.Format("v1/cards/{0}", request.Id));
        }
        #endregion
    }
}