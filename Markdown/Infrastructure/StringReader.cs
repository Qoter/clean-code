using System;

namespace Markdown.Infrastructure
{
    public class StringReader
    {
        private readonly string stringForRead;
        private int currentIndex;

        public StringReader(string stringForRead)
        {
            this.stringForRead = stringForRead;
        }

        public bool AtEndOfString => currentIndex >= stringForRead.Length;

        public bool IsLocatedOn(string str)
        {
            return stringForRead.StartsWith(str, currentIndex);
        }

        public string Read(int charsCount)
        {
            var startIndex = currentIndex;
            currentIndex = currentIndex + charsCount;
            return stringForRead.Substring(startIndex, currentIndex - startIndex);
        }

        public Context GetContext(string str)
        {
            if (!IsLocatedOn(str))
                throw new ArgumentException($"Reader is not located on {str}");

            return new Context(GetCharOn(currentIndex - 1), str, GetCharOn(currentIndex + str.Length));
        }

        private char? GetCharOn(int index)
        {
            if (index < 0 || index >= stringForRead.Length)
                return null;
            return stringForRead[index];
        }
    }
}