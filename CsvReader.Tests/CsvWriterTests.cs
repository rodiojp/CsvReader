using System;
using System.Collections.Generic;
using CsvReader.Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvReader.Tests
{
    [TestClass]
    public class CsvWriterTests
    {
        [TestMethod]
        [DeploymentItem(@"cwi_test.csv")]
        public void CsvWriterTest()
        {
            string[] weekDaysHeaders = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            // Two-dimensional array.
            int[,] array2D = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
            List<object[]> array2Dr = new List<object[]>() { new object[] { 1, 2 }, new object[] { 3, 4 }, 
                new object[] { 5, 6 }, new object[] { 7, 8 } };
            using (CsvWriter reader = new CsvWriter("cwi_test.csv", array2Dr, null,weekDaysHeaders))
            {
                reader.WriteCsv();
            }
            using (CsvReader.Domain.Utilities.CsvReader reader = new CsvReader.Domain.Utilities.CsvReader("cwi_test.csv"))
            {
                foreach (string[] values in reader.RowEnumerator)
                {
                    switch (reader.RowIndex)
                    {
                        case 1:
                            Assert.AreEqual(1, values.Length);
                            break;
                        case 2:
                            Assert.AreEqual(1, values.Length);
                            break;
                        case 3:
                            Assert.AreEqual(2, values.Length);
                            break;
                        case 4 - 6:
                            Assert.AreEqual(23, values.Length);
                            break;
                    }
                }
            }
        }
    }
}
