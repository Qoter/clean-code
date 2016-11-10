namespace Markdown.SubstringHandlers
{
    public class StrongHandler : ISubstringHandler
    {
        public string HandleSubstring(StringReader reader)
        {
            return null;
        }

        public bool CanHandle(StringReader reader)
        {
            return false;
        }
    }
}