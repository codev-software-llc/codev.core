//-----------------------------------------------------------------------------
// <copyright file="IExportProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Common.Interface
{
    using System;
    using System.Data;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This interface defines the means to export information into a 
    /// destination format.
    /// </summary>
    ///------------------------------------------------------------------------
    public interface IExportProvider : IProvider
    {
        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Return the format of the export provider (xml, htm, csv, txt).
        /// </summary>
        ///---------------------------------------------------------------
        String Format { get; }
        #endregion

        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Convert the table into the specified format.
        /// </summary>
        ///---------------------------------------------------------------
        Byte[] Convert(
            DataTable source);

        ///---------------------------------------------------------------
        /// <summary>
        /// Convert the table into the specified format that uses a
        /// stylesheet.
        /// </summary>
        ///---------------------------------------------------------------
        Byte[] Convert(
            DataTable source,
            String    styleSheet);
        #endregion
    }
}
