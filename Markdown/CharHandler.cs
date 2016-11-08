using System;

namespace Markdown
{
    public class CharHandler : ISubstringHandler
    {
        public string Handle(string str, ref int startIndex)
        {
            return str[startIndex++].ToString();
        }

        public bool CanHandle(string str, int startIndex)
        {
            return true;
        }
    }
}