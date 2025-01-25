//-----------------------------------------------------------------------------
// <copyright file="EntityComparer.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Base
{
    using System;
    using System.Collections.Generic;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This provides a helper class to compare assignments.
    /// </summary>
    ///------------------------------------------------------------------------
    public class EntityComparer<T> : IEqualityComparer<T>
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Return whether the two objects are the same.
        /// </summary>
        ///--------------------------------------------------------------------
        public Boolean Equals(
            T p1,
            T p2)
        {
            BaseEntity bp1 = p1 as BaseEntity;
            BaseEntity bp2 = p2 as BaseEntity;

            if ((bp1 != null) && (bp2 != null))
            {
                return bp1.Id == bp2.Id;
            }
            else
            {
                throw new InvalidCastException("Item is not a BaseEntity");
            }
        }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Return the hashcode (unique).
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 GetHashCode(
            T p)
        {
            BaseEntity be = p as BaseEntity;

            if (be != null)
            {
                return be.Id.GetHashCode();
            }
            else
            {
                throw new InvalidCastException("Item is not a BaseEntity");
            }
        }
        #endregion
    }
}