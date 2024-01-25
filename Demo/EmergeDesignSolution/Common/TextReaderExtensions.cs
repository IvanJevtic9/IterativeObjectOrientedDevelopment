using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Demo.Common
{
    static class TextReaderExtensions
    {
        public static IEnumerable<string> IncomingLines(this TextReader reader, Action prompt) =>
            reader.NullableIncomingLines(prompt).TakeWhile(line => !ReferenceEquals(line, null));

        private static IEnumerable<string> NullableIncomingLines(this TextReader reader, Action prompt)
        {
            while (true)
            {
                prompt();
                yield return reader.ReadLine();
            }
        }
    }
}
