using System;
using System.Text;

namespace Markdown.SubstringReaders
{
    public class EmphasisReader : ISubstringReader
    {
        private readonly ReaderCombiner innerTextReader = new ReaderCombiner(new EscapeReader(), new CharReader());
        public string ReadSubstring(Tokenizer tokenizer)
        {
            if (!CanReadSubsting(tokenizer))
                throw new InvalidOperationException("Can't read emphasis substring");

            tokenizer.ReadNext();
            var innerText = ReadInnerText(tokenizer);
            tokenizer.ReadNext();

            return Tag.Emphasis.Wrap(innerText);
        }

        private string ReadInnerText(Tokenizer tokenizer)
        {
            var result = new StringBuilder();

            while (!tokenizer.OutOfRange && !(tokenizer.OnCharacter('_') && !tokenizer.InsideWord))
            {
                var nextSubstring = innerTextReader.Read(tokenizer);
                result.Append(nextSubstring);
            }

            return result.ToString();
        }

        public bool CanReadSubsting(Tokenizer tokenizer)
        {
            return tokenizer.CurrentChar == '_' && !tokenizer.NextChar.IsWhiteSpace();
        }
    }
}
