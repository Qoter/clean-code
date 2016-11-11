using System;
using System.Security.AccessControl;

namespace Markdown
{
    public class StringReader
    {
        public char? this[int index] => index < 0 || index >= stringForRead.Length ? null : (char?) stringForRead[index];

        public char? PreviousChar => this[CurrentIndex - 1];
        public char? CurrentChar => this[CurrentIndex];
        public char? NextChar => this[CurrentIndex + 1];

        public int CurrentIndex { get; private set; }

        public bool AtEndOfString => CurrentIndex >= stringForRead.Length;

        private readonly string stringForRead;

        public StringReader(string stringForRead)
        {
            this.stringForRead = stringForRead;
        }

        public bool IsLocatedOn(string str)
        {
            return stringForRead.StartsWith(str, CurrentIndex);
        }

        public string Read(int charsCount)
        {
            var startIndex = CurrentIndex;
            CurrentIndex = CurrentIndex + charsCount;
            return stringForRead.Substring(startIndex, CurrentIndex - startIndex);
        }

        public Context GetContext(string str)
        {
            if (!IsLocatedOn(str))
                throw new ArgumentException($"Reader is not located on {str}");

            return new Context(PreviousChar, str, this[CurrentIndex + str.Length]);
        }

    }
}