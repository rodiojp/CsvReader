using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CsvReader.Domain.Utilities
{
    public sealed class CsvReader : IDisposable
    {
        private readonly TextReader reader;
        private static readonly Regex RegexCsvSplitter = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        public long RowIndex
        {
            get;
            private set;
        }

        public IEnumerable RowEnumerator
        {
            get
            {
                if (reader == null)
                {
                    throw new ArgumentNullException("stream is emapty");
                }
                RowIndex = 0L;
                while (true)
                {
                    string text;
                    string sLine = text = reader.ReadLine();
                    if (text != null)
                    {
                        RowIndex += 1L;
                        string[] values = RegexCsvSplitter.Split(sLine);
                        for (int ii = 0; ii < values.Length; ii++)
                        {
                            values[ii] = Csv.Unescape(values[ii]);
                        }
                        yield return (object)values;
                        continue;
                    }
                    break;
                }
                reader.Close();
            }
        }

        public CsvReader(string fileName)
            :this(new FileStream(fileName, FileMode.Open, FileAccess.Read))
        { }

        public CsvReader(Stream stream)
        {
            RowIndex = 0L;
            reader = new StreamReader(stream);
        }
        public void Dispose()
        {
            if (reader != null)
            {
                reader.Dispose();
            }
        }
    }
}
