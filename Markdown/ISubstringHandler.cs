namespace Markdown
{
    public interface ISubstringHandler
    {   
        string Handle(string str, ref int startIndex);
        bool CanHandle(string str, int startIndex);
    }
}