namespace Markdown
{
    public interface ISubstringReader
    {   
        string ReadSubstring(Tokenizer tokenizer);
        bool CanReadSubsting(Tokenizer tokenizer);
    }
}