//-----------------------------------------------------------------------------
// <copyright file="WebApiResponse.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Net;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the response for an api call to the server.
    /// </summary>
    ///------------------------------------------------------------------------
    public class WebApiResponse<T>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the request for a success.
        /// </summary>
        ///--------------------------------------------------------------------
        public WebApiResponse()
        {
            this.StatusCode   = HttpStatusCode.OK;
            this.ErrorMessage = String.Empty;
            this.Value        = Activator.CreateInstance<T>();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the request for a success.
        /// </summary>
        ///--------------------------------------------------------------------
        public WebApiResponse(
            HttpStatusCode code,
            T              value)
        {
            this.StatusCode   = code;
            this.ErrorMessage = String.Empty;
            this.Value        = value;
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the request for an error.
        /// </summary>
        ///--------------------------------------------------------------------
        public WebApiResponse(
            HttpStatusCode code,
            String         message)
        {
            this.StatusCode   = code;
            this.ErrorMessage = message;
            this.Value        = Activator.CreateInstance<T>();
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the request for an exception message.
        /// </summary>
        ///--------------------------------------------------------------------
        public WebApiResponse(
            Exception e) : this(HttpStatusCode.InternalServerError, e.Message)
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the response of the call.
        /// </summary>
        ///--------------------------------------------------------------------
        public HttpStatusCode StatusCode { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the error message.
        /// </summary>
        ///--------------------------------------------------------------------
        public String ErrorMessage { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the value of the response.
        /// </summary>
        ///--------------------------------------------------------------------
        public T Value { get; set; }
        #endregion
    }
}