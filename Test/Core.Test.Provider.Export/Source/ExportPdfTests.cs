//-----------------------------------------------------------------------------
// <copyright file="CipherAesTests.cs" company="Codev Software, LLC">
// Copyright © 2025
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Provider.Export
{
    using System;
    using System.Data;
    using Codev.Core.Base;
    using Codev.Core.Interface;
    using Codev.Core.Provider.Export;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This is the test for Aes provider.
    /// </summary>
    ///------------------------------------------------------------------------
    [TestClass]
    public class ExportPdfTests
    {
        #region Methods
        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with empty string.
        /// </summary>
        /// -------------------------------------------------------------------
        [TestMethod]
        public void ExportPdf_Default()
        {
            IExportProvider provider = new ExportPdfProvider();

            DataTable table = new DataTable();

            DataColumn column1 = new DataColumn("Column1", typeof(String)) { DefaultValue = "Value1" };
            DataColumn column2 = new DataColumn("Column2", typeof(String)) { DefaultValue = "Valud2" };

            table.Columns.Add(column1);
            table.Columns.Add(column2);

            table.Rows.Add(new object[] { "Row1Value1", "Row1Value2" });

            Byte[] bytes = provider.Convert(table);

        }
        #endregion
    }
}