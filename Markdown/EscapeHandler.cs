using System;

namespace Markdown
{
    public class EscapeHandler : ISubstringHandler
    {
        public string Handle(string str, ref int startIndex)
        {
            throw new NotImplementedException();
        }

        public bool CanHandle(string str, int startIndex)
        {
            return str[startIndex] == '\\';
        }
    }
}