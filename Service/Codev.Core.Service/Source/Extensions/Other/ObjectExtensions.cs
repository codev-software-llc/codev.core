//-----------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service
{
    using Codev.Core.Base;

    ///------------------------------------------------------------------------
    /// <summary>
    /// Provides common extensions for various objects.
    /// </summary>
    ///------------------------------------------------------------------------
    public static class ObjectExtensions
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the entity to a reference.
        /// </summary>
        ///--------------------------------------------------------------------
        public static Reference<T> AsReference<T>(
            this BaseModel model)
        {
            return new Reference<T>(model.Id);
        }
        #endregion
    }
}
