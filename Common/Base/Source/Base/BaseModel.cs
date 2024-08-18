//-----------------------------------------------------------------------------
// <copyright file="BaseModel.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the base model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the model.
        /// </summary>
        ///--------------------------------------------------------------------
        protected BaseModel()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 Id { get; set; }
        #endregion
    }
}
