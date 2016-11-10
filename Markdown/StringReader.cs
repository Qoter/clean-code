using System.Security.AccessControl;

namespace Markdown
{
    public class StringReader
    {
        private readonly string stringForRead;

        public StringReader(string stringForRead)
        {
            this.stringForRead = stringForRead;
        }

        public char? this[int index]
            => index < 0 || index >= stringForRead.Length ? null : (char?) stringForRead[index];

        public char? PreviousChar => this[CurrentIndex - 1];
        public char? CurrentChar => this[CurrentIndex];
        public char? NextChar => this[CurrentIndex + 1];

        public int CurrentIndex { get; private set; }

        public bool AtEndOfString => CurrentIndex < 0 || CurrentIndex >= stringForRead.Length;

        public bool InsideWord
            => PreviousChar.IsDigitOrLetter() && !CurrentChar.IsWhiteSpace() && NextChar.IsDigitOrLetter();

        public bool OnCharacter(char character)
        {
            return CurrentChar == character;
        }

        public string Read(int charsCount)
        {
            var startIndex = CurrentIndex;
            CurrentIndex = CurrentIndex + charsCount;
            return stringForRead.Substring(startIndex, CurrentIndex - startIndex);
        }
    }
}