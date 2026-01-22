//-----------------------------------------------------------------------------
// <copyright file="IExportService.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Service.Export
{
    using System;
    using System.Data;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the means to export information into a 
    /// destination format.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IExportService : IService
    {
        #region Methods
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the table into the format specified.
        /// </summary>
        ///--------------------------------------------------------------------
        Byte[] Convert(
            DataTable source,
            String format);

        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the table into the format specified.
        /// </summary>
        ///--------------------------------------------------------------------
        Byte[] Convert(
            DataTable source,
            String format,
            String styleSheet);
        #endregion
    }
}
