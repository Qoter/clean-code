namespace Markdown
{
    public interface ISubstringHandler
    {
        string HandleSubstring(StringReader reader);
        bool CanHandle(StringReader reader);
    }
}