//-----------------------------------------------------------------------------
// <copyright file="CardCreateWebApi.cs" company="Codev Software, LLC">
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
    /// This is the web api call create a new card in stripe.
    /// </summary>
    ///------------------------------------------------------------------------
    public class CardCreateWebApi : BaseWebApi<CreateCardResponse>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public CardCreateWebApi(
            String baseUrl,
            String accessToken) : base(baseUrl, accessToken)
        {
        }
        #endregion

        #region Methods   
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the web api for a card creation.
        /// </summary>
        ///--------------------------------------------------------------------
        public async Task Invoke(
            CreateCardRequest request)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();

            parameters.Add("name"       , request.Name );
            parameters.Add("object"     , "card"       );
            parameters.Add("exp_month"  , String.Format("{0:00}", request.DateExpiration.Month));
            parameters.Add("exp_year"   , request.DateExpiration.Year.ToString());
            parameters.Add("number"     , request.Number);
            parameters.Add("cvc"        , request.Code);
            parameters.Add("address_zip", request.PostalCode);

            await this.Post("v1/cards", parameters);
        }
        #endregion
    }
}