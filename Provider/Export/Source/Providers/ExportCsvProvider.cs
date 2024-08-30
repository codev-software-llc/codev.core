//-----------------------------------------------------------------------------
// <copyright file="ExportCsvProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using Codev.Core.Common.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IExport to dump data into a destination format.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class ExportCsvProvider : IExportProvider
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///---------------------------------------------------------------
        public ExportCsvProvider()
        {
        }
        #endregion

        #region Properties
        ///---------------------------------------------------------------
        /// <summary>
        /// Return the name of the export provider.
        /// </summary>
        ///---------------------------------------------------------------
        public String Format
        {
            get
            {
                return "csv";
            }
        }
        #endregion

        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to a CSV format.
        /// </summary>
        ///---------------------------------------------------------------
        public Byte[] Convert(
            DataTable source,
            String    styleSheet)
        {
            return this.Convert(source);
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to a CSV format.
        /// </summary>
        ///---------------------------------------------------------------
        public Byte[] Convert(
            DataTable source)
        {
            Byte[] response = new Byte[] { };

            if ((source != null) && (source.Rows != null) && (source.Rows.Count > 0))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(stream);

                    this.PrintHeader(source, sw);

                    this.PrintRows(source, sw);

                    sw.Flush();

                    stream.Seek(0, SeekOrigin.Begin);

                    StreamReader sr = new StreamReader(stream);

                    String data = sr.ReadToEnd();

                    response = Encoding.UTF8.GetBytes(data);
                }
            }

            return response;
        }
        #endregion

        #region Methods (Private)
        ///---------------------------------------------------------------
        /// <summary>
        /// Output the header information.
        /// </summary>
        ///---------------------------------------------------------------
        public void PrintHeader(
            DataTable    source,
            StreamWriter writer)
        {
            Int32 columnCount = source.Columns.Count;

            for (Int32 idx = 0; idx < columnCount; idx++)
            {
                if (idx == (columnCount - 1))
                {
                    writer.Write("{0}\n", source.Columns[idx].ColumnName);
                }
                else
                {
                    writer.Write("{0},", source.Columns[idx].ColumnName);
                }
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Output the rows.
        /// </summary>
        ///---------------------------------------------------------------
        public void PrintRows(
            DataTable    source,
            StreamWriter writer)
        {
            if ((source.Rows != null) && (source.Rows.Count > 0))
            {
                Int32 columnCount = source.Columns.Count;

                foreach (DataRow row in source.Rows)
                {
                    for (Int32 idx = 0; idx < columnCount; idx++)
                    {
                        if (idx == (columnCount - 1))
                        {
                            writer.Write("\"{0}\"\n", row[idx]);
                        }
                        else
                        {
                            writer.Write("\"{0}\",", row[idx]);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
