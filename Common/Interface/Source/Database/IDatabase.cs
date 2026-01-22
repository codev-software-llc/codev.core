//-----------------------------------------------------------------------------
// <copyright file="IDatabase.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Interface
{
    using System.Reflection;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the database access.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IDatabase
    {
        #region Methods
        ///------------------------------------------------------------------------
        /// <summary>
        /// This will initialize the database.
        /// </summary>
        ///------------------------------------------------------------------------
        void Initialize(
            Assembly assembly);
        #endregion
    }
}
