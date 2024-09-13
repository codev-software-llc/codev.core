//-----------------------------------------------------------------------------
// <copyright file="DataTableHelper.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Export
{
    using System;
    using System.Data;
    using System.IO;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This contains helpers for a data table.
    /// </summary>
    ///------------------------------------------------------------------------
    public class DataTableHelper
    {
        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to a HTML format.
        /// </summary>
        ///---------------------------------------------------------------
        public static String ToHtml(
            DataTable source,
            String    styleSheet)
        {
            String response = String.Empty;

            if ((source != null) && (source.Rows != null) && (source.Rows.Count > 0))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(stream);

                    String styleReference = String.Format("<link rel='stylesheet' type='text/css' href='{0}'></link>", styleSheet);

                    sw.Write("<html>");
                    sw.Write("<head>");
                    sw.Write(styleReference);
                    sw.Write("</head>");

                    sw.Write("<body>");

                    sw.Write("<table class='table table-striped border border-secondary'>");

                    PrintHeader(source, sw);

                    PrintRows(source, sw);

                    PrintFooter(source, sw);

                    sw.Write("</table>");

                    sw.Write("</body>");

                    sw.Write("</html>");

                    sw.Flush();

                    stream.Seek(0, SeekOrigin.Begin);

                    StreamReader sr = new StreamReader(stream);

                    response = sr.ReadToEnd();
                }
            }

            return response;
        }
        #endregion

        #region Methods (Private)
        ///---------------------------------------------------------------
        /// <summary>
        /// Output the header.
        /// </summary>
        ///---------------------------------------------------------------
        private static void PrintHeader(
            DataTable    source,
            StreamWriter writer)
        {
            Int32 columnCount = source.Columns.Count;

            // Add the header information.
            // 
            writer.Write("<thead>");

            writer.Write("<tr>");

            for (Int32 idx = 0; idx < columnCount; idx++)
            {
                writer.Write("<th>{0}</th>", source.Columns[idx].ColumnName);
            }

            writer.Write("</tr>");

            writer.Write("</thead>");
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Output the rows.  The last row in the datatable represents
        /// the footer, so this will exclude that row.
        /// </summary>
        ///---------------------------------------------------------------
        private static void PrintRows(
            DataTable    source,
            StreamWriter writer)
        {
            if ((source.Rows != null) && (source.Rows.Count > 0))
            {
                Int32 columnCount = source.Columns.Count;

                writer.Write("<tbody>");

                Int32 rowCount = source.Rows.Count - 1;

                for (Int32 idx = 0; idx < rowCount; idx++)
                {
                    DataRow row = source.Rows[idx];

                    writer.Write("<tr>");

                    for (Int32 idy = 0; idy < columnCount; idy++)
                    {
                        writer.Write("<td>{0}</td>", row[idy]);
                    }

                    writer.Write("</tr>");
                }

                writer.Write("</tbody>");
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Output the footer.  The footer is represented as the last
        /// row in the datatable.
        /// </summary>
        ///---------------------------------------------------------------
        private static void PrintFooter(
            DataTable    source,
            StreamWriter writer)
        {
            writer.Write("<tfoot>");

            if ((source.Rows != null) && (source.Rows.Count > 0))
            {
                writer.Write("<tr>");

                Int32 rowIdx = source.Rows.Count - 1;

                DataRow row = source.Rows[rowIdx];

                for (Int32 idx = 0; idx < source.Columns.Count; idx++)
                {
                    writer.Write("<td>{0}</td>", row[idx]);
                }

                writer.Write("</tr>");
            }

            writer.Write("</tfoot>");
        }
        #endregion
    }
}
