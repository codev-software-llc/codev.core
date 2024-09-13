//-----------------------------------------------------------------------------
// <copyright file="Message.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using System;
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the message model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Message : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the model.
        /// </summary>
        ///--------------------------------------------------------------------
        public Message() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set catagory (contact, support, ...)
        /// </summary>
        ///--------------------------------------------------------------------
        public String Category { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set sub-category (application, website, ...)
        /// </summary>
        ///--------------------------------------------------------------------
        public String Subcategory { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set name who sent then communication.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set whether the destination is confirmed as valid.
        /// </summary>
        ///--------------------------------------------------------------------
        public String EmailAddress { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the comments.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Comments { get; set; }
        #endregion
    }
}
