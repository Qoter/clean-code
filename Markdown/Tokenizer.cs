using System;
using System.Collections;
using JetBrains.Annotations;

namespace Markdown
{
    public class Tokenizer
    {
        public char? this[int index] => index < 0 || index >= tokenizedString.Length ? null : (char?)tokenizedString[index];
        public char? PreviousChar => this[CurrentIndex - 1];
        public char? CurrentChar => this[CurrentIndex];
        public char? NextChar => this[CurrentIndex + 1];

        public int CurrentIndex { get; private set; }

        public bool OutOfRange => CurrentIndex < 0 || CurrentIndex >= tokenizedString.Length;
        public bool InsideWord => PreviousChar.IsDigitOrLetter() && !CurrentChar.IsWhiteSpace() && NextChar.IsDigitOrLetter();


        private readonly string tokenizedString;

        public Tokenizer(string tokenizedString)
        {
            this.tokenizedString = tokenizedString;
        }

        public bool OnCharacter(char character)
        {
            return CurrentChar == character;
        }

        public string Read(int charsCount)
        {
            var startIndex = CurrentIndex;
            CurrentIndex = CurrentIndex + charsCount;
            return tokenizedString.Substring(startIndex, CurrentIndex - startIndex);
        }

        public void ReadNext()
        {
            CurrentIndex++;
        }
    }
}
