using System;
using System.Collections.Generic;

namespace Markdown.Infrastructure
{
    public class StringReader
    {
        public StringReader(string stringForRead)
        {
            String = stringForRead;
        }

        public int CurrentIndex { get; private set; }

        public string String { get; }

        public bool AtEndOfString => CurrentIndex >= String.Length;

        public bool IsLocatedOn(string str)
        {
            return String.StartsWith(str, CurrentIndex);
        }

        public string Read(int charsCount)
        {
            var startIndex = CurrentIndex;
            CurrentIndex += charsCount;

            return String.Substring(startIndex, Math.Min(charsCount, String.Length - startIndex));
        }

        public Context GetContext(string str)
        {
            if (!IsLocatedOn(str))
                throw new ArgumentException();

            return new Context(String.Substring(0, CurrentIndex), str, String.Substring(CurrentIndex + str.Length));
        }

        public char? GetCharOn(int index)
        {
            if (index < 0 || index >= String.Length)
                return null;
            return String[index];
        }

        public IEnumerable<Context> FindContextsFor(string str)
        {
            for (var index = CurrentIndex; index < String.Length; index++)
            {
                if (String.StartsWith(str, index))
                {
                    yield return new Context(String.Substring(0, index), str, String.Substring(index + str.Length));
                }
            }
        }
    }
}