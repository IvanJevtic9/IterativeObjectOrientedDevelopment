using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Demo.EmergeDesignSolution.Common
{
    internal static class TextReaderExtensions
    {
        private static IEnumerable<string> NullableIncomingLines(this TextReader reader, Action promptLineMessage)
        {
            while (true)
            {
                promptLineMessage.Invoke();
                yield return reader.ReadLine();
            }
        }

        public static IEnumerable<string> IncomingLines(this TextReader reader, Action promptLineMessage) =>
            reader.NullableIncomingLines(promptLineMessage)
                  .TakeWhile(line => !ReferenceEquals(line, null));
    }
}