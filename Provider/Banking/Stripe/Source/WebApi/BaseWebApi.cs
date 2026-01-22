//-----------------------------------------------------------------------------
// <copyright file="BaseWebApi.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the web api call to begin the authentication process.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BaseWebApi<T> : IDisposable
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the base object.
        /// </summary>
        ///--------------------------------------------------------------------
        public BaseWebApi(
            String  baseUrl,
            String  accessToken)
        {
            this.Client = new();

            // Initialize the http client parameters.
            //
            this.Client.BaseAddress = new Uri(baseUrl);

            this.Client.DefaultRequestHeaders.Accept.Clear();
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (String.IsNullOrWhiteSpace(accessToken) == false)
            {
                this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Called when the object is disposed.
        /// </summary>
        ///--------------------------------------------------------------------
        ~BaseWebApi()
        {
            this.Dispose(false);
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set http client object.
        /// </summary>
        ///--------------------------------------------------------------------
        private HttpClient Client { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether we have already disposed of the object.
        /// </summary>
        ///--------------------------------------------------------------------
        private Boolean IsDisposed { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the api response object.
        /// </summary>
        ///--------------------------------------------------------------------
        public WebApiResponse<T> Response { get; set; }
        #endregion

        #region Disposble
        ///--------------------------------------------------------------------
        /// <summary>
        /// Dispose of the resources allocated on behalf of this class.
        /// </summary>
        ///--------------------------------------------------------------------
        public void Dispose()
        {
            this.Dispose(true);

            // Suppressing the finalize will prevent our Dispose from
            // potentially being called twice.
            //
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke a get call.
        /// </summary>
        ///--------------------------------------------------------------------
        protected async Task<WebApiResponse<T>> Get(
            String apiCall)
        {
            try
            {
                HttpResponseMessage response = await this.Client.GetAsync(apiCall);

                if (response.IsSuccessStatusCode)
                {
                    WebApiResponse<T> payload = await GetResponse(response);

                    this.SetResponse(payload);
                }
                else
                {
                    this.SetResponse(response);
                }
            }
            catch (Exception e)
            {
                this.SetResponse(e);
            }
            finally
            {
                this.Dispose();
            }

            return this.Response;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the Post.
        /// </summary>
        ///--------------------------------------------------------------------
        protected async Task<WebApiResponse<T>> Post<TT>(
            String apiCall,
            TT     request)
        {
            try
            {
                String data = JsonSerializer.Serialize(request);

                StringContent content = new(data, Encoding.UTF8, "application/json");

                //HttpResponseMessage response = await this.Client.PostAsync<TT>(apiCall, request, this.Formatter);
                HttpResponseMessage response = await this.Client.PostAsync(apiCall, content);

                if (response.IsSuccessStatusCode)
                {
                    WebApiResponse<T> payload = await GetResponse(response);

                    this.SetResponse(payload);
                }
                else
                {
                    this.SetResponse(response);
                }
            }
            catch (Exception e)
            {
                this.SetResponse(e);
            }
            finally
            {
                this.Dispose();
            }

            return this.Response;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the Post using a dictionary of parameters.
        /// </summary>
        ///--------------------------------------------------------------------
        protected async Task<WebApiResponse<T>> Post(
            String                     apiCall,
            Dictionary<String, String> parameters)
        {
            try
            {
                HttpContent content = new FormUrlEncodedContent(parameters);

                HttpResponseMessage response = await this.Client.PostAsync(apiCall, content);

                if (response.IsSuccessStatusCode)
                {
                    WebApiResponse<T> payload = await GetResponse(response);

                    this.SetResponse(payload);
                }
                else
                {
                    this.SetResponse(response);
                }
            }
            catch (Exception e)
            {
                this.SetResponse(e);
            }
            finally
            {
                this.Dispose();
            }

            return this.Response;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Invoke the Delete using a dictionary of parameters.
        /// </summary>
        ///--------------------------------------------------------------------
        protected async Task<WebApiResponse<T>> Delete(
            String apiCall)
        {
            try
            {
                HttpResponseMessage response = await this.Client.DeleteAsync(apiCall);

                if (response.IsSuccessStatusCode)
                {
                    WebApiResponse<T> payload = await GetResponse(response);

                    this.SetResponse(payload);
                }
                else
                {
                    this.SetResponse(response);
                }
            }
            catch (Exception e)
            {
                this.SetResponse(e);
            }
            finally
            {
                this.Dispose();
            }

            return this.Response;
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// This will handle the disposal of our work.
        /// </summary>
        ///--------------------------------------------------------------------
        private void Dispose(
            Boolean isDisposing)
        {
            if (isDisposing && (this.IsDisposed == false))
            {
                if (this.Client != null)
                {
                    this.Client.Dispose();

                    this.Client = null;
                }

                this.IsDisposed = true;
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get the response from the call.
        /// </summary>
        ///--------------------------------------------------------------------
        private static async Task<WebApiResponse<T>> GetResponse(
            HttpResponseMessage response)
        {
            String data = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<WebApiResponse<T>>(data);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the response for an exception.
        /// </summary>
        ///--------------------------------------------------------------------
        private void SetResponse(
            Exception e)
        {
            this.Response = new WebApiResponse<T>(e);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set response when error occurs.
        /// </summary>
        ///--------------------------------------------------------------------
        private void SetResponse(
            HttpResponseMessage response)
        {
            this.Response = new WebApiResponse<T>(response.StatusCode, response.ReasonPhrase);
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Set the response for a successfull call.
        /// </summary>
        ///--------------------------------------------------------------------
        private void SetResponse(
            WebApiResponse<T> response)
        {
            this.Response = response;
        }
        #endregion
    }
}