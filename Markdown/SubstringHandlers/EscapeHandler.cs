namespace Markdown.SubstringHandlers
{
    public class EscapeHandler : ISubstringHandler
    {
        public string HandleSubstring(StringReader reader)
        {
            reader.Read(1); //Skip backslash
            return reader.Read(1);
        }

        public bool CanHandle(StringReader reader)
        {
            return reader.CurrentChar == '\\';
        }
    }
}