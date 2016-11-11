using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class CharHandler : ISubstringHandler
    {
        public string HandleSubstring(StringReader reader)
        {
            return reader.Read(1);
        }

        public bool CanHandle(StringReader reader)
        {
            return true;
        }
    }
}