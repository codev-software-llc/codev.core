//-----------------------------------------------------------------------------
// <copyright file="ExportXmlProvider.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider.Export
{
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IExport to dump data into a destination format.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class ExportXmlProvider : IExportProvider
    {
        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///---------------------------------------------------------------
        public ExportXmlProvider()
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
                return "xml";
            }
        }
        #endregion

        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to a Xml format.
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
        /// Export the data table to a Xml format.
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
                    XmlWriter xm = XmlWriter.Create(stream);

                    Int32 columnCount = source.Columns.Count;
                    Int32 idxRow      = 0;

                    xm.WriteStartDocument();

                    xm.WriteStartElement("Report");

                    foreach (DataRow row in source.Rows)
                    {
                        xm.WriteStartElement(String.Format("Response{0}", idxRow++));

                        for (Int32 idx = 0; idx < columnCount; idx++)
                        {
                            xm.WriteElementString(source.Columns[idx].ColumnName, row[idx].ToString());
                        }

                        xm.WriteEndElement();
                    }

                    xm.WriteEndElement();

                    xm.WriteEndDocument();

                    xm.Flush();

                    stream.Seek(0, SeekOrigin.Begin);

                    StreamReader sr = new StreamReader(stream);

                    String data = sr.ReadToEnd();

                    response = Encoding.UTF8.GetBytes(data);
                }
            }

            return response;
        }
        #endregion
    }
}
