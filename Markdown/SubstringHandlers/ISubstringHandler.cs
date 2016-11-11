using Markdown.Infrastructure;

namespace Markdown.SubstringHandlers
{
    public interface ISubstringHandler
    {
        string HandleSubstring(StringReader reader);
        bool CanHandle(StringReader reader);
    }
}