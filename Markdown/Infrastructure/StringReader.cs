using System;
using System.Collections.Generic;

namespace Markdown.Infrastructure
{
    public class StringReader
    {
        public int CurrentIndex { get; private set; }

        public string ReadedString { get; }

        public bool AtEndOfString => CurrentIndex >= ReadedString.Length;

        public bool AtStartOfLine => CurrentIndex == 0 || (CurrentIndex > 1 && ReadedString.StartsWith("\r\n", CurrentIndex - 2));

        public StringReader(string readedString)
        {
            ReadedString = readedString;
        }

        public bool IsLocatedOn(string str)
        {
            return ReadedString.StartsWith(str, CurrentIndex);
        }

        public string Read(int charsCount)
        {
            var startIndex = CurrentIndex;
            CurrentIndex += charsCount;

            return ReadedString.Substring(startIndex, Math.Min(charsCount, ReadedString.Length - startIndex));
        }

        public string ReadLine()
        {
            if (!AtStartOfLine)
                throw new InvalidOperationException();

            var endLineIndex = ReadedString.IndexOf("\r\n", CurrentIndex, StringComparison.Ordinal);
            return endLineIndex == -1 
                ? Read(ReadedString.Length - CurrentIndex) : 
                Read(endLineIndex - CurrentIndex + "\r\n".Length);
        }

        public Context GetContext(string str)
        {
            if (!IsLocatedOn(str))
                throw new ArgumentException();

            return new Context(ReadedString.Substring(0, CurrentIndex), str, ReadedString.Substring(CurrentIndex + str.Length));
        }

        public char? GetCharOn(int index)
        {
            if (index < 0 || index >= ReadedString.Length)
                return null;
            return ReadedString[index];
        }

        public IEnumerable<Context> FindContextsFor(string str)
        {
            for (var index = CurrentIndex; index < ReadedString.Length; index++)
            {
                if (ReadedString.StartsWith(str, index))
                {
                    yield return new Context(ReadedString.Substring(0, index), str, ReadedString.Substring(index + str.Length));
                }
            }
        }
    }
}