//-----------------------------------------------------------------------------
// <copyright file="Identity.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using Codev.Core.Base;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines the Identity model.
    /// </summary>
    ///------------------------------------------------------------------------
    public class Identity : BaseModel
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the base entity.
        /// </summary>
        ///--------------------------------------------------------------------
        public Identity() : base()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the time zone.
        /// </summary>
        ///--------------------------------------------------------------------
        public DateTimeZone TimeZone { get; set; }
        #endregion
    }
}
