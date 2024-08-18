//-----------------------------------------------------------------------------
// <copyright file="CardDeleteWebApi.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call to delete a card in stripe.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CardDeleteWebApi : BaseWebApi<DeleteCardResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public CardDeleteWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api to delete.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            DeleteCardRequest request)
        {
            await this.Delete(String.Format("v1/cards/{0}", request.Id));
        }
        #endregion
    }
}