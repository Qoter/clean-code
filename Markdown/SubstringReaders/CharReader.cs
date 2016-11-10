namespace Markdown.SubstringReaders
{
    public class CharReader : ISubstringReader
    {
        public string ReadSubstring(Tokenizer tokenizer)
        { 
            return tokenizer.Read(1);
        }

        public bool CanReadSubsting(Tokenizer tokenizer)
        {
            return true;
        }
    }
}