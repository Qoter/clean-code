using System.Linq;

namespace Markdown.Infrastructure
{
    public class Context
    {
        private readonly string left;
        private readonly string right;
        private readonly string str;

        public Context(string left, string str, string right)
        {
            this.left = left;
            this.str = str;
            this.right = right;
        }

        public char? NextChar => right == "" ? null : (char?) right.First();
        public char? PreviousChar => left == "" ? null : (char?) left.Last();

        public StringReader RightReader
        {
            get
            {
                var rightReader = new StringReader(left + str + right);
                rightReader.Read(left.Length + str.Length);
                return rightReader;
            }
        }

        public bool InsidePrintable => PreviousChar.IsPrintable() && NextChar.IsPrintable();
    }
}