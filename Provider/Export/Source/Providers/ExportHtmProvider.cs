//-----------------------------------------------------------------------------
// <copyright file="ExportHtmProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider
{
    using System;
    using System.Data;
    using System.Text;
    using Codev.Core.Common.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IExport to dump data into a destination format.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class ExportHtmProvider : IExportProvider
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///---------------------------------------------------------------
        public ExportHtmProvider(
            String name)
        {
            this.Name = name;
        }
        #endregion

        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the name of the provider.
        /// </summary>
        ///---------------------------------------------------------------
        public String Name { get; private set; }
        #endregion

        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to a HTML format.
        /// </summary>
        ///---------------------------------------------------------------
        public Byte[] Convert(
            DataTable source,
            String    styleSheet)
        {
            String data = DataTableHelper.ToHtml(source, styleSheet);

            return Encoding.UTF8.GetBytes(data);
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to a HTML format.
        /// </summary>
        ///---------------------------------------------------------------
        public Byte[] Convert(
            DataTable source)
        {
            return this.Convert(source, String.Empty);
        }
        #endregion
    }
}
