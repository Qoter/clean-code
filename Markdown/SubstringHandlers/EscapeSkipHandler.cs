using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public class EscapeSkipHandler : ISubstringHandler
    {
        public string HandleSubstring(StringReader reader)
        {
            return reader.Read(2);
        }

        public bool CanHandle(StringReader reader)
        {
            return reader.IsLocatedOn("\\");
        }
    }
}