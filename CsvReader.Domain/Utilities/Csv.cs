using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReader.Domain.Utilities
{
    public static class Csv
    {
        private const string QUOTE = "\"";
        private const string ESCAPED_QUOTE = "\"\"";
        private static readonly char[] CharactersThatMustBeQuoted = new char[3]
        {
            ',',
            '"',
            '\n'
        };
        public static string Escape(string str)
        {
            if (str.Contains(QUOTE))
            {
                str = str.Replace(QUOTE, ESCAPED_QUOTE);
            }
            if (str.IndexOfAny(CharactersThatMustBeQuoted)>-1)
            {
                str = $"{QUOTE}{str}{QUOTE}";
            }
            return str;
        }
        public static string Unescape(string str)
        {
            if (str.StartsWith(QUOTE) && str.EndsWith(QUOTE)) 
            {
                str = str.Substring(1, str.Length - 2);
                if (str.Contains(ESCAPED_QUOTE))
                {
                    str = str.Replace(ESCAPED_QUOTE, QUOTE);
                }
            }
            return str;    
        }
    }
}
