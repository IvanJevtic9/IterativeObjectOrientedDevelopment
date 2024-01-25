using System;
using System.Collections.Generic;
using Demo.Common;

namespace Demo.Domain
{
    class ConsoleInputsReader
    {
        private string PromptLabel { get; }

        public ConsoleInputsReader() : this("Input numbers: ") {}

        public ConsoleInputsReader(string promptLabel)
        {
            this.PromptLabel = promptLabel;
        }

        public IEnumerable<IEnumerable<int>> ReadAll() =>
            Console.In.IncomingLines(this.Prompt).NonNegativeIntegerSequences();

        private void Prompt() =>
            Console.Write(this.PromptLabel);
    }
}