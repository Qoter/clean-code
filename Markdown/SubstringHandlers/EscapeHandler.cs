namespace Markdown.SubstringHandlers
{
    public class EscapeHandler : ISubstringHandler
    {
        public string HandleSubstring(StringReader reader)
        {
            return reader.Read(2)[1].ToString();
        }

        public bool CanHandle(StringReader reader)
        {
            return reader.CurrentChar == '\\';
        }
    }
}