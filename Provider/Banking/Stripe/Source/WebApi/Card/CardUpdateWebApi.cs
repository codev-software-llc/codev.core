//-----------------------------------------------------------------------------
// <copyright file="CardUpdateWebApi.cs" company="Codev Software, LLC">
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
    /// This is the web api call to update the card information.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CardUpdateWebApi : BaseWebApi<UpdateCardResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public CardUpdateWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api for a card update.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            UpdateCardRequest request)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            parameters.Add("name"       , request.Name );
            parameters.Add("object"     , "card"       );
            parameters.Add("exp_month"  , String.Format("{0:00}", request.DateExpiration.Month));
            parameters.Add("exp_year"   , request.DateExpiration.Year.ToString());
            parameters.Add("address_zip", request.PostalCode);

            await this.Post(String.Format("v1/cards/{0})", request.Id), parameters);
        }
        #endregion
    }
}