//-----------------------------------------------------------------------------
// <copyright file="ExportService.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Export
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Model;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IExportService to dump data into a destination
    /// format.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed partial class ExportService : IExportService
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///---------------------------------------------------------------
        public ExportService(
            IEnumerable<IExportProvider> exportProviders)
        {
            Validation.ValidateParameter<IEnumerable<IExportProvider>>("exportProviders", exportProviders);

            this.ExportProviders = exportProviders;
        }
        #endregion

        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Get or set the export provider.
        /// </summary>
        ///---------------------------------------------------------------
        public IEnumerable<IExportProvider> ExportProviders { get; private set; }
        #endregion

        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to the desired format.
        /// </summary>
        ///---------------------------------------------------------------
        public Byte[] Convert(
            DataTable source,
            String    format)
        {
            IExportProvider exportProvider = this.ExportProviders.Where(x => String.Compare(format, x.Format, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();

            if (exportProvider != null)
            {
                return exportProvider.Convert(source);
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Format is not supported");
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to the desired format.
        /// </summary>
        ///---------------------------------------------------------------
        public Byte[] Convert(
            DataTable source,
            String    format,
            String    styleSheet)
        {
            IExportProvider exportProvider = this.ExportProviders.Where(x => String.Compare(format, x.Format, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();

            if (exportProvider != null)
            {
                return exportProvider.Convert(source, styleSheet);
            }
            else
            {
                throw new CoreProviderException(CoreErrorCode.DoesNotExist, "Format is not supported");
            }
        }
        #endregion
    }
}
