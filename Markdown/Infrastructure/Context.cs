using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Markdown.Infrastructure
{
    public class Context
    {
        public  char? NextChar => right == "" ? null : (char?)right.First();
        public char? PreviousChar => left == "" ? null : (char?)left.Last();

        private readonly string left;
        private readonly string str;
        private readonly string right;

        public Context(string left, string str, string right)
        {
            this.left = left;
            this.str = str;
            this.right = right;
        }

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