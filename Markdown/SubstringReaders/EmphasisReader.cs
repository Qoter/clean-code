namespace Markdown.SubstringReaders
{
    public class EmphasisReader : ISubstringReader
    {
        public string ReadSubstring(Tokenizer tokenizer)
        {
            var innerTextWithUnderscore = tokenizer.ReadUntil(t => t.CurrentChar == '_' && t[t.CurrentIndex + 1] != '_');
            var innerText = innerTextWithUnderscore.Substring(1, innerTextWithUnderscore.Length - 2);

            return Tag.Emphasis.Wrap(innerText);
        }

        public bool CanReadSubsting(Tokenizer tokenizer)
        {
            return false;
        }
    }
}
