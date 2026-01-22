//-----------------------------------------------------------------------------
// <copyright file="ReportRow.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using System.Collections.Generic;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the model for a row in a report.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ReportRow
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public ReportRow()
        {
            this.Columns = new List<ReportColumn>();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the row index.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 Idx { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the columns in the row.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<ReportColumn> Columns { get; set; }
        #endregion
    }
}