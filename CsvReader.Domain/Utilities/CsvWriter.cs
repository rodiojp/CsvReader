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
    public sealed class CsvWriter : IDisposable
    {
        private readonly TextWriter textWriter;
        private readonly IEnumerable<object[]> Values;
        private readonly IEnumerable<string> Headers;

        public CsvWriter(string fileName, IEnumerable<object[]> values, Encoding coding = null, IEnumerable<string> headers = null)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
            }

            Values = values ?? throw new ArgumentNullException(nameof(values));
            Headers = headers ?? throw new ArgumentNullException(nameof(headers));
            textWriter = new StreamWriter(fileName, false, coding ?? Encoding.Default);
        }
        public void WriteCsv()
        {
            using (textWriter)
            {
                if (Headers != null && Headers.Any())
                {
                    textWriter.WriteLine(CreateLine(Headers));
                }
                foreach (object[] value in Values)
                {
                    textWriter.WriteLine(CreateLine(value));
                }
                textWriter.Flush();
                textWriter.Close();
            }
        }

        private static string CreateLine(IEnumerable<object> fields)
        {
            string text = string.Empty;
            IEnumerator<object> enumerator = fields.GetEnumerator();
            if (enumerator.MoveNext())
            {
                text = Csv.Escape(enumerator.Current.ToString());
                while (enumerator.MoveNext())
                {
                    text = $"{text},{enumerator.Current.ToString()}";
                }
            }
            return text;
        }

        public void Dispose()
        {
            if (textWriter != null)
            {
                textWriter.Dispose();
            }
        }
    }
}
