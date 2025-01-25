//-----------------------------------------------------------------------------
// <copyright file="Reference.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is used as a reference to an object.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Reference<T>
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the model.
        /// </summary>
        ///--------------------------------------------------------------------
        public Reference(
            Int32 id)
        {
            this.Id            = id;
            this.ReferenceType = typeof(T);
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the base identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 Id { get; private set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the base identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public Type ReferenceType { get; private set; }
        #endregion
    }
}