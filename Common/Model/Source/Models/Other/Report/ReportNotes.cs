//-----------------------------------------------------------------------------
// <copyright file="ReportNotes.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Model
{
    using System;
    using System.Collections.Generic;
    using NodaTime;

    ///--------------------------------------------------------------------
    /// <summary>
    /// This contains the notes for the account.
    /// </summary>
    ///--------------------------------------------------------------------
    public class ReportNotes
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Construct the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public ReportNotes()
        {
            this.Notes = new List<String>();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the date where the task allotments are aggregated.d
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDate Date { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the aggregated notes collection.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<String> Notes { get; set; }
        #endregion
    }
}