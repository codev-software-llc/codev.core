//-----------------------------------------------------------------------------
// <copyright file="FakeCard.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Banking.Memory
{
    using System;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This defines a fake-card for testing purposes.
    /// </summary>
    ///------------------------------------------------------------------------
    public class FakeCard
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the fake card object.
        /// </summary>
        ///--------------------------------------------------------------------
        public FakeCard()
        {
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Properties for our fake card.
        /// </summary>
        ///--------------------------------------------------------------------
        public String          CardHolder      { get; set; }
        public Decimal         CardBalance     { get; set; }
        public String          CardNumber      { get; set; }
        public String          CardCode        { get; set; }
        public LocalDate       DateExpiration  { get; set; }
        public FakeFailureType ExpectedFailure { get; set; }
        #endregion
    }
}