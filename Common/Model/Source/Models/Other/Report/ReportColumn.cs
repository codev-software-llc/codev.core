//-----------------------------------------------------------------------------
// <copyright file="ReportColumn.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Model
{
    using System;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the model for a column in a report.
    /// </summary>
    ///------------------------------------------------------------------------
    public class ReportColumn
    {
        #region Constructors
        ///--------------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///--------------------------------------------------------------------
        public ReportColumn()
        {
            this.Name     = String.Empty;
            this.DataType = String.Empty;
        }
        #endregion

        #region Properties
        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the index relative to other columns.
        /// </summary>
        ///--------------------------------------------------------------------
        public Int32 Idx { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the colunn name.
        /// </summary>
        ///--------------------------------------------------------------------
        public String Name { get; set; }

        ///--------------------------------------------------------------------
        /// <summary>
        /// Get or set the data type of the header.
        /// </summary>
        ///--------------------------------------------------------------------
        public String DataType { get; set; }
        #endregion
    }
}