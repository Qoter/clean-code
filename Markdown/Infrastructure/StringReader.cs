using System;
using System.Collections.Generic;

namespace Markdown.Infrastructure
{
    public class StringReader
    {
        private readonly string stringForRead;
        private int currentIndex;

        public int CurrentIndex => currentIndex;
        public string String => stringForRead;

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
            currentIndex += charsCount;

            return stringForRead.Substring(startIndex, Math.Min(charsCount, stringForRead.Length - startIndex));
        }

        public Context GetContext(string str)
        {
            if (!IsLocatedOn(str))
                throw new ArgumentException();

            return new Context(stringForRead.Substring(0, currentIndex), str, stringForRead.Substring(CurrentIndex + str.Length));
        }

        public char? GetCharOn(int index)
        {
            if (index < 0 || index >= stringForRead.Length)
                return null;
            return stringForRead[index];
        }

        public IEnumerable<Context> FindContextsFor(string str)
        {
            for (var index = CurrentIndex; index < stringForRead.Length; index++)
            {
                if (stringForRead.StartsWith(str, index))
                {
                    yield return new Context(stringForRead.Substring(0, index), str, stringForRead.Substring(index + str.Length));
                }
            }
        }
    }
}