//-----------------------------------------------------------------------------
// <copyright file="ExportPdfProvider.cs" company="Codev Software, LLC">
// Copyright © 2024
// </copyright>
//-----------------------------------------------------------------------------
namespace Codev.Core.Provider
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Codev.Core.Interface;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This implements the IExport to dump data into a destination format.
    /// </summary>
    ///------------------------------------------------------------------------
    public sealed class ExportPdfProvider : IExportProvider
    {
        #region Constants
        ///--------------------------------------------------------------------
        /// <summary>
        /// This is the name of the directory containing the resources.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String ResourceDirectoryName = "wwwroot\\resources";

        ///--------------------------------------------------------------------
        /// <summary>
        /// This is the executable that converts html to pdf.
        /// </summary>
        ///--------------------------------------------------------------------
        private const String ExecutableName = "wkhtmltopdf.exe";
        #endregion

        #region Constructors
        ///---------------------------------------------------------------
        /// <summary>
        /// Instantiate the object.
        /// </summary>
        ///---------------------------------------------------------------
        public ExportPdfProvider()
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
                return "pdf";
            }
        }
        #endregion

        #region Methods
        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to a PDF format.
        /// </summary>
        ///---------------------------------------------------------------
        public Byte[] Convert(
            DataTable source,
            String    styleSheet)
        {
            String html = DataTableHelper.ToHtml(source, styleSheet);

            Assembly assembly = Assembly.GetExecutingAssembly();

            String path = assembly.Location.TrimEnd(new Char[] { '/', '\\' });

            Int32 idx = path.LastIndexOfAny(new Char[] { '/', '\\' });

            if (idx >= 0)
            {
                path = path.Substring(0, idx);

                String fullPath = Path.Combine(path, ResourceDirectoryName, "WkHtmlToPdf");

                return this.ConvertHtml(fullPath, html, styleSheet);
            }

            return new Byte[] { };
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Export the data table to a PDF format.
        /// </summary>
        ///---------------------------------------------------------------
        public Byte[] Convert(
            DataTable source)
        {
            return this.Convert(source, String.Empty);
        }
        #endregion

        #region Methods (Private)
        ///--------------------------------------------------------------------
        /// <summary>
        /// Convert the html to a byte stream taking into account the
        /// style sheet.
        /// </summary>
        ///--------------------------------------------------------------------
        private Byte[] ConvertHtml(
            String wkhtmlPath,
            String html,
            String styleSheet)
        {
            // Switches:
            //   "-q"  - silent output, only errors - no progress messages.
            //   " -"  - switch output to stdout.
            //   "- -" - switch input to stdin and output to stdout.
            String switches = "-q -";

            // Generate PDF from given HTML string, not from URL.
            //
            if (String.IsNullOrEmpty(html) == false)
            {
                switches += " -";

                html = this.EncodeSpecialCharacters(html);
            }

            Process proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName               = Path.Combine(wkhtmlPath, ExecutableName),
                        Arguments              = switches,
                        UseShellExecute        = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError  = true,
                        RedirectStandardInput  = true,
                        WorkingDirectory       = wkhtmlPath,
                        CreateNoWindow         = true
                    }
                };

            proc.Start();

            // Generate PDF from given HTML string.
            //
            if (String.IsNullOrEmpty(html) == false)
            {
                using (StreamWriter inputStream = proc.StandardInput)
                {
                    inputStream.WriteLine(html);
                }
            }

            // Get the response and convert the stream.
            //
            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream outputStream = proc.StandardOutput.BaseStream)
                {
                    Byte[] buffer = new byte[4096];

                    Int32 read;

                    while ((read = outputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                String error = proc.StandardError.ReadToEnd();

                if (ms.Length == 0)
                {
                    throw new Exception(error);
                }

                proc.WaitForExit();

                return ms.ToArray();
            }
        }

        ///---------------------------------------------------------------
        /// <summary>
        /// Encode any special characters from the content.
        /// </summary>
        ///---------------------------------------------------------------
        private String EncodeSpecialCharacters(
            String html)
        {
            Char[] characters = html.ToCharArray();

            StringBuilder result = new StringBuilder(html.Length + (Int32)(html.Length * 0.1));

            foreach (Char c in characters)
            {
                Int32 value = System.Convert.ToInt32(c);

                if (value > 127)
                {
                    result.AppendFormat("&#{0};", value);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
        #endregion
    }
}
