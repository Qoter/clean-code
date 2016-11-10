namespace Markdown.SubstringReaders
{
    public class EscapeReader : ISubstringReader
    {
        public string ReadSubstring(Tokenizer tokenizer)
        {
            return tokenizer.Read(2)[1].ToString();
        }

        public bool CanReadSubsting(Tokenizer tokenizer)
        {
            return tokenizer.CurrentChar == '\\';
        }
    }
}