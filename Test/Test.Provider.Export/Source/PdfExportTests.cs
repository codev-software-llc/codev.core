//-----------------------------------------------------------------------------
// <copyright file="PdfExportTests.cs" company="Codev Software, LLC">
// Copyright © 2026
// </copyright>
//-----------------------------------------------------------------------------
namespace Core.Test.Provider.Export
{
    using System.Data;
    using Codev.Core.Interface;
    using Codev.Core.Provider.Export;

    ///------------------------------------------------------------------------
    /// <summary>
    /// This impolements tests for the deserialization methods.
    /// </summary>
    ///------------------------------------------------------------------------
    public class PdfExportTests
    {
        #region Setup
        ///---------------------------------------------------------------
        /// <summary>
        /// Setup the class.
        /// </summary>
        ///---------------------------------------------------------------
        [SetUp]
        public void Setup()
        {
        }
        #endregion

        /// -------------------------------------------------------------------
        /// <summary>
        /// Test with empty string.
        /// </summary>
        /// -------------------------------------------------------------------
        [Test]
        public void Export_Convert()
        {
            /*
            IExportProvider provider = new ExportPdfProvider();

            DataTable table = new DataTable();

            DataColumn column1 = new DataColumn("Column1", typeof(String)) { DefaultValue = "Value1" };
            DataColumn column2 = new DataColumn("Column2", typeof(String)) { DefaultValue = "Valud2" };

            table.Columns.Add(column1);
            table.Columns.Add(column2);

            table.Rows.Add(new object[] { "Row1Value1", "Row1Value2" });

            Byte[] bytes = provider.Convert(table);
            */

            Assert.Fail();
        }
    }
}