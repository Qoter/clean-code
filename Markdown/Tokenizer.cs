using System;

namespace Markdown
{
    public class Tokenizer
    {
        public char? CurrentChar => this[CurrentIndex];
        public int CurrentIndex { get; private set; }
        public bool OutOfRange => CurrentIndex < 0 || CurrentIndex >= tokenizedString.Length;
        public char? this[int index] => OutOfRange ? null : (char?)tokenizedString[CurrentIndex];

        private readonly string tokenizedString;

        public Tokenizer(string tokenizedString)
        {
            this.tokenizedString = tokenizedString;
        }

        public string ReadUntil(Func<Tokenizer, bool> isStopConfiguration)
        {
            var startIndex = CurrentIndex;
            while (!OutOfRange && !isStopConfiguration.Invoke(this))
            {
                ReadNext();
            }
            var endIndex = CurrentIndex;

            return tokenizedString.Substring(startIndex, endIndex - startIndex);
        }

        public string Read(int charsCount)
        {
            var startIndex = CurrentIndex;
            var endIndex = CurrentIndex = CurrentIndex + charsCount;
            return tokenizedString.Substring(startIndex, endIndex - startIndex);
        }

        public void ReadNext()
        {
            CurrentIndex++;
        }
    }
}
