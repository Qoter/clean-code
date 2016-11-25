using System.Linq;

namespace Markdown.Infrastructure
{
    public class Context
    {
        public readonly string Left;
        public readonly string Right;
        private readonly string str;

        public Context(string left, string str, string right)
        {
            this.Left = left;
            this.str = str;
            this.Right = right;
        }

        public char? NextChar => Right == "" ? null : (char?) Right.First();
        public char? PreviousChar => Left == "" ? null : (char?) Left.Last();

        public StringReader RightReader
        {
            get
            {
                var rightReader = new StringReader(Left + str + Right);
                rightReader.Read(Left.Length + str.Length);
                return rightReader;
            }
        }

        public bool InsidePrintable => PreviousChar.IsPrintable() && NextChar.IsPrintable();
    }
}