using System.Linq;

namespace Markdown.Infrastructure
{
    public class Context
    {
        public readonly char? PreviousChar;
        public readonly string String;
        public readonly char? NextChar;

        public Context(char? previousChar, string s, char? nextChar)
        {
            PreviousChar = previousChar;
            String = s;
            NextChar = nextChar;
        }

        public bool InsideWordOrDigit => PreviousChar.IsDigitOrLetter() && !String.Any(char.IsWhiteSpace) && NextChar.IsDigitOrLetter();
    }
}
