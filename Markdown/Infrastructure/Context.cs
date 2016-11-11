using System.Linq;

namespace Markdown.Infrastructure
{
    public class Context
    {
        public readonly char? PreviousChar;
        public readonly string String;
        public readonly char? NextChar;
        public bool InsidePrintable => PreviousChar.IsPrintable() && NextChar.IsPrintable();

        public Context(char? previousChar, string s, char? nextChar)
        {
            PreviousChar = previousChar;
            String = s;
            NextChar = nextChar;
        }
    }
}
