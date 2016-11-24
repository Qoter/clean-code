using System.Collections;
using System.Collections.Generic;

namespace Markdown.Infrastructure
{
    public class Context
    {
        public readonly char? NextChar;
        public readonly char? PreviousChar;
        public readonly string String;

        public Context(char? previousChar, string s, char? nextChar)
        {
            PreviousChar = previousChar;
            String = s;
            NextChar = nextChar;
        }

        public bool InsidePrintable => PreviousChar.IsPrintable() && NextChar.IsPrintable();
    }
}