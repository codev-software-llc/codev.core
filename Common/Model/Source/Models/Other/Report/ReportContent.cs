//-----------------------------------------------------------------------------
// <copyright file="ReportContent.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;
    using System.Collections.Generic;
    using NodaTime;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the model for the generated report.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ReportContent
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public ReportContent()
        {
            this.Title       = String.Empty;
            this.Description = String.Empty;
            this.Header      = new List<ReportColumn>();
            this.Footer      = new List<ReportColumn>();
            this.Body        = new List<ReportRow>();
            this.Notes       = new List<ReportNotes>();
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the report identifier.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 Id { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the start date (actual) of the report.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDate DateStart { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the end date (actual) of the report.
        /// </summary>
        ///--------------------------------------------------------------------
        public LocalDate DateEnd { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the title of the report.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Title { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set a description of the report.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Description { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the header columns that represent the report.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<ReportColumn> Header { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the footer columns for the report.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<ReportColumn> Footer { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the body rows.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<ReportRow> Body { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the report notes.
        /// </summary>
        ///--------------------------------------------------------------------
        public List<ReportNotes> Notes { get; set; }
        #endregion
    }
}