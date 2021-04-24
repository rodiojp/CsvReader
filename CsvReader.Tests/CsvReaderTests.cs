using System;
using CsvReader.Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsvReader.Tests
{
    [TestClass]
    public class CsvReaderTests
    {
        [TestMethod]
        [DeploymentItem(@"cwi.csv")]
        public void ParseCwi_RowEnumerator_Return1Line()
        {
            using (CsvReader.Domain.Utilities.CsvReader reader = new CsvReader.Domain.Utilities.CsvReader("cwi.csv"))
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
                        case 4-6:
                            Assert.AreEqual(23, values.Length);
                            break;
                    }
                }        
            }
        }
    }
}
